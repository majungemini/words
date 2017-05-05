using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppHelper;
using System.IO;

namespace Words
{
    public partial class frmRecite : FormAdd
    {
        private List<Words> listwords = new List<Words>();
        private List<Words> workList = new List<Words>();//set worklist size as 10
        private List<Words> wrongList = new List<Words>();
        private List<Words> passList = new List<Words>();
        const string Words_XML_FILE = "wordslist";
        const string Pass_XML_FILE = "passlist";
        const string Wrong_XML_FILE = "wronglist";


        string targetword, guessword,str_temp = "";
        int guesscount, guessleft = 10,rightguess=0,wrongguess=0;
        Random r = new Random();

        public frmRecite()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            listwords = LoadData<List<Words>>(Words_XML_FILE);

            //listwords = LoadData<List<Words>>(XML_FILE);
            //if (listwords == null) listwords = new List<Words>();
            passList = LoadData<List<Words>>(Pass_XML_FILE);
            if (passList == null) {
                passList = new List<Words>();
            } 

            wrongList = LoadData<List<Words>>(Wrong_XML_FILE);
            if (wrongList == null)
            {
                wrongList = new List<Words>();
            }

            workList.Clear();
            //txtWin.Text = this.rightguess.ToString();
            //txtLose.Text = this.wrongguess.ToString();

        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            LoadData();
            //for (int i = 0; i < 10; i++)
            //{
            //var both = listwords[r.Next(listwords.Count)];

            //listwords[rowIndex].Word = this.txtName.Text;
            //listwords[rowIndex].Definition = this.txtDefinition.Text;
         //   this.txtTarget.Text = null;

            str_temp = "";
            txtCount.Text = "0";
            txtLeft.Text = "10";
            this.guesscount = 0;
            this.guessleft = 10;
            txtGuess.Text = "";
            btnCheck.Enabled = true;
            rowIndex = r.Next(listwords.Count);
            for (int i = 0; i < listwords[rowIndex].Word.Length; i++)
            {
                str_temp = str_temp + "?";
            }
            this.txtTarget.Text = str_temp;
            //this.txtTarget.Text = listwords[rowIndex].Word.ToUpper();
            targetword = listwords[rowIndex].Word.ToUpper();

            this.txtDefinition.Text = listwords[rowIndex].Definition;
            // this.txtTarget.Text = listwords[rowIndex].Word; 
            this.txtLenght.Text = listwords[rowIndex].Word.Length.ToString();




            //for (int j = 0; j < both.Word.Count(); j++)
            //{
            //    str_temp = str_temp + "#";
            //}
            //txtTarget.Text = both.Word;



            //} 
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {


            //if (txtGuess.Text.Length == 0)
            //{
            //    MessageBox.Show("Must write down something to guess");
            //}
            //else if (guessword.Length != targetword.Length)
            //{
            //    MessageBox.Show("Please do correct guess amount!");
            //}
            //else if (targetword != guessword)
            //{
            //    MessageBox.Show("Please do other try!");
            //}
            //else {
            //    MessageBox.Show("You got it!");
            //}

            // _________________________________________________________________________

            guessword = txtGuess.Text.ToUpper();

            //try
            //{

            //}
            //catch (Exception)
            //{

            //    System.Console.WriteLine("Enter alphabet only");
            //}

            if (!validate())
            {
                MessageBox.Show("Must write down alphabet to guess");
            }
            else if (txtGuess.Text.Length == 0)
            {
                MessageBox.Show("Must write down something to guess");
            }
            else if (guessword.Length != targetword.Length)
            {
                MessageBox.Show("Please do correct guess amount!");
            }
            else if (targetword != guessword)
            {
                for (int idx = 0; idx < guessword.Length; idx++)
                {
                    if (guessword[idx] == targetword[idx])
                    {
                        str_temp = replace(str_temp, idx, guessword[idx]);
                        this.txtTarget.Text = str_temp;
                        //str_temp[idx] = targetword[idx]; 

                    }
                }

                guessleft -= 1;
                txtLeft.Text = guessleft.ToString();
                guesscount += 1;
                txtCount.Text = guesscount.ToString();


                if (guessleft < 1)
                {

                    wrongList.Add(listwords[rowIndex]);
                    //listwords.RemoveAt(rowIndex);
                    listwords.RemoveAt(rowIndex);
                    this.wrongguess++;
                    MessageBox.Show("You lose it");
                    btnCheck.Enabled = false;
                    txtTarget.Text = targetword;
                    txtGuess.Text = "";
                    btnPass.Enabled = false;
                    
                    txtLose.Text = this.wrongguess.ToString();
                }
                else {
                    MessageBox.Show("Please do other try!");
                }
                
            }
            else
            {
                var passwords = new Words(this.txtTarget.Text,
                                       this.txtDefinition.Text);

                passList.Add(passwords);
                //passList.Add(listwords[rowIndex]);
                //listwords.RemoveAt(rowIndex);
                listwords.RemoveAt(rowIndex);
                MessageBox.Show("You got it!");
                this.rightguess++;
                btnCheck.Enabled = false;
                btnPass.Enabled = false;
                txtWin.Text = this.rightguess.ToString();
                
            }

            


            //____________________________________________________________________________



        }
        

        private void btnPass_Click(object sender, EventArgs e)
        {
            wrongList.Add(listwords[rowIndex]);
            //listwords.RemoveAt(rowIndex);
            LoadData();
            str_temp = "";
            txtCount.Text = "0";
            txtLeft.Text = "10";
            this.guesscount = 0;
            this.guessleft = 10;
            txtGuess.Text = "";
            btnCheck.Enabled = true;
            rowIndex = r.Next(listwords.Count);
            for (int i = 0; i < listwords[rowIndex].Word.Length; i++)
            {
                str_temp = str_temp + "?";
            }
            this.txtTarget.Text = str_temp;

            targetword = listwords[rowIndex].Word.ToUpper();

            this.txtDefinition.Text = listwords[rowIndex].Definition;

            this.txtLenght.Text = listwords[rowIndex].Word.Length.ToString();
        }

        private void frmRecite_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(listwords);
            SaveData(passList);
            SaveData(wrongList);
            this.rightguess = 0;
            this.wrongguess = 0;
            rightguess = 0;
            wrongguess = 0;
        }
        private void frmRecite_Load(object sender, EventArgs e)
        {
            txtWin.Text = "0";
            txtLose.Text = "0";
        }
        private void btnRecords_Click(object sender, EventArgs e)
        {
            var recordsForm = new frmRecords();
            recordsForm.ShowDialog();
            //frmRecite_FormClosing;
           // frmRecite.ActiveForm.Close();
        }



        private void ckbDefinition_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbDefinition.Checked)
            {
                txtDefinition.Visible = true;
            }
            else
            {
                txtDefinition.Visible = false;
            }
        }


        private string replace(string old_text, int index, char new_char) {
            string new_text = old_text.Substring(0,index) + new_char + old_text.Substring(index + 1);

            return new_text;

        }

        

        private bool validate() {
            
            string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < guessword.Length; i++)
            {
                if (!alph.Contains(guessword[i]))
                {
                    return false;
                }

            }
            return true;
                    

        }

        //public int getRightguess()
        //{
        //    return this.rightguess;
        //}
        //public int getWrongguess()
        //{
        //    return this.wrongguess;
        //}
        private string frmRecordright;
        public string frmrecordright
        {
            get
            {
                return frmrecordright;
            }
            set
            {
                frmrecordright = this.rightguess.ToString();
            }
        }
        private string frmRecordwrong;
        public string frmrecordwrong
        {
            get
            {
                return frmrecordwrong;
            }
            set
            {
                frmrecordright = this.wrongguess.ToString();
            }
        }




        //public int Wrongguess
        //{
        //    get
        //    {
        //        return this.wrongguess;
        //    }
        //}

     




    }
}
