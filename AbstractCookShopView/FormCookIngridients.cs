using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace AbstractCookShopView
{
    public partial class FormCookIngridients : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public CookIngridientsViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly IIngridientsService service;
        private CookIngridientsViewModel model;
        public FormCookIngridients(IIngridientsService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormCookIngridients_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngridientsViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxIngridient.DisplayMember = "IngridientsName";
                    comboBoxIngridient.ValueMember = "Id";
                    comboBoxIngridient.DataSource = list;
                    comboBoxIngridient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxIngridient.Enabled = false;
                comboBoxIngridient.SelectedValue = model.IngridientsId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngridient.SelectedValue == null)
            {
                MessageBox.Show("Выберите Ингридиент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new CookIngridientsViewModel
                    {
                        IngridientsId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                        IngridientsName = comboBoxIngridient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

