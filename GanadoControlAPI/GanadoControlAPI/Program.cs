using Data.Repository;
using Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConectionString"];
builder.Services.AddScoped<IFarmacoRepository>(provider => new FarmacoData(connectionString));
builder.Services.AddScoped<IRecordatorioRepository>(provider => new RecordatorioData(connectionString));
builder.Services.AddScoped<ITratamientoRepository>(provider => new TratamientoData(connectionString));
builder.Services.AddScoped<IProblemaFisicoRepository>(provider => new ProblemaFisicoData(connectionString));
builder.Services.AddScoped<IDetalleGanadoRepository>(provider => new DetalleGanadoData(connectionString));
builder.Services.AddScoped<IGanadoRepository>(provider => new GanadoData(connectionString));
builder.Services.AddScoped<IGrupoRepository>(provider => new GrupoData(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
