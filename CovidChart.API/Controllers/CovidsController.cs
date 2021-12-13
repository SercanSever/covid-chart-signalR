using CovidChart.Service.Abstract;
using CovidChart.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovidChart.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class CovidsController : ControllerBase
   {
      private readonly ICovidService _covidService;

      public CovidsController(ICovidService covidService)
      {
         _covidService = covidService;
      }

      [HttpGet]
      public async Task<IActionResult> InitializeCovid()
      {
         Random random = new Random();
         Enumerable.Range(1, 10).ToList().ForEach(x =>
         {
            foreach (ECity city in Enum.GetValues(typeof(ECity)))
            {
               var newCovid = new Covid { City = city, Count = random.Next(100, 1000), CovidDate = DateTime.Now.AddDays(x) };
               _covidService.SaveCovidAsync(newCovid).Wait();
               System.Threading.Thread.Sleep(1000);
            };
         });
         return Ok();
      }
      [HttpPost]
      public async Task<IActionResult> SaveCovidAsync(Covid covid)
      {
         await _covidService.SaveCovidAsync(covid);
         // IQueryable<Covid> covidList = _covidService.GetCovidList();
         return Ok(_covidService.GetCovidListForChart());
      }
   }
}