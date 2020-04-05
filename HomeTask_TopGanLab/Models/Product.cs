using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask_TopGanLab.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 4)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Размер")]
        
        public uint Size { get; set; }
        public ProductSizeType SizeType { get; set; }

    }

    public enum ProductSizeType {
        [Description("шт.")]
        AMOUNT,

        [Description("гр.")]
        GRAMS,

        [Description("кг.")]
        KILOGRAMS,

        [Description("ящ.")]
        BOXES,
    }
}
