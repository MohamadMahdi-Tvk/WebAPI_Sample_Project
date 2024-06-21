using MyProject.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // => برای مستند سازی
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

//-----------------------------------------------------------------------------------------------------------------------------
//Minimal API: همان کاری که در لایه ی سیمپل انجام دادیم با این روش راحت تر میتوانیم پیاده کنیم و برای کارهای کوچک خوب است؛ ولی یکسری ناتوانی ها دارد
//مثلا اگر خواسته باشیم یکی دوتا ای پی آی بنویسیم و کار خاصی زیادی نداشته باشیم مناسب هست، ولی برای یک اپلیکیشن بزرگ مناسب نیست

app.MapGet("/GetProductList", async (HttpContext context, ProductDbContext dbContext) =>
{
    var productList = dbContext.Products.ToList();

    return Results.Ok(productList); // => دیگر لازم نیست درگیر تبدیل به جیسان و ست کردن استاتوس کد و غیره شویم و از امکانات توکار خود کور استفاده کردیم 
});


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}