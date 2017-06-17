using System.Linq.Expressions;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Shell.Models.DataModel;
using Shell.Views.Controls;

namespace Shell.ViewModels.Controls
{
    public class WindowDataGridFacultyViewModel : ViewModelBase
    {
        public WindowDataGridFacultyViewModel(Faculty faculty = null)
        {
            FacultyCollection = faculty ?? new Faculty();
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();
            viewLocator.Register(typeof(WindowDataGridFacultyViewModel), typeof(WindowDataGridFacultyView));
        }

        #region FacultyCollection

        [Model]
        public Faculty FacultyCollection
        {
            get { return GetValue<Faculty>(FacultyCollectionProperty); }
            private set { SetValue(FacultyCollectionProperty, value); }
        }

        public static readonly PropertyData FacultyCollectionProperty = RegisterProperty("FacultyCollection", typeof(Faculty));

        [ViewModelToModel("FacultyCollection")]
        public string University
        {
            get { return GetValue<string>(UniversityProperty); }
            set { SetValue(UniversityProperty, value); }
        }


        public static readonly PropertyData UniversityProperty = RegisterProperty("University", typeof(string), null);

        #region Faculty property

        /// <summary>
        /// Gets or sets the Faculty value.
        /// </summary>
        [ViewModelToModel("FacultyCollection")]
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// Faculty property data.
        /// </summary>
        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string), null);

        #endregion

        #endregion
    }
}