using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisAn
{
    public partial class Form2 : Form
    {
        List<ListBox> listsIn = new List<ListBox>();
        List<ListBox> listOut = new List<ListBox>();
        

        public Form2(List<ListBox> l)
        {
            InitializeComponent();
            listsIn = l;
            listOut.Add(listBox1); listOut.Add(listBox2);
            listOut.Add(listBox3);
            listOut.Add(listBox4);
            listOut.Add(listBox5);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < listsIn[i].Items.Count; j++)
                    listOut[i].Items.Add(listsIn[i].Items[j]);
            
        }
    }
}
