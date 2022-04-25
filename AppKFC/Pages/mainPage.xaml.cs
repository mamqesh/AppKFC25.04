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
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        danilEntities connection = new danilEntities();
        //FastFoodEntities connection = new FastFoodEntities();
        private Order selectedOrder = null;
        public mainPage()
        {
            InitializeComponent();
            ListInProccess.DisplayMemberPath = "Name";
            LoadOrders();
            LoadReadyOrder();
        }
        private void LoadOrders()
        {
            var orders = connection.Order.ToList();
            foreach (var _orders in orders)
            {
                if (_orders.Status == "В работе")
                {
                    ListOrders.Items.Add(_orders); //ДОБАВЛЕНИЕ В РАБОТЕ
                }
            }
        }
        private void LoadReadyOrder()
        {
            var orders = connection.Order.ToList();
            foreach (var _orders in orders)
            {
                if (_orders.Status == "Выполнен")
                {
                    ListReady.Items.Add(_orders); //ДОБАВЛЕНИЕ В ГОТОВО
                }
            }
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            ListInProccess.Items.Clear();
            LabelIngredients.Content = "";
        }
        private void buttonDone_Click(object sender, RoutedEventArgs e) //ВЗЯТЬ В РАБОТУ
        {
            if (ListOrders.SelectedIndex == -1)
            {
                return;
            }
            selectedOrder = ListOrders.SelectedItem as Database.Order;
            var compounds = selectedOrder.OrderCompound.ToList();
            ListInProccess.Items.Clear();
            LabelIngredients.Content = "";
            foreach (var _compound in compounds)
            {
                ListInProccess.Items.Add(_compound.Dish1);
            }
        }
        private void ListInProccess_SelectionChanged(object sender, SelectionChangedEventArgs e) //КЛИК НА БЛЮДО
        {
            var dish = ListInProccess.SelectedItem as Database.Dish;
            if (dish != null)
            {
                var compound = dish.DishCompound.ToList();
                LabelIngredients.Content = "";
                foreach (var _compound in compound)
                {
                    LabelIngredients.Content += _compound.Ingredient1.Name + "\n";
                }
            }
        }
        private void buttonReady_Click(object sender, RoutedEventArgs e) //ГОТОВО
        {
            if (ListInProccess.SelectedIndex != -1)
            {
                LabelIngredients.Content = "";
                ListInProccess.Items.RemoveAt(this.ListInProccess.SelectedIndex);
                if (ListInProccess.Items.Count==0)
                {
                    ListReady.Items.Add(ListOrders.SelectedItem); // ДОБАВЛЕНИЕ ВЫПОЛНЕННОГО
                    ListOrders.Items.RemoveAt(ListOrders.SelectedIndex);
                    var order = connection.Order.Where(x => x.ID == selectedOrder.ID).FirstOrDefault();
                    if (order != null)
                    {
                        order.Status = "Выполнен";
                        connection.SaveChanges();
                    }
                }   
            }
            else
            {
                MessageBox.Show("Выберите приготовленное блюдо");
            }
        }

        private void Button_ClickAddRandom(object sender, RoutedEventArgs e) // ДОБАВЛЕНИЕ ЗАКАЗА ИЗ СЛУЧАЙНЫХ БЛЮД
        {
            List<Database.Dish> dishes = new List<Database.Dish>();
            var list = connection.Dish.ToList();
            foreach (var dish in list)
            {
                if (connection.DishCompound.Where(d => d.Dish == dish.ID).Count() > 0)
                {
                    dishes.Add(dish);
                }
            }
            Random random = new Random();
            Dish randomDish = dishes[random.Next(dishes.Count)];
            Order order = new Order();
            order.ID = connection.Order.Count() + 1;
            order.Status = "В работе";
            order.Date = DateTime.Now;
            order.Employee = "9295479015";
            order.Client = "9639090322";

            OrderCompound compound = new OrderCompound();
            compound.Order = order.ID;
            compound.Dish = randomDish.ID;
            compound.Price = randomDish.Price;
            compound.Status = "В работе";
            compound.Count = 1;
            connection.Order.Add(order);
            connection.OrderCompound.Add(compound);
            connection.SaveChanges();
            ListOrders.Items.Clear();
            ListReady.Items.Clear();
            LoadOrders();
            LoadReadyOrder();

        }
        private void ListReady_SelectionChanged(object sender, SelectionChangedEventArgs e)//ПОДТВЕРЖДЕНИЕ К ВЫДАЧЕ
        {


        }
        private void ListOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}
