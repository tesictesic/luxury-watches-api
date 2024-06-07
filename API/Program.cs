using Application;
using Application.Logging;
using Application.UseCases.Commands.BrandsCommands;
using Application.UseCases.Commands.GenderCommands;
using Application.UseCases.Commands.UserCommands;
using Application.UseCases.Queries;
using DataAcess;
using Implementation;
using Implementation.Logging;
using Implementation.UseCases.Commands.Brands;
using Implementation.UseCases.Commands.Genders;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries;
using Implementation.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// moji dodaci
builder.Services.AddScoped<ASPContext>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<IApplicationActor, Actor>();
builder.Services.AddTransient<IApplicationActorProvider,FakeActor>();
builder.Services.AddTransient<IGetGenderQuery,EfGetGender>(); // kada neko zatrazi IGetGender (aplikaciju) mi mu vratimo EfGetGende (implementaciju) klasu.
builder.Services.AddTransient<ICreateGenderCommand, EfCreateGender>();
builder.Services.AddTransient<CreateUpdateGenderDTOValidation>();
builder.Services.AddTransient<CreateUpdateBrandsDTOValdiation>();
builder.Services.AddTransient<IUpdateGenderCommand,EfUpdateGender>();
builder.Services.AddTransient<ICreateBrandCommand, EfCreateBrand>();
builder.Services.AddTransient<IUpdateBrandCommand, EfUpdateBrand>();
builder.Services.AddTransient<UserRegisterDTOValidation>();
builder.Services.AddTransient<IUserRegisterCommand, EfUserRegister>();







builder.Services.AddTransient<IExceptionLogger, ConsoleUseCaseLogger>();
builder.Services.AddTransient<IUseCaseLogger, UseCaseLogger>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
