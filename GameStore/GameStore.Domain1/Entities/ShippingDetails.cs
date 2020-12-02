using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адрес")]
        [Display(Name = "Адрес доставки(вулиця,будинок,квартира)")]
        public string Line1 { get; set; }
        [RegularExpression(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$", 
         ErrorMessage ="Ведіть номер телефону коректно")]

        [Required(ErrorMessage = "Телефон")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "E-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
       ErrorMessage = "Ведіть емайл телефону коректно")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Місто")]
        [Display(Name = "Вкажіть місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Адрес поштового відділення")]
        [Display(Name = "Вкажіть адрес поштового відділення")]
        public string vid { get; set; }

        public bool GiftWrap { get; set; }
    }
}
