using CajaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace CajaSistema.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentity, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Becados
        public DbSet<BecadosAlumnoBusqueda> becadosListaALumnosBusqueda { get; set; } = default!;
        public DbSet<BecadosListaBecados> becadosListaAlumnos { get; set; } = default!;
        public DbSet<BecadosAlumnoBecado> becadosAlumnoBecados { get; set; } = default!;

        //Descuento
        public DbSet<DescuentoListaDescuentos> descuentoListaDescuentos { get; set; } = default!;
        public DbSet<DescuentoDescuento> descuentoDescuento { get; set; } = default!;
        public DbSet<DescuentoLista> descuentoListas { get; set; } = default!;


        //public DbSet<UserIdentity> _identityUserDBSet { get; set; }

        //Persona
        public DbSet<PersonaPersona> personaPersona { get; set; } = default!;

        //Cajero
        public DbSet<CajeroCajeroActivo> cajeroCajeroActivos { get; set; } = default!;

        //Sede
        public DbSet<InstitucionSede> institucionSedes { get; set; } = default!;

        //Periodo Matricula
        //public DbSet<PeriodoIntranet> periodoIntranets { get; set; }
        public DbSet<CajeroAsignacionCajero> cajeroAsignacionCajeros { get; set; } = default!;

        //Auxiliar

        public DbSet<AuxiliarString> auxiliarStrings { get; set; } = default!;
        public DbSet<AuxDobleString> auxDobleStrings { get; set; } = default!;

        //Bancos
        public DbSet<BancoBancos> bancoBancos { get; set; } = default!;
        public DbSet<BancoCanalesPago> bancoCanalesPagos { get; set; } = default!;
        public DbSet<BancoFormasPago> bancoFormasPagos { get; set; } = default!;

        //Caja Transacciones
        public DbSet<CajaTransaccionCabecera> cajaTransaccionCabecera { get; set; } = default!;
        public DbSet<CajaTransaccionListaPendientes> cajaTransaccionListaPendientes { get; set; } = default!;
        public DbSet<CajaTransaccionListaTodos> cajaTransaccionListaTodos { get; set; } = default!;
        public DbSet<CajaTransaccionDetalleCabecera> cajaTransaccionDetalleCabeceras { get; set; } = default!;
        public DbSet<CajaTransaccionDetalleCuerpo> cajaTransaccionDetalleCuerpos { get; set; } = default!;
        public DbSet<CajaTransaccionPagados> cajaTransaccionPagados { get; set; } = default!;

        //Periodo Matricula
        public DbSet<ListaPeriodo> listaPeriodosPeriodos { get; set; } = default!;

        //Redireccion
        public DbSet<RedireccionCajeros> redireccionCajeros { get; set; } = default!;

        public DbSet<resumenCajerosPagos> resumenCajerosPagosCaja { get; set; } = default!;

        public DbSet<ListaPendientesAlumno> listaPendientesAlumnos { get; set; } = default!;

        public DbSet<ListaEliminarAumno> listaEliminarAumnos { get; set; } = default!;

        //Reporte Caja
        public DbSet<CajaReporteProcesado> cajaReporteProcesados { get; set; } = default!;

        //Examen de Ubicacion
        public DbSet<UbicacionListaExamen> ubicacionListaExamenes { get; set; } = default!;

        public DbSet<UbicacionAlumnoBusqueda> ubicacionAlumnoBusquedas { get; set; } = default!;
        public DbSet<UbicacionDetalleModal> ubicacionDetalleModal { get; set; } = default!;

        //Tarifario Otros
        public DbSet<TarifarioOtrosLista> tarifarioOtrosListas { get; set; } = default!;
        public DbSet<PostergacionesCursosLista> postergacionesCursosListas { get; set; } = default!;
        public DbSet<AlumnoBusqueda> alumnoBusquedas { get; set; } = default!;
        public DbSet<OtrosConceptosVarios> otrosConceptosVarios { get; set; } = default!;

        //Certificado
        public DbSet<CertificadosLista> certificadosListas { get; set; } = default!;

        public DbSet<ReporteNotas> reporteNotasListas { get; set; } = default!;

        //Passwords

        public DbSet<UsuarioPassword> usuarioPasswords { get; set; } = default!;

        //Conceptos Libres
        public DbSet<TablaConceptosLibres> tablaConceptosLibre { get; set; } = default!;

        // Editar Transacciones
        public DbSet<EditarTransacciones> editarTransaccionesLista { get; set; } = default!;

        //Transacciones
        public DbSet<IntranetTransacciones> intranetTransaccionesLista { get; set; } = default!;
        public DbSet<IntranetTransaccionesDetalle> IntranetTransaccionesDetalleLista { get; set; } = default!;

        //Registro Actividad
        public DbSet<RegistroActividad> registroActividades { get; set; } = default!;

        //Reporte Actividad
        public DbSet<CalendarioReporte> calendarioReportes { get; set; } = default!;

        //Periodo Graduacion
        public DbSet<PeriodoGraduacion> listaPeriodoGraduacions { get; set; } = default!;

        //programacion
        public DbSet<ProgramacionNivel> programacionNivels { get; set; } = default!;
        public DbSet<ProgramacionCursos> ProgramacionCursos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");
            builder.Entity<UserIdentity>(e => { e.ToTable(name: "NetCoreUsers", schema: "CajaWeb"); });
            builder.Entity<IdentityRoleClaim<string>>(e => { e.ToTable(name: "NetCoreRoleClaims", schema: "CajaWeb"); });
            builder.Entity<IdentityRole>(e => { e.ToTable(name: "NetCoreRoles", schema: "CajaWeb"); });
            builder.Entity<IdentityUserClaim<string>>(e => { e.ToTable(name: "NetCoreUserClaims", schema: "CajaWeb"); });
            builder.Entity<IdentityUserLogin<string>>(e => { e.ToTable(name: "NetCoreUserLogins", schema: "CajaWeb"); });
            builder.Entity<IdentityUserToken<string>>(e => { e.ToTable(name: "NetCoreUserTokens", schema: "CajaWeb"); });
            builder.Entity<IdentityUserRole<string>>(e => { e.ToTable(name: "NetCoreUserRoles", schema: "CajaWeb"); });
            

            //Becados
            builder.Entity<BecadosAlumnoBecado>(act =>
            {
                act.HasKey(col => col.idBecado);
                act.Property(col => col.idPersona);
                act.Property(col => col.estado);
                act.Property(col => col.usuarioRegistro);
                act.Property(col => col.fechaRegistro);

            });

            builder.Entity<BecadosAlumnoBecado>().ToTable(name: "tb_Becados", schema: "Intranet", t => t.ExcludeFromMigrations());

            builder.Entity<BecadosAlumnoBusqueda>().HasNoKey();
            builder.Entity<BecadosAlumnoBusqueda>().ToTable(nameof(BecadosAlumnoBusqueda), t => t.ExcludeFromMigrations());

            builder.Entity<BecadosListaBecados>().HasNoKey();
            builder.Entity<BecadosListaBecados>().ToTable(nameof(BecadosListaBecados), t => t.ExcludeFromMigrations());

            //Descuentos
            builder.Entity<DescuentoListaDescuentos>(act =>
            {
                act.HasKey(col => col.IdDescuento);
                act.Property(col => col.IdDescuento).HasColumnType("TINYINT").HasConversion<byte>();
                act.Property(col => col.DesDescuento);
                act.Property(col => col.Estado).HasColumnType("TINYINT").HasConversion<byte>();
            });
            builder.Entity<DescuentoListaDescuentos>().ToTable(name: "Descuento", schema: "Instituciones", t => t.ExcludeFromMigrations());

            builder.Entity<DescuentoDescuento>(act =>
            {
                act.HasKey(col => col.idDescuento);
                act.Property(col => col.idPersona);
                act.Property(col => col.idDescuentoConcepto).HasColumnType("TINYINT").HasConversion<byte>();
                act.Property(col => col.monto).HasColumnType("decimal").HasConversion<double>();
                act.Property(col => col.estado);
                act.Property(col => col.usuarioRegistro);
            });
            builder.Entity<DescuentoDescuento>().ToTable(name: "tb_Descuento", schema: "Intranet", t => t.ExcludeFromMigrations());

            builder.Entity<DescuentoLista>().HasNoKey().ToTable(nameof(DescuentoLista), t => t.ExcludeFromMigrations());


            //Persona
            builder.Entity<PersonaPersona>(
                act =>
                {
                    act.HasNoKey();
                    act.Property(col => col.IdPersona);
                    act.Property(col => col.ApellidoPaterno);
                    act.Property(col => col.ApellidoMaterno);
                    act.Property(col => col.Nombres1);
                    act.Property(col => col.Nombres2);
                    act.Property(col => col.Name);
                    act.Property(col => col.DNI);
                    act.Property(col => col.FNacimiento);
                    act.Property(col => col.Email);
                    act.Property(col => col.Telefono);

                });
            builder.Entity<PersonaPersona>().ToView(name: "v_Persona");

            //Cajero
            builder.Entity<CajeroCajeroActivo>(
                act =>
                {
                    act.HasKey(col=>col.idCajeroActivo);
                    act.Property(col => col.usuarioCajero);
                    act.Property(col => col.usuarioRegistro);
                    act.Property(col => col.fechaRegistro);
                }
            );
            builder.Entity<CajeroCajeroActivo>().ToTable(name: "tb_CajerosActivo", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            //Sede
            builder.Entity<InstitucionSede>(
                act =>
                {
                    act.HasKey(col => col.IdSede);
                    act.Property(col => col.IdSede).HasColumnType("TINYINT").HasConversion<byte>();
                    act.Property(col => col.Sede);
                    act.Property(col => col.Estado).HasColumnType("TINYINT").HasConversion<byte>(); ;
                }
            );
            builder.Entity<InstitucionSede>().ToTable(name: "Sede", schema: "Instituciones", t => t.ExcludeFromMigrations());

          
            builder.Entity<CajeroAsignacionCajero>(
                act =>
                {
                    act.HasKey(col => col.idCajeroAsignacion);
                    act.Property(col=>col.idSede);
                    act.Property(col => col.periodo);
                    act.Property(col => col.usuarioCajero);
                    act.Property(col => col.estado);
                    act.Property(col => col.fechaRegistro);
                    act.Property(col => col.usuarioRegistro);
                });
            builder.Entity<CajeroAsignacionCajero>().ToTable(name: "tb_AsignacionCajero", schema: "CajaWeb", t => t.ExcludeFromMigrations());


            builder.Entity<AuxDobleString>().HasNoKey().ToTable(nameof(AuxDobleString), t => t.ExcludeFromMigrations());

            builder.Entity<AuxiliarString>().HasNoKey().ToTable(nameof(AuxiliarString), t => t.ExcludeFromMigrations());


            //Bancos
            builder.Entity<BancoBancos>(
                act =>
                {
                    act.HasKey(col => col.idBanco);
                    act.Property(col => col.codigoBanco);
                    act.Property(col => col.descripcionBanco);
                    act.Property(col => col.fechaRegistro);
                    act.Property(col => col.usuarioRegistro);

                });
            builder.Entity<BancoBancos>().ToTable(name: "tb_BancoBancos", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            builder.Entity<BancoCanalesPago>(
                act =>
                {
                    act.HasKey(col => col.idCanalBanco);
                    act.Property(col => col.codigoCanal);
                    act.Property(col => col.descripcionCanal);
                    act.Property(col => col.fechaRegistro);
                    act.Property(col => col.usuarioRegistro);

                });
            builder.Entity<BancoCanalesPago>().ToTable(name: "tb_BancoCanalesPago", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            builder.Entity<BancoFormasPago>(
                act =>
                {
                    act.HasKey(col => col.idFormaPago);
                    act.Property(col => col.codigoFormaPago);
                    act.Property(col => col.descripcionFormaPago);
                    act.Property(col => col.fechaRegistro);
                    act.Property(col => col.usuarioRegistro);

                });
            builder.Entity<BancoFormasPago>().ToTable(name: "tb_BancoFormasPago", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            //Caja Transaccion
            builder.Entity<CajaTransaccionCabecera>().HasNoKey().ToTable(nameof(CajaTransaccionCabecera),t=>t.ExcludeFromMigrations());


            builder.Entity<CajaTransaccionListaPendientes>().HasNoKey().ToTable(nameof(CajaTransaccionListaPendientes), t => t.ExcludeFromMigrations());

            builder.Entity<CajaTransaccionListaTodos>().HasNoKey().ToTable(nameof(CajaTransaccionListaTodos), t => t.ExcludeFromMigrations());


            builder.Entity<CajaTransaccionDetalleCabecera>(
                act =>
                {
                    act.Property(col => col.Monto).HasColumnType("decimal(18,2)").HasConversion<decimal>();
                });
             builder.Entity < CajaTransaccionDetalleCabecera>().HasNoKey().ToTable(nameof(CajaTransaccionDetalleCabecera), t => t.ExcludeFromMigrations());
            builder.Entity<CajaTransaccionDetalleCuerpo>(

                act =>
                {
                    act.Property(col => col.Monto).HasColumnType("decimal(18,2)").HasConversion<decimal>();
                });
            builder.Entity<CajaTransaccionDetalleCuerpo>().HasNoKey().ToTable(nameof(CajaTransaccionDetalleCuerpo),t=>t.ExcludeFromMigrations());


            //Lista Periodo
            builder.Entity<ListaPeriodo>(
                act =>
                {
                    act.HasKey(col => col.idPeriodoMatricula);
                    act.Property(col => col.periodoTexto);
                    act.Property(col => col.periodoDescripcion);
                    act.Property(col => col.fechaInicioMatricula);
                    act.Property(col => col.fechaFinMatricula);
                    act.Property(col => col.estado);
                }
            );
            builder.Entity<ListaPeriodo>().ToTable(name: "tb_PeriodoMatricula", schema: "Intranet", t => t.ExcludeFromMigrations());

            //Redireccion Cajeros
            builder.Entity<RedireccionCajeros>().HasNoKey().ToTable(nameof(RedireccionCajeros), t => t.ExcludeFromMigrations());

            builder.Entity<CajaTransaccionPagados>().HasNoKey().ToTable(nameof(CajaTransaccionPagados), t => t.ExcludeFromMigrations());

            builder.Entity<resumenCajerosPagos>().HasNoKey().ToTable(nameof(resumenCajerosPagos), t => t.ExcludeFromMigrations());


            builder.Entity<ListaPendientesAlumno>().HasNoKey().ToTable(nameof(ListaPendientesAlumno), t => t.ExcludeFromMigrations());

            builder.Entity<ListaEliminarAumno>().HasNoKey().ToTable(nameof(ListaEliminarAumno), t => t.ExcludeFromMigrations());

            //Reporte Cajero

            builder.Entity<CajaReporteProcesado>().HasNoKey().ToTable(nameof(CajaReporteProcesado), t => t.ExcludeFromMigrations());



            //Examen de Ubicacion
            builder.Entity<UbicacionListaExamen>(
                act =>
                {
                    act.HasKey(col => col.IdExamenUbicacion);
                    act.Property(col => col.IdPersona);
                    act.Property(col => col.CodCurso);
                    act.Property(col => col.Estado);
                    act.Property(col => col.Periodo);
                    act.Property(col => col.UsuarioRegistro);
                    act.Property(col => col.FechaRegistro);
                }
            );
            builder.Entity<UbicacionListaExamen>().ToTable(name: "tb_ExamenUbicacion", schema: "Intranet", t => t.ExcludeFromMigrations());

            builder.Entity<UbicacionAlumnoBusqueda>().HasNoKey().ToTable(nameof(UbicacionAlumnoBusqueda), t => t.ExcludeFromMigrations());

            builder.Entity<UbicacionDetalleModal>().HasNoKey().ToTable(nameof(UbicacionDetalleModal), t => t.ExcludeFromMigrations());


            //Tarifario otros

            builder.Entity<TarifarioOtrosLista>(
                act =>
                {
                    act.HasKey(col => col.idTarifario);
                    act.Property(col => col.descripcion);
                    act.Property(col => col.academico);
                    act.Property(col => col.administrado);
                    act.Property(col => col.monto);
                    act.Property(col => col.estado);
                    act.Property(col => col.usuarioRegistro);
                    act.Property(col => col.fechaRegistro);
                }
            );
            builder.Entity<TarifarioOtrosLista>().ToTable(name: "tb_Tarifario_Otros", schema: "Intranet", t => t.ExcludeFromMigrations());


            builder.Entity<PostergacionesCursosLista>(
                act =>
                {
                    act.HasKey(col => col.idPostergacion);
                    act.Property(col => col.idPersona);
                    act.Property(col => col.descripcion);
                    act.Property(col => col.estado);
                    act.Property(col => col.usuarioRegistro);
                    act.Property(col => col.fechaRegistro);
                }
            );

            builder.Entity<PostergacionesCursosLista>().ToTable(name: "tb_PostergacionesCursos", schema: "Intranet", t => t.ExcludeFromMigrations());

            builder.Entity<AlumnoBusqueda>().HasNoKey().ToTable(nameof(AlumnoBusqueda), t => t.ExcludeFromMigrations());


            builder.Entity<OtrosConceptosVarios>(
                act =>
                {
                    act.HasKey(col => col.idConceptosVarios);
                    act.Property(col => col.idMatricula);
                    act.Property(col => col.idPersona);
                    act.Property(col => col.descripcion);
                    act.Property(col => col.idSede);
                    act.Property(col => col.codCurso);
                    act.Property(col => col.periodo);
                    act.Property(col => col.estado);
                    act.Property(col => col.idTarifarioOtros);
                    act.Property(col => col.idTransaccion);
                    act.Property(col => col.usuarioRegistro);
                    act.Property(col => col.fechaRegistro);
                }
            );
            builder.Entity<OtrosConceptosVarios>().ToTable(name: "tb_OtrosConceptosVarios", schema: "Intranet", t => t.ExcludeFromMigrations());

            //Certificado Lista

            builder.Entity<CertificadosLista>().HasNoKey().ToTable(nameof(CertificadosLista), t => t.ExcludeFromMigrations());

            builder.Entity<ReporteNotas>().HasNoKey().ToTable(nameof(ReporteNotas), t => t.ExcludeFromMigrations());

            builder.Entity<UsuarioPassword>().HasNoKey().ToTable(nameof(UsuarioPassword), t => t.ExcludeFromMigrations());

            builder.Entity<TablaConceptosLibres>().HasNoKey().ToTable(nameof(TablaConceptosLibres), t => t.ExcludeFromMigrations());
            builder.Entity<EditarTransacciones>().HasNoKey().ToTable(nameof(EditarTransacciones), t => t.ExcludeFromMigrations());


            //Intranet Transacciones

            builder.Entity<IntranetTransacciones>(
                act =>
                {
                    act.HasKey(col => col.IdTransacciones);
                    act.Property(col => col.Idpersona);
                    act.Property(col => col.Periodo);
                    act.Property(col => col.Estado);
                    act.Property(col => col.MontoTotal);
                    act.Property(col => col.IdSede);
                    act.Property(col => col.FechaRegistro);
                    act.Property(col => col.UsuarioRegistro);
                    act.Property(col => col.FechaActualizacion);
                    act.Property(col => col.UsuarioActualizacion);
                }
            );

            builder.Entity<IntranetTransacciones>().ToTable(name: "tb_Transacciones", schema: "Intranet", t => t.ExcludeFromMigrations());


            builder.Entity<IntranetTransaccionesDetalle>(
                act =>
                {
                    act.HasKey(col => col.IdTransaccionesDetalle);
                    act.Property(col => col.IdTransacciones);
                    act.Property(col => col.IdConceptoMatricula);
                    act.Property(col => col.IdConceptoOtros);
                    act.Property(col => col.IdDescuento);
                    act.Property(col => col.IdMatricula);
                    act.Property(col => col.Descripcion);
                    act.Property(col => col.Monto);
                    act.Property(col => col.FechaRegistro);
                    act.Property(col => col.UsuarioRegistro);
                    act.Property(col => col.FechaActualizacion);
                    act.Property(col => col.UsuarioActualizacion);
                    act.Property(col => col.idCiclo);
                }
            );
            builder.Entity<IntranetTransaccionesDetalle>().ToTable(name: "tb_TransaccionesDetalle", schema: "Intranet", t => t.ExcludeFromMigrations());

            //Registro Actividad

            builder.Entity<RegistroActividad>(
                act =>
                {
                    act.HasKey(col => col.intIdRegistro);
                    act.Property(col => col.strNombre);
                    act.Property(col => col.strFechaRegistro);
                    act.Property(col => col.strUsuarioRegistro);
                    act.Property(col => col.strColor);
                }
            );
            builder.Entity<RegistroActividad>().ToTable(name: "tb_RegistroActividades", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            //Reporte Calendario

            builder.Entity<CalendarioReporte>().HasNoKey().ToTable(nameof(CalendarioReporte), t => t.ExcludeFromMigrations());

            //Registro Actividad

            builder.Entity<PeriodoGraduacion>(
                act =>
                {
                    act.HasKey(col => col.idPeriodoGraduacion);
                    //act.Property(col => col.periodoTexto);
                    act.Property(col => col.periodoDescripcion);
                    act.Property(col => col.estado);
                    act.Property(col => col.fechaRegistro);
                    act.Property(col => col.usuarioRegistro);
                }
            );
            builder.Entity<PeriodoGraduacion>().ToTable(name: "tb_PeriodoGraduacion", schema: "CajaWeb", t => t.ExcludeFromMigrations());

            //Programacion Nivel
            builder.Entity<ProgramacionNivel>(
                act =>
                {
                    act.HasKey(col => col.CodNivelCurso);
                    act.Property(col => col.IdMallaCurricular);
                    act.Property(col => col.DesNivelCurso);
                    act.Property(col => col.Orden);
                    act.Property(col => col.Estado);
                    act.Property(col => col.EsCurso);
                }
            );
            builder.Entity<ProgramacionNivel>().ToTable(name: "NivelCurso", schema: "Programacion", t => t.ExcludeFromMigrations());

            //Programacion Curso
            builder.Entity<ProgramacionCursos>(
                act =>
                {
                    act.HasKey(col => col.CodCurso);
                    act.Property(col => col.CodNivelCurso);
                    act.Property(col => col.DesCurso);
                    act.Property(col => col.Orden);
                    act.Property(col => col.Estado);
                }
            );
            builder.Entity<ProgramacionCursos>().ToTable(name: "Curso", schema: "Programacion", t => t.ExcludeFromMigrations());




        }
    }
}
