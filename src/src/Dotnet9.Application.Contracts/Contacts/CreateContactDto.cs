using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Contacts;

public class CreateContactDto
{
    [Required]
    [StringLength(ContactConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(ContactConsts.MaxEmailLength)]
    public string Email { get; set; }

    [Required]
    [StringLength(ContactConsts.MaxSubjectLength)]
    public string Subject { get; set; }

    [Required]
    [StringLength(ContactConsts.MaxMessageLength)]
    public string Message { get; set; }
}