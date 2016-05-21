using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class ExtraServices
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }

        public ExtraServices()
        {
            Active = true;
        }

        public ExtraServices(string name, double price)
        {
            Active = true;
            this.Name = name;
            this.Price = price;
        }

        public virtual ICollection<Room> AssocRooms { get; set; }
    }
}
