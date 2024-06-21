using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace MyProject.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }

        //میخواهیم اگر این پراپرتی مقدارش نال بود، سریالایز نشود و در خروجی نوع جی سانی که بر میگردد، نادیده بگیرد و نمایش ندهد
        //اما در این روش مجبوریم به ازای تک تک پراپرتی هایی که داریم این کار را انجام دهیم و باید از راهکار جایگزین کانفیگ جی سان آپشن در پروگرم استفاده کنیم
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Brand Brand { get; set; }
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=ProductDb;Integrated Security=true");
        }
    }
         
}
