using DataAccess.Repositorio;
using VantaGym.Aplicacao;
using VantaGym.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IExercicioAplicacao, ExercicioAplicacao>();
builder.Services.AddScoped<IFichaTreinoAplicacao, FichaTreinoAplicacao>();
builder.Services.AddScoped<IHistoricoTreinoAplicacao, HistoricoTreinoAplicacao>();

// Add interfaces to the databases
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IExercicioRepositorio, ExercicioRepositorio>();
builder.Services.AddScoped<IFichaTreinoRepositorio, FichaTreinoRepositorio>();
builder.Services.AddScoped<IHistoricoTreinoRepositorio, HistoricoTreinoRepositorio>();

builder.Services.AddControllers();

builder.Services.AddDbContext<VantaGymContexto>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();