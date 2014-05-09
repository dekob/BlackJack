using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class WebBrowser : Form
    {
        public WebBrowser(String url)
        {
            InitializeComponent();
            this.webBrowser1.Url = new Uri(url);
            
        }
    }
}
