using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Shell.Models.DataModel;
using Shell.Services.Interfaces;

namespace Shell.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        internal static bool LoginCheck;
        internal static string AuthTitle;
        public AuthViewModel()
        {
            AuthTitle = Title;
            AuthButtonCommand = new Command<object>(AuthButton);
            BrushLogo = Brushes.White;
        }

        public override string Title => "Авторизация";

        #region Convert

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString =
                    Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion


        #region Methods

         public Brush BrushLogo
        {
            get => GetValue<Brush>(BrushLogoProperty);
            set => SetValue(BrushLogoProperty, value);
        }

        public static readonly PropertyData BrushLogoProperty = RegisterProperty("BrushLogo", typeof(Brush));

        public string LoginTextBoxCheck
        {
            get => GetValue<string>(LoginTextBoxCheckProperty);
            set => SetValue(LoginTextBoxCheckProperty, value);
        }

        /// <summary>
        ///     LoginTextBoxCheck property data.
        /// </summary>
        public static readonly PropertyData LoginTextBoxCheckProperty = RegisterProperty("LoginTextBoxCheck",
            typeof(string));

        #endregion

        #region Commands

        public ICommand AuthButtonCommand { get; }

        private void AuthButton(object parameter)
        {           
            var dependencyResolver = this.GetDependencyResolver();
            var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            
            var passwordContainer = parameter as IHavePassword;

            if (passwordContainer != null)
            {
                pleaseWaitService.Show("Подключение к серверу...");
                var secureString = passwordContainer.Password;
                var password = ConvertToUnsecureString(secureString);
                var Model = new Model();
          
                if (Model.Users.FirstOrDefault(x => x.Login == LoginTextBoxCheck &&
                                                    x.Password == password) !=
                    null)
                {
                    pleaseWaitService.Hide();
                    LoginCheck = true;
                    BrushLogo = Brushes.LightGreen;                   
                    return;
                }
                pleaseWaitService.Hide();
                BrushLogo = Brushes.Red;
                LoginCheck = false;
            }
    }

        private bool CanAuthButton()
        {
            if (LoginCheck == false)
                return true;
            return false;
        }

        #endregion

        #region Async

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();


            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }

        #endregion
    }
}