using CovidChart.Service.Models;
using CovidChart.Services.Models;

namespace CovidChart.Service.Abstract
{
   public interface ICovidService
   {
      IQueryable<Covid> GetCovidList();
      List<Chart> GetCovidListForChart();
      Task SaveCovidAsync(Covid covid);
   }
}