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
    class EditReceptViewModel: ViewModelBase
    {
        EditReceptView editReceptView;
        Service.Service service = new Service.Service();
        
        public vwRecept ReceptBeforeEdit { get; set; }

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

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        public EditReceptViewModel(EditReceptView editReceptView, vwRecept recept)
        {
            this.editReceptView = editReceptView;
            Recept = recept;
            ReceptBeforeEdit = new vwRecept
            {
                ReceptName = recept.ReceptName,
                ReceptType = recept.ReceptType,
                CreationDate = DateTime.Now,
                PersonNumber = recept.PersonNumber,
                ReceptText = recept.ReceptText,
                UserID = recept.UserID,
                ComponentAmount=recept.ComponentAmount,
                ComponentName=recept.ComponentName,
            };

        }
        
        /// <summary>
        /// 
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                service.EditRecept(Recept);
                editReceptView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanSaveExecute()
        {

            return true;
                }

        private void CloseExecute()
        {
            editReceptView.Close();
        }

        private bool CanCloseExecute()
        {
            return true;
        }
    }
}

   