namespace whitodo_csharp
{
    partial class FuckNotifyIcon
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuckNotifyIcon));
            this.whitodoIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.whitodomenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.whitodomenu.SuspendLayout();
            // 
            // whitodoIcon
            // 
            this.whitodoIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("whitodoIcon.Icon")));
            this.whitodoIcon.Text = "WhiToDo";
            this.whitodoIcon.Visible = true;
            this.whitodoIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.whitodo_MouseDoubleClick);
            // 
            // whitodomenu
            // 
            this.whitodomenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMainWindow,
            this.closeMainWindow});
            this.whitodomenu.Name = "contextMenuStrip1";
            this.whitodomenu.Size = new System.Drawing.Size(149, 48);
            // 
            // openMainWindow
            // 
            this.openMainWindow.Name = "openMainWindow";
            this.openMainWindow.Size = new System.Drawing.Size(148, 22);
            this.openMainWindow.Text = "打开控制界面";
            // 
            // closeMainWindow
            // 
            this.closeMainWindow.Name = "closeMainWindow";
            this.closeMainWindow.Size = new System.Drawing.Size(148, 22);
            this.closeMainWindow.Text = "退出";
            this.whitodomenu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon whitodoIcon;
        private System.Windows.Forms.ContextMenuStrip whitodomenu;
        private System.Windows.Forms.ToolStripMenuItem openMainWindow;
        private System.Windows.Forms.ToolStripMenuItem closeMainWindow;
    }
}
