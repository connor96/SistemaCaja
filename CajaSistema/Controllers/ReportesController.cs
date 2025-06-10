using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Drawing;

namespace CajaSistema.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;

        private string idUsuarioActivo = "0000000001";


        public ReportesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult ReportePagosRealizados(string periodo)
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            idUsuarioActivo = HttpContext.Session.GetString("idPersona");

            var parameters = new[]
            {
                new SqlParameter("@idCajero",idUsuarioActivo),
                new SqlParameter("@periodo",periodo),

            };

            var reporteCaja = _appdbContext.cajaReporteProcesados.FromSqlRaw("CajaWeb.sp_CajaReporteProcesados @idCajero,@periodo", parameters).ToList();

            try
            {
                using (var libro = new ExcelPackage())
                {

                    var worksheet = libro.Workbook.Worksheets.Add("Reporte Caja");
                    worksheet.Cells["A3:O3"].Merge = true;
                    worksheet.Cells["A3"].Value = "Reporte Caja";

                    worksheet.Cells["A3"].Style.Font.Bold = true;
                    worksheet.Cells["A3"].Style.Font.Size = 14;
                    worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells["A4"].Value = "N°";
                    worksheet.Cells["B4"].Value = "FECHA";
                    worksheet.Cells["C4"].Value = "HORA";
                    worksheet.Cells["D4"].Value = "TIPO PAGO";
                    worksheet.Cells["E4"].Value = "CLIENTE";
                    worksheet.Cells["F4"].Value = "NOMBRE Y APELLIDO";
                    worksheet.Cells["G4"].Value = "USUARIO";
                    worksheet.Cells["H4"].Value = "CURSO";
                    worksheet.Cells["I4"].Value = "IMPORTE";
                    worksheet.Cells["J4"].Value = "DSCTO";
                    worksheet.Cells["K4"].Value = "PAGO MIN";
                    worksheet.Cells["L4"].Value = "BANCO";
                    worksheet.Cells["M4"].Value = "CUENTA";
                    worksheet.Cells["N4"].Value = "SERVICIO";
                    worksheet.Cells["O4"].Value = "NUM OPERACION";


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

                    foreach (var item in reporteCaja)
                    {
                        celdaA = "A" + nroCelda;
                        celdaB = "B" + nroCelda;
                        celdaC = "C" + nroCelda;
                        celdaD = "D" + nroCelda;
                        celdaE = "E" + nroCelda;
                        celdaF = "F" + nroCelda;
                        celdaG = "G" + nroCelda;
                        celdaH = "H" + nroCelda;
                        celdaI = "I" + nroCelda;
                        celdaJ = "J" + nroCelda;
                        celdaK = "K" + nroCelda;
                        celdaL = "L" + nroCelda;
                        celdaM = "M" + nroCelda;
                        celdaN = "N" + nroCelda;
                        celdaO = "O" + nroCelda;

                        worksheet.Cells[celdaA].Value = nroCelda - 4;
                        worksheet.Cells[celdaB].Value = item.fechaTransaccion;
                        worksheet.Cells[celdaC].Value = item.horaTransaccion;
                        worksheet.Cells[celdaD].Value = item.tipoPago;
                        worksheet.Cells[celdaE].Value = item.dni;
                        worksheet.Cells[celdaF].Value = item.nombreCompleto;
                        worksheet.Cells[celdaG].Value = item.usuario;
                        worksheet.Cells[celdaH].Value = item.curso;
                        worksheet.Cells[celdaI].Value = item.importe;
                        worksheet.Cells[celdaJ].Value = item.descuento;
                        worksheet.Cells[celdaK].Value = item.pagoMinimo;
                        worksheet.Cells[celdaL].Value = item.banco;
                        worksheet.Cells[celdaM].Value = item.cuenta;
                        worksheet.Cells[celdaN].Value = item.servicio;
                        worksheet.Cells[celdaO].Value = item.numOperacion;

                        worksheet.Cells[celdaA].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaA].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaB].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        worksheet.Cells[celdaC].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaC].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaC].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaD].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaD].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaD].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaE].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaE].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaE].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaG].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaG].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaH].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaH].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaH].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[celdaH].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //worksheet.Cells[celdaH].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                        worksheet.Cells[celdaI].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaI].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaI].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaJ].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaJ].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaJ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[celdaJ].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //worksheet.Cells[celdaJ].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                        worksheet.Cells[celdaK].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaK].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaK].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaL].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaL].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaL].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[celdaM].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaM].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaM].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[celdaM].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //worksheet.Cells[celdaM].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                        worksheet.Cells[celdaN].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaN].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaN].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[celdaN].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //worksheet.Cells[celdaN].Style.Fill.BackgroundColor.SetColor(Color.LightPink);

                        worksheet.Cells[celdaO].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaO].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[celdaO].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[celdaO].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //worksheet.Cells[celdaO].Style.Fill.BackgroundColor.SetColor(Color.LightPink);

                        nroCelda++;
                    }

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
