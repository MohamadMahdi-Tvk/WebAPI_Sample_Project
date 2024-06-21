using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyProject.Model;

namespace MyProject.MVC_NewtonsoftJsonSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext Context)
        {
            _context = Context;
        }


        //Http Patch: میتوانیم یک بخشی از انتیتی خود یعنی چند تا پراپرتی خود را در فرآیند سریالایز عوض کنیم
        [HttpPatch("{id}")]
        public async Task<Product> PatchSupplier(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            Product p = await _context.Products.FindAsync(id);

            if (p != null)
            {
                patchDoc.ApplyTo(p);
                await _context.SaveChangesAsync();
            }

            return p;

            //در پست من به این ترتیب مقدار را میدهیم
            /* [
             * { "op":"replace", "path": "name", "value": "Name Patched Change"},
             * { "op":"replace", "path": "description", "value": "Description Patched Change"},
             * ]
             */

        }
    }
}
