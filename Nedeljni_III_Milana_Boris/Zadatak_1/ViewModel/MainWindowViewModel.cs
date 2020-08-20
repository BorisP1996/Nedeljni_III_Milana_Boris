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
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        Entity context = new Entity();
        Service.Service service = new Service.Service();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                OnPropertyChanged("FullName");
            }
        }

        #endregion

        #region Commands
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
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        /// <summary>
        /// Determins whos logged and opens new window according to that
        /// </summary>
        private void LoginExecute()
        {
            try
            {
                if (Username == "Admin" && Password == "Admin123")
                {
                    AdminView adminView = new AdminView();
                    main.Close();
                    adminView.ShowDialog();
                }
                //if username does not exist=>first registration...that means that full name can not be empty
                else if (service.UsernameExist(Username)==true && !String.IsNullOrEmpty(FullName))
                {
                    tblUser newUser = new tblUser();
                    newUser.Username = Username;
                    newUser.Pasword = Password;
                    newUser.FullName = FullName;
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    MessageBox.Show("User is added to the database");
                    UserView userView = new UserView(Username);
                    userView.ShowDialog();
                }
                else if (service.UsernameExist(Username)==false && service.CredentialsMatch(Username,Password)==true)
                {
                    MessageBox.Show("We recognize you! Welcome!");
                    UserView userView = new UserView(Username);
                    userView.ShowDialog();
                }
                else if (service.UsernameExist(Username)==false && !String.IsNullOrEmpty(FullName))
                {
                    MessageBox.Show("Username already exists.Please try another one.");
                }
                else
                {
                    MessageBox.Show("Invalid parametres\nIf this is your first registration please populate full name field.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
