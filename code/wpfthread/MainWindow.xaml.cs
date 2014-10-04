using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfthread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
        }

        #region block thread operation for 10 seconds
        private string LongRunningIOMethod()
        {
            Thread.Sleep(10000);   
            return "Completed!";
        }
        #endregion

        #region blocking the ui thread here
        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            BlockButton.Content = "Blocked!!";
            string result = LongRunningIOMethod();
            BlockButton.Content = result;
        }
        #endregion


        #region using new thread, not blocking ui
        private void ThreadButton_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                string result = LongRunningIOMethod();
                Dispatcher.BeginInvoke((Action)(() => ThreadButton.Content = result));
            }).Start();

            ThreadButton.Content = "Busy...";
        }
        #endregion


        #region  using a delegate/APM api, not blocking ui, uses threadpool thread

        delegate string LongRunner();

        private void DelegateATMButton_Click(object sender, RoutedEventArgs e)
        {
            LongRunner del = new LongRunner(LongRunningIOMethod);
            IAsyncResult iar = del.BeginInvoke(UpdateCallback, del);  
            DelegateATMButton.Content = "Busy...";
        }

        private void UpdateCallback(IAsyncResult iar)
        {
            LongRunner del = (LongRunner)iar.AsyncState;
            string msg = del.EndInvoke(iar);
            Dispatcher.BeginInvoke((Action)(() => DelegateATMButton.Content = msg));

        }

        #endregion

    }
}
