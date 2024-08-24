using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MWalksProject.API.CustomMiddelwarre;
using MWalksProject.Infastructure.Data;
using MWalksProject.Infastructure.Repository;
using MWalksProject.Infastructure.Services;
using MWalksProject.Infastructure.UnitOfWork;
using MWlaksProject.Core.Helper; 
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Services;
using MWlaksProject.Core.Utilities;
using Serilog;
using Serilog.Core;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


# region logging configuration
var Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/EgyptWalks_Logs.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Logger);
Logger.Information("Logger has been configured.");
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region dependancy registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<PaginationServices>();
builder.Services.AddAutoMapper(typeof(Program));
#endregion

#region identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); 
#endregion

#region Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
#endregion

# region Jwt Configuration
builder.Services.Configure<JwtHelper>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication
    (x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
            ValidIssuer = builder.Configuration["Jwt : Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
        };
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddelWar>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
