namespace SisAn
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.альтернативаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЭкспертовToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.матрицаПредпочтенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЭкспертовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокАльтернативToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.матрицаПредпочтенийToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lstbxAltList = new System.Windows.Forms.ListBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.txtbxAddAlt = new System.Windows.Forms.TextBox();
            this.btnAddAlt = new System.Windows.Forms.Button();
            this.btnDelAlt = new System.Windows.Forms.Button();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.btnEditing = new System.Windows.Forms.Button();
            this.txtbxEditing = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstbxAltNewList = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.list_Alt_new = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listPredp = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listRang = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddExp = new System.Windows.Forms.TextBox();
            this.add_exp = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.del_exp = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.edit_exp = new System.Windows.Forms.Button();
            this.txtAddEval = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtExpEdit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtgrdwExp = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtgrdwMatrix1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.dtgrdwMatrix2 = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.dtgrdwMatrix3 = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.dtgrdwMatrix4 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwExp)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix4)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.очиститьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1115, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.альтернативаToolStripMenuItem,
            this.списокЭкспертовToolStripMenuItem1,
            this.матрицаПредпочтенийToolStripMenuItem});
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            // 
            // альтернативаToolStripMenuItem
            // 
            this.альтернативаToolStripMenuItem.Name = "альтернативаToolStripMenuItem";
            this.альтернативаToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.альтернативаToolStripMenuItem.Text = "Список альтернатив";
            this.альтернативаToolStripMenuItem.Click += new System.EventHandler(this.альтернативаToolStripMenuItem_Click);
            // 
            // списокЭкспертовToolStripMenuItem1
            // 
            this.списокЭкспертовToolStripMenuItem1.Name = "списокЭкспертовToolStripMenuItem1";
            this.списокЭкспертовToolStripMenuItem1.Size = new System.Drawing.Size(205, 22);
            this.списокЭкспертовToolStripMenuItem1.Text = "Список экспертов";
            this.списокЭкспертовToolStripMenuItem1.Click += new System.EventHandler(this.списокЭкспертовToolStripMenuItem1_Click);
            // 
            // матрицаПредпочтенийToolStripMenuItem
            // 
            this.матрицаПредпочтенийToolStripMenuItem.Name = "матрицаПредпочтенийToolStripMenuItem";
            this.матрицаПредпочтенийToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.матрицаПредпочтенийToolStripMenuItem.Text = "Матрица предпочтений";
            this.матрицаПредпочтенийToolStripMenuItem.Click += new System.EventHandler(this.матрицаПредпочтенийToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокЭкспертовToolStripMenuItem,
            this.списокАльтернативToolStripMenuItem,
            this.матрицаПредпочтенийToolStripMenuItem1});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // списокЭкспертовToolStripMenuItem
            // 
            this.списокЭкспертовToolStripMenuItem.Name = "списокЭкспертовToolStripMenuItem";
            this.списокЭкспертовToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.списокЭкспертовToolStripMenuItem.Text = "Список экспертов";
            this.списокЭкспертовToolStripMenuItem.Click += new System.EventHandler(this.списокЭкспертовToolStripMenuItem_Click);
            // 
            // списокАльтернативToolStripMenuItem
            // 
            this.списокАльтернативToolStripMenuItem.Name = "списокАльтернативToolStripMenuItem";
            this.списокАльтернативToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.списокАльтернативToolStripMenuItem.Text = "Список альтернатив";
            this.списокАльтернативToolStripMenuItem.Click += new System.EventHandler(this.списокАльтернативToolStripMenuItem_Click);
            // 
            // матрицаПредпочтенийToolStripMenuItem1
            // 
            this.матрицаПредпочтенийToolStripMenuItem1.Name = "матрицаПредпочтенийToolStripMenuItem1";
            this.матрицаПредпочтенийToolStripMenuItem1.Size = new System.Drawing.Size(205, 22);
            this.матрицаПредпочтенийToolStripMenuItem1.Text = "Матрица предпочтений";
            this.матрицаПредпочтенийToolStripMenuItem1.Click += new System.EventHandler(this.матрицаПредпочтенийToolStripMenuItem1_Click);
            // 
            // очиститьToolStripMenuItem
            // 
            this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.очиститьToolStripMenuItem.Text = "Очистить";
            this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.очиститьToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lstbxAltList
            // 
            this.lstbxAltList.FormattingEnabled = true;
            this.lstbxAltList.Location = new System.Drawing.Point(13, 82);
            this.lstbxAltList.Name = "lstbxAltList";
            this.lstbxAltList.Size = new System.Drawing.Size(300, 160);
            this.lstbxAltList.TabIndex = 2;
            this.lstbxAltList.DoubleClick += new System.EventHandler(this.lstbxAltList_DoubleClick);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(968, 277);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(135, 60);
            this.btnSort.TabIndex = 3;
            this.btnSort.Text = "Упорядочить";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtbxAddAlt
            // 
            this.txtbxAddAlt.Location = new System.Drawing.Point(13, 277);
            this.txtbxAddAlt.Multiline = true;
            this.txtbxAddAlt.Name = "txtbxAddAlt";
            this.txtbxAddAlt.Size = new System.Drawing.Size(301, 29);
            this.txtbxAddAlt.TabIndex = 6;
            // 
            // btnAddAlt
            // 
            this.btnAddAlt.Location = new System.Drawing.Point(322, 266);
            this.btnAddAlt.Name = "btnAddAlt";
            this.btnAddAlt.Size = new System.Drawing.Size(100, 40);
            this.btnAddAlt.TabIndex = 7;
            this.btnAddAlt.Text = "Добавить альтернативу\r\n";
            this.btnAddAlt.UseVisualStyleBackColor = true;
            this.btnAddAlt.Click += new System.EventHandler(this.Add_altern_Click);
            // 
            // btnDelAlt
            // 
            this.btnDelAlt.Location = new System.Drawing.Point(428, 266);
            this.btnDelAlt.Name = "btnDelAlt";
            this.btnDelAlt.Size = new System.Drawing.Size(100, 40);
            this.btnDelAlt.TabIndex = 8;
            this.btnDelAlt.Text = "Удалить альтернативу";
            this.btnDelAlt.UseVisualStyleBackColor = true;
            this.btnDelAlt.Click += new System.EventHandler(this.Del_altern_Click);
            // 
            // btnDelAll
            // 
            this.btnDelAll.Location = new System.Drawing.Point(968, 82);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(135, 60);
            this.btnDelAll.TabIndex = 9;
            this.btnDelAll.Text = "Очистить все\r\n";
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.Del_All_Click);
            // 
            // btnEditing
            // 
            this.btnEditing.Location = new System.Drawing.Point(322, 314);
            this.btnEditing.Name = "btnEditing";
            this.btnEditing.Size = new System.Drawing.Size(100, 40);
            this.btnEditing.TabIndex = 10;
            this.btnEditing.Text = "Редактировать";
            this.btnEditing.UseVisualStyleBackColor = true;
            this.btnEditing.Click += new System.EventHandler(this.btnEditing_Click);
            // 
            // txtbxEditing
            // 
            this.txtbxEditing.Location = new System.Drawing.Point(13, 325);
            this.txtbxEditing.Multiline = true;
            this.txtbxEditing.Name = "txtbxEditing";
            this.txtbxEditing.Size = new System.Drawing.Size(300, 28);
            this.txtbxEditing.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(551, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 495);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.dtgrdwMatrix1);
            this.tabPage1.Controls.Add(this.lstbxAltNewList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 469);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Метод парных сравнений";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstbxAltNewList
            // 
            this.lstbxAltNewList.FormattingEnabled = true;
            this.lstbxAltNewList.Location = new System.Drawing.Point(0, 20);
            this.lstbxAltNewList.Name = "lstbxAltNewList";
            this.lstbxAltNewList.Size = new System.Drawing.Size(301, 134);
            this.lstbxAltNewList.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.dtgrdwMatrix2);
            this.tabPage2.Controls.Add(this.list_Alt_new);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 469);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Метод взвешенных экспертных оценок";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // list_Alt_new
            // 
            this.list_Alt_new.FormattingEnabled = true;
            this.list_Alt_new.Location = new System.Drawing.Point(3, 20);
            this.list_Alt_new.Name = "list_Alt_new";
            this.list_Alt_new.Size = new System.Drawing.Size(301, 134);
            this.list_Alt_new.TabIndex = 22;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.dtgrdwMatrix3);
            this.tabPage3.Controls.Add(this.listPredp);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(403, 469);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Метод предпочтений";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listPredp
            // 
            this.listPredp.FormattingEnabled = true;
            this.listPredp.Location = new System.Drawing.Point(3, 20);
            this.listPredp.Name = "listPredp";
            this.listPredp.Size = new System.Drawing.Size(301, 134);
            this.listPredp.TabIndex = 23;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.dtgrdwMatrix4);
            this.tabPage4.Controls.Add(this.listRang);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(403, 469);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Метод ранга";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listRang
            // 
            this.listRang.FormattingEnabled = true;
            this.listRang.Location = new System.Drawing.Point(0, 20);
            this.listRang.Name = "listRang";
            this.listRang.Size = new System.Drawing.Size(301, 134);
            this.listRang.TabIndex = 23;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(306, 141);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Полное попарное сопоставление";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Исходный список альтернатив:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Проблема:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(761, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Добавление альтернативы:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Редактирование альтернативы:";
            // 
            // txtAddExp
            // 
            this.txtAddExp.Location = new System.Drawing.Point(1, 163);
            this.txtAddExp.Multiline = true;
            this.txtAddExp.Name = "txtAddExp";
            this.txtAddExp.Size = new System.Drawing.Size(257, 26);
            this.txtAddExp.TabIndex = 29;
            // 
            // add_exp
            // 
            this.add_exp.Location = new System.Drawing.Point(416, 152);
            this.add_exp.Name = "add_exp";
            this.add_exp.Size = new System.Drawing.Size(100, 40);
            this.add_exp.TabIndex = 30;
            this.add_exp.Text = "Добавить эксперта\r\n";
            this.add_exp.UseVisualStyleBackColor = true;
            this.add_exp.Click += new System.EventHandler(this.add_exp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Редактирование эксперта:";
            // 
            // del_exp
            // 
            this.del_exp.Location = new System.Drawing.Point(416, 100);
            this.del_exp.Name = "del_exp";
            this.del_exp.Size = new System.Drawing.Size(100, 40);
            this.del_exp.TabIndex = 31;
            this.del_exp.Text = "Удалить эксперта";
            this.del_exp.UseVisualStyleBackColor = true;
            this.del_exp.Click += new System.EventHandler(this.del_exp_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "Оценка компетентности:";
            // 
            // edit_exp
            // 
            this.edit_exp.Location = new System.Drawing.Point(268, 197);
            this.edit_exp.Name = "edit_exp";
            this.edit_exp.Size = new System.Drawing.Size(100, 40);
            this.edit_exp.TabIndex = 27;
            this.edit_exp.Text = "Редактировать ";
            this.edit_exp.UseVisualStyleBackColor = true;
            this.edit_exp.Click += new System.EventHandler(this.edit_exp_Click);
            // 
            // txtAddEval
            // 
            this.txtAddEval.Location = new System.Drawing.Point(268, 163);
            this.txtAddEval.Multiline = true;
            this.txtAddEval.Name = "txtAddEval";
            this.txtAddEval.ReadOnly = true;
            this.txtAddEval.Size = new System.Drawing.Size(130, 26);
            this.txtAddEval.TabIndex = 33;
            this.txtAddEval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddEval_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-1, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Добавление эксперта:";
            // 
            // txtExpEdit
            // 
            this.txtExpEdit.Location = new System.Drawing.Point(2, 217);
            this.txtExpEdit.Multiline = true;
            this.txtExpEdit.Name = "txtExpEdit";
            this.txtExpEdit.Size = new System.Drawing.Size(256, 32);
            this.txtExpEdit.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Эксперты:";
            // 
            // dtgrdwExp
            // 
            this.dtgrdwExp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdwExp.Location = new System.Drawing.Point(0, 19);
            this.dtgrdwExp.Name = "dtgrdwExp";
            this.dtgrdwExp.ReadOnly = true;
            this.dtgrdwExp.Size = new System.Drawing.Size(398, 121);
            this.dtgrdwExp.TabIndex = 32;
            this.dtgrdwExp.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgrdwExp_CellMouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.del_exp);
            this.groupBox1.Controls.Add(this.dtgrdwExp);
            this.groupBox1.Controls.Add(this.txtAddExp);
            this.groupBox1.Controls.Add(this.add_exp);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtExpEdit);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.edit_exp);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtAddEval);
            this.groupBox1.Location = new System.Drawing.Point(14, 356);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 265);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Исходная матрица";
            // 
            // dtgrdwMatrix1
            // 
            this.dtgrdwMatrix1.AllowUserToOrderColumns = true;
            this.dtgrdwMatrix1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgrdwMatrix1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdwMatrix1.Location = new System.Drawing.Point(6, 177);
            this.dtgrdwMatrix1.Name = "dtgrdwMatrix1";
            this.dtgrdwMatrix1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgrdwMatrix1.Size = new System.Drawing.Size(321, 261);
            this.dtgrdwMatrix1.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Исходная матрица";
            // 
            // dtgrdwMatrix2
            // 
            this.dtgrdwMatrix2.AllowUserToOrderColumns = true;
            this.dtgrdwMatrix2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgrdwMatrix2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdwMatrix2.Location = new System.Drawing.Point(6, 176);
            this.dtgrdwMatrix2.Name = "dtgrdwMatrix2";
            this.dtgrdwMatrix2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgrdwMatrix2.Size = new System.Drawing.Size(321, 261);
            this.dtgrdwMatrix2.TabIndex = 48;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 51;
            this.label12.Text = "Исходная матрица";
            // 
            // dtgrdwMatrix3
            // 
            this.dtgrdwMatrix3.AllowUserToOrderColumns = true;
            this.dtgrdwMatrix3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgrdwMatrix3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdwMatrix3.Location = new System.Drawing.Point(3, 176);
            this.dtgrdwMatrix3.Name = "dtgrdwMatrix3";
            this.dtgrdwMatrix3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgrdwMatrix3.Size = new System.Drawing.Size(321, 261);
            this.dtgrdwMatrix3.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-3, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Исходная матрица";
            // 
            // dtgrdwMatrix4
            // 
            this.dtgrdwMatrix4.AllowUserToOrderColumns = true;
            this.dtgrdwMatrix4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgrdwMatrix4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdwMatrix4.Location = new System.Drawing.Point(0, 176);
            this.dtgrdwMatrix4.Name = "dtgrdwMatrix4";
            this.dtgrdwMatrix4.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgrdwMatrix4.Size = new System.Drawing.Size(321, 261);
            this.dtgrdwMatrix4.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(166, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "Итоговый список альтернатив:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Итоговый список альтернатив:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(166, 13);
            this.label15.TabIndex = 52;
            this.label15.Text = "Итоговый список альтернатив:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 13);
            this.label16.TabIndex = 52;
            this.label16.Text = "Итоговый список альтернатив:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(968, 164);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 94);
            this.checkedListBox1.TabIndex = 44;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 641);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstbxAltList);
            this.Controls.Add(this.btnEditing);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnDelAlt);
            this.Controls.Add(this.txtbxEditing);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnAddAlt);
            this.Controls.Add(this.txtbxAddAlt);
            this.Controls.Add(this.btnDelAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwExp)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdwMatrix4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox lstbxAltList;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.ToolStripMenuItem альтернативаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem матрицаПредпочтенийToolStripMenuItem;
        private System.Windows.Forms.TextBox txtbxAddAlt;
        private System.Windows.Forms.Button btnAddAlt;
        private System.Windows.Forms.Button btnDelAlt;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.ToolStripMenuItem матрицаПредпочтенийToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem списокАльтернативToolStripMenuItem;
        private System.Windows.Forms.Button btnEditing;
        private System.Windows.Forms.TextBox txtbxEditing;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox lstbxAltNewList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox list_Alt_new;
        private System.Windows.Forms.ToolStripMenuItem списокЭкспертовToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem списокЭкспертовToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddExp;
        private System.Windows.Forms.Button add_exp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button del_exp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button edit_exp;
        private System.Windows.Forms.TextBox txtAddEval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtExpEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dtgrdwExp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listPredp;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox listRang;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dtgrdwMatrix1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dtgrdwMatrix2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dtgrdwMatrix3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dtgrdwMatrix4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}

