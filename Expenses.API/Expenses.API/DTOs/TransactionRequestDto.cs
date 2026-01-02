using System.ComponentModel.DataAnnotations;

public class TransactionRequestDto
{
    [Required]
    public double Amount { get; set; }

    [Required]
    public string Type { get; set; } 

    [Required]
    public string Category { get; set; } 
}
