using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Ratings;

public class CreateRatingDto
{
    public short StarCount { get; set; }
}