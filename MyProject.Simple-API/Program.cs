//در این پروژه، به ساده ترین روش ممکن یک وب ای پی آی میسازیم

using Microsoft.Extensions.DependencyInjection;
using MyProject.Model;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

//ابتدا سرویس دیبی کانتکس را ادد میکنیم
builder.Services.AddDbContext<ProductDbContext>();

var app = builder.Build();

//میتوانیم یک اندپوینت برای ای پی آی خود ایجاد کنیم که لیست محصولات را در قالب یک فایل جی سان به ما ارائه دهد
app.MapGet("GetProductList", async (HttpContext context, ProductDbContext dbContext) =>
{
    var productsList = dbContext.Products.ToList();

    var productListString = JsonConvert.SerializeObject(productsList); // => تبدیل لیست خروجی گرفته از دیتابیس به جی سان

    context.Response.StatusCode = 200;

    await context.Response.WriteAsync(productListString);
});


app.MapGet("/", () => "Hello World!");

app.Run();
