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
    class AdminViewModel: ViewModelBase
    {
       AdminView adminView;
       Service.Service service = new Service.Service();

        public AdminViewModel(AdminView adminView)
        {
            this.adminView = adminView;
            ReceptList = service.GetAllReceptView();

        }


        private vwRecept recept;
        public vwRecept Recept
        {
            get
            {
                return recept;
            }
            set
            {
                recept = value;
                OnPropertyChanged("Recept");
            }
        }
       
        private List<vwRecept> receptList;
        public List<vwRecept> ReceptList
        {
            get { return receptList; }
            set
            {
                receptList = value;
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
                AddReceptView addReceptView = new AddReceptView("Admin");
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



        private ICommand editRecept;

        public ICommand EditRecept
        {
            get
            {
                if (editRecept == null)
                {
                    editRecept = new RelayCommand(param => EditReceptExecute(), param => CanEditReceptExecute());
                }
                return editRecept;
            }
        }


        public bool CanEditReceptExecute()
        {
            return true;
        }

        public void EditReceptExecute()
        {
            try
            {
                EditReceptView editReceptView = new EditReceptView(Recept);
                editReceptView.ShowDialog();
                ReceptList = service.GetAllReceptView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        

        private ICommand deleteRecept;
        public ICommand DeleteRecept
        {
            get
            {
                if (deleteRecept == null)
                {
                    deleteRecept = new RelayCommand(param => DeleteReceptExecute(), param => CanDeleteReceptExecute());
                }
                return deleteRecept;
            }
        }
        private bool CanDeleteReceptExecute()
        {
            if (Recept != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DeleteReceptExecute()
        {
            try
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult resultMessageBox = MessageBox.Show("Are you sure you want to delete recept?", "Warning", btnMessageBox, icnMessageBox);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    service.DeleteRecept(Recept);
                    MessageBox.Show("Recept is deleted");
                    ReceptList = service.GetAllReceptView();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
        }
        private ICommand logOut;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logOut;
            }
        }
        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                adminView.Close();
            }
        }
    }
}

   
