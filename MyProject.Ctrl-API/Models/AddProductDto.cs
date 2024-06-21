using MyProject.Model;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Ctrl_API.Models
{
    public class AddProductDto
    {
        //Validation: اگر تعیین کردیم که پراپرتی هایمان باید چه خصوصیاتی داشته باشند؛ از آن طرف که اطلاعات میگیریم حتما باید چک کنیم که مدل استیت ما
        //ایز ولید باشد؛ در اکشن پست ویت دی تی او این کار را انجام داده ایم
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }

        public Product ToProduct() => new Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            BrandId = BrandId
        };       
        
    }
}
