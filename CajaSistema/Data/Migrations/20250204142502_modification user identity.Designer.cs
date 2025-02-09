﻿// <auto-generated />
using System;
using CajaSistema.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CajaSistema.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250204142502_modification user identity")]
    partial class modificationuseridentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CajaSistema.Models.AuxDobleString", b =>
                {
                    b.Property<string>("cadena1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cadena2")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("AuxDobleString", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BancoBancos", b =>
                {
                    b.Property<int>("idBanco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idBanco"));

                    b.Property<string>("codigoBanco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcionBanco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idBanco");

                    b.ToTable("tb_BancoBancos", "CajaWeb", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BancoCanalesPago", b =>
                {
                    b.Property<int>("idCanalBanco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCanalBanco"));

                    b.Property<string>("codigoCanal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcionCanal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCanalBanco");

                    b.ToTable("tb_BancoCanalesPago", "CajaWeb", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BancoFormasPago", b =>
                {
                    b.Property<int>("idFormaPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idFormaPago"));

                    b.Property<string>("codigoFormaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcionFormaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idFormaPago");

                    b.ToTable("tb_BancoFormasPago", "CajaWeb", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BecadosAlumnoBecado", b =>
                {
                    b.Property<int>("idBecado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idBecado"));

                    b.Property<bool?>("estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("idPersona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idBecado");

                    b.ToTable("tb_Becados", "Intranet", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BecadosAlumnoBusqueda", b =>
                {
                    b.Property<string>("fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idPersona")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BecadosAlumnoBusqueda", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.BecadosListaBecados", b =>
                {
                    b.Property<string>("estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fechaRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idBecado")
                        .HasColumnType("int");

                    b.Property<string>("idPersona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombresApellidos")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BecadosListaBecados", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.CajaTransaccionCabecera", b =>
                {
                    b.Property<DateOnly>("FechaTransaccion")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("HoraTransaccion")
                        .HasColumnType("time");

                    b.Property<string>("Horario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTransaccionesPagadas")
                        .HasColumnType("int");

                    b.Property<string>("Modalidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("curso")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CajaTransaccionCabecera", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.CajaTransaccionDetalleCabecera", b =>
                {
                    b.Property<string>("Alumno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CanalPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoBanco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cuenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetalleSede")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("FechaTransaccion")
                        .HasColumnType("date");

                    b.Property<string>("FormaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("HoraTransaccion")
                        .HasColumnType("time");

                    b.Property<string>("IdPersona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSede")
                        .HasColumnType("int");

                    b.Property<int>("IdTransaccionesPagadas")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumOperacionBanco")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CajaTransaccionDetalleCabecera", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.CajaTransaccionDetalleCuerpo", b =>
                {
                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTransaccionesDetalle")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("aula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("curso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("docente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("horario")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CajaTransaccionDetalleCuerpo", "dbo", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.CajeroAsignacionCajero", b =>
                {
                    b.Property<int>("idCajeroAsignacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCajeroAsignacion"));

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("idSede")
                        .HasColumnType("int");

                    b.Property<string>("periodo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usuarioCajero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCajeroAsignacion");

                    b.ToTable("tb_AsignacionCajero", "CajaWeb", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.CajeroCajeroActivo", b =>
                {
                    b.Property<int>("idCajeroActivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idCajeroActivo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCajeroActivo"));

                    b.Property<DateTime?>("fechaRegistro")
                        .HasColumnType("datetime2")
                        .HasColumnName("fechaRegistro");

                    b.Property<string>("usuarioCajero")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("usuarioCajero");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("usuarioRegistro");

                    b.HasKey("idCajeroActivo");

                    b.ToTable("tb_CajerosActivo", "CajaWeb", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.DescuentoDescuento", b =>
                {
                    b.Property<int>("idDescuento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idDescuento");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idDescuento"));

                    b.Property<int?>("estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<byte?>("idDescuentoConcepto")
                        .HasColumnType("TINYINT")
                        .HasColumnName("idDescuentoConcepto");

                    b.Property<string>("idPersona")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("idPersona");

                    b.Property<decimal?>("monto")
                        .HasColumnType("decimal")
                        .HasColumnName("monto");

                    b.Property<string>("usuarioRegistro")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("usuarioRegistro");

                    b.HasKey("idDescuento");

                    b.ToTable("tb_Descuento", "Intranet", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.DescuentoListaDescuentos", b =>
                {
                    b.Property<byte>("IdDescuento")
                        .HasColumnType("TINYINT")
                        .HasColumnName("IdDescuento");

                    b.Property<string>("DesDescuento")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DesDescuento");

                    b.Property<byte?>("Estado")
                        .HasColumnType("TINYINT")
                        .HasColumnName("Estado");

                    b.HasKey("IdDescuento");

                    b.ToTable("Descuento", "Instituciones", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.InstitucionSede", b =>
                {
                    b.Property<byte>("IdSede")
                        .HasColumnType("TINYINT");

                    b.Property<byte>("Estado")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Sede")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSede");

                    b.ToTable("Sede", "Instituciones", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.PeriodoIntranet", b =>
                {
                    b.Property<int>("idPeriodoMatricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPeriodoMatricula"));

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<string>("periodoDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("periodoTexto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idPeriodoMatricula");

                    b.ToTable("tb_PeriodoMatricula", "Intranet", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("CajaSistema.Models.PersonaPersona", b =>
                {
                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DNI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdPersona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("v_Persona", "dbo");
                });

            modelBuilder.Entity("CajaSistema.Models.UserIdentity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("aMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("confirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idPersona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombres1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombres2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("NetCoreUsers", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("NetCoreRoles", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("NetCoreRoleClaims", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("NetCoreUserClaims", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("NetCoreUserLogins", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("NetCoreUserRoles", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("NetCoreUserTokens", "CajaWeb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CajaSistema.Models.UserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CajaSistema.Models.UserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CajaSistema.Models.UserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CajaSistema.Models.UserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
