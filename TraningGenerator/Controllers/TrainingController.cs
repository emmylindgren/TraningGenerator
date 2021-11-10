using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraningGenerator.Models;

namespace TraningGenerator.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ILogger<TrainingController> _logger;

        private List<SelectListItem> trainingList;

        private List<SelectListItem> hoursOfTrainingList;

        public TrainingController(ILogger<TrainingController> logger)
        {
            _logger = logger;

            trainingList = new List<SelectListItem>();
            trainingList.Add(new SelectListItem { Text = "Gym", Value = "0" });
            trainingList.Add(new SelectListItem { Text = "Löpning", Value = "1" });
            trainingList.Add(new SelectListItem { Text = "Simning", Value = "2" });

            hoursOfTrainingList = new List<SelectListItem>();
            hoursOfTrainingList.Add(new SelectListItem { Text = "0", Value = "0" });
            hoursOfTrainingList.Add(new SelectListItem { Text = "1-3", Value = "1" });
            hoursOfTrainingList.Add(new SelectListItem { Text = "4-5", Value = "2" });
            hoursOfTrainingList.Add(new SelectListItem { Text = "6-7", Value = "3" });
            hoursOfTrainingList.Add(new SelectListItem { Text = ">7", Value = "4" });

        }

        [HttpGet]
        public IActionResult Index()
        {

            ViewData["traningSelection"] = trainingList;
            ViewBag.hoursOfTrainingSelection = hoursOfTrainingList;

            return View();
        }

        [HttpPost]
        public IActionResult TrainingRecommendation(IFormCollection col)
        {
            PersonTrainingInfo pti = new PersonTrainingInfo();
            pti.Name = col["Name"];
            pti.YearOfBirth = Convert.ToInt32(col["YearOfBirth"]);
            pti.CalculateAge();


            int valueOfHoursOfTraining = Convert.ToInt32(col["HoursOfTrainingNow"]);
            pti.HoursOfTrainingNow= hoursOfTrainingList[valueOfHoursOfTraining].Text;

            //Calculate new weekly hours of training. 
            pti.HoursOfRecommendedTraining = valueOfHoursOfTraining + 4;

            int valueOfFavoriteTraining= Convert.ToInt32(col["FavoriteTraining"]);
            pti.FavoriteTraining =trainingList[valueOfFavoriteTraining].Text;

            //Calculate new training that's not the persons favorite. 
            List<int> indexList = new List<int>();
            indexList.AddRange(Enumerable.Range(0, trainingList.Count));
            indexList.Remove(valueOfFavoriteTraining);

            pti.NewTraining = trainingList[indexList[(new Random()).Next(1)]].Text;

            string s = JsonConvert.SerializeObject(pti);
            HttpContext.Session.SetString("ptisession", s);

            return View(pti);
        }

        [HttpPost]
        public IActionResult TrainingEvaluation(IFormCollection col)
        {
            string s = HttpContext.Session.GetString("ptisession");
            PersonTrainingInfo pti = JsonConvert.DeserializeObject<PersonTrainingInfo>(s);

            int GradeOnRecommendation = Convert.ToInt32(col["Grade"]);
            bool LikedRecommendation = true;
            if (GradeOnRecommendation != 1)
            {
                LikedRecommendation = false;
            }

            ViewBag.likedRecommendation = LikedRecommendation;
            return View(pti);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
