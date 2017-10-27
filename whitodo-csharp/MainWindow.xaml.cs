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
        
        public void AddNotifyMenu(NotifyIcon notifyIcon)
        {
            System.Windows.Forms.ContextMenu notifyMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem openMainWindow = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem close = new System.Windows.Forms.MenuItem();

            openMainWindow.Text = "打开控制栏";
            close.Text = "退出";

            openMainWindow.Click += new EventHandler(delegate {  });
            close.Click += new EventHandler(delegate { this.Close(); });

            notifyMenu.MenuItems.Add(openMainWindow);
            notifyMenu.MenuItems.Add(close);
            notifyIcon.ContextMenu = notifyMenu;
        }

        public MainWindow()
        {
            InitWhitodo();
            InitializeComponent();
            this.ShowInTaskbar = false;
            FuckNotifyIcon FuckNotify = new FuckNotifyIcon();
            AddNotifyMenu(FuckNotify.GetNotifyIcon());

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
            return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition(this);
                this.Left += (mousePos.X - mouseOffset.X);
                this.Top += (mousePos.Y - mouseOffset.Y);
            }
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, RedButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, BlueButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, GreenButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void YellowButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, YellowButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void WhiteButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, WhiteButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, BlackButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isBold = false;
                try
                {
                    FontWeight fw = (FontWeight)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty);
                    if (fw == FontWeights.Bold)
                    {
                        isBold = true;
                    }
                }
                catch (Exception) { }
                if (isBold)
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty, FontWeights.Normal);
                    this.BoldButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty, FontWeights.Bold);
                    this.BoldButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isItalic = false;
                try
                {
                    FontStyle fs = (FontStyle)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty);
                    if (fs == FontStyles.Italic)
                    {
                        isItalic = true;
                    }
                }
                catch (Exception) { }
                if (isItalic)
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty, FontStyles.Normal);
                    this.ItalicButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty, FontStyles.Italic);
                    this.ItalicButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void UnderLineButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isUnderline = false;
                try
                {
                    TextDecorationCollection ul = (TextDecorationCollection)text.GetPropertyValue(TextBlock.TextDecorationsProperty);
                    if (ul == TextDecorations.Underline)
                    {
                        isUnderline = true;
                    }
                }
                catch (Exception) { }
                if (isUnderline)
                {
                    text.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
                    this.UnderLineButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);
                    this.UnderLineButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void WhitodoText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isBold = false;
                bool isItalic = false;
                bool isUnderline = false;
                try
                {
                    TextDecorationCollection ul = (TextDecorationCollection)text.GetPropertyValue(TextBlock.TextDecorationsProperty);
                    if (ul == TextDecorations.Underline)
                    {
                        isUnderline = true;
                    }
                }
                catch (Exception) { }
                try
                {
                    FontWeight fw = (FontWeight)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty);
                    if (fw == FontWeights.Bold)
                    {
                        isBold = true;
                    }
                }
                catch (Exception) { }
                try
                {
                    FontStyle fs = (FontStyle)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty);
                    if (fs == FontStyles.Italic)
                    {
                        isItalic = true;
                    }
                }
                catch (Exception) { }
                this.BoldButton.Background = isBold ? Brushes.LightPink : Brushes.White;
                this.ItalicButton.Background = isItalic ? Brushes.LightPink : Brushes.White;
                this.UnderLineButton.Background = isUnderline ? Brushes.LightPink : Brushes.White;
            }
            this.WhitodoText.Focus();
        }
    }
}
