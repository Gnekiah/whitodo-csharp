using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace whitodo_csharp
{
    public partial class FuckNotifyIcon: Component
    {    
        public FuckNotifyIcon()
        {
            InitializeComponent();
        }

        public NotifyIcon GetNotifyIcon()
        {
            return this.whitodoIcon;
        }
    }
}
