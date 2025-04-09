using Adopet.Data;
using Adopet.Exceptions.Handlers;
using Adopet.Repositories;
using Adopet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("sqliteConnection");

builder.Services.AddDbContext<AdopetContext>(opts =>
    opts.UseLazyLoadingProxies().UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<AdocaoRepository>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<TutorRepository>();

builder.Services.AddScoped<AdocaoService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<TutorService>();
builder.Services.AddScoped<ImageStorageService>();

builder.Services.AddExceptionHandler<NullReferenceExceptionHandle>();
builder.Services.AddExceptionHandler<PetIndisponivelExceptionHandle>();
builder.Services.AddExceptionHandler<PetEstaSendoAdotadoExceptionHandle>();
builder.Services.AddExceptionHandler<TutorComLimiteAtingidoExceptionHandle>();

var app = builder.Build();

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
