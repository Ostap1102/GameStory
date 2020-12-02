using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class PagingInfo
    {
        // Кількість товарів
        public int TotalItems { get; set; }

        // Кількість товарів на одній сторінці
        public int ItemsPerPage { get; set; }

        // Номер відображуваної сторінки
        public int CurrentPage { get; set; }

        // Загальна кількість сторінок
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}