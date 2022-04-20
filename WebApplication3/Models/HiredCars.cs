using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class HiredCars
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int CarId { get; set; }

        public Users Users { get; set; }

        public int UsersId { get; set; }
    }
}