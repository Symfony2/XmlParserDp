using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Infrastructure.Model;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace XmlParser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<ExcelMatchModel> excelMatchList;
        private JsonMatchModel[] jsonMatchList;
        private XmlDocument xmlDocument;

        public MainViewModel()
        {
            excelMatchList = new List<ExcelMatchModel>();
            xmlDocument = new XmlDocument();
        }

        #region Properties

        private string excelFileName;

        public string ExcelFileName
        {
            get { return excelFileName; }
            set
            {
                excelFileName = value;
                RaisePropertyChanged(() => ExcelFileName);
            }
        }

        private string jsonFileName;

        public string JsonFileName
        {
            get { return jsonFileName; }
            set
            {
                jsonFileName = value;
                RaisePropertyChanged(() => JsonFileName);
            }
        }

        private string xmlFileName;

        public string XmlFileName
        {
            get { return xmlFileName; }
            set
            {
                xmlFileName = value;
                RaisePropertyChanged(() => XmlFileName);
            }
        }

        private bool isJsonParsing;

        public bool IsJsonParsing
        {
            get { return isJsonParsing; }
            set
            {
                isJsonParsing = value;
                RaisePropertyChanged(() => IsJsonParsing);
            }
        }

        private bool isXmlParsing;

        public bool IsXmlParsing
        {
            get { return isXmlParsing; }
            set
            {
                isXmlParsing = value;
                RaisePropertyChanged(() => IsXmlParsing);
            }
        }

        #endregion

        public ICommand OpenExcelDoc
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string fileName = OpenFileDialogResult("Excel Files|*.xls;*.xlsx;*.xlsm");
                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    {
                        ExcelFileName = fileName;
                    }
                });
            }
        }

        public ICommand OpenJsonDoc
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string fileName = OpenFileDialogResult("Json Files|*.json");
                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    {
                        JsonFileName = fileName;
                    }
                });
            }
        }

        public ICommand OpenXmlDoc
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string fileName = OpenFileDialogResult("Xml Files|*.xml");
                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    {
                        XmlFileName = fileName;
                    }
                });
            }
        }

        public ICommand Process
        {
            get { return new RelayCommand(() => { }); }
        }

        private Task ProcessExcelDoc()
        {
            return Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        excelMatchList.Clear();
                        var dataTable = ReadAsDataTable(ExcelFileName);
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var newCatId = int.Parse(row.Field<string>("New_Cat_ID"));
                            var oldCatId = int.Parse(row.Field<string>("Old_Cat_ID"));
                            excelMatchList.Add(new ExcelMatchModel {NewCatId = newCatId, OldCatId = oldCatId});
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                });
        }

        private Task ProcessJsonDoc()
        {
            return Task.Factory.StartNew(() =>
            {
                IsJsonParsing = true;
                try
                {
                    var jsonSerializer = new JsonSerializer();
                    using (var streamReader = new StreamReader(JsonFileName))
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        jsonMatchList = jsonSerializer.Deserialize<JsonMatchModel[]>(reader);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    IsJsonParsing = false;
                }
            });
        }

        private void ProcessXmlDoc()
        {
            Task.Factory.StartNew(() =>
            {
                IsXmlParsing = true;
                try
                {
                    StreamNodes(XmlFileName, new[] {"offer"});
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    IsXmlParsing = false;
                }
            });
        }

        private void StreamNodes(string path, string[] tagNames)
        {
            var settings = new XmlReaderSettings {DtdProcessing = DtdProcessing.Parse};
            var doc = new XmlDocument();

            using (var streamReader = new StreamReader(path))
            using (var xr = XmlReader.Create(streamReader, settings))
            {
                xr.MoveToContent();
                while (true)
                {
                    if (xr.NodeType == XmlNodeType.Element &&
                        tagNames.Contains(xr.Name))
                    {
                        var node = doc.ReadNode(xr);
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
        }

        private string OpenFileDialogResult(string filter)
        {
            var file = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = false
            };
            file.ShowDialog();

            return file.FileName;
        }

        private DataTable ReadAsDataTable(string fileName)
        {
            var dataTable = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets =
                    spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                var worksheetPart = (WorksheetPart) spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                var sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
            dataTable.Rows.RemoveAt(0);

            return dataTable;
        }

        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
    }
}