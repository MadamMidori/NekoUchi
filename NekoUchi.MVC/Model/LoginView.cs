using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class LoginView
    {
        [Required(ErrorMessage = "Email adresa je obavezno polje")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Oblik adrese nije ispravan")]
        public string Email { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
