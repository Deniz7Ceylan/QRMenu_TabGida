using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_TabGida.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = "";
    }
}
