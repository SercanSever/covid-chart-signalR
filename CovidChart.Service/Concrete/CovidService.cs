using System;
using System.Linq;
using CovidChart.Service.Abstract;
using CovidChart.Service.Models;
using CovidChart.Services.Context;
using CovidChart.Services.Hubs;
using CovidChart.Services.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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
         await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidListForChart());
      }
      public List<Chart> GetCovidListForChart()
      {
         List<Chart> covidChart = new List<Chart>();
         using (var command = _context.Database.GetDbConnection().CreateCommand())
         {
            command.CommandText = "select Tarih,[1],[2],[3],[4],[5] from (select [City],[Count],Cast([CovidDate] as date) as Tarih from Covids) as CovidTable pivot (sum([Count]) for City in([1],[2],[3],[4],[5])) as PivotTable order by tarih asc";

            command.CommandType = System.Data.CommandType.Text;
            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
               while (reader.Read())
               {
                  Chart chart = new Chart();
                  chart.CovidDate = reader.GetDateTime(0).ToShortDateString();
                  Enumerable.Range(1, 5).ToList().ForEach(x =>
                  {
                     if (DBNull.Value.Equals(reader[x]))
                     {
                        chart.Counts.Add(0);
                     }
                     else
                     {
                        chart.Counts.Add(reader.GetInt32(x));
                     }
                  });
                  covidChart.Add(chart);
               }
            }
            _context.Database.CloseConnection();
            return covidChart;
         }
      }
   }
}