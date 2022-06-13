using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Task_3.Models
{
    public class CarInfo
    {
        public int id { get; set; }
        public string HeaderText { get; set; }
        public string Model { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public string Luggage { get; set; }
        public string Transmission { get; set; }
        public bool AirCondition { get; set; }
        public int MinAge { get; set; }
        public string ImgUrl { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        [NotMapped]
        public IFormFile Photos { get; set; }
    }
}
