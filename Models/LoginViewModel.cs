using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}