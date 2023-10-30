
using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Models.Entities.Other;
using Home.Source.BusinessLayer;
using Home.Source.Database;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Home
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            ConfigureApp(builder);
        }

        private static void ConfigureApp(WebApplicationBuilder builder)
        {
            var app = builder.Build();
            InitSeed(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.MapControllers();

            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var message = new List<string>();
                        var error = string.Empty;

                        // CUSTOM_ERROR: users can see
                        // SERVER_ERROR: developers can see

                        if (contextFeature.Error.Message.Contains("CUSTOM_ERROR"))
                        {
                            error = contextFeature.Error.Message.Replace("CUSTOM_ERROR - ", "");
                        }
                        else
                        {
                            var tmp = string.Empty;
                            if (contextFeature.Error.Message.Contains("SERVER_ERROR"))
                            {
                                tmp = contextFeature.Error.Message.Replace("SERVER_ERROR - ", "");
                            } 
                            else
                            {
                                tmp = contextFeature.Error.Message;
                            }
                            // TODO: log 'tmp' error for devs


                            error = $"Internal server error. Please try again later. {tmp}";
                        }

                        message.Add(error);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(message));
                    }
                });
            });

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();

            builder.Services
                .AddIdentity<User, IdentityRole>(p =>
                {
                    p.User.RequireUniqueEmail = true;
                    p.Password.RequireDigit = false;
                    p.Password.RequiredUniqueChars = 0;
                    p.Password.RequireLowercase = false;
                    p.Password.RequireNonAlphanumeric = false;
                    p.Password.RequireUppercase = false;
                    p.Password.RequiredLength = 3;
                })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<DatabaseContext>(p =>
            {
                p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            _ = builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
                    ClockSkew = TimeSpan.Zero,
                });

            // services
            builder.Services.AddHttpContextAccessor();

            // repositories
            builder.Services.AddScoped<AspNetRepository>();
            builder.Services.AddScoped<PersonRepository>();
            builder.Services.AddScoped<EventRepository>();
            builder.Services.AddScoped<ActivityRepository>();
            builder.Services.AddScoped<ExpenseRepository>();
            builder.Services.AddScoped<SqlRepository>();

            // layers
            builder.Services.AddScoped<SeedLayer>();
            builder.Services.AddScoped<UserLayer>();
            builder.Services.AddScoped<PersonLayer>();
            builder.Services.AddScoped<EventLayer>();
            builder.Services.AddScoped<ActivityLayer>();
            builder.Services.AddScoped<ExpensesLayer>();

            builder.Services.AddAutoMapper(p =>
            {
                p.CreateMap<Person, PersonDTO>();
                p.CreateMap<PersonEditorDTO, Person>();

                p.CreateMap<Event, EventDTO>();
                p.CreateMap<EventEditorDTO, Event>();

                p.CreateMap<Activity, ActivityDTO>();
                p.CreateMap<ActivityEditorDTO, Activity>();
                p.CreateMap<ActivityStatusEditorDTO, Activity>();

                p.CreateMap<SuperCategory, SuperCategoryDTO>();
                p.CreateMap<SuperCategoryEditorDTO, SuperCategory>();

                p.CreateMap<Category, CategoryDTO>();
                p.CreateMap<CategoryEditorDTO, Category>();

                p.CreateMap<Account, AccountDTO>();
                p.CreateMap<AccountEditorDTO, Account>();

                p.CreateMap<Entry, EntryDTO>();
                p.CreateMap<EntryEditorDTO, Entry>();
            }, typeof(Program));
        }

        private static void InitSeed(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var init = scope.ServiceProvider.GetService<SeedLayer>();
            init?.InitAsync().Wait();
        }
    }
}