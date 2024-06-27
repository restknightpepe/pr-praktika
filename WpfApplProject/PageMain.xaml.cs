using System;
using System.Linq;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplProject
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
            labelName.Visibility = Visibility.Hidden;
            labelPass1.Visibility = Visibility.Hidden;
        }
        
        public class DataTransfer
        {
            public static int UserID { get; set; }
            public static string Username { get; set; }
            public static string Password { get; set; }
            public static string Role { get; set; }

        }
        private void BtEntry(object sender, RoutedEventArgs e)
        {
            string passw = (inputPass1.Password);
            labelName.Content = "";
            labelPass1.Content = "";

            if (inputName.Text == "")
            {
                labelName.Content = "Введите Логин";
                labelName.Visibility = Visibility.Visible;
            }
            if ((inputPass1.Password) == "")
            {
                labelPass1.Content = "Введите Пароль";
                labelPass1.Visibility = Visibility.Visible;
            }

            User db = new User();
            db = ErrorsEntities.GetContext().Users.Where(b => b.Username == inputName.Text
                                                          && b.Password == passw).FirstOrDefault();

            if (db == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            DataTransfer.Username = db.Username;
            DataTransfer.Password = db.Password;
            DataTransfer.Role = db.Role;


            if (DataTransfer.Role == "admin")
            {
                NavigationService.Navigate(new PageEmployee());
            }
            else
            {
                NavigationService.Navigate(new PageEmployeeUser());
            }
        }

        private void BtExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
