using API.Core;
using Application;
using Application.Email;
using Application.Logging;
using Application.UseCases.Commands.BrandsCommands;
using Application.UseCases.Commands.CartCommands;
using Application.UseCases.Commands.ColorCommands;
using Application.UseCases.Commands.ContactCommands;
using Application.UseCases.Commands.GenderCommands;
using Application.UseCases.Commands.ProductCommands;
using Application.UseCases.Commands.SpecificationCommands;
using Application.UseCases.Commands.UserCommands;
using Application.UseCases.Commands.UseUserCaseCommands;
using Application.UseCases.Queries;
using DataAcess;
using Implementation;
using Implementation.Email;
using Implementation.Logging;
using Implementation.UseCases.Commands.Brands;
using Implementation.UseCases.Commands.Cart;
using Implementation.UseCases.Commands.Colors;
using Implementation.UseCases.Commands.Contacts;
using Implementation.UseCases.Commands.Genders;
using Implementation.UseCases.Commands.Products;
using Implementation.UseCases.Commands.Specifications;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Commands.UserUseCases;
using Implementation.UseCases.Queries;
using Implementation.Validations.Brand;
using Implementation.Validations.Color;
using Implementation.Validations.Contact;
using Implementation.Validations.Gender;
using Implementation.Validations.Products;
using Implementation.Validations.Specification;
using Implementation.Validations.User;
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
builder.Services.AddTransient<IUpdateGenderCommand, EfUpdateGender>();
builder.Services.AddTransient<IDeleteGenderCommand, EfDeleteGender>();
builder.Services.AddTransient<CreateGenderDTOValidation>();
builder.Services.AddTransient<UpdateGenderDTOValidation>();
builder.Services.AddTransient<CreateBrandsDTOValdiation>();
builder.Services.AddTransient<UpdateBrandsDTOValidation>();
builder.Services.AddTransient<IGetBrandQuery,EfGetBrand>();
builder.Services.AddTransient<ICreateBrandCommand, EfCreateBrand>();
builder.Services.AddTransient<IUpdateBrandCommand, EfUpdateBrand>();
builder.Services.AddTransient<IDeleteBrandCommand, EfDeleteBrand>();
builder.Services.AddTransient<IGetAuditLogQuery,EfGetAuditLog>();
builder.Services.AddTransient<UserRegisterDTOValidation>();
builder.Services.AddTransient<IGetUserQuery, EfGetUser>();
builder.Services.AddTransient<IUserRegisterCommand, EfUserRegister>();
builder.Services.AddTransient<IUserUpdateCommand, EfUserUpdate>();
builder.Services.AddTransient<IUserDeleteCommand, EfUserDelete>();
builder.Services.AddTransient<UpdateUserDTOValidation>();
builder.Services.AddTransient<IEmailSender,SMTPEmailSender>();
builder.Services.AddTransient<IExceptionLogger, ExceptionLogger>();
builder.Services.AddTransient<IUseCaseLogger, UseCaseLogger>();
builder.Services.AddTransient<IGetSpecificationQuery, EfGetSpecification>();
builder.Services.AddTransient<ICreateSpecificationCommand, EfCreateSpecification>();
builder.Services.AddTransient<IUpdateSpecificationCommand, EfUpdateSpecification>();
builder.Services.AddTransient<IDeleteSpecificationCommand, EfDeleteSpecification>();
builder.Services.AddTransient<UpdateSpecificationDTOValidation>();
builder.Services.AddTransient<IGetColorQuery, EfGetColor>();
builder.Services.AddTransient<ICreateColorCommand, EfCreateColor>();
builder.Services.AddTransient<IUpdateColorCommand, EfUpdateColor>();
builder.Services.AddTransient<IDeleteColorCommand, EfDeleteColor>();
builder.Services.AddTransient<CreateColorDTOValidation>();
builder.Services.AddTransient<UpdateColorDTOValidation>();
builder.Services.AddTransient<CreateSpecificationDTOValidation>();
builder.Services.AddTransient<IGetProductQuery,EfGetProduct>();
builder.Services.AddTransient<IGetProductSinglePage,EfGetProductSinglePage>();
builder.Services.AddTransient<ICreateProductCommand, EfCreateProduct>();
builder.Services.AddTransient<IDeleteProductCommand, EfDeleteProduct>();
builder.Services.AddTransient<IUpdateProductCommande, EfUpdatProduct>();
builder.Services.AddTransient<CreateProductDTOValidation>();
builder.Services.AddTransient<UpdateProductDTOValidation>();
builder.Services.AddTransient<IGetCartQuery, EfGetCart>(); 
builder.Services.AddTransient<ICreateCartCommand, EfCartCommand>();
builder.Services.AddTransient<ICreateUserUseCaseCommand, EfCreateUseUserCase>();
builder.Services.AddTransient<IDeleteUserUseCaseCommand, EfDeleteUseUserCase>();
builder.Services.AddTransient<IGetOneUser,EfGetOneUser>();
builder.Services.AddTransient<CreateUserUseCaseDTOValidation>();
builder.Services.AddTransient<CartDTOValidation>();
builder.Services.AddTransient<ICreateContact, EfCreateContact>();
builder.Services.AddTransient<IGetContactQuery, EfGetContactQuery>();
builder.Services.AddTransient<CreateContactValidator>();




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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseCors("AllowLocalhost");
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
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
    var jwtManager = context.RequestServices.GetRequiredService<JWTManager>();

    if (!string.IsNullOrEmpty(token))
    {
        var tokenId = jwtManager.GetTokenIdFromJwt(token);

        if (jwtManager.IsTokenRevoked(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }
    }

    await next();
});

app.MapControllers();

app.Run();
