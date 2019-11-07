using System.ComponentModel.DataAnnotations;

namespace Instituto_Frigga_Backend.ViewModel
{
    public class LoginViewModel
    {
       [Required]
       public string Email {get; set;} 
       
       [StringLength(255, MinimumLength = 5)]
       public string Senha {get; set;}
    }
}