using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Application.Contracts.Albums;

public class AddOrUpdateAlbumDto
{
    public int? Id { get; set; }
    public int? ParentId { get; set; }

    [Display(Name = "名称")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(32, ErrorMessage = "{0}长度不能超过32个字符")]
    public string Name { get; set; } = null!;

    [Display(Name = "别名")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(256, ErrorMessage = "{0}长度不能超过256个字符")]
    public string Slug { get; set; } = null!;

    [Display(Name = "封面")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(128, ErrorMessage = "{0}长度不能超过128个字符")]
    public string Cover { get; set; } = null!;

    [Display(Name = "描述")]
    [MaxLength(256, ErrorMessage = "{0}长度不能超过256个字符")]
    public string? Description { get; set; }

    [Display(Name = "索引")]
    [Range(0, 300, ErrorMessage = "{0}范围在[0, 300]")]
    public int Index { get; set; }

    [Display(Name = "是否显示")] public bool IsShow { get; set; }
}