
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Procuct Name can not be null.")]
        public string Name { get; set; } 
        [Required(ErrorMessage = "Procuct Valu is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The 'Value' field must be greater than zero.")]
        public double Value { get; set; }
    }
}