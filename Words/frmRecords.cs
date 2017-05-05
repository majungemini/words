using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using AppHelper;
using System.IO;



namespace Words
{
    public partial class frmRecords : FormAdd
    {
        frmRecite frmrecite;
        private List<Words> listwords = null;

        const string XML_FILE = "wordslist";
        int rightw, wrongw;
        public frmRecords()
        {
            InitializeComponent();
        }

        private void Records_Load(object sender, EventArgs e)
        {
            listwords = LoadData<List<Words>>(XML_FILE);
            if (listwords == null) listwords = new List<Words>();
            this.dgvWords.DataSource = listwords;
            
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRecords_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(listwords);
        }
        private void InputReset()
        {
            this.txtName.Text = "";
            this.txtDefinition.Text = "";


            this.txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ErrorHandling()) return;

            if (editMode)
            {
                listwords[rowIndex].Word = this.txtName.Text;
                listwords[rowIndex].Definition = this.txtDefinition.Text;

            }
            else
            {
                var words = new Words(this.txtName.Text,
                    this.txtDefinition.Text);

                listwords.Add(words);
            }

            this.dgvWords.DataSource = null;
            this.dgvWords.DataSource = listwords;
            this.dgvWords.AutoResizeColumns();

            InputReset();

            editMode = false;
        }

        private bool ErrorHandling()
        {
            errList.Clear();
            this.lblErrName.Visible = false;
            this.lblErrDefinition.Visible = false;
           

            if (this.txtName.TextLength == 0)
            {
                errList.Add("Word name must be entered.");
                this.lblErrName.Visible = true;
            }
            if (this.txtDefinition.TextLength == 0)
            {
                errList.Add("Word definition must be entered.");
                this.lblErrDefinition.Visible = true;
            }


            var errMsg = new StringBuilder();

            foreach (var text in errList)
                errMsg.AppendLine(text);

            if (errList.Count > 0)
            {
                MessageBox.Show(errMsg.ToString());
                return true;
            }

            return false;
        }





        private void btnExcel_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Word";
                oSheet.Cells[1, 2] = "Definition";


                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "B1").Font.Bold = true;
                oSheet.get_Range("A1", "B1").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;

                


                for (int i = 0; i < dgvWords.RowCount; i++)
                {
                    oSheet.Cells[i+2, 1] = listwords[i].Word;
                    oSheet.Cells[i+2, 2] = listwords[i].Definition;
                }


                
                oRng = oSheet.get_Range("A1", "B1");
                oRng.EntireColumn.AutoFit();

                

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void dgvWords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvWords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = this.dgvWords.Rows[e.RowIndex];

            this.txtName.Text = row.Cells[0].Value.ToString();
            this.txtDefinition.Text = row.Cells[1].Value.ToString();


            editMode = true;
            rowIndex = e.RowIndex;
        }

        private void dgvWords_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteOK())
            {
                listwords.RemoveAt(rowIndex);
                this.dgvWords.DataSource = null;
                this.dgvWords.DataSource = listwords;
            }
        }

        //frmRecite myfrmrecite = new frmRecite();
        //string frmRecordright = myfrmrecite.frmrecordright;

        private void btnRecords_Click(object sender, EventArgs e)
        {
            string path = "MyRecords.txt";
            
            //  string path = @"E:\IS153\MyRecords.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (TextWriter writer = File.CreateText(path))
                {
                    writer.WriteLine(System.DateTime.Now);
                    writer.Write("RightWords: ");
                    writer.WriteLine(frmrecite.frmrecordright);
                    writer.Write("WrongWords: ");
                    writer.Write(frmrecite.frmrecordwrong);
                    writer.WriteLine();
                }
            }
            //else
            //{
            //    using (TextWriter writer = File.AppendText(path))
            //    {
            //        writer.WriteLine(System.DateTime.Now);
            //        writer.Write("RightWords: ");
            //        writer.WriteLine(frmrecite.frmrecordright);
            //        writer.Write("WrongWords: ");
            //        writer.Write(frmrecite.frmrecordwrong);
            //        writer.WriteLine();
            //    }

            //}

            //Open the file to read from.

            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    recordsList.Add(s);
                }
                var recordMsg = new StringBuilder();
                foreach (var text in recordsList) {
                    recordMsg.AppendLine(text);
                }
                if (recordsList.Count > 0)
                {
                    MessageBox.Show(recordMsg.ToString());
                }
                else {
                    MessageBox.Show("Not records input");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reciteForm = new frmRecite();
            reciteForm.ShowDialog();
        }
    }
    }
