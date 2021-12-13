using Microsoft.AspNetCore.SignalR;

namespace CovidChart.Services.Hubs
{
   public class CovidHub : Hub
   {
      public async Task GetCovidList()
      {
         await Clients.All.SendAsync("ReceiveCovidList", "Get covid19 list from service");
      }
   }
}