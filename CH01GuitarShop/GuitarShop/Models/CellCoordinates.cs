using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace GuitarShop.Models
{
    public class CellCoordinates
    {

        //[Required(ErrorMessage = "Please enter an ID.")]
        public string? ID { get; set; } = string.Empty; 
        public string? color { get; set; } = "lightsteelblue";
    }
}
