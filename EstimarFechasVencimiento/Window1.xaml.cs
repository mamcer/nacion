using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EstimarFechasVencimiento
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (txtFromDate.Text != string.Empty)
            {
                StringBuilder sb = new StringBuilder();
                string[] dates = txtFromDate.Text.Split('/');
                DateTime dt = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));
                for (int i = 0; i < 240; i++)
                {
                    sb.AppendLine(dt.ToShortDateString());
                    while (dt.Day > 10)
                    {
                        dt = dt.AddDays(-1);
                    }
                    dt = dt.AddMonths(1);
                    if (dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        dt = dt.AddDays(2);
                    }
                    else
                    {
                        if (dt.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dt = dt.AddDays(1);
                        }
                    }
                }
                txtExpirationDates.Text = sb.ToString();
            }
        }

        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(txtExpirationDates.Text);
        }
    }
}
