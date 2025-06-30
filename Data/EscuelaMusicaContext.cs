using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Entities;

namespace EscuelaMusica.API.Data{
    public class EscuelaMusicaContext : DbContext
    {
        public EscuelaMusicaContext(DbContextOptions<EscuelaMusicaContext> options) : base(options)
        {
        }

        public DbSet<Escuela> Escuelas { get; set; } = null!;
        public DbSet<Profesor> Profesores { get; set; } = null!;
        public DbSet<Alumno> Alumnos { get; set; } = null!;
        public DbSet<AlumnoEscuela> AlumnoEscuelas { get; set; } = null!;
        public DbSet<AlumnoProfesor> AlumnoProfesores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlumnoProfesor>().ToTable("AlumnoProfesor");
            modelBuilder.Entity<AlumnoEscuela>().ToTable("AlumnoEscuela");

            modelBuilder.Entity<AlumnoProfesor>()
                .HasKey(ap => new { ap.AlumnoId, ap.ProfesorId });

            modelBuilder.Entity<AlumnoProfesor>()
                .HasOne(ap => ap.Alumno)
                .WithMany(a => a.Profesores)
                .HasForeignKey(ap => ap.AlumnoId);

            modelBuilder.Entity<AlumnoProfesor>()
                .HasOne(ap => ap.Profesor)
                .WithMany(p => p.Alumnos)
                .HasForeignKey(ap => ap.ProfesorId);

            modelBuilder.Entity<AlumnoEscuela>()
                .HasKey(ae => new { ae.AlumnoId, ae.EscuelaId });

            modelBuilder.Entity<AlumnoEscuela>()
                .HasOne(ae => ae.Alumno)
                .WithMany(a => a.Escuelas)
                .HasForeignKey(ae => ae.AlumnoId);

            modelBuilder.Entity<AlumnoEscuela>()
                .HasOne(ae => ae.Escuela)
                .WithMany(e => e.Alumnos)
                .HasForeignKey(ae => ae.EscuelaId);
        }

    }
}