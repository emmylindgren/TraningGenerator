using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrainingRecommendation(IFormCollection col)
        {
            PersonTrainingInfo pti = new PersonTrainingInfo();
            pti.Name = col["Name"];
            pti.YearOfBirth = Convert.ToInt32(col["YearOfBirth"]);

            pti.TrainingNow = Convert.ToInt32(col["TrainingNow"]);
            pti.FavoriteTraining = col["FavoriteTraining"];

            //Lägg till när vi har fått till beräkningen 
            pti.CalculateNewTraining();

            string s = JsonConvert.SerializeObject(pti);
            HttpContext.Session.SetString("ptisession", s);

            return View(pti);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
