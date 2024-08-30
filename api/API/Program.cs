using API.BLL.Services.Implementations;
using API.BLL.Services.Interfaces;
using API.DAL.Contexts;
using API.DAL.Entities;
using API.DAL.Repositories.Implementations;
using API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DocStorageDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SigningKey").Value!)),
        ValidateLifetime = true
    };
});

builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
//builder.Services.AddScoped<IBaseRepository<Document>, BaseRepository<Document>>();
//builder.Services.AddScoped<IBaseRepository<Publication>, BaseRepository<Publication>>();
builder.Services.AddScoped<IBaseRepository<ConfirmationCode>, BaseRepository<ConfirmationCode>>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<IConfirmationCodeRepository, ConfirmationCodeRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<TwoStepAuthService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

var app = builder.Build();

app.UseResponseCompression();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true);
});

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
