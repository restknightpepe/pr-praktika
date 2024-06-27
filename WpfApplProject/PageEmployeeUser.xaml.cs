using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplProject
{
    public partial class PageEmployeeUser : Page
    {
        public partial class Software
        {
            public string Name { get; set; }
        }

        public partial class ErrorLog
        {
            public int Log_Id { get; set; }
            public string Software { get; set; }
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
            public string StackTrace { get; set; }
            public DateTime DateTime { get; set; }
        }

        private static readonly HttpServer _server = new HttpServer("http://localhost:5000/", HandleRequestAsync);
        private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private string connectionString = "Server=LAPTOP-ABALRHT7;Database=Errors;Trusted_Connection=True;";

        public PageEmployeeUser()
        {
            InitializeComponent();
            StartServer();
        }

        private static async Task HandleRequestAsync(string json)
        {
            try
            {
                ErrorLog errorLog = JsonConvert.DeserializeObject<ErrorLog>(json);
                if (errorLog != null)
                {
                    await SaveToDatabaseAsync(errorLog);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling request: " + ex.Message);
            }
        }

        private static async Task SaveToDatabaseAsync(ErrorLog errorLog)
        {
            string connectionString = "Server=LAPTOP-ABALRHT7;Database=Errors;Trusted_Connection=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO ErrorLogs (Log_Id, Software, ErrorType, ErrorMessage, StackTrace, DateTime) VALUES (@Log_Id, @Software, @ErrorType, @ErrorMessage, @StackTrace, @DateTime)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Log_Id", errorLog.Log_Id);
                        cmd.Parameters.AddWithValue("@Software", errorLog.Software);
                        cmd.Parameters.AddWithValue("@ErrorType", errorLog.ErrorType);
                        cmd.Parameters.AddWithValue("@ErrorMessage", errorLog.ErrorMessage);
                        cmd.Parameters.AddWithValue("@StackTrace", errorLog.StackTrace);
                        cmd.Parameters.AddWithValue("@DateTime", errorLog.DateTime);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving to database: " + ex.Message);
            }
        }

        private void StartServer()
        {
            Task.Run(() => _server.StartAsync(_cancellationTokenSource.Token));
        }

        private void StopServer()
        {
            _server.Stop();
            _cancellationTokenSource.Cancel();
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {

            // Пример использования отправки лога ошибки на сервер
            ErrorLog exampleErrorLog = new ErrorLog
            {
                Log_Id = 1,
                Software = "Example Software",
                ErrorType = "Example Type",
                ErrorMessage = "Example Message",
                StackTrace = "Example Stack Trace",
                DateTime = DateTime.Now
            };

            // Отправка примера лога ошибки на сервер
            _ = SendErrorLogAsync(exampleErrorLog);
        }

        private async Task SendErrorLogAsync(ErrorLog errorLog)
        {
            try
            {
                string json = JsonConvert.SerializeObject(errorLog);
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error log successfully sent to the local server.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to send error log. Status Code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }
    }
}
