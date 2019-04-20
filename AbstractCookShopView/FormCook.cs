using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace AbstractCookShopView
{
    public partial class FormCook : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ICookService service;
        private int? id;
        private List<CookIngridientsViewModel> CookIngridients;
        public FormCook(ICookService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormCook_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CookViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.CookName;
                        textBoxPrice.Text = view.Price.ToString();
                        CookIngridients = view.CookIngridients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                CookIngridients = new List<CookIngridientsViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (CookIngridients != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = CookIngridients;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCookIngridients>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CookId = id.Value;
                    }
                    CookIngridients.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCookIngridients>();
                form.Model =
               CookIngridients[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CookIngridients[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        CookIngridients.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (CookIngridients == null || CookIngridients.Count == 0)
            {
                MessageBox.Show("Заполните ингридиенты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<CookIngridientsBindingModel> CookMateialsBM = new
               List<CookIngridientsBindingModel>();
                for (int i = 0; i < CookIngridients.Count; ++i)
                {
                    CookMateialsBM.Add(new CookIngridientsBindingModel
                    {
                        Id = CookIngridients[i].Id,
                        CookId = CookIngridients[i].CookId,
                        IngridientsId = CookIngridients[i].IngridientsId,
                        Count = CookIngridients[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new CookBindingModel
                    {
                        Id = id.Value,
                        CookName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CookIngridientss = CookMateialsBM
                    });
                }
                else
                {
                    service.AddElement(new CookBindingModel
                    {
                        CookName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CookIngridientss = CookMateialsBM
                    });
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