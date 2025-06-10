using CajaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<BecadosAlumnoBusqueda> becadosListaALumnosBusqueda { get; set; }
        public DbSet<BecadosListaBecados> becadosListaAlumnos { get; set; }
        public DbSet<BecadosAlumnoBecado> becadosAlumnoBecados { get; set; }

        //Descuento
        public DbSet<DescuentoListaDescuentos> descuentoListaDescuentos { get; set; }
        public DbSet<DescuentoDescuento> descuentoDescuento { get; set; }
        public DbSet<DescuentoLista>  descuentoListas { get; set; }


        //public DbSet<UserIdentity> _identityUserDBSet { get; set; }

        //Persona
        public DbSet<PersonaPersona> personaPersona { get; set; }

        //Cajero
        public DbSet<CajeroCajeroActivo> cajeroCajeroActivos { get; set; }

        //Sede
        public DbSet<InstitucionSede> institucionSedes { get; set; }

        //Periodo Matricula
        //public DbSet<PeriodoIntranet> periodoIntranets { get; set; }
        public DbSet<CajeroAsignacionCajero> cajeroAsignacionCajeros { get; set; }

        //Auxiliar

        public DbSet<AuxiliarString> auxiliarStrings { get; set; }
        public DbSet<AuxDobleString> auxDobleStrings { get; set; }  

        //Bancos
        public DbSet<BancoBancos> bancoBancos { get; set; }
        public DbSet<BancoCanalesPago> bancoCanalesPagos { get; set; } 
        public DbSet<BancoFormasPago> bancoFormasPagos { get; set; }

        //Caja Transacciones
        public DbSet<CajaTransaccionCabecera> cajaTransaccionCabecera { get; set; }
        public DbSet<CajaTransaccionListaPendientes> cajaTransaccionListaPendientes { get; set; }
        public DbSet<CajaTransaccionListaTodos> cajaTransaccionListaTodos { get; set; }
        public DbSet<CajaTransaccionDetalleCabecera> cajaTransaccionDetalleCabeceras { get; set; }
        public DbSet<CajaTransaccionDetalleCuerpo> cajaTransaccionDetalleCuerpos { get; set; }
        public DbSet<CajaTransaccionPagados> cajaTransaccionPagados { get; set; }

        //Periodo Matricula
        public DbSet<ListaPeriodo> listaPeriodosPeriodos { get; set; }

        //Redireccion
        public DbSet<RedireccionCajeros> redireccionCajeros { get; set; }

        public DbSet<resumenCajerosPagos> resumenCajerosPagosCaja { get; set; }

        public DbSet<ListaPendientesAlumno> listaPendientesAlumnos { get; set; }

        public DbSet<ListaEliminarAumno> listaEliminarAumnos { get; set; }

        //Reporte Caja
        public DbSet<CajaReporteProcesado> cajaReporteProcesados { get; set; }

        //Examen de Ubicacion
        public DbSet<UbicacionListaExamen> ubicacionListaExamenes { get; set; }

        public DbSet<UbicacionAlumnoBusqueda>  ubicacionAlumnoBusquedas { get; set; }
        public DbSet<UbicacionDetalleModal> ubicacionDetalleModal {  get; set; }

        //Tarifario Otros
        public DbSet<TarifarioOtrosLista> tarifarioOtrosListas { get; set; }
        public DbSet<PostergacionesCursosLista> postergacionesCursosListas { get; set; }
        public DbSet<AlumnoBusqueda> alumnoBusquedas { get; set; }
        public DbSet<OtrosConceptosVarios>  otrosConceptosVarios { get; set; }


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

            //Periodo matricula
            //builder.Entity<PeriodoIntranet>(
            //    act =>
            //    {
            //        act.HasKey(col => col.idPeriodoMatricula);
            //        act.Property(col => col.periodoDescripcion);
            //        act.Property(col => col.periodoTexto);
            //        act.Property(col => col.estado);
            //    }
            //    );
            //builder.Entity<PeriodoIntranet>().ToTable(name: "tb_PeriodoMatricula", schema: "Intranet", t => t.ExcludeFromMigrations());

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



        }
    }
}
