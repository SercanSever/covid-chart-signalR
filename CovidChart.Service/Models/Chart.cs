namespace CovidChart.Service.Models
{

   public class Chart
   {
      public Chart()
      {
         Counts = new List<int>();
      }
      public string CovidDate { get; set; }
      public List<int> Counts { get; set; }
   }
}