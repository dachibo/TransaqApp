using Serilog;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using TransaqApp.Database;

namespace TransaqApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logging.logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt")
                .CreateLogger();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            XmlConnector.Init();
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            XmlConnector.ConnectorSendCommand(ObjectToXmlGeneric<Disconnect>.Serialize(new Disconnect()));
            XmlConnector.ConnectorUnInitialize();
        }

        private void ButtonConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            if ((bool)toggle.IsChecked)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Connection connection = db.Connections.FirstOrDefault();
                    if (connection != null)
                    {
                        Connect connect = new Connect(connection.login, connection.password, connection.host,
                            connection.port);
                        string message = ObjectToXmlGeneric<Connect>.Serialize(connect);
                        XmlConnector.ConnectorSendCommand(message);
                    }
                }

                Status.Text = "CONNECTED";
            }
            else
            {
                XmlConnector.ConnectorSendCommand(ObjectToXmlGeneric<Disconnect>.Serialize(new Disconnect()));
                Status.Text = "DISCONNECTED";
            }
        }


        private void ButtonConnectionEdit_OnClick(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();
            passwordWindow.ShowDialog();
        }

        private void ShowLogs_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogsWindow());
            // LogsWindow logsWindow = ;
            // nav.Navigate(logsWindow);
            // logsWindow.Show();
        }
    }
}