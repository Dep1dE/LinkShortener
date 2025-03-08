using System.ComponentModel.DataAnnotations;

namespace LinkShortening.Models;

public class URL
{
    public Guid Id { get; set; }

    [Required, Url, MaxLength(2048)]
    public string LongUrl { get; set; } = null!;

    [Required, StringLength(17,MinimumLength = 17)]
    public string ShortUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    public int TransitionCount { get; set; } = 0;
}
