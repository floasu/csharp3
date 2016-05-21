using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public Facility()
        {
            this.Active = true;
        }

        public Facility(string name)
        {
            this.Name = name;
            this.Active = true;
        }

        public virtual ICollection<Room> AssociatedRooms { get; set; }


    }
}
