using System.Text;
using Domain;
using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface;
using Domain.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Interface;
using WebApi.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(Position.Admin.ToString()));
});
// builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
// builder.Services.AddScoped<IService<AuthorDto>, AuthorService>();
builder.Services.AddScoped<IService<BookDto>, BookService>();
builder.Services.AddScoped<IOstonaService<BookDto>, UserPageService>();
// builder.Services.AddScoped<IAuthService<UserDto>, UserService>(); 

builder.Services.AddScoped<IRDWithCRUDService<UserDto, string>, UserService>();
builder.Services.AddScoped<IUpdateService<UserDto>, UserService>();
builder.Services.AddScoped<IUpdateUsersBookForAdmin<BookDto>, UpdateUsersBookForAdminService>();


builder.Services.AddScoped<ICreateRepository<LibraryBook>, LibraryRepository>();
builder.Services.AddScoped<IRDRepository<LibraryBook>, LibraryRepository>();
builder.Services.AddScoped<ICreateService<LibraryBookDto>, LibraryBookService>();
builder.Services.AddScoped<IRDWithCRUDService<LibraryBookDto, int>, LibraryBookService>();

builder.Services.AddScoped<IUserSettingsService<ResponceDto>, UserSettingsService>();
// builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

//Auth for swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT токен в поле: Bearer {ваш токен}"
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
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();