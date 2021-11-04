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
        [Range(1900, 2021)]
        [Display(Name = "Födelseår")]
        public int YearOfBirth { get; set; }

        public int Age { get; set; }

        [Required]
        [Display(Name = "Hur mycket tränar du idag?")]
        public int TrainingNow { get; set; }

        public int NewTraining { get; set; }

        [Required]
        [Display(Name = "Vad är din favoritträning?")]
        public string FavoriteTraining { get; set; }

        public PersonTrainingInfo()
        {
            //lägg till konstruktor här. 
        }

        public void CalculateNewTraining()
        {
            Age = DateTime.Now.Year - YearOfBirth;
            //lägg till beräkning av ny träning här också! 

        }
    }
}
