using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Ctrl_API.Models;
using MyProject.Model;

namespace MyProject.Ctrl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // => وقتی از این اتربیوت استفاده کنیم، دیگر لازم نیست ایز ولید را چک کنیم و مدل بایندر هم بصورت پیش فرض سراغ بادی میرود؛ دیگر لازم
                    //نیست فرام بادی و غیره را اضافه کنیم 
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext Context) // => تزریق وابستگی در سطح کنترلر، در متد سازنده میتوان انجام داد
        {
            _context = Context;
            //اما بهتر هست بعضی مواقع تزریق وابستگی را در سطح متد استفاده کنیم
        }


        //FromServices: اگر ننویسیم فکر میکند پارامتر ورودی هست و مدل بایندر باید اطلاعات را ست کند، برای آنکه متوجه شود محتوایش از دی آی کانتینر
        //بدست ما برسد باید از این اتربیوت استفاده کنیم
        public IActionResult GetAllProducts([FromServices] ProductDbContext context)
        {
            var products = context.Products.ToList();
            return Ok(products);
        }

        //ModelBinding: میتوانیم پارامتر ورودی بگیریم، و این پارامتر ها میتوانند بایند شوند
        [HttpGet("GetProduct/{id}")]
        public IActionResult GetProduct([FromServices] ProductDbContext context, int id)
        {
            var product = context.Products.Where(t => t.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound(); // => در صورت عدم یافت شدن آن محصول
            }

            return Ok(product);

        }

        //Async API: در کوئری وقتی میره سراغ دیتابیس، بجای آنکه ترد مارا نگه دارد، ترد را آزاد میکند که برود به بقیه درخواست ها پاسخ دهد
        //و برمیگردد به ترد پول، وقتی از دیتابیس برگشت دوباره یک ترد میگیرد و نتیجه را به کاربر برمیگرداند
        [HttpGet("GetProductsAsync")]
        public async Task<IActionResult> GetProductsAsync([FromServices] ProductDbContext context)
        {
            //Include: در حالت عادی دچار خطا میشویم چون اگر بریک پوینت قرار دهیم، این انتیتی ها بهم دیگر رفرنس میدهند و میتوانیم مدام وارد یکدیگر شویم
            //برای جلوگیری از این مشکل از حلقه بصورت زیر استفاده میکنیم
            var products = await context.Products.Include(c => c.Brand).ToListAsync();

            foreach (var item in products)
            {
                item.Brand.Products = null; // => پس در فرآیند سریالایزی که روابط هم دخیل هستند، این حلقه به کمک ما می آید
            }

            return Ok(products);
        }


        //Over Binding: این اتفاق زمانی میفتد که اطلاعاتی که لازم نیست و نباید کاربر ارسال کند، مثل آی دی به هرحال ارسال میکند
        //در این مثال میشود آی دی هم با نرم افزاری مثل پست من به این اکشن ارسال کرد
        //برای جلوگیری از این اتفاق ها از ویو مدل یا دی تی او استفاده میکنیم که دیتای ضروری ارسال یا دریافت شود
        [HttpPost]
        public IActionResult PostWithEntity([FromBody] Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product.Id);
        }


        [HttpPost]
        public IActionResult PostWithDto([FromBody] AddProductDto product)
        {
            if (ModelState.IsValid)
            {
                var p = product.ToProduct();
                _context.Products.Add(p);
                _context.SaveChanges();

                return Ok(p.Id);
            }

            return BadRequest(ModelState);
        }


        
        [HttpGet("j")]
        [Produces("application/json","application/xml")]  //میخواهیم اگر کسی این متد را صدا زد، فقط جیسان یا ایکس ام ال برگرداند و نه چیز دیگر => پس کانتنت تایپ را بدین شکل میتوان تعیین کرد
        public async Task<IActionResult> GetAllProductsJson([FromServices] ProductDbContext context)
        {
            var products = await context.Products.AsNoTracking().Include(c => c.Brand).ToListAsync();

            foreach (var item in products)
            {
                item.Brand.Products = null;
            }
            return Ok(products);
        }

        [HttpGet("j/{format?}")] // اگر در ورودی میتوانستیم تعیین کنیم نوع فرمت خروجی چه چیزی باشد
        [FormatFilter] //کارش => وقتی یک درخواستی ارسال می شود؛ چک میکند آیا فرمتش ورودی دارد یا خیر؛ اگر داشت، مقدار آن را می گیرد و به اکسپت هدر رکوئست
        //ارسال می کند؛ کلا فیلتر ها این امکان را می دهند که ورودی یا خروجی را تغییر دهیم هر جوری که میخواهیم 
        [Produces("application/json", "application/xml")] 
        public async Task<IActionResult> GetAllProductsJson2([FromServices] ProductDbContext context)
        {
            var products = await context.Products.AsNoTracking().Include(c => c.Brand).ToListAsync();

            foreach (var item in products)
            {
                item.Brand.Products = null;
            }
            return Ok(products);
        }


        [Consumes("application/json")] // اگر کسی ورودی که برای ما ارسال میکند، فرمتش جی سان باشد، این متد کار میکند
        [HttpPost]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }


        [Consumes("application/xml")] //و اگر ورودی که ارسال میکند ایکس ام ال باشد، این متد کار میکند
        [HttpPost]
        public string SaveProductXml(ProductBindingTarget product)
        {
            return $"XML: {product.Name}";
        }
    }
}
