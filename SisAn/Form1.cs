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
        int _altCount = 0;
        int _expCount = 0;
        bool load = false;
        List<DataGridView> myGrid = new List<DataGridView>();
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
                    case 4://если 5 алгоритм
                        {
                            if (checkBox.GetItemChecked(ind))
                            {
                                
                                float[,] F = new float[_expCount, _altCount];
                                float[,] _fi=new float[_expCount, _altCount];
                                float[] V=new float[_altCount];
                                string[] sortedList = new string[_altCount];
                                for (int i = 0; i < _altCount; i++)
                                {
                                    sortedList[i] = lstbxAltList.Items[i].ToString();
                                }
                                int N = 0;

                                for (int i = 0; i < _expCount; i++)
                                {
                                    for (int j = 0; j < _altCount; j++)
                                    {
                                        F[i, j] = 0;
                                        _fi[i, j] = 0;
                                    }
                                }
                                float number = 0;
                                for (int i = 0; i < _expCount; i++)//подсчитали сумму в строках каждой матрицы
                                {                           
                                    for (int j = 0; j < _altCount; j++)
                                    {
                                       
                                        for (int jj = 0; jj < myGrid[i].ColumnCount; jj++)
                                        {
                                            if (jj != j)
                                            {
                                                if (myGrid[i][jj, j].Value.ToString().Contains('/'))
                                                {
                                                    string[] numb = myGrid[i][jj, j].Value.ToString().Split('/');
                                                    sizeSh.Text = numb[1].ToString();
                                                    //дробь
                                                    number = Convert.ToSingle(numb[0])/Convert.ToSingle(numb[1]);
                                                }
                                                else
                                                    number = Convert.ToSingle(myGrid[i][jj, j].Value.ToString());
                                                F[i, j] += number;

                                            }
                                        }
                                    }
                                }
                                N = _altCount*(_altCount - 1);
                                //определим нормированные оценки
                                for (int i = 0; i < _expCount; i++)
                                {
                                    for (int j = 0; j < _altCount; j++)
                                    {
                                         _fi[i, j] = F[i,j]/N;

                                    }
                                }
                                //найдем искомые веса альтернатив
                                for (int i = 0; i < _expCount; i++)//подсчитали сумму в строках каждой матрицы
                                {
                                    for (int j = 0; j < _altCount; j++)
                                    {
                                        V[j] += _fi[i, j];

                                    }
                                }
                                BubbleSort(V, sortedList); //сортируем
                                listPar.Items.Clear();
                                listPar.Items.AddRange(sortedList);

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
                case 4:
                {
                    
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
                _altCount++;
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
                int ind = 0;
                while (_expCount != dtgrdwMatrix2.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix2.Rows.Add();
                    dtgrdwMatrix2.Rows[ind].HeaderCell.Value = "Э" + (ind + 1).ToString();
                    ind++;
                }
                //добавляется в третий метод     

                dtgrdwMatrix3.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
                int ind2 = 0;
                while (_expCount != dtgrdwMatrix3.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix3.Rows.Add();
                    dtgrdwMatrix3.Rows[ind2].HeaderCell.Value = "Э" + (ind2 + 1).ToString();
                    ind2++;
                }
                //добавляется в четвертый метод

                dtgrdwMatrix4.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                    "z" + lstbxAltList.Items.Count.ToString());
                int ind3 = 0;
                while (_expCount != dtgrdwMatrix4.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix4.Rows.Add();
                    dtgrdwMatrix4.Rows[ind3].HeaderCell.Value = "Э" + (ind3 + 1).ToString();
                    ind3++;
                }
                //добавляется 5 метод
                int ind4 = 0;
                while (_expCount != tabControl2.TabCount)//если добавляем альтернативы после добавления экспертов
                {
                    TabPage newTabPage = new TabPage();
                    tabControl2.TabPages.Add(newTabPage); //добавим новую вкладку
                    DataGridView dtrdwView = new DataGridView(); //создадим на ней табличку

                    dtrdwView.Name = "dtrdwView" + _expCount.ToString();
                    dtrdwView.AutoResizeColumns();
                    //дадим ей имя, для удобства обращения к ней
                    dtrdwView.Size = new Size(321, 261);
                    myGrid.Add(dtrdwView);

                    newTabPage.Controls.Add(myGrid[ind4]);
                    ind4++;
                }
                  foreach (var dg in myGrid)
                    {
                        dg.Columns.Add("z" + lstbxAltList.Items.Count.ToString(),
                            "z" + lstbxAltList.Items.Count.ToString());
                        dg.Rows.Add();
                        dg.Rows[_altCount-1].HeaderCell.Value = "Z" + _altCount.ToString();
                        dg.AllowUserToAddRows = false;
                        dg.AutoSize = true;
                        dg.AutoResizeColumns();
                    }
                

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
                foreach (var dg in myGrid)
                {
                    dg.Columns.RemoveAt(k-1);
                    dg.Rows.RemoveAt(k-1);
                }
                lstbxAltList.Items.RemoveAt(lstbxAltList.SelectedIndex);//удаляется в списке альтернатив
                for (int i = 0; i < lstbxAltList.Items.Count; i++)
                {
                    dtgrdwMatrix1.Rows[i].HeaderCell.Value = "z" + (i + 1).ToString();
                    dtgrdwMatrix1.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix2.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix3.Columns[i].HeaderText = "z" + (i + 1).ToString();
                    dtgrdwMatrix4.Columns[i].HeaderText = "z" + (i + 1).ToString();

                    foreach (var dg in myGrid)
                    {
                        dg.Columns[i].HeaderText="z" + (i + 1).ToString();
                        dg.Rows[i].HeaderCell.Value="z" + (i + 1).ToString();
                    }
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
            _expCount = 0;
            _altCount = 0;
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
                case 4:
                    {
                        listPar.Items.Clear();
                        dtgrdwExp.Rows.Clear();
                        tabControl2.TabPages.Clear();
                        myGrid.Clear();
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
                    _altCount++;
                    alts[i - 1] = "[" + i.ToString() + "] " + alts[i - 1];
                }
                lstbxAltList.Items.AddRange(alts);
                int ind = 0;
                while (_expCount != dtgrdwMatrix2.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix2.Rows.Add();
                    dtgrdwMatrix2.Rows[ind].HeaderCell.Value = "Э" + (ind + 1).ToString();
                    ind++;
                }
                int ind2 = 0;
                while (_expCount != dtgrdwMatrix3.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix3.Rows.Add();
                    dtgrdwMatrix3.Rows[ind2].HeaderCell.Value = "Э" + (ind2 + 1).ToString();
                    ind2++;
                }
                int ind3 = 0;
                while (_expCount != dtgrdwMatrix4.Rows.Count)//если добавляем альтернативы после добавления экспертов
                {
                    dtgrdwMatrix4.Rows.Add();
                    dtgrdwMatrix4.Rows[ind3].HeaderCell.Value = "Э" + (ind3 + 1).ToString();
                    ind3++;
                }
                for (int i = 0; i < _expCount; i++)
                {
                    TabPage newTabPage = new TabPage();
                    tabControl2.TabPages.Add(newTabPage); //добавим новую вкладку
                    DataGridView dtrdwView = new DataGridView(); //создадим на ней табличку
                    dtrdwView.AllowUserToAddRows = false;
                    dtrdwView.Name = "dtrdwView" + _expCount.ToString();   
                    dtrdwView.Size = new Size(321, 261);
                   //dtrdwView.AutoResizeColumns();
                    //дадим ей имя, для удобства обращения к ней
                    myGrid.Add(dtrdwView);
                    newTabPage.Controls.Add(myGrid[i]);
                    for (int j = 0; j < _altCount; j++)
                    {
                        
                        dtrdwView.Columns.Add("z" + (j+1).ToString(),
                            "z" + (j+1).ToString());
                        dtrdwView.Rows.Add();
                        dtrdwView.Columns[j].Width = 40;
                        dtrdwView.Rows[j].HeaderCell.Value = "z" + (j + 1).ToString();
                    }
                   
                    
                }
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
                case 4:
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string str = File.ReadAllText(openFileDialog1.FileName, Encoding.Default);
                        string[] matrix = str.Split('+');

                        for (int i = 0; i < matrix.Length; i++)
                        {
                            int pos = 0;

                            string[] strings = matrix[i].Split('\n');
                            for (int j = 0; j < strings.Length ; j++)
                            {
                                string[] buf = strings[j].Split('\t','\r');
                                for (int jj = 0; jj < myGrid[i].ColumnCount; jj++)
                                {
                                    if(buf[jj].Contains('/'))//чтобы вывести размер шкалы
                                    {
                                        string[] numb = buf[jj].Split('/');
                                        sizeSh.Text = numb[1].ToString();
                                    }
                                    myGrid[i][jj, j].Value = buf[jj];
                                    myGrid[i].AutoResizeColumns();
                                    pos++;
                                    if (j == jj)
                                    {
                                        myGrid[i][j, j].Value = "";
                                        myGrid[i][j, j].ReadOnly = true;
                                        myGrid[i][j, j].Style.BackColor = Color.Aqua;

                                    }
                                }
                            }
                        }
                        
                    }
                }
                    break;
            }
        }

        private void btnEditing_Click(object sender, EventArgs e) //редактирование альтернативы
        {
            if (txtbxEditing.Text != "")
            {
                lstbxAltList.Items[lstbxAltList.SelectedIndex] = lstbxAltList.SelectedItem.ToString().Substring(0, 4) +
                                                                 txtbxEditing.Text;
            }
            txtbxEditing.Text = "";
        }

        private void lstbxAltList_DoubleClick(object sender, EventArgs e)//при двойном щелчке переместить в форму для редактирвоания  
        {
            if (lstbxAltList.SelectedIndices.Count != 0)
            {
                txtbxEditing.Text = lstbxAltList.Items[lstbxAltList.SelectedIndex].ToString().Substring(4);
            }
        }



        private void add_exp_Click(object sender, EventArgs e) //добавить эксперта
        {
            switch (tabControl1.SelectedIndex)//для 2,3,4 
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        if (txtAddExp.Text != "" && txtAddEval.Text != "")
                        {
                            _expCount++;
                            dtgrdwExp.Rows.Add(txtAddExp.Text, txtAddEval.Text);//добавим в список экспертов
                            dtgrdwExp.Rows[_expCount-1].HeaderCell.Value = _expCount.ToString();
                            txtAddExp.Text = "";
                            txtAddEval.Text = "";
                            //для 5 метода
                            TabPage newTabPage = new TabPage();
                            tabControl2.TabPages.Add(newTabPage); //добавим новую вкладку
                            newTabPage.Text = "Э" + _expCount.ToString();
                            DataGridView dtrdwView = new DataGridView(); //создадим на ней табличку

                            dtrdwView.Name = "dtrdwView" + _expCount.ToString();
                            dtrdwView.AllowUserToAddRows = false;
                            dtrdwView.Size = new Size(321, 261);
                            dtrdwView.AutoResizeColumns();
                            int ind = 0;
                            while (ind != _altCount)
                            {
                                dtrdwView.Columns.Add("z" + (ind+1).ToString(),
                                    "z" + (ind+1).ToString());
                                
                                dtrdwView.Rows.Add();
                                dtrdwView.Rows[ind].HeaderCell.Value = "Z" + (ind + 1).ToString();
                                dtrdwView.Columns[ind].Width = 40;
                                ind++;
                            }
                            //дадим ей имя, для удобства обращения к ней
                            myGrid.Add(dtrdwView);

                            newTabPage.Controls.Add(myGrid[_expCount - 1]);
                            //для остальных методов
                            //добавим строки в матрицы
                            if (lstbxAltList.Items.Count != 0)
                            {
                                dtgrdwMatrix2.Rows.Add();
                                dtgrdwMatrix2.Rows[_expCount - 1].HeaderCell.Value = "Э" + _expCount;
                                dtgrdwMatrix3.Rows.Add();
                                dtgrdwMatrix3.Rows[_expCount - 1].HeaderCell.Value = "Э" + _expCount;
                                dtgrdwMatrix4.Rows.Add();
                                dtgrdwMatrix4.Rows[_expCount - 1].HeaderCell.Value = "Э" + _expCount;
                            }
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
            if (lstbxAltList.Items.Count != 0)
            {
                _expCount--;
                //выпиливаем из матриц строки
                dtgrdwMatrix2.Rows.RemoveAt(dtgrdwExp.SelectedCells[0].RowIndex);
                dtgrdwMatrix3.Rows.RemoveAt(dtgrdwExp.SelectedCells[0].RowIndex);
                dtgrdwMatrix4.Rows.RemoveAt(dtgrdwExp.SelectedCells[0].RowIndex);
                dtgrdwExp.Rows.RemoveAt(dtgrdwExp.SelectedCells[0].RowIndex);//из списка экспертов
                
                tabControl2.TabPages.RemoveAt(dtgrdwExp.SelectedCells[0].RowIndex);
                for (int i = 0; i < dtgrdwExp.Rows.Count; i++)
                {
                    dtgrdwExp.Rows[i].HeaderCell.Value = (i+1).ToString();
                    tabControl2.TabPages[i].Text = "Э" + (i + 1).ToString();
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
                case 4:
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
                            {
                                c[i] = "[" + (i + 1).ToString() + "] ";
                            }
                            //для списка экспертов
                            dtgrdwExp.Columns.Add("exp", "Эксперт");
                            dtgrdwExp.Columns.Add("Eval", "Оценка");
                            dtgrdwExp.Rows.Add(alts.Length);
                            for (int i = 0; i < dtgrdwExp.RowCount; i++)
                            {
                                dtgrdwExp.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                string[] buf = alts[i].Split('\t');
                                for (int j = 0; j < dtgrdwExp.ColumnCount; j++)
                                {
                                    dtgrdwExp[j, i].Value = buf[j];
                                    pos++;
                                }
                                _expCount++;
                            }

                            int posit = 0;

                            if (_altCount != 0) //если уже добавлены альтернативы
                            {
                                dtgrdwMatrix2.Rows.Clear();
                                dtgrdwMatrix3.Rows.Clear();
                                dtgrdwMatrix4.Rows.Clear();
                                
                                for (int i = 0; i < _expCount; i++)
                                {
                                    TabPage newTabPage = new TabPage();
                                    newTabPage.Text = "Э" + (i + 1).ToString();
                                    tabControl2.TabPages.Add(newTabPage); //добавим новую вкладку
                                    DataGridView dtrdwView = new DataGridView(); //создадим на ней табличку

                                    dtrdwView.Name = "dtrdwView" + _expCount.ToString();
                                    dtrdwView.AllowUserToAddRows = false;
                                    dtrdwView.Size = new Size(321, 261);
                                    dtrdwView.AutoResizeColumns();

                                    //дадим ей имя, для удобства обращения к ней
                                    myGrid.Add(dtrdwView);

                                    newTabPage.Controls.Add(myGrid[i]);
                                    int ind = 0;
                                    while (ind != _altCount)
                                    {
                                        dtrdwView.Columns.Add("z" + (ind+1).ToString(),
                                            "z" + (ind+1).ToString());
                                        dtrdwView.Rows.Add();
                                        dtrdwView.Rows[ind].HeaderCell.Value = "z" + (ind + 1).ToString();
                                        dtrdwView.Columns[ind].Width = 40;
                                        ind++;
                                    }
                                    dtgrdwMatrix2.Rows.Add();
                                    dtgrdwMatrix3.Rows.Add();
                                    dtgrdwMatrix4.Rows.Add();
                                    dtgrdwMatrix2.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                                    dtgrdwMatrix3.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                                    dtgrdwMatrix4.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();
                                }
                                for (int j = 0; j < _expCount; j++)
                                {
                                    for (int i = 0; i < _expCount; i++)
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
                case 4:
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
