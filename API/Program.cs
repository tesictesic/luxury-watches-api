using API.Core;
using Application;
using Application.Email;
using Application.Logging;
using Application.UseCases.Commands.BrandsCommands;
using Application.UseCases.Commands.CartCommands;
using Application.UseCases.Commands.ColorCommands;
using Application.UseCases.Commands.GenderCommands;
using Application.UseCases.Commands.ProductCommands;
using Application.UseCases.Commands.SpecificationCommands;
using Application.UseCases.Commands.UserCommands;
using Application.UseCases.Queries;
using DataAcess;
using Implementation;
using Implementation.Email;
using Implementation.Logging;
using Implementation.UseCases.Commands.Brands;
using Implementation.UseCases.Commands.Cart;
using Implementation.UseCases.Commands.Colors;
using Implementation.UseCases.Commands.Genders;
using Implementation.UseCases.Commands.Products;
using Implementation.UseCases.Commands.Specifications;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries;
using Implementation.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// moji dodaci
builder.Services.AddHttpContextAccessor(); // preko ovoga mozemo da pristupimo hederu http-a i da izvuicemo podatke
builder.Services.AddScoped<ASPContext>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<IApplicationActor, Actor>();
builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<ASPContext>();

    return new JwtAuthorization(authHeader, context);
});
builder.Services.AddTransient<IGetGenderQuery,EfGetGender>(); // kada neko zatrazi IGetGender (aplikaciju) mi mu vratimo EfGetGende (implementaciju) klasu.
builder.Services.AddTransient<ICreateGenderCommand, EfCreateGender>();
builder.Services.AddTransient<CreateUpdateGenderDTOValidation>();
builder.Services.AddTransient<CreateUpdateBrandsDTOValdiation>();
builder.Services.AddTransient<IUpdateGenderCommand,EfUpdateGender>();
builder.Services.AddTransient<ICreateBrandCommand, EfCreateBrand>();
builder.Services.AddTransient<IUpdateBrandCommand, EfUpdateBrand>();
builder.Services.AddTransient<IGetAuditLogQuery,EfGetAuditLog>();
builder.Services.AddTransient<UserRegisterDTOValidation>();
builder.Services.AddTransient<IUserRegisterCommand, EfUserRegister>();
builder.Services.AddTransient<IEmailSender,SMTPEmailSender>();
builder.Services.AddTransient<IExceptionLogger, ExceptionLogger>();
builder.Services.AddTransient<IUseCaseLogger, UseCaseLogger>();
builder.Services.AddTransient<ICreateSpecification, EfCreateSpecification>();
builder.Services.AddTransient<ICreateColorCommand, EfCreateColor>();
builder.Services.AddTransient<CreateUpdateColorDTOValidation>();
builder.Services.AddTransient<CreateUpdateSpecificationDTOValidation>();
builder.Services.AddTransient<ICreateProductCommand, EfCreateProduct>();
builder.Services.AddTransient<ProductDTOValidation>();
builder.Services.AddTransient<ICreateCartCommand, EfCartCommand>();
builder.Services.AddTransient<CartDTOValidation>();
builder.Services.AddTransient<JWTManager>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "luxury_watches_api",
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("31kldkasldlaskdlkalk3241l414kldkasldklaskdlsakdlkasl34214214")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
