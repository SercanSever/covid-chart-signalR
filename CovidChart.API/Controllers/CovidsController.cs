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

      public IActionResult GetCovidList()
      {
         return Ok();
      }
      [HttpPost]
      public async Task<IActionResult> SaveCovidAsync(Covid covid)
      {
         await _covidService.SaveCovidAsync(covid);
         IQueryable<Covid> covidList = _covidService.GetCovidList();
         return Ok(covidList);
      }
   }
}