using Application.UseCases.Queries;
using DataAcess;
using Implementation.UseCases.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// moji dodaci
builder.Services.AddScoped<ASPContext>();
builder.Services.AddTransient<IGetGender,EfGetGender>(); // kada neko zatrazi IGetGender (aplikaciju) mi mu vratimo EfGetGende (implementaciju) klasu.





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
