using Microsoft.AspNetCore.Mvc;
using MyProject.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//برای استفاده از کنترلر برای ساخت وب ای پی آی ، ابتدا باید سرویس آن را اضافه کنیم و الان فقط نیاز به کنترلر داریم
builder.Services.AddControllers();


builder.Services.AddDbContext<ProductDbContext>();

//الان دیگه هر پراپرتی که نال بود سریالایز نخواهد شد و لازم نیست روی تک تک پراپرتی ها اتربیوت قرار دهیم
builder.Services.Configure<JsonOptions>(c =>
{
    c.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

//کار بعد اضافه کردن مپ کنترلرز هست؛ الان این نرم افزار آمادگی دارد بعنوان یک وب ای پی آی به ما خدمات بده
app.MapControllers();

app.Run();
