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
    class EditReceptViewModel : ViewModelBase
    {
        EditReceptView editReceptView;
        Service.Service service = new Service.Service();
        Entity context = new Entity();

        public EditReceptViewModel(EditReceptView editReceptView, vwRecept recept)
        {
            this.editReceptView = editReceptView;
            Recept = recept;
            TypeList = GetReceptType();
            ReceptType = new ReceptType();
            Component1 = new tblComponent();
            Component2 = new tblComponent();
            Component3 = new tblComponent();
            List<tblComponent> componentList = (from r in context.tblComponents where r.ReceptID == Recept.ReceptID select r).ToList();
            Component1.ComponentName = componentList[0].ComponentName;
            Component1.ComponentAmount = componentList[0].ComponentAmount;
            Component2.ComponentName = componentList[1].ComponentName;
            Component2.ComponentAmount = componentList[1].ComponentAmount;
            Component3.ComponentName = componentList[2].ComponentName;
            Component3.ComponentAmount = componentList[2].ComponentAmount;
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

        private ReceptType receptType;

        public ReceptType ReceptType
        {
            get { return receptType; }
            set
            {
                receptType = value;
                OnPropertyChanged("ReceptType");
            }
        }

        private List<ReceptType> typeList;

        public List<ReceptType> TypeList
        {
            get { return typeList; }
            set
            {
                typeList = value;
                OnPropertyChanged("TypeList");
            }
        }

        private tblComponent component1;

        public tblComponent Component1
        {
            get { return component1; }
            set
            {
                component1 = value;
                OnPropertyChanged("Component1");
            }
        }

        private tblComponent component2;

        public tblComponent Component2
        {
            get { return component2; }
            set
            {
                component2 = value;
                OnPropertyChanged("Component2");
            }
        }

        private tblComponent component3;

        public tblComponent Component3
        {
            get { return component3; }
            set
            {
                component3 = value;
                OnPropertyChanged("Component3");
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

        public void SaveExecute()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    tblRecept receptToEdit = context.tblRecepts.Where(x => x.ReceptID == Recept.ReceptID).FirstOrDefault();
                    receptToEdit.ReceptName = Recept.ReceptName;
                    receptToEdit.ReceptType = ReceptType.Name;
                    receptToEdit.PersonNumber = Recept.PersonNumber;
                    receptToEdit.Author = Recept.Author;
                    receptToEdit.ReceptText = Recept.ReceptText;
                    receptToEdit.CreationDate = DateTime.Now;
                    List<tblComponent> componentList = (from r in context.tblComponents where r.ReceptID == Recept.ReceptID select r).ToList();

                    componentList[0].ComponentName = Component1.ComponentName;
                    componentList[0].ComponentAmount = Component1.ComponentAmount;

                    componentList[1].ComponentName = Component2.ComponentName;
                    componentList[1].ComponentAmount = Component2.ComponentAmount;

                    componentList[2].ComponentName = Component3.ComponentName;
                    componentList[2].ComponentAmount = Component3.ComponentAmount;

                    MessageBox.Show("Recept is edited");

                    editReceptView.Close();

                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanSaveExecute()
        {

            if (String.IsNullOrEmpty(Recept.ReceptName) || String.IsNullOrEmpty(Recept.ReceptText) || String.IsNullOrEmpty(ReceptType.Name) || String.IsNullOrEmpty(Component1.ComponentName) || String.IsNullOrEmpty(Component2.ComponentName) || String.IsNullOrEmpty(Component3.ComponentName) || Recept.PersonNumber == 0 || Component1.ComponentAmount == 0 || Component2.ComponentAmount == 0 || Component3.ComponentAmount == 0)
            {
                return false;
            }
            else
            {
                return true;
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
        private void CloseExecute()
        {
            editReceptView.Close();
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        private List<ReceptType> GetReceptType()
        {
            ReceptType rt = new ReceptType("Predjelo");
            ReceptType rt2 = new ReceptType("Glavno jelo");
            ReceptType rt3 = new ReceptType("Desert");

            List<ReceptType> list = new List<ReceptType>();
            list.Add(rt);
            list.Add(rt2);
            list.Add(rt3);
            return list;
        }
    }

    public class ReceptTypeEdit
    {

        public string Name { get; set; }

        public ReceptTypeEdit()
        {

        }
        public ReceptTypeEdit(string n)
        {
            Name = n;
        }
    }

}

   