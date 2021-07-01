using DewanSoftTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DewanSoftTask
{
    /// <summary>
    /// Interaction logic for InvoiceDetails.xaml
    /// </summary>
    public partial class InvoiceDetails : Window
    {
        private MainWindow mainWindow;

        public InvoiceDetails()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCustomer.Text = mainWindow.tbCustomerName.Text;
            txtBranch.Text = mainWindow.tbBranchName.Text;
            txtDate.Text = mainWindow.dpDate.SelectedDate.Value.Date.ToShortDateString();
            txtTotal.Text = mainWindow.TotalPrice;
            dgItems.ItemsSource = mainWindow.dgItems.ItemsSource;
        }
    }
}
