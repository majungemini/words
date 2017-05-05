using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AppHelper;

namespace Words
{
    public partial class FormAdd : Form
    {
        protected List<string> errList = new List<string>();
        protected List<string> recordsList = new List<string>();
        private XMLSerializer xml = null;

        protected bool editMode = false;

        protected int rowIndex = -1;
        
        public FormAdd()
        {
            InitializeComponent();
        }

        protected T LoadData<T>(string fileName)
        {
            xml = new XMLSerializer(fileName);
            if (!File.Exists(xml.XMLPath)) return default(T);
            return (T)xml.LoadData<T>();
        }

        protected void SaveData(object IClass)
        {
            xml.SaveData(IClass);
        }

        protected bool DeleteOK()       //return true if ok to delete
        {
            if (rowIndex == -1)
                return false;
            else
                return DialogResult.Yes ==
                    MessageBox.Show("Are you sure?",
                    "Please Confirm", MessageBoxButtons.YesNo);
        }




    }
}
