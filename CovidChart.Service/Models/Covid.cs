namespace CovidChart.Services.Models
{
   public enum ECity
   {
      Istanbul = 1,
      Ankara = 2,
      İzmir = 3,
      Bursa = 4,
      Eskişehir = 5
   }
   public class Covid
   {
      public int Id { get; set; }
      public ECity City { get; set; }
      public int Count { get; set; }
      public DateTime CovidDate { get; set; }
   }
}