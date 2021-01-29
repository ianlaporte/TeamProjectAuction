using System;
using System.Collections.Generic;
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

namespace TeamProjectAuction
{
    /// <summary>
    /// Interaction logic for PaymentDetailsWindow.xaml
    /// </summary>
    public partial class PaymentDetailsWindow : Window
    {
        private ClientPayment _updateClientPayment = new ClientPayment();
        public PaymentDetailsWindow(ClientPayment targeClientPayment, bool IsNew)
        {
            InitializeComponent();
            _updateClientPayment = targeClientPayment;
            OnloadWindow();
        }

        private void OnloadWindow()
        {
            txtDeposit.Text = _updateClientPayment.ClientDeposit.ToString();
            txtCreditCardNumber.Text = _updateClientPayment.ClientCreditCardNumber;
            txtCreditCardExpireDate.Text = _updateClientPayment.ClientCreditCardExpireDate;
            txtCreditCardSecurity.Text = _updateClientPayment.ClientCreditCardSecurityCode.ToString();
            chbCheque.IsChecked = _updateClientPayment.ClientCheque;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (chbCheque.IsChecked == true)
            {
                _updateClientPayment.ClientCheque = true;
            }
            else
            {
                _updateClientPayment.ClientCheque = false;
            }

            int securityCode;
            double deposit;
            
            _updateClientPayment.ClientCreditCardNumber = txtCreditCardNumber.Text;
            if (!double.TryParse(txtDeposit.Text, out deposit))
            {
                MessageBox.Show("Deposit parse failed.");
                return;
            }
            if (!Int32.TryParse(txtCreditCardSecurity.Text, out securityCode))
            {
                MessageBox.Show("Deposit parse failed.");
                return;
            }

            _updateClientPayment.ClientCreditCardExpireDate = txtCreditCardExpireDate.Text;
            _updateClientPayment.ClientCreditCardSecurityCode = securityCode;
            _updateClientPayment.ClientDeposit = deposit;
            _updateClientPayment.ClientCreditCardNumber = txtCreditCardNumber.Text;
            DialogResult = true;
        }
    }
}
