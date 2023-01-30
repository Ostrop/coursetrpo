
namespace CourseProjectTRPO
{
    partial class MedCard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgwExams = new System.Windows.Forms.DataGridView();
            this.Client_dates = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgwProcedures = new System.Windows.Forms.DataGridView();
            this.dgwDiagnoses = new System.Windows.Forms.DataGridView();
            this.Exambutton = new System.Windows.Forms.Button();
            this.Procedurebutton = new System.Windows.Forms.Button();
            this.Realodbutton = new System.Windows.Forms.Button();
            this.Changebutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Client_dates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwProcedures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDiagnoses)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwExams
            // 
            this.dgwExams.AllowUserToAddRows = false;
            this.dgwExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExams.Location = new System.Drawing.Point(12, 139);
            this.dgwExams.Name = "dgwExams";
            this.dgwExams.ReadOnly = true;
            this.dgwExams.RowHeadersVisible = false;
            this.dgwExams.Size = new System.Drawing.Size(1201, 202);
            this.dgwExams.TabIndex = 0;
            // 
            // Client_dates
            // 
            this.Client_dates.AllowUserToAddRows = false;
            this.Client_dates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Client_dates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Client_dates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Client_dates.Location = new System.Drawing.Point(12, 12);
            this.Client_dates.Name = "Client_dates";
            this.Client_dates.RowHeadersVisible = false;
            this.Client_dates.Size = new System.Drawing.Size(1201, 81);
            this.Client_dates.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Осмотры:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Процедуры:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 608);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Диагнозы:";
            // 
            // dgwProcedures
            // 
            this.dgwProcedures.AllowUserToAddRows = false;
            this.dgwProcedures.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwProcedures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwProcedures.Location = new System.Drawing.Point(12, 382);
            this.dgwProcedures.Name = "dgwProcedures";
            this.dgwProcedures.ReadOnly = true;
            this.dgwProcedures.RowHeadersVisible = false;
            this.dgwProcedures.Size = new System.Drawing.Size(1201, 209);
            this.dgwProcedures.TabIndex = 5;
            // 
            // dgwDiagnoses
            // 
            this.dgwDiagnoses.AllowUserToAddRows = false;
            this.dgwDiagnoses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwDiagnoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDiagnoses.Location = new System.Drawing.Point(12, 634);
            this.dgwDiagnoses.Name = "dgwDiagnoses";
            this.dgwDiagnoses.ReadOnly = true;
            this.dgwDiagnoses.RowHeadersVisible = false;
            this.dgwDiagnoses.Size = new System.Drawing.Size(1201, 146);
            this.dgwDiagnoses.TabIndex = 6;
            this.dgwDiagnoses.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgwDiagnoses_UserAddedRow);
            // 
            // Exambutton
            // 
            this.Exambutton.Location = new System.Drawing.Point(1219, 283);
            this.Exambutton.Name = "Exambutton";
            this.Exambutton.Size = new System.Drawing.Size(137, 58);
            this.Exambutton.TabIndex = 7;
            this.Exambutton.Text = "Записать на осмотр";
            this.Exambutton.UseVisualStyleBackColor = true;
            this.Exambutton.Click += new System.EventHandler(this.Exambutton_Click);
            // 
            // Procedurebutton
            // 
            this.Procedurebutton.Location = new System.Drawing.Point(1219, 533);
            this.Procedurebutton.Name = "Procedurebutton";
            this.Procedurebutton.Size = new System.Drawing.Size(137, 58);
            this.Procedurebutton.TabIndex = 8;
            this.Procedurebutton.Text = "Записать на процедуру";
            this.Procedurebutton.UseVisualStyleBackColor = true;
            this.Procedurebutton.Click += new System.EventHandler(this.Procedurebutton_Click);
            // 
            // Realodbutton
            // 
            this.Realodbutton.Location = new System.Drawing.Point(1219, 725);
            this.Realodbutton.Name = "Realodbutton";
            this.Realodbutton.Size = new System.Drawing.Size(137, 55);
            this.Realodbutton.TabIndex = 9;
            this.Realodbutton.Text = "Обновить данные";
            this.Realodbutton.UseVisualStyleBackColor = true;
            this.Realodbutton.Click += new System.EventHandler(this.Realodbutton_Click);
            // 
            // Changebutton
            // 
            this.Changebutton.Location = new System.Drawing.Point(1219, 24);
            this.Changebutton.Name = "Changebutton";
            this.Changebutton.Size = new System.Drawing.Size(137, 55);
            this.Changebutton.TabIndex = 10;
            this.Changebutton.Text = "Подтвердить изменения";
            this.Changebutton.UseVisualStyleBackColor = true;
            this.Changebutton.Click += new System.EventHandler(this.Changebutton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1219, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 107);
            this.button1.TabIndex = 11;
            this.button1.Text = "Удалить выделенную строку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1219, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 107);
            this.button2.TabIndex = 12;
            this.button2.Text = "Удалить выделенную строку";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1219, 634);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 61);
            this.button3.TabIndex = 13;
            this.button3.Text = "Сохранить изменения";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1219, 634);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(137, 61);
            this.button4.TabIndex = 14;
            this.button4.Text = "Полисы";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MedCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 785);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Changebutton);
            this.Controls.Add(this.Realodbutton);
            this.Controls.Add(this.Procedurebutton);
            this.Controls.Add(this.Exambutton);
            this.Controls.Add(this.dgwDiagnoses);
            this.Controls.Add(this.dgwProcedures);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Client_dates);
            this.Controls.Add(this.dgwExams);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MedCard";
            this.Text = "MedCard";
            ((System.ComponentModel.ISupportInitialize)(this.dgwExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Client_dates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwProcedures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDiagnoses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwExams;
        private System.Windows.Forms.DataGridView Client_dates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgwProcedures;
        private System.Windows.Forms.DataGridView dgwDiagnoses;
        private System.Windows.Forms.Button Exambutton;
        private System.Windows.Forms.Button Procedurebutton;
        private System.Windows.Forms.Button Realodbutton;
        private System.Windows.Forms.Button Changebutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}