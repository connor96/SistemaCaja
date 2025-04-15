using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Drawing;

namespace CajaSistema.Controllers
{
    public class ReportesController : Controller
    {
        [HttpGet]
        public IActionResult ReportePagosRealizados(string periodo)
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


            try
            {
                using (var libro = new ExcelPackage())
                {

                    var worksheet = libro.Workbook.Worksheets.Add("Reporte Notas");
                    worksheet.Cells["A3:O3"].Merge = true;
                    worksheet.Cells["A3"].Value = "Reporte de Notas";

                    worksheet.Cells["A3"].Style.Font.Bold = true;
                    worksheet.Cells["A3"].Style.Font.Size = 14;
                    worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells["A4"].Value = "N°";
                    worksheet.Cells["B4"].Value = "FECHA";
                    worksheet.Cells["C4"].Value = "HORA";
                    worksheet.Cells["D4"].Value = "CLIENTE";
                    worksheet.Cells["E4"].Value = "NOMBRE Y APELLIDO";
                    worksheet.Cells["F4"].Value = "USUARIO";
                    worksheet.Cells["G4"].Value = "SERVICIO";
                    worksheet.Cells["H4"].Value = "CURSO";
                    worksheet.Cells["I4"].Value = "IMPORTE";
                    worksheet.Cells["J4"].Value = "DSCTO";
                    worksheet.Cells["K4"].Value = "PAGO MIN";
                    worksheet.Cells["L4"].Value = "BANCO";
                    worksheet.Cells["M4"].Value = "SERVICIO";
                    worksheet.Cells["N4"].Value = "NUM OPERACION";


                    worksheet.Cells["A4:O4"].Style.Numberformat.Format = "#,##0";
                    worksheet.Cells["A4:O4"].Style.Font.Bold = true;
                    worksheet.Cells["A4:O4"].Style.Font.Size = 12;
                    worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells["A4:O4"].Style.Font.Color.SetColor(Color.Black);
                    worksheet.Cells["A4:O4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A4:O4"].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells["A4:O4"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                    int nroCelda = 5;

                    string celdaA = "A";
                    string celdaB = "B";
                    string celdaC = "C";
                    string celdaD = "D";
                    string celdaE = "E";
                    string celdaF = "F";
                    string celdaG = "G";
                    string celdaH = "H";
                    string celdaI = "I";
                    string celdaJ = "J";
                    string celdaK = "K";
                    string celdaL = "L";
                    string celdaM = "M";
                    string celdaN = "N";
                    string celdaO = "O";

                    //foreach (var item in _notasCurso)
                    //{
                    //    celdaA = "A" + nroCelda;
                    //    celdaB = "B" + nroCelda;
                    //    celdaC = "C" + nroCelda;
                    //    celdaD = "D" + nroCelda;
                    //    celdaE = "E" + nroCelda;
                    //    celdaF = "F" + nroCelda;
                    //    celdaG = "G" + nroCelda;
                    //    celdaH = "H" + nroCelda;
                    //    celdaI = "I" + nroCelda;
                    //    celdaJ = "J" + nroCelda;
                    //    celdaK = "K" + nroCelda;
                    //    celdaL = "L" + nroCelda;
                    //    celdaM = "M" + nroCelda;
                    //    celdaN = "N" + nroCelda;
                    //    celdaO = "O" + nroCelda;

                    //    worksheet.Cells[celdaA].Value = nroCelda - 4;
                    //    worksheet.Cells[celdaB].Value = item.ApAlumno;
                    //    worksheet.Cells[celdaC].Value = item.obj_s1;
                    //    worksheet.Cells[celdaD].Value = item.obj_s2;
                    //    worksheet.Cells[celdaE].Value = item.obj_sw;
                    //    worksheet.Cells[celdaF].Value = item.obj_w1;
                    //    worksheet.Cells[celdaG].Value = item.obj_w2;
                    //    worksheet.Cells[celdaH].Value = item.A;
                    //    worksheet.Cells[celdaI].Value = item.alp;
                    //    worksheet.Cells[celdaJ].Value = item.B;
                    //    worksheet.Cells[celdaK].Value = item.final_w;
                    //    worksheet.Cells[celdaL].Value = item.final_o;
                    //    worksheet.Cells[celdaM].Value = item.C;
                    //    worksheet.Cells[celdaN].Value = item.NotaFinal;
                    //    worksheet.Cells[celdaO].Value = item.finalgrade;

                    //    worksheet.Cells[celdaA].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaA].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaB].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    //    worksheet.Cells[celdaC].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaC].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaC].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaD].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaD].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaD].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaE].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaE].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaE].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaG].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaG].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaH].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaH].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaH].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    worksheet.Cells[celdaH].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[celdaH].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                    //    worksheet.Cells[celdaI].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaI].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaI].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaJ].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaJ].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaJ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    worksheet.Cells[celdaJ].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[celdaJ].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                    //    worksheet.Cells[celdaK].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaK].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaK].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaL].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaL].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaL].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //    worksheet.Cells[celdaM].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaM].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaM].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    worksheet.Cells[celdaM].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[celdaM].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                    //    worksheet.Cells[celdaN].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaN].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaN].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    worksheet.Cells[celdaN].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[celdaN].Style.Fill.BackgroundColor.SetColor(Color.LightPink);

                    //    worksheet.Cells[celdaO].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaO].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //    worksheet.Cells[celdaO].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //    worksheet.Cells[celdaO].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[celdaO].Style.Fill.BackgroundColor.SetColor(Color.LightPink);

                    //    nroCelda++;
                    //}

                    worksheet.Cells.AutoFitColumns(0);
                    return File(libro.GetAsByteArray(), excelContentType, "Reporte.xlsx");
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                Console.WriteLine(error);
                throw;
            }



        }

    }
}
