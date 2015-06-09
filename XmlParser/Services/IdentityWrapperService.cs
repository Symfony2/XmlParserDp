using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Infrastructure.Model;

namespace XmlParser.Services
{
    public class IdentityWrapperService
    {
        private readonly IEnumerable<ExcelMatchModel> excelMatchModels;
        private readonly IEnumerable<JsonMatchModel> jsonMatchModels;
        private Dictionary<string, string> pars;

        public IdentityWrapperService(IEnumerable<ExcelMatchModel> excelMatchModels, IEnumerable<JsonMatchModel> jsonMatchModels)
        {
            this.excelMatchModels = excelMatchModels;
            this.jsonMatchModels = jsonMatchModels;
            pars = new Dictionary<string, string>();
        }

        public void Initialize()
        {
            var tidyJsonModels = jsonMatchModels.Where(jm => jm.CampaignId == "1001" && jm.CampaignName == "Lamoda RU").ToArray();

            var jsonModels = tidyJsonModels
                .Where(j => !string.IsNullOrEmpty(j.AdmitadCategoryId) && !string.IsNullOrEmpty(j.OriginalId))
                .Select(j => new {j.AdmitadCategoryId, j.OriginalId });

            var query = (from em in excelMatchModels
                         join jm in jsonModels on em.OldCatId equals jm.OriginalId
                         select new KeyValuePair<string, string>(em.NewCatId, jm.AdmitadCategoryId)).Distinct();

            foreach (KeyValuePair<string, string> pair in query)
                pars.Add(pair.Key, pair.Value);
        }

        public void ExportCsv(string fileName)
        {
            if (fileName == null)
                return;
            
// ReSharper disable once AssignNullToNotNullAttribute
            string csvFile = Path.Combine(Path.GetDirectoryName(fileName),"identities.csv");
            if(File.Exists(csvFile))
                File.Delete(csvFile);

            using (TextWriter writer = File.CreateText(csvFile))
            {
                var csvWriter = new CsvWriter(writer);
                foreach (KeyValuePair<string, string> par in pars)
                {
                    csvWriter.WriteField(par.Key);
                    csvWriter.WriteField(par.Value);
                    csvWriter.NextRecord();
                }
            }
        }

        public string this[string newCategoryId]
        {
            get
            {
                if(pars.ContainsKey(newCategoryId))
                    return pars[newCategoryId];
                return null;
            }
        }
    }
}