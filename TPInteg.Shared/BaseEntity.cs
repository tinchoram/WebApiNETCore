using System.ComponentModel.DataAnnotations.Schema;

namespace TPInteg.Shared;
public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateOnly? FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; }
}