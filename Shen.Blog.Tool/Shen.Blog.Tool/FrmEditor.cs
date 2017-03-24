using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shen.Blog.Tool
{
    public partial class FrmEditor : Form
    {
        public FrmEditor()
        {
            InitializeComponent();
        }

        private void FrmEditor_Load(object sender, EventArgs e)
        {
            string editorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Html", "Editor.Html");

            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            this.webBrowser1.Url = new Uri(editorPath);
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("init", new object[] { "<h2>设置内容</h2>" });
        }
    }
}
