using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_TabGida.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

        public string Image { get; set; } = "";

        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Status Status { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
