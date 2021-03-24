using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace testapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IntPtr windowDocked;

        private readonly System.Windows.Forms.Panel panel;

        private readonly AccuTermClasses.AccuTerm app;


        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
       
        public MainWindow()
        {
            InitializeComponent();

            Loaded += Window_Loaded;

            panel = new System.Windows.Forms.Panel();
            Type AccuTermType = Type.GetTypeFromProgID("Atwin80.AccuTerm");

            app = (AccuTermClasses.AccuTerm)Activator.CreateInstance(AccuTermType);

            app.Activate();

           // var popSessionFileName = ConfigurationManager.AppSettings["PopSession"];

           // var strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

           // var popSessionPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(strExeFilePath), popSessionFileName);


          //  app.Sessions.Add(popSessionPath);

            windowDocked = new IntPtr(app.hWnd);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (this.app != null)
            {
                this.app.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LaunchChildProcess();
        }

        private void LaunchChildProcess()
        {
            SetParent(windowDocked, panel.Handle);
            winFormsHost.Child = panel;
        }

    }
}
