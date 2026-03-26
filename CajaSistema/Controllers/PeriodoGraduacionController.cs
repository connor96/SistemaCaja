using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class PeriodoGraduacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PeriodoGraduacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PeriodosGraduacion
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> _ListPeriodoGraduacionPartial()
        {
            var lista = await _context.listaPeriodoGraduacions.ToListAsync();
            return PartialView(lista);
        }

        [HttpGet]
        public async Task<PartialViewResult> _ModalRegistrarPeriodoGraduacion()
        {
            var periodoGraduacion= new PeriodoGraduacion();
            return PartialView(periodoGraduacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearPeriodoGraduacion(PeriodoGraduacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }
            PeriodoGraduacion control = new PeriodoGraduacion();

            control = _context.listaPeriodoGraduacions.Where(x => x.estado==true).FirstOrDefault();

            if (control is not null)
            {
                return Json(new { success = false, message = "Solo se permite un periodo activo" });
            }

            model.fechaRegistro = DateTime.Now;
            model.estado = true;
            model.usuarioRegistro = User.Identity?.Name ?? "Sistema";

            _context.listaPeriodoGraduacions.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Periodo registrado correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> _ModalEditarPeriodoGraduacion(int id)
        {
           
            var periodoGraduacion = await _context.listaPeriodoGraduacions
                                .FirstOrDefaultAsync(x => x.idPeriodoGraduacion == id);

            return PartialView(periodoGraduacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarPeriodoGraduacion(PeriodoGraduacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            model.fechaRegistro = DateTime.Now;
            model.usuarioRegistro = User.Identity?.Name ?? "Sistema";

            _context.listaPeriodoGraduacions.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Periodo actualizado correctamente" });
        }

        [HttpPost]
        public async Task<JsonResult> EliminarPeriodoGraduacion(int? id)
        {
            var periodoGraduacion = await _context.listaPeriodoGraduacions
                .FirstOrDefaultAsync(x=>x.idPeriodoGraduacion==id);
            if (periodoGraduacion != null)
            {
                _context.listaPeriodoGraduacions.Remove(periodoGraduacion);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Periodo eliminado correctamente" });
        }


    }
}
