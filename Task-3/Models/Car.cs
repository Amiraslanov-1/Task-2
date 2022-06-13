using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_3.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CarInfo> carInfos { get; set; }
    }
}
