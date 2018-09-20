using System;
using System.ComponentModel.DataAnnotations;

namespace EnjoyCodes.eShopOnContainers.WebMVC.Models
{
    public class OrderDTO
    {
        [Required]
        public string OrderNumber { get; set; }
    }
}