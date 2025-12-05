using EmpList.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmpList.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }


        public DbSet<Employee> Employee { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<PasswordResetToken> PasswordResetToken { get; set; }

        public DbSet<MDepartment> MDepartment { get; set; }

        public DbSet<MQualification> MQualification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var utcConverter = new ValueConverter<DateTime, DateTime>(
                toDb => DateTime.SpecifyKind(toDb, DateTimeKind.Utc),
                fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc)
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(utcConverter);
                    }
                }
            }

          
        }

    }

}


