using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Service
{
    public class Service
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
                        ReceptID = recept.ReceptID,
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

        public bool UsernameExist(string usernameInput)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblUser> userList = context.tblUsers.ToList();
                    List<string> usernameList = new List<string>();

                    foreach (tblUser item in userList)
                    {
                        usernameList.Add(item.Username);
                    }

                    if (!usernameList.Contains(usernameInput))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;

            }
        }

        public bool CredentialsMatch(string inputUsername, string inputPassword)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblUser> userList = context.tblUsers.ToList();

                    foreach (tblUser item in userList)
                    {
                        if (item.Username == inputUsername && item.Pasword == inputPassword)
                        {
                            return true;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
      
      
      public void DeleteRecept(vwRecept recept)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    tblRecept viaRecept = (from r in context.tblRecepts where r.ReceptID == recept.ReceptID select r).FirstOrDefault();

                    //find every component for this recept

                    List<tblComponent> componentList = context.tblComponents.ToList();

                    foreach (tblComponent item in componentList)
                    {
                        if (item.ReceptID==viaRecept.ReceptID)
                        {
                            context.tblComponents.Remove(item);
                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    context.tblRecepts.Remove(viaRecept);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

    }
}
           
   
