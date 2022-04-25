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
    /// Логика взаимодействия для registrationPageClient.xaml
    /// </summary>
    public partial class registrationPageClient : Page
    {
        danilEntities connection = new danilEntities(); 
        public registrationPageClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //ДОБАВЛЕНИЕ КЛИЕНТА
        {
            string phone = textBoxPhone.Text.Trim();
            string name= textBoxName.Text.Trim();   
            string address = textBoxAddress.Text.Trim();
            int error=0;
            if (phone.Length == 0 || name.Length == 0 || address.Length==0)
            {
                MessageBox.Show("Вы ввели не все данные!");
            }
            else
            {
                var users = connection.Client.ToList();
                foreach (var _user in users)
                {
                    if (phone == _user.Phone)
                    {
                        MessageBox.Show("Данный номер телефона зарегестрирован");
                        error++;
                    }
                }
                if (error==0)
                {
                    if (phone.Length == 10 || name.Length > 5 || address.Length > 10)
                    {
                        Database.Client client = new Database.Client();
                        client.FullName = name;
                        client.Address = address;
                        client.Phone = phone;
                        connection.Client.Add(client);
                        int  result = connection.SaveChanges();
                        if (result == 1)
                        {
                            name = "";
                            address = "";
                            phone = "";

                            MessageBox.Show("Пользователь успешно добавлен!");
                            textBoxPhone.Clear();
                            textBoxName.Clear();
                            textBoxAddress.Clear();
                            NavigationService.Navigate(MainWindow.pageUserPage);
                        }
                        else
                        {
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

        private void Button_Click_1(object sender, RoutedEventArgs e) //НАЗАД
        {
            NavigationService.GoBack();
        }
    }
}
