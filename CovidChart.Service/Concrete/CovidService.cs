using System.Linq;
using CovidChart.Service.Abstract;
using CovidChart.Services.Context;
using CovidChart.Services.Hubs;
using CovidChart.Services.Models;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.Service.Concrete
{
   public class CovidService : ICovidService
   {
      private readonly CovidDBContext _context;
      private readonly IHubContext<CovidHub> _hubContext;
      public CovidService(CovidDBContext context, IHubContext<CovidHub> hubContext)
      {
         _context = context;
         _hubContext = hubContext;
      }

      public IQueryable<Covid> GetCovidList()
      {
         return _context.Covids.AsQueryable();
      }
      public async Task SaveCovidAsync(Covid covid)
      {
         await _context.Covids.AddAsync(covid);
         await _context.SaveChangesAsync();
         await _hubContext.Clients.All.SendAsync("ReceiveCvidList", "data");
      }

   }
}