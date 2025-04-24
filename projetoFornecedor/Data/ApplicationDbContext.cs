namespace projetoFornecedor.Data;
using projetoFornecedor.Models;
using Microsoft.EntityFrameworkCore;






public class ApplicationDbContext : DbContext

{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)

        : base(options)

    {

    }


    //cria as tabelas
    public DbSet<Fornecedor> Fornecedores { get; set; }

    public DbSet<Produto> Produtos { get; set; }

} 