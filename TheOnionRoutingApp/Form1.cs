
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
using System.Net;
using com.LandonKey.SocksWebProxy;
using com.LandonKey.SocksWebProxy.Proxy;


namespace TheOnionRoutingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            webBrowser1.Visible = false;
         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = RunParallel(textBox1.Text);
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = RunParallel(textBox1.Text, proxyActived: false);
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Visible = true;
        }
        private static string RunParallel(string url, bool proxyActived = true)
        {
            try
            {
                var proxy = new SocksWebProxy(new ProxyConfig
                    (IPAddress.Parse("127.0.0.1"), 12345, IPAddress.Parse("127.0.0.1"), 9150, ProxyConfig.SocksVersion.Five));
                var client = new WebClient { Proxy = proxyActived ? proxy : null };
                var doc = new HtmlAgilityPack.HtmlDocument();
                var html = client.DownloadString(url);
                doc.LoadHtml(html);
                return html;
            }
            catch 
            {
                MessageBox.Show("Connection Error");
                return null ;

            }
        }
    }
}
