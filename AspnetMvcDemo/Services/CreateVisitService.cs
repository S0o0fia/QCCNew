using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Services.Discovery;

namespace AspnetMvcDemo.Services
{
    public class CreateVisitService
    {
        VisitService visitServ;
        UserService userServ;
        FactoryService factoryServ;
        DateTime dateTime;
        bool result1;
        bool result2;
        bool result3;
        bool result4;
        QCEntities entities;
        List<User> Users;

        public CreateVisitService()
        {
            visitServ = new VisitService();
            userServ = new UserService();
            factoryServ = new FactoryService();
            entities = new QCEntities();

        }

        public void CreateVisit()
        {
            var Count = 0;
            dateTime = DateTime.Today;
            var queryResult = new List<Visitviewmodel>();
            Users = userServ.UserDetails();
            while (Count < 30)
            {
                queryResult = visitServ.GetVisits();
                if (queryResult.Count == 0)
                {

                    autoCreate(dateTime , Count);

                  
                }
                else
                {
                    dateTime = queryResult.LastOrDefault().visitDate;
                    dateTime = dateTime.AddDays(1);
                    autoCreate(dateTime , Count);
                   
                }
                Count += 1;
                
            }
           
        }

        private void autoCreate(DateTime dateTime , int Count)
        {
          
                //if day is Thursday
                if ((int)dateTime.DayOfWeek == 4)
                {
                    dateTime = dateTime.AddDays(2);
                }
                //if day is Friday
                else if ((int)dateTime.DayOfWeek == 5)
                {
                    dateTime = dateTime.AddDays(1);
                }
                //foreach of every user for today
                foreach (var user in Users)
                {
                    //check the last factid for this user in VisitDetails 
                    

                    var lastfact = entities.VisitDetails.ToList().LastOrDefault();
                    var totalvisitdate = (from vs in entities.VisitDetails 
                                          join f in entities.Factory11 
                                          on vs.FactoryId equals f.Id
                                          where f.Location_Id == user.Location_Id && vs.MonitorId == user.Id
                                          select vs.Id
                                          ).ToList();

                //to Detect the last monitor
                var yeserday = dateTime;
                yeserday = yeserday.AddDays(-1);
                var Mid = (from vs in entities.VisitDetails
                            join fac in  entities.Factory11
                            on vs.FactoryId equals fac.Id
                            where fac.Location_Id == user.Location_Id && DbFunctions.TruncateTime(vs.VisitDate) == DbFunctions.TruncateTime(yeserday)
                            select vs.MonitorId
                            ).ToList().LastOrDefault();

                var factid = (from vs in entities.VisitDetails
                              join f in entities.Factory11
                              on vs.FactoryId equals f.Id
                              where f.Location_Id == user.Location_Id && vs.MonitorId == Mid
                              select vs.FactoryId
                                          ).ToList().LastOrDefault();

                if (factid != 0)
                    {
                        if(user == Users[0])
                        {
                        if (totalvisitdate.Count != 0)
                        {
                            if (totalvisitdate.Count % 4 != 0 && Mid == user.Id)
                            {
                                AssignnewVisit(user, dateTime);
                            }

                            else
                            {
                                //case if Mid == User[0].Id
                                if (Mid == Users[0].Id)
                                {
                                    AssignIncrementVisit(Users[1].Id, factid, user.Location_Id, dateTime);
                                }
                                else if (Mid == Users[1].Id)
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits2User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits2User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[1].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                        AssignIncrementVisit(Users[2].Id, factid, user.Location_Id, dateTime);

                                }

                                else
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits3User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits3User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[2].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                    {
                                        AssignIncrementVisit(Users[0].Id, factid, user.Location_Id, dateTime);
                                    }
                                 
                                }
                            }
                        }
                        else
                        {
                            AssignnewVisit(Users[0], dateTime);
                        }
                 }

                        if (user == Users[1])
                        {
                        if (totalvisitdate.Count != 0)
                        {

                            if (totalvisitdate.Count % 4 != 0 || totalvisitdate.Count == 2 && Mid == user.Id)
                            {
                                AssignnewVisit(user, dateTime);
                            }

                            else
                            {
                                //case if Mid == User[0].Id
                                if (Mid == Users[1].Id)
                                {
                                    AssignIncrementVisit(Users[2].Id, factid, user.Location_Id, dateTime);
                                }
                                else if (Mid == Users[2].Id)
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits2User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits2User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[2].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                        AssignIncrementVisit(Users[0].Id, factid, user.Location_Id, dateTime);

                                }

                                else
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits3User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits3User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[0].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                    {
                                        AssignIncrementVisit(Users[1].Id, factid, user.Location_Id, dateTime);
                                    }
                                }

                            }
                        }

                        else
                        {
                            AssignnewVisit(Users[1], dateTime);
                        }
                        }
                       if( user == Users[2])
                        {
                        if (totalvisitdate.Count != 0)
                        {

                            if (totalvisitdate.Count % 4 != 0 || totalvisitdate.Count == 2 && Mid == user.Id)
                            {
                                AssignnewVisit(user, dateTime);
                            }

                            else
                            {
                                //case if Mid == User[0].Id
                                if (Mid == Users[2].Id)
                                {
                                    AssignIncrementVisit(Users[0].Id, factid , user.Location_Id, dateTime);
                                }
                                else if (Mid == Users[0].Id)
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits5User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits5User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[0].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                        AssignIncrementVisit(Users[1].Id, factid, user.Location_Id, dateTime);

                                }

                                else
                                {
                                    //retives all visits for this user in this location
                                    var totalvisits6User = (from vs in entities.VisitDetails
                                                            join f in entities.Factory11
                                                            on vs.FactoryId equals f.Id
                                                            where f.Location_Id == user.Location_Id
                                                            && vs.MonitorId == Mid
                                                            select vs.Id
                                                            ).ToList();


                                    if (totalvisits6User.Count % 4 != 0)
                                    {
                                        AssignIncrementVisit(Users[1].Id, factid, user.Location_Id, dateTime);
                                    }
                                    else
                                    {
                                        AssignIncrementVisit(Users[2].Id, factid, user.Location_Id, dateTime);
                                    }


                                }
                            }
                        }

                        else
                        {
                            AssignnewVisit(Users[2], dateTime);
                        }
                        }

                    }
                    else
                    {

                        AssignnewVisit(user , dateTime);
                    }



                   
             }

                dateTime.AddDays(1);
        }

        public void AssignnewVisit (User user , DateTime date)
        {
            var Count = (from count in entities.VisitDetails
                         join f in entities.Factory11
                         on count.FactoryId equals f.Id
                         where f.Location_Id == user.Location_Id
                         select count.Id).ToList().Count;
            var totalfact = entities.Factory11.Where(f => f.Location_Id == user.Location_Id && f.State == true).ToList().Count;
            Factory11[] fact  ;
            if (Count == totalfact)
            {
                 fact = entities.Factory11.Where(f => f.Location_Id == user.Location_Id && f.State == true).OrderBy(f => f.Id).ToArray();
            }
            else
            {
                 fact = entities.Factory11.Where(f => f.Location_Id == user.Location_Id && f.State == true).OrderBy(f => f.Id).Skip(Count).ToArray();

              
            }

            if (fact.Length > 0)
            {
                result1 = visitServ.CreateVisits(new VisitDetail
                {
                    FactoryId = fact[0].Id,
                    MonitorId = user.Id,
                    VisitDate = date
                });

                if (result1 == true && fact.Length > 1)
                {
                    result2 = visitServ.CreateVisits(new VisitDetail
                    {
                        FactoryId = fact[1].Id,
                        MonitorId = user.Id,
                        VisitDate = date
                    });
                }
            }
        }

        public void AssignIncrementVisit(int Mid, int factid, int? locationid, DateTime date)
        {
            var Count = (from count in entities.VisitDetails
                         join f in entities.Factory11
                         on count.FactoryId equals f.Id
                         where f.Location_Id == locationid
                         select count.Id).ToList().Count;

            var totalfact = entities.Factory11.Where(f => f.Location_Id == locationid && f.State == true).ToList().Count;
            Factory11[] fact;
            if (Count == totalfact)
            {
                fact = entities.Factory11.Where(f => f.Location_Id == locationid && f.State == true).OrderBy(f => f.Id).ToArray();
            }
            else
            {
                fact = entities.Factory11.Where(f => f.Location_Id == locationid && f.State == true).OrderBy(f => f.Id).Skip(Count).ToArray();


            }


            if (fact.Length > 0)
            {
                result1 = visitServ.CreateVisits(new VisitDetail
                {
                    FactoryId = fact[0].Id,
                    MonitorId = Mid,
                    VisitDate = date
                });

                if (result1 == true && fact.Length > 1)
                {
                    result2 = visitServ.CreateVisits(new VisitDetail
                    {
                        FactoryId = fact[1].Id,
                        MonitorId = Mid,
                        VisitDate = date
                    });
                }
            }
        }

        }
}