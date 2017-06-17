using System.Collections.ObjectModel;
using Catel.Data;
using Catel.IoC;
using Shell.Models.DataModel;
using Shell.Views.Controls;

namespace Shell.ViewModels.Controls
{
    using Catel.MVVM;


    public class WindowDataGridGroupsViewModel : ViewModelBase
    {
        public WindowDataGridGroupsViewModel(Groups groups = null)
        {
            GroupsCollection = groups ?? new Groups();
            FacultyCollection = DataGridFacultyViewModel.FacultyCollection;
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();
            viewLocator.Register(typeof(WindowDataGridGroupsViewModel), typeof(WindowDataGridGroupsView));
        }

        #region FacultyCollection property

        /// <summary>
        /// Gets or sets the FacultyCollection value.
        /// </summary>
        public ObservableCollection<Faculty> FacultyCollection
        {
            get { return GetValue<ObservableCollection<Faculty>>(FacultyCollectionProperty); }
            set { SetValue(FacultyCollectionProperty, value); }
        }

        /// <summary>
        /// FacultyCollection property data.
        /// </summary>
        public static readonly PropertyData FacultyCollectionProperty = RegisterProperty("FacultyCollection", typeof(ObservableCollection<Faculty>));

        #endregion

        #region GroupsCollection model property

        /// <summary>
        /// Gets or sets the DbGroups value.
        /// </summary>
        [Model]
        public Groups GroupsCollection
        {
            get { return GetValue<Groups>(GroupsCollectionProperty); }
            private set { SetValue(GroupsCollectionProperty, value); }
        }

        /// <summary>
        /// DbGroups property data.
        /// </summary>
        public static readonly PropertyData GroupsCollectionProperty = RegisterProperty("GroupsCollection", typeof(Groups));

        [ViewModelToModel("GroupsCollection")]
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string));


        [ViewModelToModel("GroupsCollection")]
        public int? Courses
        {
            get { return GetValue<int?>(CoursesProperty); }
            set { SetValue(CoursesProperty, value); }
        }


        public static readonly PropertyData CoursesProperty = RegisterProperty("Courses", typeof(int?));


        [ViewModelToModel("GroupsCollection")]
        public string Faculty
        {
            get { return GetValue<string>(FacultyProperty); }
            set { SetValue(FacultyProperty, value); }
        }

        public static readonly PropertyData FacultyProperty = RegisterProperty("Faculty", typeof(string));

        [ViewModelToModel("GroupsCollection")]
        public string Semester
        {
            get { return GetValue<string>(SemesterProperty); }
            set { SetValue(SemesterProperty, value); }
        }

        public static readonly PropertyData SemesterProperty = RegisterProperty("Semester", typeof(string));

        #region Training property

        /// <summary>
        /// Gets or sets the Training value.
        /// </summary>
        [ViewModelToModel("GroupsCollection")]
        public string Training
        {
            get { return GetValue<string>(TrainingProperty); }
            set { SetValue(TrainingProperty, value); }
        }

        /// <summary>
        /// Training property data.
        /// </summary>
        public static readonly PropertyData TrainingProperty = RegisterProperty("Training", typeof(string));

        #endregion

        #endregion
    }
}
