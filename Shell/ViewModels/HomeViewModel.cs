using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Shell.Models;
using Shell.Models.DataModel;
using Shell.ViewModels.Controls;

namespace Shell.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        internal static Model _hmdb;
        public static string HomeTitle;
        public static ObservableCollection<Groups> DbGroupsCollection;
        private readonly List<IViewModel> _view;

        public HomeViewModel()
        {
            HomeTitle = Title;
            _hmdb = new Model();
            _view = new List<IViewModel>
            {
                new DataGridGroupsViewModel(),
                new DataGridStudentsViewModel(),
                new DataGridFacultyViewModel()
            };
            LoadDataGridStudents = _view[1];
            LoadDataGridGroups = _view[0];
            LoadDataGridFaculty = _view[2];
        }

        public override string Title
        {
            get { return "База данных"; }
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

        #region Property

        #region LoadDataGridStudents property

        /// <summary>
        ///     Gets or sets the LoadDataGridStudents value.
        /// </summary>
        public IViewModel LoadDataGridStudents
        {
            get { return GetValue<IViewModel>(LoadDataGridStudentsProperty); }
            set { SetValue(LoadDataGridStudentsProperty, value); }
        }

        /// <summary>
        ///     LoadDataGridStudents property data.
        /// </summary>
        public static readonly PropertyData LoadDataGridStudentsProperty = RegisterProperty("LoadDataGridStudents",
            typeof(IViewModel));

        #endregion

        #region LoadDataGridGroups property

        /// <summary>
        ///     Gets or sets the LoadDataGridStudents value.
        /// </summary>
        public IViewModel LoadDataGridGroups
        {
            get { return GetValue<IViewModel>(LoadDataGridGroupsProperty); }
            set { SetValue(LoadDataGridGroupsProperty, value); }
        }

        /// <summary>
        ///     LoadDataGridStudents property data.
        /// </summary>
        public static readonly PropertyData LoadDataGridGroupsProperty = RegisterProperty("LoadDataGridGroups",
            typeof(IViewModel));

        #endregion

        #region LoadDataGridGroups property

        /// <summary>
        ///     Gets or sets the LoadDataGridStudents value.
        /// </summary>
        public IViewModel LoadDataGridFaculty
        {
            get { return GetValue<IViewModel>(LoadDataGridFacultyProperty); }
            set { SetValue(LoadDataGridFacultyProperty, value); }
        }

        /// <summary>
        ///     LoadDataGridStudents property data.
        /// </summary>
        public static readonly PropertyData LoadDataGridFacultyProperty = RegisterProperty("LoadDataGridFaculty",
            typeof(IViewModel));

        #endregion

        #endregion
    }
}