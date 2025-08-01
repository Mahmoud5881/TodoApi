using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs;

public class SignUpDTO
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    public string Email { get; set; }
}