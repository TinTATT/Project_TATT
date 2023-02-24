namespace QuanLyCuaHangAnUong
{
    partial class FrmChuyenBan
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
            this.btnChuyenBan = new System.Windows.Forms.Button();
            this.drbChuyenBan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChuyenBan
            // 
            this.btnChuyenBan.BackColor = System.Drawing.Color.Azure;
            this.btnChuyenBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChuyenBan.Location = new System.Drawing.Point(135, 125);
            this.btnChuyenBan.Margin = new System.Windows.Forms.Padding(2);
            this.btnChuyenBan.Name = "btnChuyenBan";
            this.btnChuyenBan.Size = new System.Drawing.Size(180, 45);
            this.btnChuyenBan.TabIndex = 6;
            this.btnChuyenBan.Text = "CHUYỂN BÀN";
            this.btnChuyenBan.UseVisualStyleBackColor = false;
            this.btnChuyenBan.Click += new System.EventHandler(this.btnChuyenBan_Click);
            // 
            // drbChuyenBan
            // 
            this.drbChuyenBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drbChuyenBan.FormattingEnabled = true;
            this.drbChuyenBan.Items.AddRange(new object[] {
            "Bàn 0",
            "Bàn 1",
            "Bàn 2",
            "Bàn 3",
            "Bàn 4",
            "Bàn 5",
            "Bàn 6",
            "Bàn 7",
            "Bàn 8",
            "Bàn 9",
            "Bàn 10",
            "Bàn 11",
            "Bàn 12",
            "Bàn 13",
            "Bàn 14",
            "Bàn 15",
            "Bàn 16",
            "Bàn 17",
            "Bàn 18",
            "Bàn 19",
            "Bàn 20",
            "Bàn 21",
            "Bàn 22",
            "Bàn 23",
            "Bàn 24",
            "Bàn 25",
            "Bàn 26",
            "Bàn 27",
            "Bàn 28",
            "Bàn 29",
            "Bàn 30",
            "Bàn 31",
            "Bàn 32",
            "Bàn 33",
            "Bàn 34",
            "Bàn 35",
            "Bàn 36",
            "Bàn 37",
            "Bàn 38",
            "Bàn 39",
            "Bàn 40",
            "Bàn 41",
            "Bàn 42",
            "Bàn 43",
            "Bàn 44",
            "Bàn 45",
            "Bàn 46",
            "Bàn 47",
            "Bàn 48",
            "Bàn 49",
            "Bàn 50",
            "Bàn 51",
            "Bàn 52",
            "Bàn 53",
            "Bàn 54",
            "Bàn 55",
            "Bàn 56",
            "Bàn 57",
            "Bàn 58",
            "Bàn 59 ",
            "Bàn 60",
            "Bàn 61",
            "Bàn 62",
            "Bàn 63",
            "Bàn 64",
            "Bàn 65",
            "Bàn 66",
            "Bàn 67",
            "Bàn 68",
            "Bàn 69",
            "Bàn 70",
            "Bàn 71",
            "Bàn 72",
            "Bàn 73",
            "Bàn 74",
            "Bàn 75",
            "Bàn 76",
            "Bàn 77",
            "Bàn 78",
            "Bàn 79",
            "Bàn 80"});
            this.drbChuyenBan.Location = new System.Drawing.Point(135, 78);
            this.drbChuyenBan.Margin = new System.Windows.Forms.Padding(2);
            this.drbChuyenBan.Name = "drbChuyenBan";
            this.drbChuyenBan.Size = new System.Drawing.Size(180, 21);
            this.drbChuyenBan.TabIndex = 5;
            this.drbChuyenBan.SelectedIndexChanged += new System.EventHandler(this.drbChuyenBan_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "CHUYỂN BÀN:";
            // 
            // FrmChuyenBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(434, 245);
            this.Controls.Add(this.btnChuyenBan);
            this.Controls.Add(this.drbChuyenBan);
            this.Controls.Add(this.label1);
            this.Name = "FrmChuyenBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmChuyenBan";
            this.Load += new System.EventHandler(this.FrmChuyenBan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChuyenBan;
        private System.Windows.Forms.ComboBox drbChuyenBan;
        private System.Windows.Forms.Label label1;
    }
}