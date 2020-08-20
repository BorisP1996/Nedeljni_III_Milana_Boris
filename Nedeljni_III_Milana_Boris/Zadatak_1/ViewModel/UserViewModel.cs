using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        UserView userView;
        Entity context = new Entity();
        Service.Service service = new Service.Service();

        public UserViewModel(UserView userOpen,string username)
        {
            userView = userOpen;
            Username = username;
            ReceptList = GetUserRecepts();

        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private vwRecept recept;

        public vwRecept Recept
        {
            get { return recept; }
            set { recept = value;
                OnPropertyChanged("Recept");
            }
        }

        private List<vwRecept> receptList;

        public List<vwRecept> ReceptList
        {
            get { return receptList; }
            set { receptList = value;
                OnPropertyChanged("ReceptList");
            }
        }

        private ICommand addRecept;
        /// <summary>
        /// Add recept command
        /// </summary>
        public ICommand AddRecept
        {
            get
            {
                if (addRecept == null)
                {
                    addRecept = new RelayCommand(param => AddReceptExecute(), param => CanAddReceptExecute());
                }
                return addRecept;
            }
        }

        /// <summary>
        /// Add recept execute
        /// </summary>
        private void AddReceptExecute()
        {
            try
            {
                AddReceptView addReceptView = new AddReceptView(Username);
                addReceptView.ShowDialog();
                if ((addReceptView.DataContext as AddReceptViewModel).IsUpdateRecept == true)
                {
                    ReceptList = service.GetAllReceptView().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// Can add recept
        /// </summary>
        /// <returns></returns>
        private bool CanAddReceptExecute()
        {
            return true;
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close==null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        private void CloseExecute()
        {
            userView.Close();
        }

        public List<vwRecept> GetUserRecepts()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwRecept> receptList = context.vwRecepts.ToList();
                    List<vwRecept> userRecept = new List<vwRecept>();
                    List<tblRecept> allRecepts = new List<tblRecept>();

                    tblUser viaUser = (from r in context.tblUsers where r.Username == Username select r).FirstOrDefault();
                    int id = viaUser.UserId;

                    foreach (vwRecept item in receptList)
                    {
                        if (item.UserID == id)
                        {
                            userRecept.Add(item);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return userRecept;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
