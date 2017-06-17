using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Shell.Models.DataModel;

namespace Shell.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        internal static bool _LoginCheck;
        internal static string AuthTitle;
        private Command _authButtonCommand;


        public AuthViewModel()
        {
            AuthTitle = Title;
            BrushLogo = Brushes.White;
        }

        public override string Title
        {
            get { return "Авторизация"; }
        }

        #region Methods

        public Brush BrushLogo
        {
            get { return GetValue<Brush>(BrushLogoProperty); }
            set { SetValue(BrushLogoProperty, value); }
        }

        public static readonly PropertyData BrushLogoProperty = RegisterProperty("BrushLogo", typeof(Brush));

        public string PasswordPasswordBoxCheck
        {
            get { return GetValue<string>(PasswordPasswordBoxCheckProperty); }
            set { SetValue(PasswordPasswordBoxCheckProperty, value); }
        }

        public static readonly PropertyData PasswordPasswordBoxCheckProperty =
            RegisterProperty("PasswordPasswordBoxCheck", typeof(string));

        public string LoginTextBoxCheck
        {
            get { return GetValue<string>(LoginTextBoxCheckProperty); }
            set { SetValue(LoginTextBoxCheckProperty, value); }
        }

        /// <summary>
        ///     LoginTextBoxCheck property data.
        /// </summary>
        public static readonly PropertyData LoginTextBoxCheckProperty = RegisterProperty("LoginTextBoxCheck",
            typeof(string));

        #endregion

        #region Commands

        public Command AuthButtonCommand
        {
            get
            {
                return _authButtonCommand ??
                       (_authButtonCommand = new Command(AuthButton, CanAuthButton));
            }
        }

        private void AuthButton()
        {
            var dependencyResolver = this.GetDependencyResolver();
            var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            pleaseWaitService.Show("Подключение к серверу...");
            var c = new Model();

            if (c.Users.FirstOrDefault(x => x.Login == LoginTextBoxCheck && x.Password == PasswordPasswordBoxCheck) !=
                null)
            {
                pleaseWaitService.Hide();
                _LoginCheck = true;
                BrushLogo = Brushes.LightGreen;
                return;
            }
            pleaseWaitService.Hide();
            BrushLogo = Brushes.Red;
            _LoginCheck = false;
        }

        private bool CanAuthButton()
        {
            if (_LoginCheck == false)
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