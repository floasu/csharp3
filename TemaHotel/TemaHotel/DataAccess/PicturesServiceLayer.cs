using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaHotel.Model;

namespace TemaHotel.DataAccess
{
    public class PicturesServiceLayer
    {


        public void AddPicture(Picture picture)
        {
            using (var ctx = new FriendContext())
            {
                ctx.Pictures.Add(picture);
                ctx.SaveChanges();
            }
        }

        public Picture PictureExistInDb(String st)
        {
            using (var context = new FriendContext())
            {
                var activeUsers = from pict in context.Pictures
                                  where (pict.Path.Equals(st) && pict.Active == true)
                                  select pict;
                if (activeUsers.ToList().Count != 1)
                {
                    return null;
                }
                Picture us = activeUsers.FirstOrDefault();
                return us;

            }
        }

        public List<Picture> GetPictures()
        {
            using (var context = new FriendContext())
            {
                var activeUsers = from pict in context.Pictures
                                  where pict.Active == true
                                  select pict;
                return activeUsers.ToList();

            }
        }

        public List<String> getPicturesByRoom(int roomId)
        {
            using (var context = new FriendContext())
            {
                var clientIdParameter = new SqlParameter("@RoomId", roomId);

                var result = context.Database
                    .SqlQuery<String>("GetPicturesRooms @RoomId", clientIdParameter);

                return result.ToList();
            }
        }

        public void deletePicturesByRoom(int roomId)
        {
            using (var context = new FriendContext())
            {
                var a = context.Rooms.Find(roomId);
                var removals = a.RoomPictures.ToList(); //or you could filter to only remove B objects matching a specific criteria, etc.
                foreach (var remove in removals)
                {
                    a.RoomPictures.Remove(remove);
                }
                context.SaveChanges();
            }
        }
     
        public void InsertPictureToRoom(int roomId, Collection<String> pict)
        {
            using (var context = new FriendContext())
            {
                Collection<Picture> col = new Collection<Picture>();
                foreach (String st in pict)
                {
                    var result = from pc in context.Pictures
                                 where (pc.Path.Equals(st) && pc.Active == true)
                                 select pc;
                    Picture pic = result.FirstOrDefault();
                    col.Add(pic);
                }
                var a = context.Rooms.Find(roomId);
                foreach (Picture pct in col)
                {
                    a.RoomPictures.Add(pct);
                }
                context.SaveChanges();
            }
        }
    }
}
