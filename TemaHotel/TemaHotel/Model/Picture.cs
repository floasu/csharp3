using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class Picture
    {
        public int Id { get; set; }
        public String Path { get; set; }
        public bool Active { get; set; }

        public Picture()
        {
            this.Active = true;
        }

        public Picture(string path)
        {
            Active = true;
            this.Path = path;
        }


        public virtual ICollection<Room> AssociatedRooms { get; set; }
    }
}
