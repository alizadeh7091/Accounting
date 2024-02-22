using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting1.DataLeyer.Context;

namespace Accounting1.App
{
    public partial class frmNewAccounting : Form
    {
        UnitOfWork db = new UnitOfWork();
        public int AccountID = 0 ;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers();
            if (AccountID != 0)
            {
                var account = db.AccountingRepository.GetById(AccountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description.ToString();
                txtName.Text = db.CustomerRepository.GetCustomerNameBId(account.CustomerID);
                if (account.TypeID == 1)
                {
                    rbRecieve.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";

            }
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAmount.Text))
            {
                 MessageBox.Show("لطفا مقادیر طرف حساب و مبلغ را وارد کنید", "توجه", MessageBoxButtons.OK);
            }
            else if (rbPay.Checked || rbRecieve.Checked)
            {
                DataLeyer.Accounting accounting = new DataLeyer.Accounting()
                {
                    Amount = int.Parse(txtAmount.Value.ToString()),
                    Description = txtDescription.Text,
                    CustomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                    TypeID = (rbRecieve.Checked) ? 1 : 2,
                    DateTime = DateTime.Now
                };
                if (AccountID == 0)
                {
                    db.AccountingRepository.Insert(accounting);
                    db.Save();
                }
                else
                {
                    using (UnitOfWork db2 = new UnitOfWork())
                    {
                        accounting.ID = AccountID;
                        db2.AccountingRepository.Update(accounting);
                        db2.Save();
                    }
                    
                }
                
                
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
            }
        }
    }
}
