using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace task
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Comment selectecComment;
        public Comment SelectedComment
        {
            get => selectecComment;
            set
            {
                selectecComment=value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Comment> _comments;


        public ObservableCollection<Comment> comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            comments = new ObservableCollection<Comment>();
            DataContext = this;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;
            var newComments = await FetchCommentsAsync(url);
            comments.Clear();
            foreach (var comment in newComments)
            {
                comments.Add(comment);
            }

            DataGrid.ItemsSource = comments;
            DelayAsync();
        }

        private async Task<Comment[]> FetchCommentsAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Comment[]>(content);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var aw = new AddWindow(comments);
            aw.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                var ew = new EditWindow(SelectedComment);
                ew.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a comment to edit.");
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                comments.Remove(SelectedComment);
            }
            else
            {
                MessageBox.Show("Wrong");
            }
        }
        private async Task DelayAsync()
        {
            await Task.Delay(1000);
        }
    }
}