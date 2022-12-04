using System;
using System.IO;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using PsixLibrary.Entity;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelHelper : IDisposable
    {
        private Application _excel;
        private Workbook _workbook;
        private _Worksheet _worksheet1;
        private _Worksheet _worksheet2;
        private Excel.Range _excelRange;
        private string _filePath;

        public ExcelHelper()
        {
            _excel = new Excel.Application();
        }

        public bool Open(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                _workbook = _excel.Workbooks.Add();
                _filePath = filePath;
                _workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public bool OpenNewExcel(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    _workbook = _excel.Workbooks.Open(filePath, false, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public void SetAnswers(List<Answer> answers, string startDt, string finishDt)
        {
            try
            {
                _worksheet1 = (Excel.Worksheet) _workbook.Worksheets.get_Item(1);
                _worksheet1.Name = "Отчет";
                _worksheet1.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                object[,] exportAnswer = new object[answers.Count, 4];

                for (int i = 0; i < answers.Count; i++)
                {
                    exportAnswer[i, 1] = answers[i].DateTime.ToString("d");
                    exportAnswer[i, 2] = $"{answers[i].Appeal.User.Surname} {answers[i].Appeal.User.Name}";
                    exportAnswer[i, 3] = answers[i].Appeal.TypeAppeal.TypeName;
                }

                for (int i = 0; i < answers.Count; i++)
                {
                    exportAnswer[i, 0] = i + 1;
                }

                _excelRange = _worksheet1.get_Range("A5", Missing.Value);
                _excelRange = _excelRange.get_Resize(answers.Count, 4);
                _excelRange.set_Value(Missing.Value, exportAnswer);
                _excelRange.Columns.AutoFit();
                _worksheet1.Cells[1, 5] =
                    $"Отчет от {DateTime.Parse(startDt).ToString("d")} до {DateTime.Parse(finishDt).ToString("d")}";
                _worksheet1.Cells[2, 4] = "Специалист:";
                _worksheet1.Cells[2, 5] =
                    $"{answers[0].Psychologist.Surname} {answers[0].Psychologist.Name} {answers[0].Psychologist.MiddleName}";
                _worksheet1.Cells[4, 1] = "№";
                _worksheet1.Cells[4, 2] = "Дата Обращения";
                _worksheet1.Cells[4, 3] = "Фамилия Имя";
                _worksheet1.Cells[4, 4] = "Тип обращения";
                _worksheet1.Cells[3, 4] = "Обработанных обращений:";
                _worksheet1.Cells[3, 5] = answers.Count;
                _worksheet1.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void Dispose()
        {
            try
            {
                if (!_excel.Visible)
                {
                    _workbook.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                _workbook.SaveAs(_filePath);
                _filePath = null;
            }
            else
            {
                _workbook.Save();
            }
        }
    }
}