using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TransaqApp.Database;

namespace TransaqApp
{
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
            Loaded += PasswordWindow_Loaded;
        }

        // при загрузке окна
        private void PasswordWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Connection connection = db.Connections.FirstOrDefault();
                if (connection != null)
                {
                    Id.Uid = connection.id.ToString();
                    Login.Text = connection.login;
                    Password.Password = connection.password;
                    Host.Text = connection.host;
                    Port.Text = connection.port;
                }
            }
        }

        private void ButtonSaveConnection_OnClick(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Connection connection = db.Connections.FirstOrDefault();
                if (connection != null)
                {
                    connection.login = Login.Text;
                    connection.password = Password.Password;
                    connection.host = Host.Text;
                    connection.port = Port.Text;
                }
                else
                {
                    Connection connect = new Connection(
                        login: Login.Text,
                        password: Password.Password,
                        host: Host.Text,
                        port: Port.Text);
                    db.Add(connect);
                }

                db.SaveChanges();
            }

            this.DialogResult = true;
        }
    }
}