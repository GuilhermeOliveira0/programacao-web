using System.ComponentModel.DataAnnotations.Schema;

namespace projetoFornecedor.Models;
public class Produto

{

    public int ProdutoId { get; set; }

    public string Nome { get; set; } = string.Empty;
    //forçar a ser decimal 
    [Column(TypeName = "numeric(10,2)")]
    public decimal Preco { get; set; }



    public int FornecedorId { get; set; }

    public Fornecedor? Fornecedor { get; set; }

}
