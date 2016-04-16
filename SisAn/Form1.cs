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
            dtgrdwMatrix1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            dtgrdwMatrix2.AllowUserToAddRows = false;
            dtgrdwMatrix3.AllowUserToAddRows = false;
            dtgrdwMatrix4.AllowUserToAddRows = false;
            dtgrdwExp.AllowUserToAddRows = false;
            dtgrdwExp.Columns.Add("exp", "Эксперт");
            dtgrdwExp.Columns.Add("Eval", "Оценка");
            groupBox1.Visible = false;
           
        }

        #region

        private void button1_Click(object sender, EventArgs e) //сам алгоритм
        {
           for (int ind = 0; ind < checkBox.Items.Count; ind++)//проходим по элементам чекбокса
                {
                    switch (ind)
                    {
                        case 0:
                        {
                            if (checkBox.GetItemChecked(ind))//если отмечен, то 1 алгоритм
                            {
                                
                                    int count = lstbxAltList.Items.Count;
                                    float[] C = new float[count];
                                    string[] sortedList = new string[count];
                                    for (int i = 0; i < count; i++)
                                    {
                                        sortedList[i] = lstbxAltList.Items[i].ToString();
                                    }

                                    float R = 0;
                                    for (int i = 0; i < count; i++)
                                        C[i] = 0;

                                    for (int i = 0; i < count; i++)
                                    {
                                        for (int j = 0; j < count; j++)
                                        {
                                            if (j != i)
                                            {
                                                C[i] += Convert.ToSingle(dtgrdwMatrix1[i, j].Value.ToString().Replace(".", ","));
                                                R += Convert.ToSingle(dtgrdwMatrix1[i, j].Value.ToString().Replace(".", ","));
                                            }
                                        }
                                    }
                                    for (int i = 0; i < count; i++)
                                        C[i] = C[i] / R;
                                    BubbleSort(C, sortedList);
                                    lstbxAltNewList.Items.Clear();
                                    lstbxAltNewList.Items.AddRange(sortedList);
                                }
                            }
                        
                            break;
                        case 1:
                        {
                            if (checkBox.GetItemChecked(ind))//если отмечен, то 2 алгоритм
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
                                            V[j] += Convert.ToSingle(dtgrdwMatrix2[j, i].Value.ToString().Replace(".", ",")) *
                                                    S[i];
                                        }
                                    }
                                    BubbleSort(V, sortedList); //сортируем

                                    list_Alt_new.Items.Clear();
                                    list_Alt_new.Items.AddRange(sortedList);
                                }
                            }
                        break;
                    case 2:
                        {
                            if (checkBox.GetItemChecked(ind))//если отмечен, то 3 алгоритм
                            {
                                    int countExp = dtgrdwExp.Rows.Count;
                                    int countAlt = lstbxAltList.Items.Count;
                                    string[] sortedList = new string[countAlt];
                                    for (int i = 0; i < countAlt; i++)
                                    {
                                        sortedList[i] = lstbxAltList.Items[i].ToString();
                                    }

                                    float L = 0;
                                    float[] L_j = new float[countAlt];
                                    float[] V = new float[countAlt];
                                    float[,] newMatr = new float[countExp, countAlt];
                                    for (int i = 0; i < countAlt; i++) //модифицированная матрица предпочтений
                                    {
                                        for (int j = 0; j < countExp; j++)
                                        {
                                            newMatr[j, i] = countAlt - Convert.ToSingle(dtgrdwMatrix3[i, j].Value);
                                        }
                                    }
                                    for (int j = 0; j < countAlt; j++) //суммарные оценки предпочтений
                                    {
                                        for (int i = 0; i < countExp; i++)
                                        {
                                            L_j[j] += newMatr[i, j];
                                        }
                                        L += L_j[j]; //сумма всех альтернатив
                                    }

                                    for (int j = 0; j < countAlt; j++) //веса альтернатив
                                    {
                                        V[j] = L_j[j] / L;
                                    }
                                    BubbleSort(V, sortedList); //сортируем
                                    listPredp.Items.Clear();
                                    listPredp.Items.AddRange(sortedList);
                                }
                            }
                        break;
                        case 3:
                        {
                            if (checkBox.GetItemChecked(ind))//если отмечен, то 4 алгоритм
                            {
                                    int countExp = dtgrdwExp.Rows.Count;
                                    int countAlt = lstbxAltList.Items.Count;
                                    string[] sortedList = new string[countAlt];
                                    for (int i = 0; i < countAlt; i++)
                                    {
                                        sortedList[i] = lstbxAltList.Items[i].ToString();
                                    }

                                    float[] S_i = new float[countExp];
                                    float[] V = new float[countAlt];
                                    float[,] newMatr = new float[countExp, countAlt];
                                    for (int j = 0; j < countExp; j++) //суммарные оценок всех альтернатив
                                    {
                                        for (int i = 0; i < countAlt; i++)
                                        {
                                            S_i[j] += Convert.ToSingle(dtgrdwMatrix4[i, j].Value.ToString());
                                        }
                                    }
                                    for (int i = 0; i < countAlt; i++) //модифицированная матрица предпочтений
                                    {
                                        for (int j = 0; j < countExp; j++)
                                        {
                                            newMatr[j, i] = Convert.ToSingle(dtgrdwMatrix4[i, j].Value) / S_i[j];
                                        }
                                    }
                                    for (int j = 0; j < countAlt; j++)
                                    {
                                        for (int i = 0; i < countExp; i++) //определяем веса
                                        {
                                            V[j] += newMatr[i, j] / countExp;
                                        }
                                    }
                                    BubbleSort(V, sortedList); //сортируем
                                    listRang.Items.Clear();
                                    listRang.Items.AddRange(sortedList);
                                }
                            }
                        break;
                    }             
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
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                {
                    for (int i = 0; i < dtgrdwMatrix1.Rows.Count; ++i)
                    {
                        for (int j = 0; j < dtgrdwMatrix1.Columns.Count; ++j)
                        {
                            if (i != j)
                            {
                                dtgrdwMatrix1[j, i].Style.BackColor = Color.White;
                                dtgrdwMatrix1[i, j].Value = "1";
                                dtgrdwMatrix1[j, i].Value = "0";
                            }
                            else
                            {
                                dtgrdwMatrix1[i, i].Style.BackColor = Color.Aqua;
                                dtgrdwMatrix1[i, i].ReadOnly = true;
                            }
                        }
                    }
                }
                    break;
                case 1:
                    {
                        for (int i = 0; i < dtgrdwMatrix2.Rows.Count; ++i)
                        {
                            for (int j = 0; j < dtgrdwMatrix2.Columns.Count; ++j)
                            {
                                dtgrdwMatrix2[j, i].Value = "";
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < dtgrdwMatrix3.Rows.Count; ++i)
                        {
                            for (int j = 0; j < dtgrdwMatrix3.Columns.Count; ++j)
                            {
                                dtgrdwMatrix3[j, i].Value = "";
                            }
                        }
                    }
                    break;
                case 3:
                {
                    for (int i = 0; i < dtgrdwMatrix4.Rows.Count; ++i)
                    {
                        for (int j = 0; j < dtgrdwMatrix4.Columns.Count; ++j)
                        {
                            dtgrdwMatrix4[j, i].Value = "";
                        }
                    }
                }
                    break;
            }
        }

        bool check() //проверки
        {

            for (int i = 0; i < dtgrdwMatrix1.Rows.Count; i++)
            {
                for (int j = 0; j < dtgrdwMatrix1.Columns.Count; j++)
                {
                    if (i != j)
                        if ((!el.Contains(dtgrdwMatrix1[i, j].Value)))
                        {
                            MessageBox.Show("Неверно задана матрица предпочтений!");
                            dtgrdwMatrix1[i, j].Value = "1";
                            dtgrdwMatrix1[j, i].Value = "0";
                            return false;
                        }
                        else
                        {
                            float a = Convert.ToSingle(dtgrdwMatrix1[i, j].Value) +
                                      Convert.ToSingle(dtgrdwMatrix1[j, i].Value);
                            if (a !=
                                1.0)
                            {
                                MessageBox.Show("Неверно задана матрица предпочтений!");
                                dtgrdwMatrix1[i, j].Style.BackColor = dtgrdwMatrix1[j, i].Style.BackColor = Color.Red;
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
            //добавляется в 1 метод
            if (txtbxAddAlt.Text != "")
            {
                lstbxAltList.Items.Add("[" + (lstbxAltList.Items.Count + 1).ToString() + "] " + txtbxAddAlt.Text);
                txtbxAddAlt.Text = "";
                dtgrdwMatrix1.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
                dtgrdwMatrix1.Rows.Add();
                dtgrdwMatrix1.Rows[lstbxAltList.Items.Count - 1].HeaderCell.Value = "z" +
                                                                                    lstbxAltList.Items.Count
                                                                                        .ToString();
                for (int i = 0; i < dtgrdwMatrix1.Rows.Count; ++i)
                {
                    int j = dtgrdwMatrix1.Rows.Count - 1;
                    if (i == j)
                    {
                        dtgrdwMatrix1[i, i].Style.BackColor = Color.Aqua;
                        dtgrdwMatrix1[i, i].ReadOnly = true;
                    }
                    else
                    {
                        dtgrdwMatrix1[j, i].Value = "0";
                        dtgrdwMatrix1[i, j].Value = "1";
                    }
                }

                //добавляется во второй метод
               
                dtgrdwMatrix2.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
                //добавляется в третий метод     
               
                dtgrdwMatrix3.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
                //добавляется в четвертый метод
                
                dtgrdwMatrix4.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
            }
        }

        private void Del_altern_Click(object sender, EventArgs e) //удаление выбранных альтернатив
        {
            if ((lstbxAltList.Items.Count != 0) && (lstbxAltList.SelectedIndex != -1))
            {
                int ch = lstbxAltList.SelectedItem.ToString().IndexOf("]");
                int k = int.Parse(lstbxAltList.SelectedItem.ToString().Substring(1, ch - 1));
                //удаляются столбцы в матрицах
                dtgrdwMatrix1.Rows.RemoveAt(k - 1);
                dtgrdwMatrix1.Columns.RemoveAt(k - 1);
                dtgrdwMatrix2.Columns.RemoveAt(k - 1);
                dtgrdwMatrix3.Columns.RemoveAt(k - 1);
                dtgrdwMatrix4.Columns.RemoveAt(k - 1);
                lstbxAltList.Items.RemoveAt(lstbxAltList.SelectedIndex);//удаляется в списке альтернатив
                for (int i = 0; i < lstbxAltList.Items.Count; i++)
                {
                    dtgrdwMatrix1.Rows[i].HeaderCell.Value = "z" + (i + 1).ToString();
                    dtgrdwMatrix1.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix2.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix3.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix4.Columns[i].HeaderText = "z" + (i + 1).ToString();
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

        private void Del_All_Click(object sender, EventArgs e) //очистить все
        {   
            dtgrdwMatrix1.Rows.Clear();
            dtgrdwMatrix1.Columns.Clear();
            dtgrdwMatrix2.Rows.Clear();
            dtgrdwMatrix2.Columns.Clear();
            dtgrdwMatrix3.Rows.Clear();
            dtgrdwMatrix3.Columns.Clear();
            dtgrdwMatrix4.Rows.Clear();
            dtgrdwMatrix4.Columns.Clear();
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
                    }
                    break;
                case 2:
                {
                    listPredp.Items.Clear();
                    dtgrdwExp.Rows.Clear(); 
                }
                    break;
                case 3:
                    {
                        listRang.Items.Clear();
                        dtgrdwExp.Rows.Clear();                       
                    }
                    break;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) //автоподстановка для 1 метода
        {
            if (tabControl1.SelectedIndex == 0)
            {

                dtgrdwMatrix1[e.RowIndex, e.ColumnIndex].Value =
                    (1.0 - Convert.ToSingle(dtgrdwMatrix1[e.ColumnIndex, e.RowIndex].Value));
                check();
            }
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
                        for (int i = 0; i < dtgrdwMatrix1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtgrdwMatrix1.Columns.Count; j++)
                            {
                                if (i == j)
                                    Out += "_\t";
                                else
                                    Out += dtgrdwMatrix1[j, i].Value + "\t";
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
                            for (int i = 0; i < dtgrdwMatrix2.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrdwMatrix2.Columns.Count; j++)
                                {
                                    Out += dtgrdwMatrix2[j, i].Value + "\t";
                                }
                                Out += "\n";
                            }
                            File.WriteAllText(saveFileDialog1.FileName, Out);
                        }
                    }
                    break;
                case 2:
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string Out = string.Empty;
                            for (int i = 0; i < dtgrdwMatrix3.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtgrdwMatrix3.Columns.Count; j++)
                                {
                                    Out += dtgrdwMatrix3[j, i].Value + "\t";
                                }
                                Out += "\n";
                            }
                            File.WriteAllText(saveFileDialog1.FileName, Out);
                        }
                    }
                    break;
                case 3:
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string Out = string.Empty;
                        for (int i = 0; i < dtgrdwMatrix4.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtgrdwMatrix4.Columns.Count; j++)
                            {
                                Out += dtgrdwMatrix4[j, i].Value + "\t";
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
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] alts = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                if ((dtgrdwMatrix1.Rows.Count != 0) && (dtgrdwMatrix1.Rows.Count != alts.Length))
                {
                    MessageBox.Show("Несовпадение по размеру", "Ошибка");
                    return;
                }
                int pos = 0;
                dtgrdwMatrix1.Rows.Clear();

                for (int i = 0; i < alts.Length; i++)
                {
                    dtgrdwMatrix1.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                }

                dtgrdwMatrix1.Rows.Add(alts.Length);
                if (!load)
                {
                    for (int j = 0; j < alts.Length; j++)
                    {
                        dtgrdwMatrix1.Rows[j].HeaderCell.Value = "z" + (j + 1).ToString();
                        for (int i = 0; i < alts.Length; i++)
                        {
                            if (j == i)
                            {
                                dtgrdwMatrix1[i, i].Style.BackColor = Color.Aqua;
                                dtgrdwMatrix1[j, i].ReadOnly = true;
                            }
                            else
                            {
                                dtgrdwMatrix1[j, i].Value = "0";
                                dtgrdwMatrix1[i, j].Value = "1";
                            }
                        }
                    }
                }
                
                //для 2,3,4 метода
                dtgrdwMatrix2.Rows.Clear();
                dtgrdwMatrix3.Rows.Clear();
                dtgrdwMatrix4.Rows.Clear();
                for (int i = 0; i < alts.Length; i++)
                {
                    dtgrdwMatrix2.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                    dtgrdwMatrix3.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                    dtgrdwMatrix4.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
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

        private void матрицаПредпочтенийToolStripMenuItem_Click(object sender, EventArgs e) //загрузка
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        if ((dtgrdwMatrix1.Rows.Count != 0) && (dtgrdwMatrix1.Rows.Count != str.Length))
                        {
                            MessageBox.Show("Несовпадение по размеру", "Ошибка");
                            return;
                        }
                        int pos = 0;
                        dtgrdwMatrix1.Rows.Clear();
                        dtgrdwMatrix1.Columns.Clear();
                        string[] c = new string[str.Length];
                        for (int i = 0; i < c.Length; i++)
                            c[i] = "[" + (i + 1).ToString() + "] ";

                        for (int i = 0; i < str.Length; i++)
                        {
                            dtgrdwMatrix1.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }

                        dtgrdwMatrix1.Rows.Add(str.Length);

                        for (int i = 0; i < dtgrdwMatrix1.RowCount; i++)
                        {
                            string[] buf = str[i].Split('\t');
                            for (int j = 0; j < dtgrdwMatrix1.ColumnCount; j++)
                            {
                                dtgrdwMatrix1[j, i].Value = buf[j];
                                pos++;
                                if (i == j)
                                {
                                    dtgrdwMatrix1[i, i].Value = "";
                                    dtgrdwMatrix1[i, i].ReadOnly = true;
                                    dtgrdwMatrix1[i, i].Style.BackColor = Color.Aqua;
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
                    break;
                case 1:
                    {
                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                            int pos = 0;
                            dtgrdwMatrix2.Rows.Clear();
                            dtgrdwMatrix2.Columns.Clear();
                            string[] buf = str[0].Split('\t');
                            for (int i = 0; i < buf.Length; i++)
                            {
                                dtgrdwMatrix2.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                            }

                            dtgrdwMatrix2.Rows.Add(str.Length);
                            for (int i = 0; i < str.Length; i++)
                            {
                                buf = str[i].Split('\t');
                                for (int j = 0; j < buf.Length; j++)
                                {
                                    dtgrdwMatrix2[j, i].Value = buf[j];
                                    pos++;
                                    dtgrdwMatrix2.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                                }
                            }
                            load = true;
                        }
                    }
                    break;
                case 2:
                    {
                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                            int pos = 0;
                            dtgrdwMatrix3.Rows.Clear();
                            dtgrdwMatrix3.Columns.Clear();
                            string[] buf = str[0].Split('\t');
                            for (int i = 0; i < buf.Length; i++)
                            {
                                dtgrdwMatrix3.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                            }

                            dtgrdwMatrix3.Rows.Add(str.Length);
                            for (int i = 0; i < str.Length; i++)
                            {
                                buf = str[i].Split('\t');
                                for (int j = 0; j < buf.Length; j++)
                                {
                                    dtgrdwMatrix3[j, i].Value = buf[j];
                                    pos++;
                                    dtgrdwMatrix3.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                                }
                            }
                            load = true;
                        }
                    }
                    break;
                case 3:
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string[] str = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                        int pos = 0;
                        dtgrdwMatrix4.Rows.Clear();
                        dtgrdwMatrix4.Columns.Clear();
                        string[] buf = str[0].Split('\t');
                        for (int i = 0; i < buf.Length; i++)
                        {
                            dtgrdwMatrix4.Columns.Add("z" + (i + 1).ToString(), "z" + (i + 1).ToString());
                        }

                        dtgrdwMatrix4.Rows.Add(str.Length);
                        for (int i = 0; i < str.Length; i++)
                        {
                            buf = str[i].Split('\t');
                            for (int j = 0; j < buf.Length; j++)
                            {
                                dtgrdwMatrix4[j, i].Value = buf[j];
                                pos++;
                                dtgrdwMatrix4.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                            }
                        }
                        load = true;
                    }
                }
                    break;
            }
        }

        private void btnEditing_Click(object sender, EventArgs e) //редактирование альтернативы
        {
            lstbxAltList.Items[lstbxAltList.SelectedIndex] = lstbxAltList.SelectedItem.ToString().Substring(0, 4) +
                                                             txtbxEditing.Text;
            txtbxEditing.Text = "";
        }

        private void lstbxAltList_DoubleClick(object sender, EventArgs e)//при двойном щелчке переместить в форму для редактирвоания  
        {
            if (lstbxAltList.SelectedIndices.Count != 0)
            {
                txtbxEditing.Text = lstbxAltList.Items[lstbxAltList.SelectedIndex].ToString().Substring(4);
            }
        }

        #endregion

        private void add_exp_Click(object sender, EventArgs e) //добавить эксперта
        { 
            switch (tabControl1.SelectedIndex)//для 2,3,4 
            {
                case 1:
                case 2:
                case 3:
                {
                    if (txtAddExp.Text != "" && txtAddEval.Text != "")
                    {
                        dtgrdwExp.Rows.Add(txtAddExp.Text, txtAddEval.Text);//добавим в список экспертов
                        txtAddExp.Text = "";
                        txtAddEval.Text = "";
                        //добавим строки в матрицы
                        dtgrdwMatrix2.Rows.Add();
                        dtgrdwMatrix2.Rows[dtgrdwExp.Rows.Count - 1].HeaderCell.Value = "Э" + dtgrdwExp.Rows.Count.ToString();
                        dtgrdwMatrix3.Rows.Add();
                        dtgrdwMatrix3.Rows[dtgrdwExp.Rows.Count - 1].HeaderCell.Value = "Э" + dtgrdwExp.Rows.Count.ToString();
                        dtgrdwMatrix4.Rows.Add();
                        dtgrdwMatrix4.Rows[dtgrdwExp.Rows.Count - 1].HeaderCell.Value = "Э" + dtgrdwExp.Rows.Count.ToString();
                    }
                }
                    break;
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


        private void dtgrdwExp_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) //при двойном щелчке переместить в форму для редактирования
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
                //выпиливаем из матриц строки
                dtgrdwMatrix2.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);
                dtgrdwMatrix3.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);
                dtgrdwMatrix4.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);
                dtgrdwExp.Rows.RemoveAt(dtgrdwExp.SelectedRows[0].Index);//из списка экспертов
                for (int i = 0; i < dtgrdwExp.Rows.Count; i++)
                {
                    dtgrdwMatrix2.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                    dtgrdwMatrix3.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                    dtgrdwMatrix4.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                }
            }

        }
        
        private void списокЭкспертовToolStripMenuItem1_Click(object sender, EventArgs e) //загрузка списка экспертов
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                case 2:
                case 3:
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
                        dtgrdwMatrix2.Rows.Clear();
                        dtgrdwMatrix3.Rows.Clear();
                        dtgrdwMatrix4.Rows.Clear();
                        dtgrdwMatrix2.Rows.Add(alts.Length);
                        dtgrdwMatrix3.Rows.Add(alts.Length);
                        dtgrdwMatrix4.Rows.Add(alts.Length);
                        for (int i = 0; i < alts.Length; i++)
                        {
                            dtgrdwMatrix2.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                            dtgrdwMatrix3.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                            dtgrdwMatrix4.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                        }

                        if (!load)
                        {
                            for (int j = 0; j < alts.Length; j++)
                            {
                                for (int i = 0; i < alts.Length; i++)
                                {
                                    dtgrdwMatrix2[j, i].Value = "";
                                    dtgrdwMatrix3[j, i].Value = "";
                                    dtgrdwMatrix4[j, i].Value = "";
                                }
                            }
                        }
                    }
                }
                    break;
            }
        }

        private void списокЭкспертовToolStripMenuItem_Click(object sender, EventArgs e)//сохранение списка экспертов
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                case 2:
                case 3:
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

        /*private void dtgrdwMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
        }*/


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)//видимость элементов
        {
            if (tabControl1.SelectedIndex == 0)
                groupBox1.Visible = false;
            else
            {
                groupBox1.Visible = true;
            }
          
        }


    }
}
