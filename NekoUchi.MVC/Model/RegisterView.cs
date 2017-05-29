using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class RegisterView
    {
        [Required(ErrorMessage = "Email adresa je obavezno polje")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Oblik adrese nije ispravan")]
        public string Email { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ponovi lozinku")]
        [Required(ErrorMessage = "Obavezno je ponoviti lozinku")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
