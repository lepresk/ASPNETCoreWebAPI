using System.ComponentModel.DataAnnotations;

namespace RestApi.Models.Dto;

public class CreatePostDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;
}
