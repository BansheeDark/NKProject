using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Shell.Views
{
    public partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://vk.com/bansheedark");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/BansheeDark/NKProject.git");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
