namespace AbstractCookShopView
{
    partial class FormCreateOrder
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.sClientBindingModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxCook = new System.Windows.Forms.ComboBox();
            this.CookBindingModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.formClientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.formClientsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.IngridientsBindingModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sClientBindingModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CookBindingModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formClientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formClientsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IngridientsBindingModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.DataSource = this.sClientBindingModelBindingSource;
            this.comboBoxClient.DisplayMember = "ClientFIO";
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.Location = new System.Drawing.Point(82, 12);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(239, 21);
            this.comboBoxClient.TabIndex = 0;
            // 
            // sClientBindingModelBindingSource
            // 
            this.sClientBindingModelBindingSource.DataSource = typeof(AbstractCookShopServiceDAL.BindingModels.SClientBindingModel);
            // 
            // comboBoxCook
            // 
            this.comboBoxCook.DataSource = this.CookBindingModelBindingSource;
            this.comboBoxCook.DisplayMember = "CookName";
            this.comboBoxCook.FormattingEnabled = true;
            this.comboBoxCook.Location = new System.Drawing.Point(82, 39);
            this.comboBoxCook.Name = "comboBoxCook";
            this.comboBoxCook.Size = new System.Drawing.Size(239, 21);
            this.comboBoxCook.TabIndex = 1;
            // 
            // CookBindingModelBindingSource
            // 
            this.CookBindingModelBindingSource.DataSource = typeof(AbstractCookShopServiceDAL.BindingModels.CookBindingModel);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(82, 66);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(239, 20);
            this.textBoxCount.TabIndex = 2;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_CountChanged);
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(82, 92);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.Size = new System.Drawing.Size(239, 20);
            this.textBoxSum.TabIndex = 3;
            this.textBoxSum.TextChanged += new System.EventHandler(this.comboBoxCook_SumChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(119, 114);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(200, 114);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Клиент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Блюдо";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Количество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Сумма";
            // 
            // formClientsBindingSource
            // 
            this.formClientsBindingSource.DataSource = typeof(AbstractCookShopView.FormClients);
            // 
            // formClientsBindingSource1
            // 
            this.formClientsBindingSource1.DataSource = typeof(AbstractCookShopView.FormClients);
            // 
            // IngridientsBindingModelBindingSource
            // 
            this.IngridientsBindingModelBindingSource.DataSource = typeof(AbstractCookShopServiceDAL.BindingModels.IngridientsBindingModel);
            // 
            // FormCreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 149);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxCook);
            this.Controls.Add(this.comboBoxClient);
            this.Name = "FormCreateOrder";
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sClientBindingModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CookBindingModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formClientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formClientsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IngridientsBindingModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxClient;
        private System.Windows.Forms.ComboBox comboBoxCook;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource formClientsBindingSource;
        private System.Windows.Forms.BindingSource formClientsBindingSource1;
        private System.Windows.Forms.BindingSource sClientBindingModelBindingSource;
        private System.Windows.Forms.BindingSource IngridientsBindingModelBindingSource;
        private System.Windows.Forms.BindingSource CookBindingModelBindingSource;
    }
}