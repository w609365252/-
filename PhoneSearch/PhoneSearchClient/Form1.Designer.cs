namespace PhoneSearchClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Province = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExportNoSearchPhone = new System.Windows.Forms.Button();
            this.ErrorPhoneTxt = new System.Windows.Forms.Label();
            this.Loading = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lv_province = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_datalist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "导入Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mobile,
            this.Province,
            this.City,
            this.CardType});
            this.dataGridView1.Location = new System.Drawing.Point(168, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(654, 382);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Mobile
            // 
            this.Mobile.Frozen = true;
            this.Mobile.HeaderText = "手机号";
            this.Mobile.Name = "Mobile";
            this.Mobile.ReadOnly = true;
            this.Mobile.Width = 250;
            // 
            // Province
            // 
            this.Province.Frozen = true;
            this.Province.HeaderText = "省份";
            this.Province.Name = "Province";
            this.Province.ReadOnly = true;
            // 
            // City
            // 
            this.City.Frozen = true;
            this.City.HeaderText = "市";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // CardType
            // 
            this.CardType.Frozen = true;
            this.CardType.HeaderText = "运营商";
            this.CardType.Name = "CardType";
            this.CardType.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ExportNoSearchPhone);
            this.panel1.Controls.Add(this.ErrorPhoneTxt);
            this.panel1.Controls.Add(this.Loading);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 34);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ExportNoSearchPhone
            // 
            this.ExportNoSearchPhone.Location = new System.Drawing.Point(461, 6);
            this.ExportNoSearchPhone.Name = "ExportNoSearchPhone";
            this.ExportNoSearchPhone.Size = new System.Drawing.Size(171, 23);
            this.ExportNoSearchPhone.TabIndex = 4;
            this.ExportNoSearchPhone.Text = "导出未识别号码";
            this.ExportNoSearchPhone.UseVisualStyleBackColor = true;
            this.ExportNoSearchPhone.Click += new System.EventHandler(this.ExportNoSearchPhone_Click);
            // 
            // ErrorPhoneTxt
            // 
            this.ErrorPhoneTxt.AutoSize = true;
            this.ErrorPhoneTxt.Location = new System.Drawing.Point(638, 11);
            this.ErrorPhoneTxt.Name = "ErrorPhoneTxt";
            this.ErrorPhoneTxt.Size = new System.Drawing.Size(71, 12);
            this.ErrorPhoneTxt.TabIndex = 3;
            this.ErrorPhoneTxt.Text = "共0条未识别";
            // 
            // Loading
            // 
            this.Loading.AutoSize = true;
            this.Loading.Location = new System.Drawing.Point(293, 11);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(0, 12);
            this.Loading.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "导出结果";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lv_province
            // 
            this.lv_province.Location = new System.Drawing.Point(12, 56);
            this.lv_province.Name = "lv_province";
            this.lv_province.Size = new System.Drawing.Size(150, 382);
            this.lv_province.TabIndex = 3;
            this.lv_province.UseCompatibleStateImageBehavior = false;
            this.lv_province.View = System.Windows.Forms.View.List;
            this.lv_province.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_province_ColumnClick);
            this.lv_province.Click += new System.EventHandler(this.lv_province_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "省份筛选";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据列表";
            // 
            // lab_datalist
            // 
            this.lab_datalist.AutoSize = true;
            this.lab_datalist.Location = new System.Drawing.Point(225, 41);
            this.lab_datalist.Name = "lab_datalist";
            this.lab_datalist.Size = new System.Drawing.Size(0, 12);
            this.lab_datalist.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.lab_datalist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lv_province);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "归属地筛选器V1.0";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Loading;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Province;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardType;
        private System.Windows.Forms.Button ExportNoSearchPhone;
        private System.Windows.Forms.Label ErrorPhoneTxt;
        private System.Windows.Forms.ListView lv_province;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_datalist;
    }
}

