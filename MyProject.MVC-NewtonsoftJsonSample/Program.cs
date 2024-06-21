using Microsoft.AspNetCore.Mvc;
using MyProject.Model;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>();


//Install => Microsoft.AspNetCore.Mvc.NewtonsoftJson

//جایگزین کردن سریالایزر توکار با نیوتونسافت جی سان
//از این پس فرمت ایکس ام ال هم میتوان استفاده کرد و خروجی گرفت؛ در پست من در قسمت ولیو میتوان تعیین کرد ایکس امل ال خروجی بدهد
//پیش فرض جی سان هست و اگر فرمتی که در پست من دادیم نشناخت، جیسان میدهد
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.Configure<MvcNewtonsoftJsonOptions>(c =>
{
    c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.Configure<MvcOptions>(opt =>
{
    opt.RespectBrowserAcceptHeader = true;
    opt.ReturnHttpNotAcceptable = true; 
});

var app = builder.Build();


app.Run();
