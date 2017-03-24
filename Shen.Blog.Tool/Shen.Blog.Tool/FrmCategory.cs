using Shen.Blog.Tool.DAL;
using Shen.Blog.Tool.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shen.Blog.Tool
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var data = row.DataBoundItem as Category;

            if (data.Id == 0)
            {
                CategoryDAL.Insert(data);
                this.BeginInvoke(new Action(() =>
                {
                    Thread.Sleep(500);
                    FrmCategory_Load(null, null);
                }));

            }
            else
                CategoryDAL.Update(data);
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            var data = CategoryDAL.GetCategories();

            data.Add(new Category());

            dataGridView1.DataSource = data;
        }
    }
}
