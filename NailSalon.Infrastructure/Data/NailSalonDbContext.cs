using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Data;

public class NailSalonDbContext : DbContext
{
    public NailSalonDbContext(DbContextOptions<NailSalonDbContext> options) : base(options)
    {
    }

    // Khai báo các Tables
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<NailService> NailServices => Set<NailService>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tự động tìm và apply toàn bộ các class Implement IEntityTypeConfiguration (các file Configuration ta vừa viết)
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}