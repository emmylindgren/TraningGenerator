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
        public int HoursOfTrainingNow { get; set; }

        public String NewTraining { get; set; }

        [Required]
        [Display(Name = "Vad är din favoritträning?")]
        public string FavoriteTraining { get; set; }

        public PersonTrainingInfo()
        {
            //lägg till konstruktor här. 
        }

        public void CalculateAge()
        {
            Age = DateTime.Now.Year - YearOfBirth;
            //lägg till beräkning av ny träning här också! 

        }
    }
}
