using DewanSoftTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DewanSoftTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ObservableCollection<Item> Items;
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        int Total;
        // string CustomerName, BranchName, Date;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }



        private string _totalPrice;

        public string TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (string.Equals(value, _totalPrice))
                    return;
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var itemName = tbItemName.Text;
            var itemQuantity = tbQuantity.Text;
            var itemPrice = tbPrice.Text;

            bool valid = !string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(itemQuantity) && !string.IsNullOrEmpty(itemPrice);

            if (valid)
            {
                Item item = new()
                {
                    Name = itemName,
                    Price = int.Parse(itemPrice),
                    Quantity = int.Parse(itemQuantity)
                };
                item.Total = item.Price * item.Quantity;
                Items.Add(item);
                tbItemName.Text = tbQuantity.Text = tbPrice.Text = "";
                Total += item.Total;
                TotalPrice = Total + "";

            }
            else
            {
                MessageBox.Show("Complete Missing Data\n'Item Name', 'Quantity', Or 'Price'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void tbQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Items = new ObservableCollection<Item>();
            dgItems.ItemsSource = Items;
            TotalPrice = Total + "";
            dpDate.DisplayDateStart = DateTime.Now;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var customerName = tbCustomerName.Text;
            var branchName = tbBranchName.Text;
            var date = dpDate.SelectedDate;

            bool valid = !string.IsNullOrEmpty(customerName) && !string.IsNullOrEmpty(branchName) && !string.IsNullOrEmpty(date.ToString()) && Items.Count > 0;
            if (valid)
            {
                var invoiceDate = dpDate.SelectedDate.Value.Date.ToShortDateString();
                //List<Item> InvoiceItems = new List<Item>();
                //foreach(var i in Items)
                //{

                //}
                MessageBox.Show($"{customerName} {branchName} {invoiceDate}\n {valid}");
            }
            else
            {
                MessageBox.Show("Complete Missing Data\n'Customer Name', 'Branch Name', 'Date', or 'Items' might be null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
