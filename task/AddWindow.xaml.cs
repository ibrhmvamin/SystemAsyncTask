using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace task
{
    
    public partial class AddWindow : Window,INotifyPropertyChanged
    {
        private ObservableCollection<Comment> _comments;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Comment> comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }
        public AddWindow(ObservableCollection<Comment> comments)
        {
            InitializeComponent();
            this.comments = comments;
            DataContext = this;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Comment newComment = new Comment
            {
                postId = Random.Shared.Next(1,500), 
                id = comments.Count + 1, 
                name = NameTextBox.Text,
                email = EmailTextBox.Text,
                body = BodyTextBox.Text
            };

            comments.Add(newComment);

            Close();
        }
    }
}
