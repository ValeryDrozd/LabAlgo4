namespace LeraAlgoLab3
{
    partial class Form1
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
            this.delBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.findBtn = new System.Windows.Forms.Button();
            this.insertBtn = new System.Windows.Forms.Button();
            this.InsertBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.FindBox = new System.Windows.Forms.TextBox();
            this.DelBox = new System.Windows.Forms.TextBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(12, 459);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(64, 50);
            this.delBtn.TabIndex = 0;
            this.delBtn.Text = "Delete";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(12, 524);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(493, 50);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(12, 386);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(64, 50);
            this.findBtn.TabIndex = 2;
            this.findBtn.Text = "Find";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // insertBtn
            // 
            this.insertBtn.Location = new System.Drawing.Point(12, 317);
            this.insertBtn.Name = "insertBtn";
            this.insertBtn.Size = new System.Drawing.Size(64, 50);
            this.insertBtn.TabIndex = 3;
            this.insertBtn.Text = "Insert";
            this.insertBtn.UseVisualStyleBackColor = true;
            this.insertBtn.Click += new System.EventHandler(this.insertBtn_Click);
            // 
            // InsertBox
            // 
            this.InsertBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.InsertBox.Location = new System.Drawing.Point(82, 317);
            this.InsertBox.Name = "InsertBox";
            this.InsertBox.Size = new System.Drawing.Size(423, 47);
            this.InsertBox.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.textBox2.Location = new System.Drawing.Point(-411, -151);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(423, 47);
            this.textBox2.TabIndex = 5;
            // 
            // FindBox
            // 
            this.FindBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.FindBox.Location = new System.Drawing.Point(82, 389);
            this.FindBox.Name = "FindBox";
            this.FindBox.Size = new System.Drawing.Size(423, 47);
            this.FindBox.TabIndex = 6;
            // 
            // DelBox
            // 
            this.DelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.DelBox.Location = new System.Drawing.Point(82, 459);
            this.DelBox.Name = "DelBox";
            this.DelBox.Size = new System.Drawing.Size(423, 47);
            this.DelBox.TabIndex = 7;
            // 
            // StatusBox
            // 
            this.StatusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F);
            this.StatusBox.Location = new System.Drawing.Point(82, 262);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(423, 41);
            this.StatusBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Status";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 609);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.DelBox);
            this.Controls.Add(this.FindBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.InsertBox);
            this.Controls.Add(this.insertBtn);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.delBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button insertBtn;
        private System.Windows.Forms.TextBox InsertBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox FindBox;
        private System.Windows.Forms.TextBox DelBox;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.Label label1;
    }
}

