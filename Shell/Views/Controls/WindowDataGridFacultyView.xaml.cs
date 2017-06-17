using Shell.ViewModels.Controls;

namespace Shell.Views.Controls
{
    public partial class WindowDataGridFacultyView
    {
        public WindowDataGridFacultyView()
            : this(null)
        {
        }

        public WindowDataGridFacultyView(WindowDataGridFacultyViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}