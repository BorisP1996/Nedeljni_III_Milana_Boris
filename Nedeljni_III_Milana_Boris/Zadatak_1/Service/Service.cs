using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.Service
{
    class Service
    {
        /// <summary>
        /// Get All Users from db
        /// </summary>
        /// <returns></returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Get All Components from db
        /// </summary>
        /// <returns></returns>
        public List<tblComponent> GetAllComponents()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblComponent> list = new List<tblComponent>();
                    list = (from x in context.tblComponents select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get All Recepts from db
        /// </summary>
        /// <returns></returns>
        public List<tblRecept> GetAllRecepts()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblRecept> list = new List<tblRecept>();
                    list = (from x in context.tblRecepts select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwRecept> GetAllReceptView()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwRecept> list = new List<vwRecept>();
                    list = (from x in context.vwRecepts select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public bool AddRecept(vwRecept recept)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    tblComponent com = new tblComponent
                    {
                        ReceptID=recept.ReceptID,
                        ComponentName = recept.ComponentName,
                        ComponentAmount = recept.ComponentAmount,
                        
                        
                    };
                    context.tblComponents.Add(com);
                    context.SaveChanges();
                    recept.ComponentID = com.ComponentID;
                    tblRecept newRecept = new tblRecept
                    {
                        UserID = recept.UserID,
                        ReceptName = recept.ReceptName,
                        ReceptType = recept.ReceptType,
                        PersonNumber = recept.PersonNumber,
                        Author = recept.Author,
                        ReceptText = recept.ReceptText,
                        CreationDate = recept.CreationDate
                    };
                    context.tblRecepts.Add(newRecept);
                    context.SaveChanges();
                    recept.ReceptID = newRecept.ReceptID;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
