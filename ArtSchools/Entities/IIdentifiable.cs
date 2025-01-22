using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

public interface IIdentifiable<out T>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    T Id { get; }
}