using Shen.Blog.Tool.DAL;
using Shen.Blog.Tool.Model;
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
        private readonly Article m_article;
        public FrmEditor()
        {
            InitializeComponent();
        }

        internal FrmEditor(Article article) : this()
        {
            this.m_article = article;
        }

        private void FrmEditor_Load(object sender, EventArgs e)
        {
            string editorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Html", "Editor.Html");

            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            this.webBrowser1.Url = new Uri(editorPath);

            this.cboCategory.DisplayMember = "Name";
            this.cboCategory.ValueMember = "Id";

            var categories = CategoryDAL.GetCategories();

            this.cboCategory.DataSource = categories;

            this.txtTitle.Text = this.m_article.Title;
            this.cboCategory.SelectedValue = this.m_article.Id;
            this.chkHasChange.Checked = this.m_article.HasChange;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("init", new object[] { this.m_article.Content ?? "" });
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = this.webBrowser1.Document.InvokeScript("getContentTxt").ToString();
                if (txt.Trim().Length == 0)
                {
                    MessageBox.Show("请填写文章内容");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
                {
                    MessageBox.Show("请填写标题");
                    return;
                }

                if (this.cboCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择分类");
                    return;
                }

                string content = this.webBrowser1.Document.InvokeScript("getContent").ToString();

                this.m_article.Content = content;
                this.m_article.Title = this.txtTitle.Text.Trim();
                this.m_article.CategoryId = (long)(this.cboCategory.SelectedValue);
                if (txt.Length > 200)
                    this.m_article.Summary = txt.Substring(0, 200);
                else
                    this.m_article.Summary = txt;

                if (this.m_article.Id == 0)
                    ArticleDAL.Insert(this.m_article);
                else
                    ArticleDAL.Update(this.m_article);

                this.chkHasChange.Checked = this.m_article.HasChange;

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "保存失败");
            }
        }
    }
}
