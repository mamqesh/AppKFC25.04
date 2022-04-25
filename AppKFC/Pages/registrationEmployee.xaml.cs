using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppKFC.Database;

namespace AppKFC.Pages
{
    /// <summary>
    /// Логика взаимодействия для registrationEmployee.xaml
    /// </summary>
    public partial class registrationEmployee : Page
    {
        danilEntities connection = new danilEntities();
        public registrationEmployee()
        {
            InitializeComponent();
            LoadRole();
            comboBoxLoadRole.DisplayMemberPath = "Role1";
        }

        private void LoadRole()
        {
            var role = connection.Role.ToList();
            foreach (var _role in role)
            {
                comboBoxLoadRole.Items.Add(_role);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string phone = textBoxPhone.Text.Trim();
            string name = textBoxName.Text.Trim();
            string surname = textBoxSurname.Text.Trim();
            string patronymic = textBoxPatronymic.Text.Trim();
            string password = passwordBoxEmployee.Password.Trim();
            var role = connection.Role.ToList();
            int roleID=0;
            foreach (var _role in role)
            {
                if (comboBoxLoadRole.SelectedItem == _role)
                {
                    roleID = (comboBoxLoadRole.SelectedItem as Database.Role).IDRole;
                }
            }
            int error = 0;
            if (phone.Length == 0 || name.Length == 0)
            {
                MessageBox.Show("Вы ввели не все данные!");
            }
            else
            {
                var employee = connection.Employee.ToList();
                foreach (var _employee in employee)
                {
                    if (phone == _employee.Phone)
                    {
                        MessageBox.Show("Данный номер телефона зарегестрирован");
                        error++;
                    }
                }
                if (error == 0)
                {
                    if (phone.Length > 9 || name.Length > 1 || surname.Length > 1 || patronymic.Length > 1 || password.Length>8 || comboBoxLoadRole.SelectedIndex==1)
                    {
                        Database.Employee employees = new Database.Employee();
                        employees.Phone = phone;
                        employees.Role = roleID;
                        employees.Password = password;
                        employees.Name = name;
                        employees.Surname = surname;
                        employees.Patronymic = patronymic;
                        connection.Employee.Add(employees);
                        int result = connection.SaveChanges();
                        if (result == 1)
                        {
                            name = "";
                            surname = "";
                            patronymic = "";
                            phone = "";

                            MessageBox.Show("Пользователь успешно добавлен!");
                            textBoxPhone.Clear();
                            textBoxName.Clear();
                            textBoxSurname.Clear();
                            textBoxPatronymic.Clear();
                            passwordBoxEmployee.Clear();
                            NavigationService.Navigate(MainWindow.pageRegistrationPage);
                        }
                        else
                        {
                            passwordBoxEmployee.ToolTip="Пароль меньше 8-ми символов";
                            MessageBox.Show("Ошибка добавления пользователя!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данные введены некорректно!");
                    }

                }
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

