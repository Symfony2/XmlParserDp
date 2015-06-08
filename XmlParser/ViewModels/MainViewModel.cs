using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Infrastructure.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using XmlParser.Services;

namespace XmlParser.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<ExcelMatchModel> excelMatchList;
        private JsonMatchModel[] jsonMatchList;
        private CancellationTokenSource cancellationTokenSource;
        private IdentityWrapperService identityMatches;
        private XmlProcessorService xmlProcessorService;
        private TaskScheduler taskScheduler;

        public MainViewModel()
        {
            excelMatchList = new List<ExcelMatchModel>();
            cancellationTokenSource = new CancellationTokenSource();
            taskScheduler = TaskScheduler.Current;
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

        private bool replaceCategoryId;
        public bool ReplaceCategoryId
        {
            get { return replaceCategoryId; }
            set
            {
                replaceCategoryId = value;
                RaisePropertyChanged(() => ReplaceCategoryId);
            }
        }

        private bool groupingApply;
        public bool GroupingApply
        {
            get { return groupingApply; }
            set
            {
                groupingApply = value;
                RaisePropertyChanged(() => GroupingApply);
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
            get
            {
                return new RelayCommand(() =>
                {
                    const TaskContinuationOptions notOnFaulted = TaskContinuationOptions.NotOnFaulted;
                    Task.WhenAll(ProcessExcelDoc(), ProcessJsonDoc())
                                                      .ContinueWith(t =>
                                                      {
                                                          identityMatches = new IdentityWrapperService(excelMatchList, jsonMatchList);
                                                          identityMatches.Initialize();
                                                          xmlProcessorService = new XmlProcessorService(identityMatches);

                                                      }, notOnFaulted)
                                                      .ContinueWith(t => ProcessXmlDoc(), notOnFaulted);
                });
            }
        }

        public ICommand Stop
        {
            get
            {
                return new RelayCommand(() => cancellationTokenSource.Cancel());
            }
        }

        private Task ProcessExcelDoc()
        {
            CancellationToken token = cancellationTokenSource.Token;
            Task processExcelDocTask = Task.Factory.StartNew(
                () =>
                {
                    if (string.IsNullOrEmpty(ExcelFileName))
                        return;
                    try
                    {
                        excelMatchList.Clear();
                        var dataTable = ReadAsDataTable(ExcelFileName);
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var newCatId = row.Field<string>("New_Cat_ID");
                            var oldCatId = row.Field<string>("Old_Cat_ID");
                            excelMatchList.Add(new ExcelMatchModel { NewCatId = newCatId, OldCatId = oldCatId });
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }, token);
            return processExcelDocTask;
        }

        private Task ProcessJsonDoc()
        {
            CancellationToken token = cancellationTokenSource.Token;
            Task processJsonDocTask = Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(JsonFileName))
                    return;
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
            }, token);
            return processJsonDocTask;
        }

        private Task ProcessXmlDoc()
        {
            CancellationToken token = cancellationTokenSource.Token;
            Task processXmlDocTask = Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(XmlFileName))
                    return;
                IsXmlParsing = true;
                try
                {
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Xml Files|*.xml";
                    saveFileDialog.ShowDialog();


                    identityMatches.ExportCsv(saveFileDialog.SafeFileName);
                    if (!string.IsNullOrEmpty(saveFileDialog.SafeFileName))
                    {
                        xmlProcessorService.ChangeInnerStructure(XmlFileName, saveFileDialog.FileName, replaceCategoryId, groupingApply);
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    
                }
                finally
                {
                    IsXmlParsing = false;
                }
            }, token, TaskCreationOptions.LongRunning, taskScheduler);
            return processXmlDocTask;
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
                var worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
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