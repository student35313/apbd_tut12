namespace Tutorial12.DTOs;

using System.ComponentModel.DataAnnotations;

public class RegisterClientToTripDTO
{
    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(120)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    public string Telephone { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    public string Pesel { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }
}