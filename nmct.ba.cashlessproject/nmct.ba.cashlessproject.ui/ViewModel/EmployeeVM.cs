using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class EmployeeVM : ObservableObject, IPage
    {
          public EmployeeVM()
        {
           
                GetEmployees();
            
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }

        private async void GetEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:15237/api/employee");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        }

        public string Name
        {
            get { return "Employee page"; }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        private Employee _selected;
        public Employee SelectedEmployee
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("SelectedEmployee"); }
        }


        public ICommand NewEmployeeCommand
        {
            get { return new RelayCommand(NewEmployee); }
        }

        public ICommand SaveEmployeeCommand
        {
            get { return new RelayCommand(SaveEmployee); }
        }

        public ICommand DeleteEmployeeCommand
        {
            get { return new RelayCommand(DeleteEmployee); }
        }


        private void NewEmployee()
        {
            Employee c = new Employee();
            
            Employees.Add(c);
            SelectedEmployee = c;   
            
        }


        private async void SaveEmployee()
        {
            string input = JsonConvert.SerializeObject(SelectedEmployee);
            try
            {
                // check insert (no ID assigned) or update (already an ID assigned)
                if (SelectedEmployee.Id == 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.PostAsync("http://localhost:15237/api/employee", new StringContent(input, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            string output = await response.Content.ReadAsStringAsync();
                            SelectedEmployee.Id = Int32.Parse(output);
                            Error = "";
                        }
                        else
                        {
                            Console.WriteLine("error");
                        }
                    }
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.PutAsync("http://localhost:15237/api/employee", new StringContent(input, Encoding.UTF8, "application/json"));
                        Error = "";
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error = "Gelieve eerst op 'NIEUW' te klikken.";
                Console.WriteLine(ex.Message);
            }
        }

        private async void DeleteEmployee()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.DeleteAsync("http://localhost:15237/api/employee/" + SelectedEmployee.Id);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                    else
                    {
                        Employees.Remove(SelectedEmployee);
                        Error = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Error = "Gelieve eerst een waarde te selecteren.";
                Console.WriteLine(ex.Message);
            }
        }
    }
}