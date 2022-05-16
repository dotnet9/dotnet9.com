using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Application.Contracts.UrlLinks;

public class AddOrUpdateUrlLinkDto
{
    public int Id { get; set; }

    [Display(Name = "名称")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(32, ErrorMessage = "{0}长度不能超过32个字符")]
    public string Name { get; set; } = null!;

    [Display(Name = "链接")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(128, ErrorMessage = "{0}长度不能超过128个字符")]
    public string Url { get; set; } = null!;

    [Display(Name = "描述")]
    [MaxLength(256, ErrorMessage = "{0}长度不能超过256个字符")]
    public string? Description { get; set; }

    [Display(Name = "索引")]
    [Range(0, 300, ErrorMessage = "{0}范围在[0, 300]")]
    public int Index { get; set; }

    [Display(Name = "类型")]
    [Range(0, 3, ErrorMessage = "{0}范围在[0, 3]")]
    public int Kind { get; set; }
}