using Microsoft.EntityFrameworkCore;
using NailSalon.Application.Interfaces;
using NailSalon.Application.Services;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;
using NailSalon.Infrastructure.Data.Interceptors;
using NailSalon.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace NailSalon.API.Extensions;

public static class ServiceExtensions
{
    // 1. Cấu hình kết nối SQL Server & Đăng ký Interceptor
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Đăng ký Interceptor như một Singleton
        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddDbContext<NailSalonDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                   .AddInterceptors(interceptor); // Nạp Interceptor (tự động điền ngày tạo/cập nhật, soft delete) vào DbContext
        });
    }

    // 2. Cấu hình Unit of Work và Repositories
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        // Đăng ký AutoMapper
        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(NailSalon.Application.Mappings.CustomerProfile).Assembly);
        });

        // Đăng ký các Business Services
        services.AddScoped<ICustomerService, CustomerService>();
        // Sau này có thêm IAppointmentService... thì đăng ký tiếp ở đây
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<INailServiceLogic, NailServiceLogic>();
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new Exception("Thiếu cấu hình JWT Secret.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }
}