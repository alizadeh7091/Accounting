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
using Accounting1.DataLeyer;
using System.IO;

namespace Accounting1.App
{
    public partial class frmAddOrEditCustomers : Form
    {
        public int customerId = 0;
        UnitOfWork db = new UnitOfWork();   
        public frmAddOrEditCustomers()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMobile.Text))
            {
                var result = MessageBox.Show("لطفا مقادیر نام و موبایل را وارد کنید", "توجه", MessageBoxButtons.YesNo);
                if (result== DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
               
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                pcCustomer.Image.Save(Application.StartupPath + imageName);
                Customers customer = new Customers()
                {
                    Address = txtAddress.Text,
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    CustomerImage = imageName
                };
                if (customerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = customerId;
                    db.CustomerRepository.UpdateCustomer(customer);
                }
                
                db.Save();
                DialogResult = DialogResult.OK;
                
            }
            
        }

        private void frmAddOrEditCustomers_Load(object sender, EventArgs e)
        {
            if (customerId!=0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomerById(customerId);
                txtAddress.Text = customer.Address;
                txtEmail.Text = customer.Email;
                txtMobile.Text = customer.Mobile;
                txtName.Text = customer.FullName;
                pcCustomer.ImageLocation = Application.StartupPath + customer.CustomerImage;

            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
