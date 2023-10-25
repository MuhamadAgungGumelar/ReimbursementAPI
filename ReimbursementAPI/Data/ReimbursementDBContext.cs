using Microsoft.EntityFrameworkCore;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Data
{
    public class ReimbursementDBContext : DbContext
    {
        public ReimbursementDBContext(DbContextOptions<ReimbursementDBContext> options) : base(options) { }

        //Add Models to migrate
        public DbSet<AccountRoles> AccountRoles { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Finances> Finances { get; set; }
        public DbSet<Reimbursements> Reimbursements { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //menambahkan constraint Unique
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => e.PhoneNumber);
            });

            // 1 Employee has 1 Account
            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Accounts)
                .WithOne(e => e.Employees)
                .HasForeignKey<Accounts>(e => e.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Accounts has Many Account Roles
            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.AccountRoles)
                .WithOne(e => e.Accounts)
                .HasForeignKey(e => e.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Roles has Many Accounts Role
            modelBuilder.Entity<Roles>()
                .HasMany(e => e.AccountRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Employees has Many Reimbursement
            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Reimbursements)
                .WithOne(e => e.Employees)
                .HasForeignKey(e => e.EmployeeGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Employees has Many Finances
            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Finances)
                .WithOne(e => e.Employees)
                .HasForeignKey(e => e.EmployeeGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Finances has 1 Reimbursement
            modelBuilder.Entity<Reimbursements>()
                .HasOne(e => e.Finances)
                .WithOne(e => e.Reimbursements)
                .HasForeignKey<Finances>(e => e.Guid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
