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
                AddReceptView addReceptView = new AddReceptView();
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
    }
}

   
