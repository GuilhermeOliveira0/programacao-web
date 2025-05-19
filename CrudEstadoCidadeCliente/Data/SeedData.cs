using CrudEstadoCidadeCliente.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEstadoCidadeCliente.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Verifica se já existem estados
                if (context.Estados.Any())
                {
                    return; // DB já foi populado
                }

                // Adiciona estados iniciais
                var estados = new Estado[]
                {
                    new Estado { Sigla = "SP", Nome = "São Paulo" },
                    new Estado { Sigla = "RJ", Nome = "Rio de Janeiro" },
                    new Estado { Sigla = "MG", Nome = "Minas Gerais" },
                    new Estado { Sigla = "ES", Nome = "Espírito Santo" },
                    new Estado { Sigla = "PR", Nome = "Paraná" }
                };

                context.Estados.AddRange(estados);
                context.SaveChanges();

                // Adiciona cidades iniciais
                var cidades = new Cidade[]
                {
                    new Cidade { Nome = "São Paulo", EstadoId = estados[0].Id },
                    new Cidade { Nome = "Campinas", EstadoId = estados[0].Id },
                    new Cidade { Nome = "Rio de Janeiro", EstadoId = estados[1].Id },
                    new Cidade { Nome = "Niterói", EstadoId = estados[1].Id },
                    new Cidade { Nome = "Belo Horizonte", EstadoId = estados[2].Id },
                    new Cidade { Nome = "Uberlândia", EstadoId = estados[2].Id },
                    new Cidade { Nome = "Vitória", EstadoId = estados[3].Id },
                    new Cidade { Nome = "Vila Velha", EstadoId = estados[3].Id },
                    new Cidade { Nome = "Curitiba", EstadoId = estados[4].Id },
                    new Cidade { Nome = "Londrina", EstadoId = estados[4].Id }
                };

                context.Cidades.AddRange(cidades);
                context.SaveChanges();
            }
        }
    }
}