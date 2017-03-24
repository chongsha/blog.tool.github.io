using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shen.Blog.Tool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // IEVersion.BrowserEmulationSet();
        }

        private void 分类管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmCategory().ShowDialog();
        }

        private void 文章管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmArticles().ShowDialog();
        }
    }
}
