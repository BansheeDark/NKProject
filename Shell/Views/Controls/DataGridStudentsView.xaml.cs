using System.Windows.Controls;
using Shell.ViewModels.Controls;

namespace Shell.Views.Controls
{
    public partial class DataGridStudentsView
    {
        public DataGridStudentsView(DataGridStudentsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

    }
}
