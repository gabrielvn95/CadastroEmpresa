using CadastroEmpresa.Data;
using CadastroEmpresa.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CadastroEmpresa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IEmpresaInterface, EmpresaService>();
            builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var chave = builder.Configuration["Jwt:Key"];
                var chaveBytes = Encoding.UTF8.GetBytes(chave);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveBytes)
                };
            });

         
            builder.Services.AddControllers();
 
            builder.Services.AddOpenApi();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            app.Urls.Clear();
            app.Urls.Add($"http://0.0.0.0:{port}");

            app.Run();
        }
    }
}
