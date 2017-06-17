using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Threading.Tasks;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Shell.Models.DataModel;
using Shell.Views.Controls;

namespace Shell.ViewModels.Controls
{
    public class DataGridFacultyViewModel : ViewModelBase
    {
        private readonly Model _db;
        private IDependencyResolver _dependencyResolver;
        private IUIVisualizerService _uiVisualizerService;

        public DataGridFacultyViewModel()
        {
            _db = HomeViewModel._hmdb;
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();
            viewLocator.Register(typeof(DataGridFacultyViewModel), typeof(DataGridFacultyView));
        }

        internal static ObservableCollection<Faculty> FacultyCollection { get; set; }

        #region Property

        #region DbFaculty property

        /// <summary>
        ///     Gets or sets the DbFaculty value.
        /// </summary>
        public ObservableCollection<Faculty> DbFaculty
        {
            get { return GetValue<ObservableCollection<Faculty>>(DbFacultyProperty); }
            set { SetValue(DbFacultyProperty, value); }
        }

        /// <summary>
        ///     DbFaculty property data.
        /// </summary>
        public static readonly PropertyData DbFacultyProperty = RegisterProperty("DbFaculty",
            typeof(ObservableCollection<Faculty>));

        #endregion

        #region SelectionFaculty property

        /// <summary>
        ///     Gets or sets the SelectionFaculty value.
        /// </summary>
        public Faculty SelectionFaculty
        {
            get { return GetValue<Faculty>(SelectionFacultyProperty); }
            set { SetValue(SelectionFacultyProperty, value); }
        }

        /// <summary>
        ///     SelectionFaculty property data.
        /// </summary>
        public static readonly PropertyData SelectionFacultyProperty = RegisterProperty("SelectionFaculty",
            typeof(Faculty));

        #endregion

        #endregion

        #region Commands

        #region DbLoaded command

        private TaskCommand _dbLoadedCommand;

        /// <summary>
        ///     Gets the DbLoaded command.
        /// </summary>
        public TaskCommand DbLoadedCommand
        {
            get { return _dbLoadedCommand ?? (_dbLoadedCommand = new TaskCommand(DbLoaded, CanDbLoaded)); }
        }

        /// <summary>
        ///     Method to invoke when the DbLoaded command is executed.
        /// </summary>
        public Task DbLoaded()
        {
            return Task.Factory.StartNew(() =>
            {
                var dependencyResolver = this.GetDependencyResolver();
                var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();

                if (AuthViewModel._LoginCheck)
                {
                    pleaseWaitService.Show("Загружаем таблицы с сервера...");
                    if (FacultyCollection == null)
                    {
                        _db.Faculty.Load();
                        DbFaculty = new ObservableCollection<Faculty>(_db.Faculty);
                        FacultyCollection = DbFaculty;
                    }
                    else
                    {
                        DbFaculty = FacultyCollection;
                    }
                    pleaseWaitService.Hide();
                }
            });
        }

        private bool CanDbLoaded()
        {
            if (DbFaculty == null)
                return true;
            return false;
        }

        #endregion

        #region DbSync command

        private TaskCommand _dbSyncCommand;

        /// <summary>
        ///     Gets the DbSync command.
        /// </summary>
        public TaskCommand DbSyncCommand
        {
            get { return _dbSyncCommand ?? (_dbSyncCommand = new TaskCommand(DbSync, CanDbSync)); }
        }

        /// <summary>
        ///     Method to invoke when the DbSync command is executed.
        /// </summary>
        private Task DbSync()
        {
            return Task.Factory.StartNew(() =>
            {
                var dependencyResolver = this.GetDependencyResolver();
                var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
                pleaseWaitService.Show("Синхронизация данных...");
                if (FacultyCollection == null)
                {
                    _db.Faculty.LoadAsync();
                    DbFaculty = new ObservableCollection<Faculty>(_db.Faculty);
                    FacultyCollection = DbFaculty;
                }
                else
                {
                    DbFaculty = FacultyCollection;
                }
                pleaseWaitService.Hide();
            });
        }

        private bool CanDbSync()
        {
            if (DbFaculty != null)
                return true;
            return false;
        }

        #endregion

        #region DbSave command

        private Command _dbSaveCommand;

        /// <summary>
        ///     Gets the DBUpdate command.
        /// </summary>
        public Command DbSaveCommand
        {
            get { return _dbSaveCommand ?? (_dbSaveCommand = new Command(DbSave, CanDbSave)); }
        }

        /// <summary>
        ///     Method to invoke when the DBUpdate command is executed.
        /// </summary>
        private void DbSave()
        {
            var dependencyResolver = this.GetDependencyResolver();
            var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            pleaseWaitService.Show("Сохранение данных...");
            _db.SaveChanges();
            pleaseWaitService.Hide();
        }

        private bool CanDbSave()
        {
            if (DbFaculty != null)
                return true;
            return false;
        }

        #endregion

        #region DbAdd command

        private Command _dbAddCommand;

        /// <summary>
        ///     Gets the DbAdd command.
        /// </summary>
        public Command DbAddCommand
        {
            get { return _dbAddCommand ?? (_dbAddCommand = new Command(DbAdd, CanDbAddt)); }
        }

        /// <summary>
        ///     Method to invoke when the DbAdd command is executed.
        /// </summary>
        private void DbAdd()
        {
            var viewModel = new WindowDataGridFacultyViewModel();
            _dependencyResolver = this.GetDependencyResolver();
            var pleaseWaitService = _dependencyResolver.Resolve<IPleaseWaitService>();
            _uiVisualizerService = _dependencyResolver.Resolve<IUIVisualizerService>();
            _uiVisualizerService.ShowDialog(viewModel, (sender, e) =>
            {
                if (e.Result ?? false)
                {
                    pleaseWaitService.Show("Сохранение данных...");
                    _db.Faculty.Add(viewModel.FacultyCollection);
                    DbFaculty.Add(viewModel.FacultyCollection);
                    _db.SaveChanges();
                    pleaseWaitService.Hide();
                }
            });
        }

        private bool CanDbAddt()
        {
            if (DbFaculty != null)
                return true;
            return false;
        }

        #endregion

        #region DbEdit command

        private Command _dbEditCommand;

        /// <summary>
        ///     Gets the DbAdd command.
        /// </summary>
        public Command DbEditCommand
        {
            get { return _dbEditCommand ?? (_dbEditCommand = new Command(DbEdit, CanDbEdit)); }
        }

        /// <summary>
        ///     Method to invoke when the DbAdd command is executed.
        /// </summary>
        private void DbEdit()
        {
            var viewModel = new WindowDataGridFacultyViewModel(SelectionFaculty);

            _dependencyResolver = this.GetDependencyResolver();
            _uiVisualizerService = _dependencyResolver.Resolve<IUIVisualizerService>();
            _uiVisualizerService.Show(viewModel);
            _db.SaveChanges();
        }

        private bool CanDbEdit()
        {
            if (SelectionFaculty != null && DbFaculty != null)
                return true;
            return false;
        }

        #endregion

        #region DBDelete command

        private Command _dbDeleteCommand;

        /// <summary>
        ///     Gets the DBDelete command.
        /// </summary>
        public Command DbDeleteCommand
        {
            get { return _dbDeleteCommand ?? (_dbDeleteCommand = new Command(DbDelete, CanDbDelete)); }
        }

        /// <summary>
        ///     Method to invoke when the DBDelete command is executed.
        /// </summary>
        private void DbDelete()
        {
            var dependencyResolver = this.GetDependencyResolver();
            var pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            pleaseWaitService.Show("Удаление данных...");
            _db.Faculty.Remove(SelectionFaculty);
            DbFaculty.Remove(SelectionFaculty);
            _db.SaveChanges();
            pleaseWaitService.Hide();
        }

        private bool CanDbDelete()
        {
            if (SelectionFaculty != null && DbFaculty != null)
                return true;
            return false;
        }

        #endregion

        #endregion
    }
}