using System.Numerics;
using System.Windows;

namespace UAIBANCalc
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private static readonly BigInteger AcctNumberLimit = BigInteger.Pow(10, 19);
    private static readonly BigInteger BBANLimit = BigInteger.Pow(10, 25);

    private void OnComputeClick(object sender, RoutedEventArgs e)
    {
      BigInteger bankCode;
      if (!BigInteger.TryParse(BankCodeTxt.Text, out bankCode) || bankCode < 100000 || bankCode >= 1000000)
      {
        MessageBox.Show("Неверный код банка");
        return;
      }

      BigInteger acctNumber;
      if (!BigInteger.TryParse(AcctNumberTxt.Text, out acctNumber) || acctNumber >= AcctNumberLimit)
      {
        MessageBox.Show("Неверный номер счёта/карты");
        return;
      }

      var bban = bankCode * AcctNumberLimit + acctNumber;

      var baseValue = bban * 1000000 + 301000;
      var chk = 98 - baseValue % 97;

      var baseVerifyValue = bban * 1000000 + 301000 + chk;
      var chkVerify = baseVerifyValue % 97;
      if (chkVerify != 1)
      {
        MessageBox.Show("Получилась неверная контрольная сумма");
        return;
      }

      IBANTxt.Text = "UA" + (chk * BBANLimit + bban).ToString("D27");
    }
  }
}
