using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;

namespace whitodo_csharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point mouseOffset;
        private NotifyIcon notifyIcon;


        public bool InitWhitodo()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Global.mainDirPath = Path.Combine(appDataPath, Global.mainDirName);
            if (!Directory.Exists(Global.mainDirPath))
            {
                Directory.CreateDirectory(Global.mainDirPath);
            }

            Global.cacheDirPath = Path.Combine(Global.mainDirPath, Global.cacheDirName);
            if (!Directory.Exists(Global.cacheDirPath))
            {
                Directory.CreateDirectory(Global.cacheDirPath);
            }

            Global.cfgFilePath = Path.Combine(Global.mainDirPath, Global.cfgFileName);
            Global.txtFilePath = Path.Combine(Global.mainDirPath, Global.txtFileName);
            
            return true;
        }

        public void AddNotifyIcon()
        {
            if (notifyIcon != null)
            {
                return;
            }
            notifyIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon(),
                Text = "WhiToDo"
            };
            notifyIcon.Visible = true;

            System.Windows.Forms.ContextMenu notifyMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem openMainWindow = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem close = new System.Windows.Forms.MenuItem();
            openMainWindow.Text = "打开控制栏";
            close.Text = "退出";
            openMainWindow.Click += new EventHandler(delegate { this.Close(); });
            notifyMenu.MenuItems.Add(openMainWindow);
            notifyMenu.MenuItems.Add(close);

            notifyIcon.ContextMenu = notifyMenu;
        }

        public MainWindow()
        {
            InitWhitodo();
            InitializeComponent();
            AddNotifyIcon();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Initialized(object sender, EventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseOffset = e.GetPosition(this);
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition(this);
                this.Left += (mousePos.X - mouseOffset.X);
                this.Top += (mousePos.Y - mouseOffset.Y);
            }
        }
    }
}
