using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Contrib.MapperProvider;
using Contrib.XmlSerializer;
using Infrastructure;
using Infrastructure.DtoMapper;
using Infrastructure.Model;
using Infrastructure.Oxm;

namespace XmlParser.Services
{
    public class XmlProcessorService
    {
        private readonly IdentityWrapperService identityWrapperService;
        private readonly IXmlMapperProvider mapperProvider;
        private readonly IObjectMapper objectMapper;
        private readonly IXmlMapper<OfferNewModel> offerNewMapper;
        private readonly IXmlMapper<OfferOldModel> offerOldMapper;

        public XmlProcessorService(IdentityWrapperService identityWrapperService)
        {
            mapperProvider = new XmlMapperProvider();
            objectMapper = new ObjectMapper();

            mapperProvider.Register(new OfferNewModelOxm());
            mapperProvider.Register(new OfferOldModelOxm());
            mapperProvider.Register(new ParamOxm());
            mapperProvider.Register(new PictureOxm());
            mapperProvider.Register(new PictureOrigOxm());

            offerNewMapper = mapperProvider.GetMapper<OfferNewModel>();
            offerOldMapper = mapperProvider.GetMapper<OfferOldModel>();
            this.identityWrapperService = identityWrapperService;
        }


        public void ChangeInnerStructure(string xmlFilePath, string outputXmlFilePath, bool changeCategoryId, bool isGroupingNeed)
        {
            if (File.Exists(outputXmlFilePath))
                File.Delete(outputXmlFilePath);

            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };

            using (var newXmlFile = new FileStream(outputXmlFilePath, FileMode.Create, FileAccess.ReadWrite))
            using (XmlWriter xmlWriter = new XmlTextWriter(newXmlFile, Encoding.UTF8))
            using (var streamReader = new StreamReader(xmlFilePath))
            using (XmlReader xmlReader = XmlReader.Create(streamReader, settings))
            {
                xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlReader.Name == "offers")
                            {
                                XmlReader offerTree = xmlReader.ReadSubtree();
                                ReformOfferList(offerTree, xmlWriter, changeCategoryId, isGroupingNeed);
                                xmlReader.Skip();
                                break;
                            }

                            xmlWriter.WriteStartElement(xmlReader.Name);
                            if (xmlReader.HasAttributes)
                            {
                                while (xmlReader.MoveToNextAttribute())
                                {
                                    xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                }
                            }
                            break;
                        case XmlNodeType.Text:
                            xmlWriter.WriteString(xmlReader.Value);
                            break;
                        case XmlNodeType.DocumentType:
                            xmlWriter.WriteDocType(xmlReader.Name, null, xmlReader.GetAttribute("SYSTEM"), null);
                            break;
                        case XmlNodeType.EndElement:
                            xmlWriter.WriteFullEndElement();
                            break;
                    }
                }
                xmlReader.Close();
            }
        }

        private void ReformOfferList(XmlReader offerTree, XmlWriter xmlWriter, bool changeCategoryId,
            bool isGroupingNeed)
        {

            xmlWriter.WriteStartElement("offers");
            IEnumerable<OfferNewModel> offerNewModels = StreamNodes(offerTree, new[] { "offer" });

            if (changeCategoryId && isGroupingNeed)
            {
                IEnumerable<OfferNewModel> newModels = offerNewModels.GroupBy(o => o.GroupId)
                    .Select(
                        grouping =>
                        {
                            OfferNewModel model = grouping.First();
                            model.Params = grouping.SelectMany(p => p.Params).GroupBy(g => g.Name)
                                .Select(par =>
                                {
                                    Param param = par.First();
                                    param.Content = string.Join(";", par.Select(p => p.Content));
                                    return param;
                                }).ToList();
                            model.Pictures = grouping.SelectMany(pic => pic.Pictures).Distinct().ToList();

                            return model;
                        });
                foreach (var model in newModels)
                {
                    model.CategoryId = identityWrapperService[model.CategoryId];
                    WriteToXml(xmlWriter, model);
                }
            }
            else if (changeCategoryId)
            {
                foreach (var model in offerNewModels)
                {
                    model.CategoryId = identityWrapperService[model.CategoryId];
                    WriteToXml(xmlWriter, model);
                }
            }
            else if (isGroupingNeed)
            {
                IEnumerable<OfferNewModel> newModels = offerNewModels.GroupBy(o => o.GroupId)
                    .Select(
                        grouping =>
                        {
                            OfferNewModel model = grouping.First();
                            model.Params = grouping.SelectMany(p => p.Params).GroupBy(g => g.Name)
                                .Select(par =>
                                {
                                    Param param = par.First();
                                    param.Content = string.Join(";", par.Select(p => p.Content));
                                    return param;
                                }).ToList();
                            model.Pictures = grouping.SelectMany(pic => pic.Pictures).Distinct().ToList();

                            return model;
                        });
                foreach (var model in newModels)
                {
                    WriteToXml(xmlWriter, model);
                }
            }

            xmlWriter.WriteFullEndElement();
        }

        private void WriteToXml(XmlWriter xmlWriter, OfferNewModel model)
        {
            OfferOldModel offerOldModel = objectMapper.Map<OfferNewModel, OfferOldModel>(model);
            XElement element = offerOldMapper.Write(offerOldModel);
            element.WriteTo(xmlWriter);
        }

        private IEnumerable<OfferNewModel> StreamNodes(XmlReader xr, string[] tagNames)
        {
            var doc = new XmlDocument();
            xr.MoveToContent();

            while (true)
            {
                if (xr.NodeType == XmlNodeType.Element && tagNames.Contains(xr.Name))
                {
                    var node = doc.ReadNode(xr);
                    yield return Evalute(node);
                }
                else
                {
                    if (!xr.Read())
                    {
                        break;
                    }
                }
            }
            xr.Close();
        }

        private OfferNewModel Evalute(XmlNode node)
        {
            return offerNewMapper.ReadType(XElement.Parse(node.OuterXml));

        }
    }
}