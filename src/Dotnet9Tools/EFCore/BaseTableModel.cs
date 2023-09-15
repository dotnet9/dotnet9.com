using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet9Tools.EFCore;

public class BaseEntity<T>
{
    public T Id { get; set; }

    public DateTime CreateTime { get; set; }
}