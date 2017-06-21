using System.Security;
using Shell.Services.Interfaces;
using Shell.ViewModels;

namespace Shell.Views
{
    public partial class AuthView : IHavePassword
    {
        public AuthView()
        {
            InitializeComponent();
            DataContext = new AuthViewModel();
        }

        public SecureString Password => PasswordBox.SecurePassword;
    }
}