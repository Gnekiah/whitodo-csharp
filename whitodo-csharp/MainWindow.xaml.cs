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

namespace whitodo_csharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public MainWindow()
        {
            InitWhitodo();
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Initialized(object sender, EventArgs e)
        {

        }
    }
}
