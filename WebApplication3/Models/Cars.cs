using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Cars
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string EngineNumber { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int SeatsNumber { get; set; }

        public int Power { get; set; }

        public int CreationYear { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

    }
}