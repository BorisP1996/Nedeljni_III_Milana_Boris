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
    class AddReceptViewModel : ViewModelBase
    {
        AddReceptView view;
        Service.Service service = new Service.Service();
        Entity context = new Entity();

        public AddReceptViewModel(AddReceptView view, string username)
        {
            this.view = view;
            Component1 = new tblComponent();
            Component2 = new tblComponent();
            Component3 = new tblComponent();
            Recept = new tblRecept();
            Username = username;
            TypeList = GetReceptType();
        }
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private tblRecept recept;

        public tblRecept Recept
        {
            get { return recept; }
            set
            {
                recept = value;
                OnPropertyChanged("Recept");
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

        private ReceptType receptType;

        public ReceptType ReceptType
        {
            get { return receptType; }
            set { receptType = value;
                OnPropertyChanged("ReceptType");
            }
        }

        private List<ReceptType> typeList;

        public List<ReceptType> TypeList
        {
            get { return typeList; }
            set { typeList = value;
                OnPropertyChanged("TypeList");
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

        private bool CanCloseExecute()
        {
            return true;
        }

        private void CloseExecute()
        {
            view.Close();
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

        private void SaveExecute()
        {
            try
            {
                tblUser viaUser = (from r in context.tblUsers where r.Username == Username select r).FirstOrDefault();

                tblRecept newRecept = new tblRecept();
                newRecept.ReceptName = Recept.ReceptName;
                newRecept.ReceptText = Recept.ReceptText;
                newRecept.ReceptType = ReceptType.Name;
                newRecept.PersonNumber = Recept.PersonNumber;
                newRecept.CreationDate = DateTime.Now;
                newRecept.Author = viaUser.FullName;
                newRecept.UserID = viaUser.UserId;
                context.tblRecepts.Add(newRecept);
                tblComponent comp1 = new tblComponent();
                tblComponent comp2 = new tblComponent();
                tblComponent comp3 = new tblComponent();
                comp1.ReceptID = newRecept.ReceptID;
                comp1.ComponentName = Component1.ComponentName;
                comp1.ComponentAmount = Component1.ComponentAmount;
                comp2.ReceptID = newRecept.ReceptID;
                comp2.ComponentName = Component2.ComponentName;
                comp2.ComponentAmount = Component2.ComponentAmount;
                comp3.ReceptID = newRecept.ReceptID;
                comp3.ComponentName = Component3.ComponentName;
                comp3.ComponentAmount = Component3.ComponentAmount;

                context.tblComponents.Add(comp1);
                context.tblComponents.Add(comp2);
                context.tblComponents.Add(comp3);

                context.SaveChanges();
                IsUpdateRecept = true;

                MessageBox.Show("Recept is added");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
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

    public class ReceptType
    {
        
        public string Name { get; set; }

        public ReceptType( string n)
        {
            Name = n;
        }
    }
}



