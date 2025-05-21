using System;
using System.Windows.Forms;
using com.calitha.goldparser;

namespace MiniRubyParserGUI
{
    public partial class Form1 : Form
    {
        private MyParser parser;

        public Form1()
        {
            InitializeComponent();
            string grammarPath = @"MiniRuby.cgt";
            parser = new MyParser(grammarPath, listBox1,listBox2);
        }

        
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            parser.Parse(txtCode.Text);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtCode.TextChanged += txtCode_TextChanged;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            try
            {
                parser.Parse(txtCode.Text);
                listBox1.Items.Add("Parsing completed successfully.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Exception during parsing: " + ex.Message);
            }
        }

        
    }
}
