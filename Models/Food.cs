using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_TabGida.Models
{
    public class Food
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";

        public string Photo { get; set; } = "";

        [StringLength(200)]
        [Column(TypeName = "nvarchar(100)")]
        public string? NutritionalValue { get; set; } //BesinDeğeri

        [StringLength(200)]
        [Column(TypeName = "nvarchar(100)")]
        public string? Ingredients { get; set; } //Ürün İçeriği

        [Range(0, float.MaxValue)]
        public float Price { get; set; }

        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }

    }
}
