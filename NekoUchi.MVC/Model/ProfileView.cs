using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoUchi.Model;

namespace NekoUchi.MVC.Model
{
    public class ProfileView
    {        
        public string Email { get; set; }

        [Display(Name = "Registriran od")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Moji tečajevi")]
        public List<MyCourseView> MyCourses { get; set; }

        [Display(Name = "Pratim tečajeve")]
        public List<SubscribedCourseView> SubscribedCourses { get; set; }

        [Display(Name = "Statistika")]
        public UserStatsView Statistics { get; set; }
    }    

    public class UserStatsView
    {

    }
}
