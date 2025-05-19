using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudEstadoCidadeCliente.Models;

public class Estado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "A sigla é obrigatória")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla deve ter 2 caracteres")]
    public string Sigla { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Cidade>? Cidades { get; set; }
}