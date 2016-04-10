using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace SisAn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
         

        string[] el = { "0", "1", "0.5", "_" };

        bool load = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgrdwMatrix.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            dtgrdwMatrixNew.AllowUserToAddRows = false;
            dtgrdwExp.AllowUserToAddRows = false;
            dtgrdwExp.Columns.Add("exp", "эксперт");
            dtgrdwExp.Columns.Add("eval", "оценка");
            groupBox1.Visible = false;
            dtgrdwMatrixNew.Visible = false;
        }

        #region

        private void button1_Click(object sender, EventArgs e) //сам алгоритм
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        int count = lstbxAltList.Items.Count;
                        float[] C = new float[count];
                        string[] sortedList = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            sortedList[i] = lstbxAltList.Items[i].ToString();
                        }
                        /*if (!check())
                        {
                            return;
                        }*/
                        float R = 0;
                        for (int i = 0; i < count; i++)
                            C[i] = 0;

                        for (int i = 0; i < count; i++)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                if (j != i)
                                {
                                    C[i] += Convert.ToSingle(dtgrdwMatrix[i, j].Value.ToString().Replace(".", ","));
                                    R += Convert.ToSingle(dtgrdwMatrix[i, j].Value.ToString().Replace(".", ","));
                                }
                            }

                        }
                        for (int i = 0; i < count; i++)
                            C[i] = C[i] / R;
                        BubbleSort(C, sortedList);

                        lstbxAltNewList.Items.AddRange(sortedList);
                    }
                    break;
                case 1:
                    {
                        float R = 0;

                        int countExp = dtgrdwExp.Rows.Count;
                        int countAlt = lstbxAltList.Items.Count;
                        string[] sortedList = new string[countAlt];
                        for (int i = 0; i < countAlt; i++)
                        {
                            sortedList[i] = lstbxAltList.Items[i].ToString();
                        }
                        float[] R_i = new float[countExp];
                        float[] S = new float[countExp];
                        float[] V = new float[countAlt];
                        for (int i = 0; i < countExp; i++)
                        {
                            R_i[i] = Convert.ToSingle(dtgrdwExp.Rows[i].Cells[1].Value.ToString().Replace(".", ","));
                            R += R_i[i];
                        }
                        for (int j = 0; j < countExp; j++) //относительные оценки компетенции экспертов
                        {
                            S[j] = R_i[j] / R;
                        }
                        for (int j = 0; j < countAlt; j++)
                        {
                            for (int i = 0; i < countExp; i++) //определяем веса
                            {
                                V[j] += Convert.ToSingle(dtgrdwMatrix[j, i].Value.ToString().Replace(".", ",")) * S[i];
                            }

                        }
                        BubbleSort(V, sortedList); //сортируем

                        list_Alt_new.Items.Clear();
                        list_Alt_new.Items.AddRange(sortedList);
                    }
                    break;
                case 2:
                {
                     int countExp = dtgrdwExp.Rows.Count;
                     int countAlt = lstbxAltList.Items.Count;
                     string[] sortedList = new string[countAlt];
                     for (int i = 0; i < countAlt; i++)
                     {
                         sortedList[i] = lstbxAltList.Items[i].ToString();
                     }
                   for(int i = 0; i < countAlt; i++)
                        dtgrdwMatrixNew.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                   dtgrdwMatrixNew.Rows.Add(countExp);
                    float L = 0;
                   float[] L_j = new float[countAlt];
                   float[] V = new float[countAlt];
                    for (int i = 0; i < countAlt; i++)//модифицированная матрица предпочтений
                    {
                        for (int j = 0; j < countExp; j++)
                        {
                            dtgrdwMatrixNew[i, j].Value = countAlt - Convert.ToSingle(dtgrdwMatrix[i, j].Value);
                            
                        }
                    }
                    for (int j = 0; j < countAlt; j++) //суммарные оценки предпочтений
                    {
                        for (int i = 0; i < countExp; i++)
                        {
                            L_j[j] += Convert.ToSingle(dtgrdwMatrixNew[j, i].Value.ToString());
                        }
                        L += L_j[j];//сумма всех альтернатив
                    }
                   
                    for (int j = 0; j < countAlt; j++) //веса альтернатив
                    {
                        V[j] = L_j[j] / L;
                    }
                    BubbleSort(V, sortedList); //сортируем
                    listPredp.Items.Clear();
                    listPredp.Items.AddRange(sortedList);

                }
                    break;
                case 3:
                {
                    int countExp = dtgrdwExp.Rows.Count;
                    int countAlt = lstbxAltList.Items.Count;
                    string[] sortedList = new string[countAlt];
                    for (int i = 0; i < countAlt; i++)
                    {
                        sortedList[i] = lstbxAltList.Items[i].ToString();
                    }
                    for (int i = 0; i < countAlt; i++)
                        dtgrdwMatrixNew.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                    dtgrdwMatrixNew.Rows.Add(countExp);
                    float[] S_i = new float[countExp];
                    float[] V = new float[countAlt];
                    for (int j = 0; j < countExp; j++) //суммарные оценок всех альтернатив
                    {
                        for (int i = 0; i < countAlt; i++)
                        {
                            S_i[j] += Convert.ToSingle(dtgrdwMatrix[i, j].Value.ToString());
                        }
                        
                    }
                    for (int i = 0; i < countAlt; i++)//модифицированная матрица предпочтений
                    {
                        for (int j = 0; j < countExp; j++)
                        {
                            dtgrdwMatrixNew[i, j].Value = Convert.ToSingle(dtgrdwMatrix[i, j].Value)/S_i[j];

                        }
                    }
                    for (int j = 0; j < countAlt; j++)
                    {
                        for (int i = 0; i < countExp; i++) //определяем веса
                        {
                            V[j] += Convert.ToSingle(dtgrdwMatrixNew[j, i].Value.ToString()) / countExp;
                        }

                    }
                    BubbleSort(V, sortedList); //сортируем
                    listRang.Items.Clear();
                    listRang.Items.AddRange(sortedList);

                }
                    break;
            }

        }

        void BubbleSort(float[] A, string[] sortedList) //сортировка альтернатив
        {
            for (int i = 0; i < A.Length - 1; i++)
            {
                for (int j = i + 1; j < A.Length; j++)
                {
                    if (A[j] > A[i])
                    {
                        var temp = A[i];
                        A[i] = A[j];
                        A[j] = temp;
                        var tmp = sortedList[i];
                        sortedList[i] = sortedList[j];
                        sortedList[j] = tmp;
                    }
                }
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) //очистка матрицы
        {
            if (tabControl1.SelectedIndex == 0)
            {
                {
                    for (int i = 0; i < dtgrdwMatrix.Rows.Count; ++i)
                    {
                        for (int j = 0; j < dtgrdwMatrix.Columns.Count; ++j)
                        {
                            if (i != j)
                            {
                                dtgrdwMatrix[j, i].Style.BackColor = Color.White;
                                dtgrdwMatrix[i, j].Value = "1";
                                dtgrdwMatrix[j, i].Value = "0";
                            }
                            else
                            {
                                dtgrdwMatrix[i, i].Style.BackColor = Color.Aqua;
                                dtgrdwMatrix[i, i].ReadOnly = true;
                            }
                        }
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                {
                    for (int i = 0; i < dtgrdwMatrix.Rows.Count; ++i)
                    {
                        for (int j = 0; j < dtgrdwMatrix.Columns.Count; ++j)
                        {
                            dtgrdwMatrix[i, j].Value = "";
                        }
                    }
                }
            }
        }

        bool check() //проверки
        {

            for (int i = 0; i < dtgrdwMatrix.Rows.Count; i++)
            {
                for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                {
                    if (i != j)
                        if ((!el.Contains(dtgrdwMatrix[i, j].Value)))
                        {
                            MessageBox.Show("Неверно задана матрица предпочтений!");
                            dtgrdwMatrix[i, j].Value = "1";
                            dtgrdwMatrix[j, i].Value = "0";
                            return false;
                        }
                        else
                        {
                            float a = Convert.ToSingle(dtgrdwMatrix[i, j].Value) +
                                      Convert.ToSingle(dtgrdwMatrix[j, i].Value);
                            if (a !=
                                1.0)
                            {
                                MessageBox.Show("Неверно задана матрица предпочтений!");
                                dtgrdwMatrix[i, j].Style.BackColor = dtgrdwMatrix[j, i].Style.BackColor = Color.Red;
                                return false;
                            }
                            else
                                return true;
                        }
                }
            }
            return true;
        }

        private void Add_altern_Click(object sender, EventArgs e) //добавить альтернативу
        {
            if (tabControl1.SelectedIndex == 0)
            {
                {
                    if (txtbxAddAlt.Text != "")
                    {
                        lstbxAltList.Items.Add("[" + (lstbxAltList.Items.Count + 1).ToString() + "] " + txtbxAddAlt.Text);
                        txtbxAddAlt.Text = "";
                        dtgrdwMatrix.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                            "z" + lstbxAltList.Items.Count.ToString());
                        dtgrdwMatrix.Rows.Add();
                        dtgrdwMatrix.Rows[lstbxAltList.Items.Count - 1].HeaderCell.Value = "z" +
                                                                                           lstbxAltList.Items.Count
                                                                                               .ToString();
                        for (int i = 0; i < dtgrdwMatrix.Rows.Count; ++i)
                        {
                            int j = dtgrdwMatrix.Rows.Count - 1;
                            if (i == j)
                            {
                                dtgrdwMatrix[i, i].Style.BackColor = Color.Aqua;
                                dtgrdwMatrix[i, i].ReadOnly = true;
                            }
                            else
                            {
                                dtgrdwMatrix[j, i].Value = "0";
                                dtgrdwMatrix[i, j].Value = "1";
                            }
                        }
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
            {
                {
                    if (txtbxAddAlt.Text != "")
                    {
                        lstbxAltList.Items.Add("[" + (lstbxAltList.Items.Count + 1).ToString() + "] " + txtbxAddAlt.Text);
                        txtbxAddAlt.Text = "";
                        dtgrdwMatrix.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                            "z" + lstbxAltList.Items.Count.ToString());
                    }
                }
            }
        }

        private void Del_altern_Click(object sender, EventArgs e) //удаление выбранных альтернатив
        {
            if (tabControl1.SelectedIndex == 0)
            {
                {
                    if ((lstbxAltList.Items.Count != 0) && (lstbxAltList.SelectedIndex != -1))
                    {
                        int ch = lstbxAltList.SelectedItem.ToString().IndexOf("]");
                        int k = int.Parse(lstbxAltList.SelectedItem.ToString().Substring(1, ch - 1));
                        dtgrdwMatrix.Rows.RemoveAt(k - 1);
                        dtgrdwMatrix.Columns.RemoveAt(k - 1);
                        lstbxAltList.Items.RemoveAt(lstbxAltList.SelectedIndex);
                        for (int i = 0; i < lstbxAltList.Items.Count; i++)
                        {
                            dtgrdwMatrix.Rows[i].HeaderCell.Value = "z" + (i + 1).ToString();
                            dtgrdwMatrix.Columns[i].HeaderText = "z" + (i + 1).ToString();
                            int buf = lstbxAltList.Items[i].ToString().IndexOf("]", StringComparison.Ordinal);
                            int ind = int.Parse(lstbxAltList.Items[i].ToString().Substring(1, buf - 1));
                            if (ind > k)
                            {
                                //для изменения нумерации при удалении
                                lstbxAltList.Items[i] = "[" + (ind - 1).ToString() + "] " +
                                                        lstbxAltList.Items[i].ToString().Remove(0, buf + 2);
                            }
                        }
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
            {
                {
                    if ((lstbxAltList.Items.Count != 0) && (lstbxAltList.SelectedIndex != -1))
                    {
                        int ch = lstbxAltList.SelectedItem.ToString().IndexOf("]");
                        int k = int.Parse(lstbxAltList.SelectedItem.ToString().Substring(1, ch - 1));

                        dtgrdwMatrix.Columns.RemoveAt(k - 1);
                        lstbxAltList.Items.RemoveAt(lstbxAltList.SelectedIndex);
                        for (int i = 0; i < lstbxAltList.Items.Count; i++)
                        {
                            dtgrdwMatrix.Columns[i].HeaderText = "z" + (i + 1).ToString();
                            int buf = lstbxAltList.Items[i].ToString().IndexOf("]", StringComparison.Ordinal);
                            int ind = int.Parse(lstbxAltList.Items[i].ToString().Substring(1, buf - 1));
                            if (ind > k)
                            {
                                //для изменения нумерации при удалении
                                lstbxAltList.Items[i] = "[" + (ind - 1).ToString() + "] " +
                                                        lstbxAltList.Items[i].ToString().Remove(0, buf + 2);
                            }
                        }
                    }
                }
            }
        }

        private void Del_All_Click(object sender, EventArgs e) //очистить все
        {   
            dtgrdwMatrix.Rows.Clear();
            dtgrdwMatrix.Columns.Clear();
            lstbxAltList.Items.Clear();
            load = false;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {         
                        lstbxAltNewList.Items.Clear();
                    }
                    break;
                case 1:
                    {
                        list_Alt_new.Items.Clear();
                        dtgrdwExp.Rows.Clear();
                        dtgrdwExp.Columns.Clear();
                    }
                    break;
                case 2:
                {
                    listPredp.Items.Clear();
                    dtgrdwExp.Rows.Clear();
                    dtgrdwExp.Columns.Clear();
                    dtgrdwMatrixNew.Rows.Clear();
                    dtgrdwMatrixNew.Columns.Clear();
                }
                    break;
                case 3:
                    {
                        listRang.Items.Clear();
                        dtgrdwExp.Rows.Clear();
                        dtgrdwExp.Columns.Clear();
                        dtgrdwMatrixNew.Rows.Clear();
                        dtgrdwMatrixNew.Columns.Clear();
                    }
                    break;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) //автоподстановка
        {
            /*dtgrdwMatrix[e.RowIndex, e.ColumnIndex].Value =
                (1.0 - Convert.ToSingle(dtgrdwMatrix[e.ColumnIndex, e.RowIndex].Value));
            //check();*/
        }

        private void матрицаПредпочтенийToolStripMenuItem1_Click(object sender, EventArgs e) //сохранение МП
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string Out = string.Empty;
                            for (int i = 0; i < dtgrdwMatrix.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                                {
                                    if (i == j)
                                        Out += "_\t";
                                    else
                                        Out += dtgrdwMatrix[j, i].Value + "\t";
                                }
                                Out += "\n";
                            }
                            File.WriteAllText(saveFileDialog1.FileName, Out);
                        }
                    }
                    break;
                case 1:
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string Out = string.Empty;
                            for (int i = 0; i < dtgrdwMatrix.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                                {
                                    Out += dtgrdwMatrix[j, i].Value + "\t";
                                }
                                Out += "\n";
                            }
                            File.WriteAllText(saveFileDialog1.FileName, Out);
                        }
                    }
                    break;
            }
        }

        private void списокАльтернативToolStripMenuItem_Click(object sender, EventArgs e)
        //сохранение списка альтернатив
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Out = string.Empty;
                for (int i = 0; i < lstbxAltList.Items.Count; i++)
                {
                    Out += lstbxAltList.Items[i].ToString().Substring(4) + "\n";
                }
                File.WriteAllText(saveFileDialog1.FileName, Out, Encoding.Default);
            }
        }

        private void альтернативаToolStripMenuItem_Click(object sender, EventArgs e) //загрузка из файла
        {
            if (tabControl1.SelectedIndex == 0)
            {
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] alts = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        if ((dtgrdwMatrix.Rows.Count != 0) && (dtgrdwMatrix.Rows.Count != alts.Length))
                        {
                            MessageBox.Show("Несовпадение по размеру", "Ошибка");
                            return;
                        }
                        int pos = 0;
                        dtgrdwMatrix.Rows.Clear();

                        for (int i = 0; i < alts.Length; i++)
                        {
                            dtgrdwMatrix.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }

                        dtgrdwMatrix.Rows.Add(alts.Length);
                        if (!load)
                        {
                            for (int j = 0; j < alts.Length; j++)
                            {
                                dtgrdwMatrix.Rows[j].HeaderCell.Value = "z" + (j + 1).ToString();
                                for (int i = 0; i < alts.Length; i++)
                                {
                                    if (j == i)
                                    {
                                        dtgrdwMatrix[i, i].Style.BackColor = Color.Aqua;
                                        dtgrdwMatrix[j, i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        dtgrdwMatrix[j, i].Value = "0";
                                        dtgrdwMatrix[i, j].Value = "1";
                                    }
                                }
                            }
                        }
                        load = true;
                        lstbxAltList.Items.Clear();
                        for (int i = 1; i <= alts.Length; i++)
                        {
                            alts[i - 1] = "[" + i.ToString() + "] " + alts[i - 1];
                        }
                        lstbxAltList.Items.AddRange(alts);
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
            {
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] alts = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        int pos = 0;
                        dtgrdwMatrix.Rows.Clear();

                        for (int i = 0; i < alts.Length; i++)
                        {
                            dtgrdwMatrix.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }
                        load = true;
                        lstbxAltList.Items.Clear();
                        for (int i = 1; i <= alts.Length; i++)
                        {
                            alts[i - 1] = "[" + i.ToString() + "] " + alts[i - 1];
                        }
                        lstbxAltList.Items.AddRange(alts);
                    }
                }
            }
        }

        private void матрицаПредпочтенийToolStripMenuItem_Click(object sender, EventArgs e) //загрузка
        {
            if (tabControl1.SelectedIndex == 0)
            {
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        if ((dtgrdwMatrix.Rows.Count != 0) && (dtgrdwMatrix.Rows.Count != str.Length))
                        {
                            MessageBox.Show("Несовпадение по размеру", "Ошибка");
                            return;
                        }
                        int pos = 0;
                        dtgrdwMatrix.Rows.Clear();
                        dtgrdwMatrix.Columns.Clear();
                        string[] c = new string[str.Length];
                        for (int i = 0; i < c.Length; i++)
                            c[i] = "[" + (i + 1).ToString() + "] ";

                        for (int i = 0; i < str.Length; i++)
                        {
                            dtgrdwMatrix.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }

                        dtgrdwMatrix.Rows.Add(str.Length);

                        for (int i = 0; i < dtgrdwMatrix.RowCount; i++)
                        {
                            string[] buf = str[i].Split('\t');
                            for (int j = 0; j < dtgrdwMatrix.ColumnCount; j++)
                            {
                                dtgrdwMatrix[j, i].Value = buf[j];
                                pos++;
                                if (i == j)
                                {
                                    dtgrdwMatrix[i, i].Value = "";
                                    dtgrdwMatrix[i, i].ReadOnly = true;
                                    dtgrdwMatrix[i, i].Style.BackColor = Color.Aqua;
                                }
                            }
                        }
                        if (!load)
                        {
                            lstbxAltList.Items.Clear();
                            lstbxAltList.Items.AddRange(c);
                        }
                        load = true;
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
            {
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        int pos = 0;
                        dtgrdwMatrix.Rows.Clear();
                        dtgrdwMatrix.Columns.Clear();
                        string[] buf = str[0].Split('\t');
                        for (int i = 0; i < buf.Length; i++)
                        {
                            dtgrdwMatrix.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }

                        dtgrdwMatrix.Rows.Add(str.Length);
                        for (int i = 0; i < str.Length; i++)
                        {
                            buf = str[i].Split('\t');
                            for (int j = 0; j < buf.Length; j++)
                            {
                                dtgrdwMatrix[j, i].Value = buf[j];
                                pos++;
                                dtgrdwMatrix.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                            }
                        }
                        load = true;
                    }
                }
            }
        }

        private void btnEditing_Click(object sender, EventArgs e) //редактирование
        {
            lstbxAltList.Items[lstbxAltList.SelectedIndex] = lstbxAltList.SelectedItem.ToString().Substring(0, 4) +
                                                             txtbxEditing.Text;
            txtbxEditing.Text = "";
        }

        private void lstbxAltList_DoubleClick(object sender, EventArgs e)
        //при двойном щелчке переместить в форму для редактирвоания
        {
            if (lstbxAltList.SelectedIndices.Count != 0)
            {
                txtbxEditing.Text = lstbxAltList.Items[lstbxAltList.SelectedIndex].ToString().Substring(4);
            }
        }

        #endregion

        private void add_exp_Click(object sender, EventArgs e) //добавить эксперта
        {
            if (txtAddExp.Text != "" && txtAddEval.Text != "")
            {

                dtgrdwExp.Rows.Add(txtAddExp.Text, txtAddEval.Text);
                txtAddExp.Text = "";
                txtAddEval.Text = "";
                dtgrdwMatrix.Rows.Add();
                dtgrdwMatrix.Rows[dtgrdwExp.Rows.Count - 1].HeaderCell.Value = "Э" + dtgrdwExp.Rows.Count.ToString();
            }
        }

        private void edit_exp_Click(object sender, EventArgs e) //редактирование эксперта
        {
            if ((txtExpEdit.Text != "") && (dtgrdwExp.RowCount != 0))
            {
                dtgrdwExp.CurrentCell.Value = txtExpEdit.Text;
            }
            txtExpEdit.Text = "";
        }


        private void dtgrdwExp_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) //эксперта
        {
            if (dtgrdwExp.SelectedCells.Count != 0)
            {
                txtExpEdit.Text = dtgrdwExp.CurrentCell.Value.ToString();
            }
        }

        private void del_exp_Click(object sender, EventArgs e) //удаление эксперта
        {
            if ((dtgrdwExp.Rows.Count != 0) && (dtgrdwExp.SelectedRows.Count != 0))
            {
                dtgrdwMatrix.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);
                dtgrdwExp.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);
                for (int i = 0; i < dtgrdwExp.Rows.Count; i++)
                {
                    dtgrdwMatrix.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                }
            }

        }
        
        private void списокЭкспертовToolStripMenuItem1_Click(object sender, EventArgs e) //загрузка списка экспертов
        {
            if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
            {
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] alts = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        int pos = 0;
                        dtgrdwExp.Rows.Clear();
                        dtgrdwExp.Columns.Clear();
                        //для списка альтернатив
                        string[] c = new string[alts.Length];
                        for (int i = 0; i < c.Length; i++)
                            c[i] = "[" + (i + 1).ToString() + "] ";
                        //для списка экспертов
                        dtgrdwExp.Columns.Add("exp", "Эксперт");
                        dtgrdwExp.Columns.Add("Eval", "Оценка");
                        dtgrdwExp.Rows.Add(alts.Length);
                        for (int i = 0; i < dtgrdwExp.RowCount; i++)
                        {
                            string[] buf = alts[i].Split('\t');
                            for (int j = 0; j < dtgrdwExp.ColumnCount; j++)
                            {
                                dtgrdwExp[j, i].Value = buf[j];
                                pos++;
                                dtgrdwExp.Rows[i].HeaderCell.Value = (i + 1).ToString();
                            }
                        }
                        if (!load)
                        {
                            lstbxAltList.Items.Clear();
                            lstbxAltList.Items.AddRange(c);
                        }
                        load = true;
                        //для матрицы
                        int posit = 0;
                        dtgrdwMatrix.Rows.Clear();

                        dtgrdwMatrix.Rows.Add(alts.Length);
                        for (int i = 0; i < alts.Length; i++)
                        {
                            dtgrdwMatrix.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                        }

                        if (!load)
                        {
                            for (int j = 0; j < alts.Length; j++)
                            {
                                for (int i = 0; i < alts.Length; i++)
                                {
                                    dtgrdwMatrix[j, i].Value = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void списокЭкспертовToolStripMenuItem_Click(object sender, EventArgs e)//сохранение списка экспертов
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string Out = string.Empty;
                            for (int i = 0; i < dtgrdwExp.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrdwExp.Columns.Count; j++)
                                {
                                    Out += dtgrdwExp[j, i].Value + "\t";
                                }
                                Out += "\n";
                            }
                            File.WriteAllText(saveFileDialog1.FileName, Out);
                        }
                    }
                    break;
            }
        }

        private void txtExpEdit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                txtExpEdit.Text += e.KeyChar;
            if ((e.KeyChar == 8) && (txtExpEdit.Text != ""))
                txtExpEdit.Text = txtExpEdit.Text.Remove(txtExpEdit.TextLength - 1, 1);
            if ((e.KeyChar == (char)Keys.Return) && (txtExpEdit.Text != ""))
            {
                if (dtgrdwExp.RowCount != 0)
                    dtgrdwExp.CurrentCell.Value = txtExpEdit.Text;
                txtExpEdit.Text = "";
            }

        }

        private void txtAddEval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                txtAddEval.Text += e.KeyChar;
            if ((e.KeyChar == 8) && (txtAddEval.Text != ""))
                txtAddEval.Text = txtAddEval.Text.Remove(txtAddEval.TextLength - 1, 1);

        }

        private void dtgrdwMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dtgrdwMatrix.Rows.Count; i++)
            {
                double summ = 0;
                for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                {

                    summ += Convert.ToSingle(dtgrdwMatrix[j, i].Value);
                }
                if (Convert.ToSingle(summ) != 1.0)
                {
                    MessageBox.Show("Неверно задана матрица предпочтений!");
                    for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                    {
                        dtgrdwMatrix.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                }
                else
                {
                    for (int j = 0; j < dtgrdwMatrix.Columns.Count; j++)
                    {
                        dtgrdwMatrix.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }


            }
            return;
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                groupBox1.Visible = false;
            else
            {
                groupBox1.Visible = true;
            }
            if (tabControl1.SelectedIndex == 2 || tabControl1.SelectedIndex == 3)
                dtgrdwMatrixNew.Visible = true;
            else
            {
                dtgrdwMatrixNew.Visible = false;
            }
        }


    }
}
