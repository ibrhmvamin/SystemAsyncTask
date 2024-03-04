using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace task
{
    public partial class EditWindow : Window
    {
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Comment _selectedComment;

        public Comment SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                OnPropertyChanged();
            }
        }

        public EditWindow(Comment selectedComment)
        {
            InitializeComponent();
            SelectedComment = selectedComment;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            _selectedComment.name = NameTextBox.Text;
            _selectedComment.email = EmailTextBox.Text;
            _selectedComment.body = BodyTextBox.Text;
            Close();
        }
    }
}
