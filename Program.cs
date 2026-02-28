using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Usuarios API",
        Version = "v1",
        Description = "API REST para gestión de usuarios con operaciones CRUD"
    });
});

// Base de datos - SQLite (fácil de usar sin servidor)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=usuarios.db"));

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuarios API v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
