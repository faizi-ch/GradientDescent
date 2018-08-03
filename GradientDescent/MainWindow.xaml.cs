using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace GradientDescent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private double total = 0, n = 0;
        private int x = 1, y = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void XTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (XTextEdit.Text != "")
                {
                    XList.Items.Add(XTextEdit.Text);
                }
                if (x==total)
                {
                    XTextEdit.IsEnabled = false;
                    YTextEdit.Focus();
                }
                XTextEdit.Text = "";
                x++;
            }
        }

        private void YTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (YTextEdit.Text != "")
                {
                    YList.Items.Add(YTextEdit.Text);
                }
                if (y == total)
                {
                    YTextEdit.IsEnabled = false;
                    YTextEdit.Focus();
                }
                YTextEdit.Text = "";
                y++;
                
            }
        }

        private void TotalTextEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            if (TotalTextEdit.Text != "")
            {
                total = Convert.ToInt32(TotalTextEdit.Text);
            }
        }

        private void TotalTextEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (TotalTextEdit.Text != "")
            {
                total = Convert.ToInt32(TotalTextEdit.Text);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double theta0, theta1, t0, t1, j0, j1, alpha=0.05;
            if (Theta0TextEdit.Text != "" && Theta1TextEdit.Text != "" && NTextEdit.Text != "" && TotalTextEdit.Text!="" && XList.Items.Count > 0 && YList.Items.Count > 0)
            {
                total = Convert.ToInt32(TotalTextEdit.Text);
                n = Convert.ToInt32(NTextEdit.Text);
                
                theta0 = Convert.ToDouble(Theta0TextEdit.Text);
                theta1 = Convert.ToDouble(Theta1TextEdit.Text);


                
                for (int i = 0; i < n; i++)
                {
                    j0 = CalculateCostFunctionForTheta0(theta0, theta1);
                    j1 = CalculateCostFunctionForTheta1(theta0, theta1);

                    t0 = theta0 - (alpha * j0);
                    t1 = theta1 - (alpha * j1);

                    if (t0 < 0 || t1 < 0)
                    {
                        break;
                    }

                    UpdatedThetasList.Items.Add(string.Format("{0:F4}\t\t{1:F4}", t0,t1));

                    theta0 = t0;
                    theta1 = t1;
                }
            }
        }
        private double CalculateCostFunctionForTheta0(double theta0, double theta1)
        {
            double h = 0, j = 0;
            if (Theta0TextEdit.Text != "" && Theta1TextEdit.Text != "" && XList.Items.Count > 0 && YList.Items.Count > 0)
            {
                for (int i = 0; i < total; i++)
                {
                    h = theta0 + theta1 * Convert.ToDouble(XList.Items[i]);

                    j += h - Convert.ToDouble(YList.Items[i]);
                    CalculatedList.Items.Add(j.ToString());
                }
                //MessageBox.Show(j.ToString());
                //double m = (2 * total);
                double d = 1 / total;
                j *= d;

                //CalculatedLabel.Content += j.ToString();
            }
            return j;
        }
        private double CalculateCostFunctionForTheta1(double theta0, double theta1)
        {
            double h = 0, j = 0;
            if (Theta0TextEdit.Text != "" && Theta1TextEdit.Text != "" && XList.Items.Count > 0 && YList.Items.Count > 0)
            {
                for (int i = 0; i < total; i++)
                {
                    h = theta0 + (theta1 * Convert.ToDouble(XList.Items[i]));

                    j += (h - Convert.ToDouble(YList.Items[i])) * Convert.ToDouble(XList.Items[i]);
                    CalculatedList.Items.Add(j.ToString());
                }
                //MessageBox.Show(j.ToString());
                //double m = (2 * total);
                double d = 1 / total;
                j *= d;

                //CalculatedLabel.Content += j.ToString();
            }
            return j;
        }
    }
}
