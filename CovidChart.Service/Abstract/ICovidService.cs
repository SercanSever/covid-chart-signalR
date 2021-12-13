using CovidChart.Services.Models;

namespace CovidChart.Service.Abstract
{
    public interface ICovidService
    {
         IQueryable<Covid> GetCovidList();
         Task SaveCovidAsync(Covid covid);
    }
}