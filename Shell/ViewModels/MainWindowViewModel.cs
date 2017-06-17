using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;

namespace Shell.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private readonly List<IViewModel> _view;

        #endregion

        public MainWindowViewModel()
        {
            _view = new List<IViewModel>
            {
                new AuthViewModel(),
                new HomeViewModel(),
                new AboutViewModel(),
                new ReportViewModel()
            };
            TitlePage = AuthViewModel.AuthTitle;
            CurrentPage = _view.First();
        }

        #region Property

        public IViewModel CurrentPage
        {
            get { return GetValue<IViewModel>(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly PropertyData CurrentPageProperty = RegisterProperty("CurrentPage", typeof(IViewModel));

        public string TitlePage
        {
            get { return GetValue<string>(TitlePageProperty); }
            set { SetValue(TitlePageProperty, value); }
        }

        public static readonly PropertyData TitlePageProperty = RegisterProperty("TitlePage", typeof(string));

        #endregion

        #region Commands    

        #region HomeButton command

        private TaskCommand _homeButtonCommand;

        /// <summary>
        ///     Gets the HomeButton command.
        /// </summary>
        public TaskCommand HomeButtonCommand
        {
            get { return _homeButtonCommand ?? (_homeButtonCommand = new TaskCommand(HomeButton, CanHomeButton)); }
        }

        /// <summary>
        ///     Method to invoke when the HomeButton command is executed.
        /// </summary>
        private Task HomeButton()
        {
            return Task.Run(() =>
            {
                CurrentPage = _view[1];
                TitlePage = HomeViewModel.HomeTitle;
            });
        }

        /// <summary>
        ///     Method to check whether the HomeButton command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool CanHomeButton()
        {
            if (AuthViewModel._LoginCheck == false)
                return false;
            return true;
        }

        #endregion

        #region AuthButton command

        private TaskCommand _authButtonCommand;

        /// <summary>
        ///     Gets the AuthButton command.
        /// </summary>
        public TaskCommand AuthButtonCommand
        {
            get { return _authButtonCommand ?? (_authButtonCommand = new TaskCommand(AuthButton)); }
        }

        /// <summary>
        ///     Method to invoke when the AuthButton command is executed.
        /// </summary>
        private Task AuthButton()
        {
            return Task.Run(() =>
            {
                CurrentPage = _view[0];
                TitlePage = AuthViewModel.AuthTitle;
            });
        }

        #endregion

        #region ReportButton command

        private TaskCommand _reportButtonCommand;

        /// <summary>
        ///     Gets the ReportButton command.
        /// </summary>
        public TaskCommand ReportButtonCommand
        {
            get
            {
                return _reportButtonCommand ?? (_reportButtonCommand = new TaskCommand(ReportButton, CanReportButton));
            }
        }

        /// <summary>
        ///     Method to invoke when the ReportButton command is executed.
        /// </summary>
        private Task ReportButton()
        {
            return Task.Run(() =>
            {
                var dependencyResolver = this.GetDependencyResolver();
                var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
                pleaseWaitService.Show("Загрузка...");
                CurrentPage = _view[3];
                TitlePage = ReportViewModel.ReportTitle;
                Thread.Sleep(2000);
                pleaseWaitService.Hide();
            });
        }

        /// <summary>
        ///     Method to check whether the ReportButton command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool CanReportButton()
        {
            if (AuthViewModel._LoginCheck == false)
                return false;
            return true;
        }

        #endregion

        #region AboutButton command

        private TaskCommand _aboutButtonCommand;

        /// <summary>
        ///     Gets the AboutButton command.
        /// </summary>
        public TaskCommand AboutButtonCommand
        {
            get { return _aboutButtonCommand ?? (_aboutButtonCommand = new TaskCommand(AboutButton, CanAboutButton)); }
        }

        /// <summary>
        ///     Method to invoke when the AboutButton command is executed.
        /// </summary>
        private Task AboutButton()
        {
            return Task.Run(() =>
            {
                CurrentPage = _view[2];
                TitlePage = AboutViewModel.AboutTitle;
            });
            // TODO: Handle command logic here
        }

        /// <summary>
        ///     Method to check whether the AboutButton command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool CanAboutButton()
        {
            if (AuthViewModel._LoginCheck == false)
                return false;
            return true;
        }

        #endregion

        #endregion

        #region Async

        public override string Title
        {
            get { return "Shell"; }
        }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets
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