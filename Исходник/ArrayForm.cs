using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class ArrayForm : Form
    {
        private ArrayHelper arrayHelper;
        private int[] array;

        public ArrayForm()
        {
            InitializeComponent();
        }

        private void ArrayForm_Load(object sender, EventArgs e)
        {
            // Инициализация формы
            lblArraySize.Text = "Размер массива:";
            btnCreateDefaultArray.Text = "Создать массив по умолчанию";
            btnGenerateRandomArray.Text = "Сгенерировать случайный массив";
            btnSortArray.Text = "Сортировать массив";
            btnFindMaxMin.Text = "Найти макс/мин";
            btnCalculateAverage.Text = "Среднее арифметическое";
        }

        private void btnCreateDefaultArray_Click(object sender, EventArgs e)
        {
            arrayHelper = new ArrayHelper();
            array = arrayHelper.GetArray();
            DisplayArray();
        }

        private void btnGenerateRandomArray_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtArraySize.Text, out int size) && size > 0)
            {
                arrayHelper = new ArrayHelper(size);
                array = arrayHelper.GetArray();
                DisplayArray();
            }
            else
            {
                MessageBox.Show("Введите корректный размер массива.");
            }
        }

        private void btnSortArray_Click(object sender, EventArgs e)
        {
            arrayHelper.SortArray();
            array = arrayHelper.GetArray();
            DisplayArray();
        }

        private void btnFindMaxMin_Click(object sender, EventArgs e)
        {
            int max = arrayHelper.FindMax();
            int min = arrayHelper.FindMin();
            lblResult.Text = $"Максимум: {max}, Минимум: {min}";
            HighlightMaxMin(max, min);
        }

        private void btnCalculateAverage_Click(object sender, EventArgs e)
        {
            double average = arrayHelper.CalculateAverage();
            lblResult.Text = $"Среднее арифметическое: {average}";
        }

        private void DisplayArray()
        {
            dgvArray.Rows.Clear();
            dgvArray.Columns.Clear();
            dgvArray.Columns.Add("Index", "Индекс");
            dgvArray.Columns.Add("Value", "Значение");

            for (int i = 0; i < array.Length; i++)
            {
                dgvArray.Rows.Add(i, array[i]);
            }
        }

        private void HighlightMaxMin(int max, int min)
        {
            foreach (DataGridViewRow row in dgvArray.Rows)
            {
                if (row.Cells[1].Value != null && int.TryParse(row.Cells[1].Value.ToString(), out int value))
                {
                    if (value == max)
                    {
                        row.Cells[1].Style.BackColor = Color.Red;
                    }
                    else if (value == min)
                    {
                        row.Cells[1].Style.BackColor = Color.Blue;
                    }
                    else
                    {
                        row.Cells[1].Style.BackColor = Color.White;
                    }
                }
            }
        }


        private void InitializeComponent()
        {
            this.txtArraySize = new System.Windows.Forms.TextBox();
            this.lblArraySize = new System.Windows.Forms.Label();
            this.btnCreateDefaultArray = new System.Windows.Forms.Button();
            this.btnGenerateRandomArray = new System.Windows.Forms.Button();
            this.dgvArray = new System.Windows.Forms.DataGridView();
            this.btnSortArray = new System.Windows.Forms.Button();
            this.btnFindMaxMin = new System.Windows.Forms.Button();
            this.btnCalculateAverage = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArray)).BeginInit();
            this.SuspendLayout();
            // 
            // txtArraySize
            // 
            this.txtArraySize.Location = new System.Drawing.Point(12, 25);
            this.txtArraySize.Name = "txtArraySize";
            this.txtArraySize.Size = new System.Drawing.Size(100, 20);
            this.txtArraySize.TabIndex = 0;
            // 
            // lblArraySize
            // 
            this.lblArraySize.AutoSize = true;
            this.lblArraySize.Location = new System.Drawing.Point(12, 9);
            this.lblArraySize.Name = "lblArraySize";
            this.lblArraySize.Size = new System.Drawing.Size(82, 13);
            this.lblArraySize.TabIndex = 1;
            this.lblArraySize.Text = "Размер массива:";
            // 
            // btnCreateDefaultArray
            // 
            this.btnCreateDefaultArray.Location = new System.Drawing.Point(12, 51);
            this.btnCreateDefaultArray.Name = "btnCreateDefaultArray";
            this.btnCreateDefaultArray.Size = new System.Drawing.Size(200, 23);
            this.btnCreateDefaultArray.TabIndex = 2;
            this.btnCreateDefaultArray.Text = "Создать массив по умолчанию";
            this.btnCreateDefaultArray.UseVisualStyleBackColor = true;
            this.btnCreateDefaultArray.Click += new System.EventHandler(this.btnCreateDefaultArray_Click);
            // 
            // btnGenerateRandomArray
            // 
            this.btnGenerateRandomArray.Location = new System.Drawing.Point(12, 80);
            this.btnGenerateRandomArray.Name = "btnGenerateRandomArray";
            this.btnGenerateRandomArray.Size = new System.Drawing.Size(200, 23);
            this.btnGenerateRandomArray.TabIndex = 3;
            this.btnGenerateRandomArray.Text = "Сгенерировать случайный массив";
            this.btnGenerateRandomArray.UseVisualStyleBackColor = true;
            this.btnGenerateRandomArray.Click += new System.EventHandler(this.btnGenerateRandomArray_Click);
            // 
            // dgvArray
            // 
            this.dgvArray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArray.Location = new System.Drawing.Point(12, 109);
            this.dgvArray.Name = "dgvArray";
            this.dgvArray.Size = new System.Drawing.Size(240, 150);
            this.dgvArray.TabIndex = 4;
            // 
            // btnSortArray
            // 
            this.btnSortArray.Location = new System.Drawing.Point(12, 265);
            this.btnSortArray.Name = "btnSortArray";
            this.btnSortArray.Size = new System.Drawing.Size(200, 23);
            this.btnSortArray.TabIndex = 5;
            this.btnSortArray.Text = "Сортировать массив";
            this.btnSortArray.UseVisualStyleBackColor = true;
            this.btnSortArray.Click += new System.EventHandler(this.btnSortArray_Click);
            // 
            // btnFindMaxMin
            // 
            this.btnFindMaxMin.Location = new System.Drawing.Point(12, 294);
            this.btnFindMaxMin.Name = "btnFindMaxMin";
            this.btnFindMaxMin.Size = new System.Drawing.Size(200, 23);
            this.btnFindMaxMin.TabIndex = 6;
            this.btnFindMaxMin.Text = "Найти макс/мин";
            this.btnFindMaxMin.UseVisualStyleBackColor = true;
            this.btnFindMaxMin.Click += new System.EventHandler(this.btnFindMaxMin_Click);
            // 
            // btnCalculateAverage
            // 
            this.btnCalculateAverage.Location = new System.Drawing.Point(12, 323);
            this.btnCalculateAverage.Name = "btnCalculateAverage";
            this.btnCalculateAverage.Size = new System.Drawing.Size(200, 23);
            this.btnCalculateAverage.TabIndex = 7;
            this.btnCalculateAverage.Text = "Среднее арифметическое";
            this.btnCalculateAverage.UseVisualStyleBackColor = true;
            this.btnCalculateAverage.Click += new System.EventHandler(this.btnCalculateAverage_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 349);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 13);
            this.lblResult.TabIndex = 8;
            // 
            // ArrayForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCalculateAverage);
            this.Controls.Add(this.btnFindMaxMin);
            this.Controls.Add(this.btnSortArray);
            this.Controls.Add(this.dgvArray);
            this.Controls.Add(this.btnGenerateRandomArray);
            this.Controls.Add(this.btnCreateDefaultArray);
            this.Controls.Add(this.lblArraySize);
            this.Controls.Add(this.txtArraySize);
            this.Name = "ArrayForm";
            this.Load += new System.EventHandler(this.ArrayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArray)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtArraySize;
        private System.Windows.Forms.Label lblArraySize;
        private System.Windows.Forms.Button btnCreateDefaultArray;
        private System.Windows.Forms.Button btnGenerateRandomArray;
        private System.Windows.Forms.DataGridView dgvArray;
        private System.Windows.Forms.Button btnSortArray;
        private System.Windows.Forms.Button btnFindMaxMin;
        private System.Windows.Forms.Button btnCalculateAverage;
        private System.Windows.Forms.Label lblResult;
    }
}

