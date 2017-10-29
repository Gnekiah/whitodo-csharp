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
        private Point startControlArea;
        private Point endControlArea;
        private Point startTextArea;
        private Point endTextArea;
        public SettingsPDU spdu;


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

        public void HideControlPanel()
        {
            ControlPanelBackground.Visibility = Visibility.Hidden;
            ControlPanel.Visibility = Visibility.Hidden;
            WhitodoText.IsReadOnly = true;
            WhitodoText.SelectionBrush = null;
        }

        public void ShowControlPanel()
        {
            ControlPanelBackground.Visibility = Visibility.Visible;
            ControlPanel.Visibility = Visibility.Visible;
            WhitodoText.IsReadOnly = false;
            WhitodoText.SelectionBrush = Brushes.LightPink;
        }

        public void AddNotifyMenu(NotifyIcon notifyIcon)
        {
            System.Windows.Forms.ContextMenu notifyMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem openMainWindow = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem close = new System.Windows.Forms.MenuItem();

            openMainWindow.Text = "打开控制栏";
            close.Text = "退出";

            openMainWindow.Click += new EventHandler(delegate { ShowControlPanel(); });
            close.Click += new EventHandler(delegate { this.Close(); });

            notifyMenu.MenuItems.Add(openMainWindow);
            notifyMenu.MenuItems.Add(close);
            notifyIcon.ContextMenu = notifyMenu;
        }

        public void CalcRectArea()
        {
            startTextArea = WhitodoText.TranslatePoint(new Point(0, 0), this);
            endTextArea.X = startTextArea.X + WhitodoText.Width;
            endTextArea.Y = startTextArea.Y + WhitodoText.Height;
            startControlArea = RedButton.TranslatePoint(new Point(0, 0), this);
            endControlArea = WhitodoButton.TranslatePoint(new Point(0, 0), this);
            endControlArea.X += WhitodoButton.Width;
            endControlArea.Y += WhitodoButton.Height;
            /* DEBUG INFO
            string x = "startTextArea=" + startTextArea.X.ToString() + "," + startTextArea.Y.ToString();
            x += "\nendTextArea=" + endTextArea.X.ToString() + "," + endTextArea.Y.ToString();
            x += "\nstartControlArea=" + startControlArea.X.ToString() + "," + startControlArea.Y.ToString();
            x += "\nendControlArea=" + endControlArea.X.ToString() + "," + endControlArea.Y.ToString();
            System.Windows.MessageBox.Show(x);
            */
        }

        public void SetSPDU()
        {
            spdu = new SettingsPDU(TextPanelInnerBackground.Margin.Left - TextPanelOuterBackground.Margin.Left,
                WhitodoText.Width, WhitodoText.Height,
                TextPanelOuterBackground.Opacity, TextPanelInnerBackground.Opacity,
                TextPanelOuterBackground.Background, TextPanelInnerBackground.Background,
                RedButton.Background, BlueButton.Background, GreenButton.Background,
                YellowButton.Background, WhiteButton.Background, BlackButton.Background);
        }

        public MainWindow()
        {
            InitWhitodo();
            InitializeComponent();
            this.ShowInTaskbar = false;
            FuckNotifyIcon FuckNotify = new FuckNotifyIcon();
            AddNotifyMenu(FuckNotify.GetNotifyIcon());
            SetSPDU();
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
<<<<<<< HEAD

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

        private void FontSizeButton_Loaded(object sender, RoutedEventArgs e)
        {
            int[] fontsize = { 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 32, 36, 40, 48, 56, 64, 72 };
            foreach (int i in fontsize)
            {
                FontSizeButton.Items.Add(i.ToString());
            }
        }

        private void FontTypeButton_Loaded(object sender, RoutedEventArgs e)
        {
            LinkedList<string> fontnames = new LinkedList<string>();
            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                LanguageSpecificStringDictionary fontname = font.FamilyNames;
                string _fontname = null;
                if (fontname.ContainsKey(System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn")) &&
                    fontname.TryGetValue(System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn"), out _fontname))
                {
                    fontnames.AddLast(_fontname);
                }
                else
                {
                    FontTypeButton.Items.Add(font.Source);
                }
            }
            foreach (string fontname in fontnames)
            {
                FontTypeButton.Items.Add(fontname);
            }
        }

        private void FontTypeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    FontFamily font = new FontFamily(FontTypeButton.SelectedItem.ToString());
                    text.ApplyPropertyValue(TextBlock.FontFamilyProperty, font);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
        }

        private void FontSizeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.FontSizeProperty, FontSizeButton.SelectedItem);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
        }

        private void SettingButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 2;
            stf.ScaleY = 2;
            SettingButton.RenderTransformOrigin = new Point(0.5, 0.5);
            SettingButton.RenderTransform = stf;
        }

        private void SettingButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 1.0;
            stf.ScaleY = 1.0;
            SettingButton.RenderTransformOrigin = new Point(0.5, 0.5);
            SettingButton.RenderTransform = stf;
        }

        private void WhitodoButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 2;
            stf.ScaleY = 2;
            WhitodoButton.RenderTransformOrigin = new Point(0.5, 0.5);
            WhitodoButton.RenderTransform = stf;
        }

        private void WhitodoButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 1.0;
            stf.ScaleY = 1.0;
            WhitodoButton.RenderTransformOrigin = new Point(0.5, 0.5);
            WhitodoButton.RenderTransform = stf;
        }

        private void BigerButton_Click(object sender, RoutedEventArgs e)
        {
            /* 设置增大行距 */
            /*
            TextPointer tp = this.WhitodoText.CaretPosition.GetLineStartPosition(0);
            TextBlock tb = new TextBlock();
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.LineHeightProperty, 12);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
            */
        }

        private void LowerButton_Click(object sender, RoutedEventArgs e)
        {
            /* 设置缩小行距 */
            /*
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.LineHeightProperty, 12);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
            */
        }
        
        public void DoSettings(SettingsPDU spdu)
        {
            switch (spdu.id)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    TextPanelOuterBackground.Opacity = spdu.outerTransparency;
                    break;
                case 5:
                    TextPanelInnerBackground.Opacity = spdu.innerTransparency;
                    break;
                case 6:
                    TextPanelOuterBackground.Background = spdu.outerBrush;
                    break;
                case 7:
                    TextPanelInnerBackground.Background = spdu.innerBrush;
                    break;
                case 8:
                    RedButton.Background = spdu.brush1;
                    break;
                case 9:
                    BlueButton.Background = spdu.brush2;
                    break;
                case 10:
                    GreenButton.Background = spdu.brush3;
                    break;
                case 11:
                    YellowButton.Background = spdu.brush4;
                    break;
                case 12:
                    WhiteButton.Background = spdu.brush5;
                    break;
                case 13:
                    BlackButton.Background = spdu.brush6;
                    break;
                default:
                    break;
            }
        }

        private void SettingButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetSPDU();
            SettingDialog settingDialog = new SettingDialog(DoSettings, spdu);
            HideControlPanel();
            settingDialog.ShowDialog();
            ShowControlPanel();
            CalcRectArea();
        }

        private void WhitodoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideControlPanel();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

=======
>>>>>>> dev-resiable
=======
>>>>>>> dev-resiable
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseOffset = e.GetPosition(this);
            CalcRectArea();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        private void Window_MouseMove(object sender, MouseEventArgs e)
=======
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
>>>>>>> dev-resiable
=======
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
>>>>>>> dev-resiable
        {
            Point mousePos = e.GetPosition(this);
            if ((mousePos.X > startTextArea.X && mousePos.Y > startTextArea.Y &&
                mousePos.X < endTextArea.X && mousePos.Y < endTextArea.Y) || 
                (mousePos.X > startControlArea.X && mousePos.Y > startControlArea.Y &&
                mousePos.X < endControlArea.X && mousePos.Y < endControlArea.Y))
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Left += (mousePos.X - mouseOffset.X);
                this.Top += (mousePos.Y - mouseOffset.Y);
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseOffset = e.GetPosition(this);
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition(this);
                this.Width += (mousePos.X - mouseOffset.X);
                this.Height += (mousePos.Y - mouseOffset.Y);
            }
        }
=======
>>>>>>> parent of a8e36c3... backup for usable opacity window
    }
}
