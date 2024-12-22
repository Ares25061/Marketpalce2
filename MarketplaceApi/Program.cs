using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using DataAccess.Wrapper;
using Domain.Interfaces;
using Domain.Models;
using MarketplaceApi.Authorization;
using MarketplaceApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MarketplaceApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins(
                        "https://localhost:7203", // Ваш локальный домен
                        "https://oxygenmarketsite.onrender.com", // Ваш сайт
                        "https://oxygenmarketapi.onrender.com" // Ваш API
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // Разрешить отправку куки и заголовков авторизации
                });
            });

            // Настройка базы данных
            builder.Services.AddDbContext<MarketpalceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

            // Регистрация сервисов
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IChatParticipantService, ChatParticipantService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IUserFileService, UserFileService>();
            builder.Services.AddScoped<IFilePermissionService, FilePermissionService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ISearchHistoryService, SearchHistoryService>();
            builder.Services.AddScoped<IPriceHistoryService, PriceHistoryService>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IUserDiscountService, UserDiscountService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentUserService, PaymentUserService>();
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // JWT и авторизация
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Настройка Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OxygenMarketApi",
                    Description = "Маркетплейс Кислород",
                    Contact = new OpenApiContact
                    {
                        Name = "Сайт",
                        Url = new Uri("https://oxygenmarketsite.onrender.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Разраб (помогите)",
                        Url = new Uri("https://t.me/Ares250678")
                    }
                });

                // Настройка авторизации через Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // Добавление комментариев из XML-файла
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Добавление контроллеров
            builder.Services.AddControllers();

            var app = builder.Build();

            // Миграция базы данных
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MarketpalceContext>();
                await context.Database.MigrateAsync();

                // Добавление ролей, если их нет
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { RoleName = "Admin" },
                        new Role { RoleName = "User" },
                        new Role { RoleName = "Support" }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // Настройка HTTP-конвейера
            app.UseRouting();

            // Применение CORS
            app.UseCors("AllowSpecificOrigins");

            // Настройка авторизации и аутентификации
            app.UseAuthorization();

            // Настройка Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            // Настройка HTTPS
            app.UseHttpsRedirection();

            // Обработка ошибок
            if (app.Environment.IsProduction())
            {
                app.UseMiddleware<ErrorHandlerMiddleware>();
            }

            // JWT Middleware
            app.UseMiddleware<JwtMiddleware>();

            // Настройка маршрутов
            app.MapControllers();

            // Запуск приложения
            app.Run();
        }
    }
}