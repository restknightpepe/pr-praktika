using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace WpfApplProject
{
    public partial class PageEmployee : Page
    {
        private readonly ErrorsEntities _context;
        private List<ErrorLog> items = null;
        private bool isDirty = true;
        private string action = "";

        public PageEmployee()
        {
            InitializeComponent();
            _context = ErrorsEntities.GetContext();
            DataGridErrors.ItemsSource = _context.ErrorLogs.ToList();
            FillComboBoxWithCategories();
            StartHttpServer(); // Запускаем HTTP сервер
        }

        private void FillComboBoxWithCategories()
        {
            var categories = _context.ErrorLogs
                .Select(u => u.ErrorType)
                .Distinct()
                .ToList();

            Combo.ItemsSource = categories;
            Combo.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxWithCategories();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _context.SaveChanges();
            DataGridErrors.ItemsSource = _context.ErrorLogs.ToList();
            DataGridErrors.Items.Refresh();
            DataGridErrors.IsReadOnly = true;
            action = "";
            isDirty = true;
            MessageBox.Show("Сохранено");
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ErrorLog selectedErrorLog = DataGridErrors.SelectedItem as ErrorLog;
            if (selectedErrorLog != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    _context.ErrorLogs.Remove(selectedErrorLog);
                    _context.SaveChanges();
                    DataGridErrors.ItemsSource = _context.ErrorLogs.ToList();
                    DataGridErrors.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
            action = "";
            isDirty = true;
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = Combo.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var filteredItems = _context.ErrorLogs
                    .Where(log => log.ErrorType == selectedCategory)
                    .ToList();
                DataGridErrors.ItemsSource = filteredItems;
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Handle ComboBox selection change if needed
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Optional: Handle MenuItem click if needed
        }

        private void DataGridErrors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Handle DataGrid selection change if needed
        }

        private void TextBoxNazvanie_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: Handle TextBox text change if needed
        }

        // Запуск HTTP сервера
        private void StartHttpServer()
        {
            Task.Run(() =>
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:8080/api/");
                listener.Start();
                while (true)
                {
                    var context = listener.GetContext();
                    if (context.Request.HttpMethod == "POST")
                    {
                        HandlePostRequest(context);
                    }
                }
            });
        }

        // Обработка POST запроса
        private void HandlePostRequest(HttpListenerContext context)
        {
            using (var reader = new System.IO.StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var json = reader.ReadToEnd();
                var errorLog = JsonConvert.DeserializeObject<ErrorLog>(json);
                if (errorLog != null)
                {
                    _context.ErrorLogs.Add(errorLog);
                    try
                    {
                        _context.SaveChanges();
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        using (var writer = new System.IO.StreamWriter(context.Response.OutputStream))
                        {
                            writer.Write(ex.Message);
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                context.Response.Close();
            }
        }
    }

    // Класс для модели ErrorLog (Пример, измените в зависимости от вашей модели)
    public partial class ErrorLog
    {
        public int Log_Id { get; set; }
        public Software Software { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateTime { get; set; }
    }

    // Класс для модели Software (Пример, измените в зависимости от вашей модели)
    public partial class Software
    {
        public string Name { get; set; }
    }

    // Контекст базы данных (Пример, измените в зависимости от вашей реализации)
    public partial class ErrorsEntities : DbContext
    {
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public static ErrorsEntities GetContext()
        {
            return new ErrorsEntities();
        }
    }
}
