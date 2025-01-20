using CajaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CajaSistema.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentity,RoleIdentity,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }

        //Becados
        public DbSet<BecadosAlumnoBusqueda> becadosListaALumnosBusqueda { get; set; }
        public DbSet<BecadosListaBecados> becadosListaAlumnos { get; set; }
        public DbSet<BecadosAlumnoBecado> becadosAlumnoBecados {  get; set; }

        //Descuento
        public DbSet<DescuentoListaDescuentos> descuentoListaDescuentos { get; set; }
        public DbSet<DescuentoDescuento> descuentoDescuento {  get; set; }

        //Persona
        public DbSet<PersonaPersona> personaPersona {  get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");
            builder.Entity<UserIdentity>(e => { e.ToTable(name: "NetCoreUsers", schema: "CajaWeb"); });
            builder.Entity<IdentityRoleClaim<string>>(e => { e.ToTable(name: "NetCoreRoleClaims", schema: "CajaWeb"); });
            builder.Entity<RoleIdentity>(e => { e.ToTable(name: "NetCoreRoles", schema: "CajaWeb"); });
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

            builder.Entity<BecadosAlumnoBecado>().ToTable(name:"tb_Becados",schema:"Intranet", t => t.ExcludeFromMigrations());

            builder.Entity<BecadosAlumnoBusqueda>().HasNoKey();
            builder.Entity<BecadosAlumnoBusqueda>().ToTable(nameof(BecadosAlumnoBusqueda), t => t.ExcludeFromMigrations());

            builder.Entity<BecadosListaBecados>().HasNoKey();
            builder.Entity<BecadosListaBecados>().ToTable(nameof(BecadosListaBecados), t => t.ExcludeFromMigrations());

            //Descuentos
            builder.Entity<DescuentoListaDescuentos>(act =>
            {
                act.HasKey(col => col.IdDescuento);
                act.Property(col => col.DesDescuento);
                act.Property(col => col.Estado);
            });
            builder.Entity<DescuentoListaDescuentos>().ToTable(name: "Descuento", schema: "Instituciones", t => t.ExcludeFromMigrations());

            builder.Entity<DescuentoDescuento>(act =>
            {
                act.HasKey(col => col.idDescuento);
                act.Property(col => col.idPersona);
                act.Property(col => col.idDescuentoConcepto);
                act.Property(col => col.monto);
                act.Property(col => col.estado);
                act.Property(col => col.usuarioRegistro);
            });
            builder.Entity<DescuentoDescuento>().ToTable(name: "tb_Descuento", schema: "Intranet", t => t.ExcludeFromMigrations());


            //Persona
            builder.Entity<PersonaPersona>().HasNoKey();
            builder.Entity<PersonaPersona>().ToView(name: "v_Persona", schema: "dbo");

        }
    }
}
