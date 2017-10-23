using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whitodo_csharp
{
    public partial class FuckNotifyIcon: Component
    {    
        public FuckNotifyIcon()
        {
            InitializeComponent();
            whitodoIcon.Visible = true;
        }

        public FuckNotifyIcon(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void whitodo_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }
    }
}
