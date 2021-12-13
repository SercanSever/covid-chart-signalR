using CovidChart.Service.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.Services.Hubs
{
   public class CovidHub : Hub
   {
      private readonly ICovidService _covidService;

      public CovidHub(ICovidService covidService)
      {
         _covidService = covidService;
      }

      public async Task GetCovidList()
      {
         await Clients.All.SendAsync("ReceiveCovidList", _covidService.GetCovidListForChart());
      }
   }
}