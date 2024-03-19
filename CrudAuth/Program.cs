    using CrudAuth.Models;
using CrudAuth.Repository.Interfaces;
using CrudAuth.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregacion del contexto de la db para poder crearla (en este caso esta configurada para usar sqlite pero se podria configurar para SQLServer).

builder.Services.AddDbContext<AplicationDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:AcrudAuthAPIDBConnectionString"]));

#region DependencyInjection

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));





// Estas son las líneas que se deben agregar para habilitar la autenticación con JWT
builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretForKey"]))//En esta linea estoy configurando la llave privada que se va a usar para validar el token.
        };
    });

//- `AddHttpContextAccessor`: Registra el `IHttpContextAccessor` que nos permite acceder el `HttpContext`de cada solicitud (la usaremos más adelante para acceder al usuario actual autenticado)

//- `AddAutorization`: Dependencias necesarias para autorizar solicitudes (como autorización por roles)

//- `AddAuthentication`: Agrega el esquema de autenticación que queramos usar, en este caso, queremos usar por default la autenticación por Bearer Tokens (Básicamente es para decirle que vamos a usar JWT)

//- `AddJwtBearer`: Configura la autenticación por tokens, especificando qué cosas debe de validar y que llave privada utilizar.




builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("CrudAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CrudAuth" } //Tiene que coincidir con el id seteado arriba en la definición
                }, new List<string>() }
    });
});

//En el método `AddSwaggerGen` estamos configurando el swagger para que permita autenticación por tokens. Principal mente esta linea de codigo hace que nos aparezca el boton
// para enviar el token desde swagger hacia el backend.





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthentication();    // se esta linea llamada middlewares para habilitar la autenticación. (Importante es que hay que respetar el orden de los middlewares).

app.UseAuthorization();

app.MapControllers();

app.Run();