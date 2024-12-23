using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_net.Entidades;

public class Bank
{
    public int Id { get; set; }

    [MaxLength(75)]
    public string Name { get; set; } = default!;

    [MaxLength(10)]
    public string Bic { get; set; } = default!;

    [MaxLength(2)]
    public string Country { get; set; } = default!;
}
