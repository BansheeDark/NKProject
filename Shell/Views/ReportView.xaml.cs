using System;
using System.Windows;

namespace Shell.Views
{
    public partial class ReportView
    {
        private bool _isReportViewerLoaded;
        private Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1;
        private data_stDataSetTableAdapters.StudentsTableAdapter studentsTableAdapter;
        private data_stDataSet dataset;

        public ReportView()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;

        }

        private void ReportViewer_Load(object sender, System.EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                dataset = new data_stDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.Students;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "Shell.Reports.MyReport.rdlc";

                dataset.EndInit();

                //fill data into adventureWorksDataSet
                studentsTableAdapter = new data_stDataSetTableAdapters.StudentsTableAdapter();
                studentsTableAdapter.ClearBeforeFill = true;
                studentsTableAdapter.Fill(dataset.Students);

                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }

        private void TextBoxRf_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (ComboBoxRf.Text == "Фамилии")
                {
                    if (TextBoxRf.Text != "")
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.FillBy(dataset.Students, TextBoxRf.Text);
                        _reportViewer.RefreshReport();
                    }
                    else
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.Fill(dataset.Students);
                        _reportViewer.RefreshReport();
                    }
                }
                if (ComboBoxRf.Text == "Возрасту")
                {

                    if (TextBoxRf.Text != "")
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.FillByAge(dataset.Students, Convert.ToInt32(TextBoxRf.Text));
                        _reportViewer.RefreshReport();
                    }
                    else
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.Fill(dataset.Students);
                        _reportViewer.RefreshReport();
                    }
                }
                if (ComboBoxRf.Text == "Группе")
                {
                    if (TextBoxRf.Text != "")
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.FillByGroups(dataset.Students, TextBoxRf.Text);
                        _reportViewer.RefreshReport();
                    }
                    else if (TextBoxRf.Text == "")
                    {
                        studentsTableAdapter.ClearBeforeFill = true;
                        studentsTableAdapter.Fill(dataset.Students);
                        _reportViewer.RefreshReport();
                    }
                }
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
