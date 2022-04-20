using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string  Username { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SertificateNumber { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<HiredCars> HiredCars { get; set; }




    }
}