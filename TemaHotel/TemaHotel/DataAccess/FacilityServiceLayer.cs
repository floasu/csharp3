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
    public class FacilityServiceLayer
    {
    //    private static FriendContext context = new FriendContext();

        public void AddFacility(Facility newFacility)
        {
            using (var ctx = new FriendContext())
            {
                var facil = from fac in ctx.AvailableFacilities
                            where fac.Name.Equals(newFacility.Name) == true
                            select fac;
                if (facil.ToList().Count > 0)
                {
                    RestoreFacility(facil.FirstOrDefault() as Facility);
                }
                else
                {
                    ctx.AvailableFacilities.Add(newFacility);
                    ctx.SaveChanges();
                }
            }
        }

        public List<Facility> GetFacilities()
        {
            using (var context = new FriendContext())
            {
                var activeFacilities = from fac in context.AvailableFacilities
                                       where fac.Active == true
                                       select fac;
                return activeFacilities.ToList();
            }
        }

        internal OperationResult ModifyFacility(Facility facilityToChange)
        {
            try
            {
                using (var context = new FriendContext())
                {
                    var user = from us in context.AvailableFacilities
                               where us.Id == facilityToChange.Id
                               select us;
                    var userChanged = user.First();
                    userChanged.Name = facilityToChange.Name;
                    context.SaveChanges();
                    return OperationResult.OkResult;
                }
            }
            catch (Exception e)
            {
                return new OperationResult
                {
                    OperationSucceeded = false,
                    Messages = ExceptionMessageComposer.GetMessages(e)
                };
            }
        }

        internal OperationResult DeleteFacility(Facility facilityToDelete)
        {
            try
            {
                using (var context = new FriendContext())
                {
                    var fac = from us in context.AvailableFacilities
                              where us.Id == facilityToDelete.Id
                              select us;
                    var chuser = fac.First();
                    chuser.Active = false;
                    context.SaveChanges();
                    return OperationResult.OkResult;
                }
            }
            catch (Exception e)
            {
                return new OperationResult
                {
                    OperationSucceeded = false,
                    Messages = ExceptionMessageComposer.GetMessages(e)
                };
            }
        }

        internal OperationResult RestoreFacility(Facility facilityToRestore)
        {
            try
            {
                using (var context = new FriendContext())
                {
                    var user = from us in context.AvailableFacilities
                               where us.Id == facilityToRestore.Id
                               select us;
                    var chuser = user.First();
                    chuser.Active = true;
                    context.SaveChanges();
                    return OperationResult.OkResult;
                }
            }
            catch (Exception e)
            {
                return new OperationResult
                {
                    OperationSucceeded = false,
                    Messages = ExceptionMessageComposer.GetMessages(e)
                };
            }
        }

        public List<Facility> getFacilityByRoom(int roomId)
        {
            using (var context = new FriendContext())
            {
                var clientIdParameter = new SqlParameter("@RoomId", roomId);

                var result = context.Database
                    .SqlQuery<Facility>("GetFacilities @RoomId", clientIdParameter);
                  
                return result.ToList();
                
            }
        }

        public void deleteFacilitiesRoom(int roomId)
        {
            using (var context = new FriendContext())
            {
                var a = context.Rooms.Find(roomId);
                var removals = a.RoomFacilieties.ToList(); //or you could filter to only remove B objects matching a specific criteria, etc.
                foreach (var remove in removals)
                {
                    a.RoomFacilieties.Remove(remove);
                }
                context.SaveChanges();
            }
        }

        public void InsertFacilitiesToRoom(int roomId, Collection<Facility> facilities)
        {
            using (var context = new FriendContext())
            {
                Collection<Facility> col = new Collection<Facility>();
                foreach (Facility st in facilities)
                {
                    var result = from pc in context.AvailableFacilities
                                 where (pc.Id == st.Id && pc.Active == true)
                                 select pc;
                    Facility pic = result.FirstOrDefault();
                    col.Add(pic);
                }
                var a = context.Rooms.Find(roomId);
                foreach (Facility pct in col)
                {
                    a.RoomFacilieties.Add(pct);
                }
                context.SaveChanges();
            }
        }

    }
}
