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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<SelectListItem> trainingList;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            trainingList = new List<SelectListItem>();
            trainingList.Add(new SelectListItem { Text = "Gym", Value = "0" });
            trainingList.Add(new SelectListItem { Text = "Löpning", Value = "1" });
            trainingList.Add(new SelectListItem { Text = "Simning", Value = "2" });
        }

        [HttpGet]
        public IActionResult Index()
        {

            ViewData["traningSelection"] = trainingList;

            return View();
        }

        [HttpPost]
        public IActionResult TrainingRecommendation(IFormCollection col)
        {
            PersonTrainingInfo pti = new PersonTrainingInfo();
            pti.Name = col["Name"];
            pti.YearOfBirth = Convert.ToInt32(col["YearOfBirth"]);

            pti.TrainingNow = Convert.ToInt32(col["TrainingNow"]);
            int valueOfFavoriteTraining= Convert.ToInt32(col["FavoriteTraining"]);
            pti.FavoriteTraining =trainingList[valueOfFavoriteTraining].Text;

            pti.CalculateNewTraining();

            string s = JsonConvert.SerializeObject(pti);
            HttpContext.Session.SetString("ptisession", s);

            //Eventuellt skicka listan här också fast då i viewbag haha?
            // typ; sen kan du ju välja mellan dessa träningar också... 

            return View(pti);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
