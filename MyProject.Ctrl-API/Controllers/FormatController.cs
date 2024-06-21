using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Ctrl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {
        [HttpGet("str")]
        public string GetStr()
        {
            return "Hello, World";    // => Conent Type: text/plain; charset = utf-8
        }

        [HttpGet("int")]
        public int GetInt() => 1996;  // => Content Type: application/json; charset = utf-8    


        [HttpGet("obj")]
        public object GetObj() => new // => Content Type: application/json; charset = utf-8   
        {
            FirstName = "MohamadMahdi",
            LastName = "Tavakoli"
        };
    }
}
