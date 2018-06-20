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

namespace ExcelMerge
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectFile1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx";
                openFileDialog.Title = "Select an excel file";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile1.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnSelectFIle2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx";
                openFileDialog.Title = "Select an excel file";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile2.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {

            if (!File.Exists(txtFile1.Text))
            {
                MessageBox.Show("Please Select valid File 1 path of Excel document!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(txtFile2.Text))
            {
                MessageBox.Show("Please Select valid File 2 path of Excel document!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new MergeHelperOpenXML().MergeExcel(txtFile1.Text, txtFile2.Text);
            
            //new MergeHelper().MergeExcel(txtFile1.Text, txtFile2.Text);



            MessageBox.Show("Excel Merge successfully,Please check in the excel file");
        }
    }
}
