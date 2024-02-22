using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class Response
    {
        public int Value { get; set; }

        public string Description { get; set; }

        public DataTable List { get; set; } // Sonuçlar içerisinde bir liste dönülecek ise bu kısma koyulacaktır.

        public int DocEntry { get; set; } //SAP Belgesi numarası geri dönülmek istenirse kullanılır.
    }
}