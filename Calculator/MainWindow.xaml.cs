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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result = 0;
        SelectedOperator SelectedOperator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(((Button)sender).Content.ToString(), out int selectedValue))
            {
                if (resultLabel.Content.ToString() == "0")
                    resultLabel.Content = $"{selectedValue}";
                else
                    resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        private void ACBtn_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = result = 0;
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
                resultLabel.Content = 0;

            if (sender == multiplyBtn)
                SelectedOperator = SelectedOperator.Multiply;
            else if (sender == sumBtn)
                SelectedOperator = SelectedOperator.Sum;
            else if (sender == subtractBtn)
                SelectedOperator = SelectedOperator.Subtract;
            else if (sender == divideBtn)
                SelectedOperator = SelectedOperator.Divide;
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double newNumber))
            {
                switch (SelectedOperator)
                {
                    case SelectedOperator.Sum:
                        result = SimpleMath.Sum(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtract:
                        result = SimpleMath.Substract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiply:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Divide:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }
                resultLabel.Content = result;
            }
        }

        private void PointBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.ToString().Contains('.'))
                resultLabel.Content = $"{resultLabel.Content}.";
        }

        private void PercentageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double tmp))
            {
                tmp = tmp / 100;
                if (lastNumber != 0)
                    tmp *= lastNumber;
                resultLabel.Content = tmp.ToString();
            }
        }

        private void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

    }
    public enum SelectedOperator
    {
        Sum,
        Subtract,
        Divide,
        Multiply
    }

    public class SimpleMath
    {
        public static double Sum(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Substract(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }
}
