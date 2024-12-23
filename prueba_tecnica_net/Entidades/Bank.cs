using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_net.Entidades;

public class Bank
{
    [MaxLength(75)]
    string Name { get; set; } = default!;

    [MaxLength(10)]
    string Bic { get; set; } = default!;

    [MaxLength(2)]
    string Country { get; set; } = default!;
}
