using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Ctrl_API.Controllers
{
    [Route("api/[controller]")] //این اتربیوت روی کنترلر قرار میگیرد و مسیر را میتوان تعیین کرد
    public class MyController : ControllerBase // => ارث بری از این کلاس باعث میشود به یکسری پراپرتی های اچ تی تی پی کانتکست دسترسی داشته باشیم
    {

        [HttpGet("GetName1")] // => تعیین کردن آدرس اختصاصی برای این اکشن
        public string GetName()
        {
            return "My Name Is MohamadMahdi"; // => https://localhost:7210/api/my/getname1
        }


        [HttpGet("GetName2/{id}")] // => تعیین کردن آدرس اختصاصی برای این اکشن؛ همراه با پارامتر ورودی
        public string GetName2(int id)
        {
            return $"My Name Is MohamadMahdi 2 - {id}"; // => https://localhost:7210/api/my/getname2/1996
        }

        //IActionResult => میتوان از این اینترفیس استفاده کرد و ریترن تایپ مورد علاقه ی خود را در خروجی برگردانیم
        [HttpGet("GetName3/{id}")]
        public IActionResult GetName3(int id)
        {
            return Ok($"My Name Is MohamadMahdi 3 - {id}"); //هرچیزی که بخواهیم را میتوانیم برگردانیم و ریترن کنیم
        }


        //Redirect: میتوانیم منتقل شویم به آدرس دیگری
        //RedirectToLocal: اگر در ورودی، آدرس رو از کاربر میگیریم و هدف این هست که در برنامه خودمان جابجا شویم، باید از این مدل ریدایرکت استفاده شود
        // اگر این کار را نکنیم، کاربر می تواند آدرس یک سایت بیرونی را بدهد
        // و بقیه هم در سایت ما ریدایرکت بشن به اون سایت بیرونی و اپن ریدایرکت اتکت میخوریم
        [HttpGet("google")]
        public IActionResult RedirectToGoogle()
        {
            return Redirect("http://google.com"); // => https://localhost:7210/api/my/google          
        }
       
    }
}
