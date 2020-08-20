using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AddReceptViewModel: ViewModelBase
    {
        AddReceptView view;
        Service.Service service = new Service.Service();


        public AddReceptViewModel(AddReceptView view)
        {
            this.view = view;
            recept = new vwRecept();
            ReceptList = service.GetAllReceptView().ToList();

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
        private bool isUpdateRecept;
        public bool IsUpdateRecept
        {
            get
            {
                return isUpdateRecept;
            }
            set
            {
                isUpdateRecept = value;
            }
        }
    }
}

