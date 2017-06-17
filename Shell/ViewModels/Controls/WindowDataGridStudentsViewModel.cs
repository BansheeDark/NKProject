using System;
using System.Collections.ObjectModel;
using Catel.Data;
using Catel.IoC;
using Shell.Models.DataModel;
using Shell.Views.Controls;

namespace Shell.ViewModels.Controls
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class WindowDataGridStudentsViewModel : ViewModelBase
    {
        public WindowDataGridStudentsViewModel(Students students = null)
        {
            StudentsCollection = students ?? new Students();
            GroupCollection = DataGridGroupsViewModel.GroupsCollection;
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();
            viewLocator.Register(typeof(WindowDataGridStudentsViewModel), typeof(WindowDataGridStudentsView));
        }


        #region GroupCollection property

        /// <summary>
        ///     Gets or sets the GroupCollection value.
        /// </summary>
        public ObservableCollection<Groups> GroupCollection
        {
            get { return GetValue<ObservableCollection<Groups>>(GroupCollectionProperty); }
            set { SetValue(GroupCollectionProperty, value); }
        }

        /// <summary>
        ///     GroupCollection property data.
        /// </summary>
        public static readonly PropertyData GroupCollectionProperty = RegisterProperty("GroupCollection",
            typeof(ObservableCollection<Groups>));

        #endregion

        #region Students

        [Model]
        public Students StudentsCollection
        {
            get { return GetValue<Students>(AddDbStudentsProperty); }
            private set { SetValue(AddDbStudentsProperty, value); }
        }

        public static readonly PropertyData AddDbStudentsProperty = RegisterProperty("StudentsCollection",
            typeof(Students));

        [ViewModelToModel("StudentsCollection", "CodeGroups")]
        public string CodeGroups
        {
            get { return GetValue<string>(CodeGroupsProperty); }
            set { SetValue(CodeGroupsProperty, value); }
        }


        public static readonly PropertyData CodeGroupsProperty = RegisterProperty("CodeGroups", typeof(string));


        [ViewModelToModel("StudentsCollection", "FirstName")]
        public string FirstName
        {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly PropertyData FirstNameProperty = RegisterProperty("FirstName", typeof(string));

        [ViewModelToModel("StudentsCollection", "LastName")]
        public string LastName
        {
            get { return GetValue<string>(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly PropertyData LastNameProperty = RegisterProperty("LastName", typeof(string));

        [ViewModelToModel("StudentsCollection")]
        public string OtherName
        {
            get { return GetValue<string>(OtherNameProperty); }
            set { SetValue(OtherNameProperty, value); }
        }

        public static readonly PropertyData OtherNameProperty = RegisterProperty("OtherName", typeof(string));

        [ViewModelToModel("StudentsCollection")]
        public string Gender
        {
            get { return GetValue<string>(GenderProperty); }
            set { SetValue(GenderProperty, value); }
        }

        public static readonly PropertyData GenderProperty = RegisterProperty("Gender", typeof(string));

        [ViewModelToModel("StudentsCollection")]
        public DateTime? DateOfBirthday
        {
            get { return GetValue<DateTime?>(DateOfBirthdayProperty); }
            set { SetValue(DateOfBirthdayProperty, value); }
        }

        public static readonly PropertyData DateOfBirthdayProperty = RegisterProperty("DateOfBirthday",
            typeof(DateTime?));


        [ViewModelToModel("StudentsCollection")]
        public string PlaceofBirth
        {
            get { return GetValue<string>(PlaceofBirthProperty); }
            set { SetValue(PlaceofBirthProperty, value); }
        }

        public static readonly PropertyData PlaceofBirthProperty = RegisterProperty("PlaceofBirth", typeof(string));

        #endregion
    }
}
