using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudEstadoCidadeCliente.Models;

public class Cidade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Cod { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [ForeignKey("Estado")]
    public int EstadoId { get; set; }
    public Estado? Estado { get; set; }

    public ICollection<Cliente>? Clientes { get; set; }
}