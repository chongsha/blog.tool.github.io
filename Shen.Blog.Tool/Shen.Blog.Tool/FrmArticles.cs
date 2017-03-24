using Shen.Blog.Tool.DAL;
using Shen.Blog.Tool.Model;
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
    public partial class FrmArticles : Form
    {
        public FrmArticles()
        {
            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Article article = new Article();

            new FrmEditor(article).ShowDialog();

            this.FrmArticles_Load(null, null);
        }

        private void FrmArticles_Load(object sender, EventArgs e)
        {
            var categories = CategoryDAL.GetCategories();

            this.colCategoy.DisplayMember = "Name";
            this.colCategoy.ValueMember = "Id";
            this.colCategoy.DataSource = categories;


            var data = ArticleDAL.GetAllNoContent();

            this.dataGridView1.DataSource = data;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                this.修改ToolStripMenuItem.Enabled = true;
                this.删除ToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.修改ToolStripMenuItem.Enabled = false;
                this.删除ToolStripMenuItem.Enabled = false;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除这篇文章吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var id = (this.dataGridView1.SelectedRows[0].DataBoundItem as Article).Id;

                ArticleDAL.Delete(id);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = (this.dataGridView1.SelectedRows[0].DataBoundItem as Article).Id;

            var article = ArticleDAL.Get(id);

            new FrmEditor(article).ShowDialog();

            this.FrmArticles_Load(null, null);
        }
    }
}
