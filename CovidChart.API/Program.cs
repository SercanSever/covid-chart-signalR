using CovidChart.Service.Abstract;
using CovidChart.Service.Concrete;
using CovidChart.Services.Context;
using CovidChart.Services.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CovidDBContext>(opt =>
{
   opt.UseSqlServer(builder.Configuration["DefaultConnection"]);
});
builder.Services.AddSignalR();
builder.Services.AddScoped<ICovidService, CovidService>();
builder.Services.AddCors(options =>
{
   options.AddPolicy("CorsPolicy", builder =>
   {
      builder.WithOrigins("https://localhost:7262").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
   });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<CovidHub>("/covid-statistics");

app.Run();
