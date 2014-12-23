using GalaSoft.MvvmLight.CommandWpf;
using model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class WijzigWWManagement: ObservableObject, IPage
    {

        public string Name
        {
            get { return "Wachtwoord wijzigen"; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged("NewPassword"); }
        }

        private string _repeat;

        public string Repeat
        {
            get { return _repeat; }
            set { _repeat = value; OnPropertyChanged("Repeat"); }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }


        public ICommand ChangePasswordCommand
        {
            get { return new RelayCommand(ChangePassword); }
        }
        private async void ChangePassword()
        {
            Password p = new Password();
            p.Username = Username;
            p.NewPass = NewPassword;
            p.RepeatNewPass = Repeat;
        
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    string pass = JsonConvert.SerializeObject(p);
                    HttpResponseMessage response = await
                    client.PutAsync("http://localhost:15237/api/Password", new StringContent(pass,
                    Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        if (NewPassword == Repeat)
                        {
                            Error = "";
                        }
                    }
                    else
                    {Console.WriteLine("de passwoorden komen niet overeen"); }
                }
            
        }
    }
}
