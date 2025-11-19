using ApiContatos.Data;
using ApiContatos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com MySQL
var conexao = builder.Configuration.GetConnectionString("ConexaoPadrao");
builder.Services.AddDbContext<AppDbContext>(opcoes =>
    opcoes.UseMySql(conexao, ServerVersion.AutoDetect(conexao)));

// Serviços de domínio
builder.Services.AddScoped<IPessoaServico, PessoaServico>();
builder.Services.AddScoped<ITelefoneServico, TelefoneServico>();

// Controllers
builder.Services.AddControllers();

// CORS para o frontend em React
builder.Services.AddCors(opcoes =>
{
    opcoes.AddPolicy("PermitirFrontendLocalhost", politica =>
    {
        politica
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Garante que o banco e as tabelas sejam criados automaticamente
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.EnsureCreated();
}


// Swagger UI sempre habilitado para facilitar o teste
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("PermitirFrontendLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
