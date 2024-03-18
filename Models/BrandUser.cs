using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_TabGida.Models
{
    public class BrandUser
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; } = "";

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; } = "";

        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public string? SurName { get; set; } = "";

        [EmailAddress]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = "";

        public string? PhoneNumber { get; set; } = "";

        public DateTime RegisterDate { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public Status Status { get; set; }
    }
}
