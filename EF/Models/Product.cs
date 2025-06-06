using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ef.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Tensanpham",TypeName="ntext")]
        public string Name { get; set; }
        [Column(TypeName="money")]
        public decimal Price { get; set; }
        public int CateId { get; set; }
        // Foreign key
        [ForeignKey("CateId")]
        public Category Category { get; set; }
        public void PrintInfo() => Console.WriteLine($"{ProductId} - {Name} - {Price} - {CateId}");
    }
}
