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
    /// Логика взаимодействия для userPage.xaml
    /// </summary>
    public partial class userPage : Page
    {
        double Amount = 0;
        //FastFoodEntities connection = new FastFoodEntities();
        danilEntities connection = new danilEntities();
        public userPage()
        {
            InitializeComponent();
            LoadDish();

            listBoxOrder.DisplayMemberPath = "Name";
            listBoxDish.DisplayMemberPath = "Name";
        }
        void LoadDish()
        {
            var dish = connection.Dish.ToList();
            foreach (var _dish in dish)
            {
                listBoxDish.Items.Add(_dish);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) // ДОБАВЛЕНИЕ БЛЮДА В ЗАКАЗ
        {
            labelYourOrder.Content = "Ваш заказ";
            if (listBoxDish.SelectedIndex != -1)
            {
                listBoxOrder.Items.Add(listBoxDish.SelectedItem);
                var dish = listBoxDish.SelectedItem as Dish;
                double price = Convert.ToDouble(dish.Price);
                Amount += price;
                labelAmount.Content = "Стоимость заказа:" + Amount + " руб.\n";
            }
        }
        private void listBoxDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dish = listBoxDish.SelectedItem as Dish;
            labelPrice.Content = "";
            double price = Convert.ToDouble(dish.Price);
            labelPrice.Content = "Цена\n" + price.ToString() + " руб.\n";

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//УДАЛЕНИЕ БЛЮДА ИЗ ЗАКАЗА
        {
            if (listBoxOrder.SelectedIndex != -1)
            {
                var dish = listBoxOrder.SelectedItem as Dish;
                double price = Convert.ToDouble(dish.Price);
                Amount -= price;
                labelAmount.Content = "Стоимость заказа:" + Amount + " руб.\n";
                listBoxOrder.Items.RemoveAt(this.listBoxOrder.SelectedIndex);
                if (listBoxOrder.Items.Count==0)
                {
                    labelAmount.Content = "";
                    labelYourOrder.Content = "";
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //СДЕЛАТЬ ЗАКАЗ 
        {
            Database.danilEntities connection = new Database.danilEntities();
            Database.Order order = new Order();
            var id = connection.Order.ToList().Count() + 1;
            var countDish = connection.OrderCompound.ToList().Count() + 1;
            string nameClient = MainWindow.Name;
            order.ID = id;
            order.Client = nameClient;
            order.Employee = "9921161534";
            order.Date = DateTime.Now;
            order.Status = "В работе";
            connection.Order.Add(order);
            int orderAccess=0;
            if (listBoxOrder.Items.Count <= 0)
            {
                MessageBox.Show("Вы не добавили блюда в корзину");
            }
            else
            {
                foreach (var _dishes in listBoxOrder.Items)
                {
                    Database.OrderCompound orderCompound = new Database.OrderCompound();
                    var dish = _dishes as Database.Dish;
                   
                    try
                    {
                        if (dish != null)
                        {
                            orderCompound.Dish = dish.ID;
                            orderCompound.Order = id;
                            orderCompound.Price = Convert.ToInt32(Amount);
                            orderCompound.Count = 1;
                            orderCompound.Status = "В работе";
                            connection.OrderCompound.Add(orderCompound);
                            orderAccess++;
                           
                        }
                        else
                        {
                            MessageBox.Show("Ошибка в оформление заказа!");
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                int result = connection.SaveChanges();
                if (result > 0)
                {
                    listBoxOrder.Items.Clear();
                    MessageBox.Show("Ваш заказ добавлен в очередь приготовления");
                }
                else
                {
                    MessageBox.Show("Ошибка в оформление заказа!");
                }
                if (orderAccess>0)
                {
                    listBoxOrder.Items.Clear();
                    labelPrice.Content = "";
                    labelAmount.Content = "";
                    labelYourOrder.Content = "";
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //ВЫХОД
        {
            listBoxOrder.Items.Clear();
            labelPrice.Content = "";
            labelAmount.Content = "";
            Amount = 0;
            NavigationService.Navigate(MainWindow.pageLoginPageClient);
        }
    }
}
