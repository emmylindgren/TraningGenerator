using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraningGenerator.Models
{
    public class PersonTrainingInfo
    {

        [Required]
        [Display (Name ="Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Födelseår")]
        public int YearOfBirth { get; set; }

        [Display(Name = "Ålder")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Hur många timmar i veckan tränar du idag?")]
        public string HoursOfTrainingNow { get; set; }

        [Required]
        [Display(Name = "Vad är din favoritträning?")]
        public string FavoriteTraining { get; set; }

        public String NewTraining { get; set; }

        public int HoursOfRecommendedTraining { get; set; }

        public PersonTrainingInfo()
        {

        }

        public void CalculateAge()
        {
            Age = DateTime.Now.Year - YearOfBirth;

        }
    }
}
