using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Advertisement.Models
{
    /// <summary>
    /// Модель рекламы
    /// </summary>
    public class Advertisement
    {
        public int Id { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Описание обязательно для заполнения")]
        public string Description { get; set; }

        [DisplayName("Время следующего тех. обслуживания")]
        [Required(ErrorMessage = "Время следующего тех. обслуживания обязательно для заполнения")]
        public DateTime MaintenanceTime{ get; set; }

        [DisplayName("Место расположения")]
        public string Location{ get; set; }

        [DisplayName("Тип конструкции")]
        [Required]
        public string Type{ get; set; }

        [DisplayName("Высота")]
        [Required(ErrorMessage = "Высота обязательна для заполнения")]
        public float Height{ get; set; }

        [DisplayName("Ширина")]
        [Required(ErrorMessage = "Ширина обязательна для заполнения")]
        public float Width { get; set; }


        [DisplayName("Цена размещения рекламы в месяц")]
        [Required(ErrorMessage = "Цена размещения рекламы в месяц обязательна для заполнения")]
        public float MonthlyCost { get; set; }
    }
}