using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Models.Dtos;

public class BaseIdModel<T>
{
    [Required] public T Id { get; set; }
}