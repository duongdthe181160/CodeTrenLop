using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace HttpClientDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Handle "Fetch Data" button click
        private async void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;

            // Validate URL
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a valid URL.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Fetch data using HttpClient
                using (HttpClient client = new HttpClient())
                {
                    ContentTextBox.Text = "Fetching data... Please wait.";
                    string response = await client.GetStringAsync(url); // Fetch HTML content
                    ContentTextBox.Text = response; // Display fetched content
                }
            }
            catch (HttpRequestException ex)
            {
                ContentTextBox.Text = $"Error fetching data: {ex.Message}";
            }
            catch (Exception ex)
            {
                ContentTextBox.Text = $"An unexpected error occurred: {ex.Message}";
            }
        }

        // Handle "Clear" button click
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UrlTextBox.Clear();
            ContentTextBox.Clear();
        }

        // Handle "Close" button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
