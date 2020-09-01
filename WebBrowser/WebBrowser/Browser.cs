using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

            webBrowser1.Navigate("https://www.google.com");
            textBox_htmlfile.Text = "Enter File Path";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //webBrowser1.GoHome(); //msn.com
            webBrowser1.Navigate("https://www.google.com");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStripTextBox1.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                toolStripProgressBar1.Value = Convert.ToInt32(e.CurrentProgress);
                toolStripProgressBar1.Maximum = Convert.ToInt32(e.MaximumProgress);
            }
            catch (Exception)
            {
                //
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "HTML|*.html" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox_htmlfile.Text = ofd.FileName;
                    Uri uri = new Uri(ofd.FileName);
                    webBrowser1.Navigate(uri);
                }
            }
        }

        private void btnSourceCode_Click(object sender, EventArgs e)
        {
            String source = ("viewsource.txt");
            StreamWriter writer = File.CreateText(source);
            writer.Write(webBrowser1.DocumentText);
            writer.Close();
            Process.Start("notepad.exe", source);
        }

        private void textBox_htmlfile_Click(object sender, EventArgs e)
        {
            textBox_htmlfile.Text = "";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Text = webBrowser1.DocumentTitle;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripTextBox1.Text = webBrowser1.Url.ToString();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser1.Navigate(toolStripTextBox1.Text);
            }
        }
    }
}
