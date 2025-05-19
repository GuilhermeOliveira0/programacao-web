using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudEstadoCidadeCliente.Models;

public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Cod { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O sexo é obrigatório")]
    public char Sexo { get; set; }

    [Range(0, 150, ErrorMessage = "Idade inválida")]
    public int Idade { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    [ForeignKey("Cidade")]
    public int CidadeId { get; set; }
    public Cidade? Cidade { get; set; }
}