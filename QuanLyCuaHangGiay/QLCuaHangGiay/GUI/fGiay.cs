using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangGiay.GUI
{
    public partial class fGiay : MetroFramework.Forms.MetroForm
    {
        public fGiay()
        {
            InitializeComponent();
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<Giay_DTO> listGiay = null;

        private void Load()
        {
            listGiay = loadGiay();
            dgvGiay.DataSource = listGiay;
            AddBinding();
        }
        void AddBinding()
        {
            txtIDGiay.Text = dgvGiay.CurrentRow.Cells["IDGiay"].Value.ToString();
            txtTenGiay.Text = dgvGiay.CurrentRow.Cells["TenGiay"].Value.ToString();
            txtSoLuong.Text = dgvGiay.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGia.Text = dgvGiay.CurrentRow.Cells["DonGia"].Value.ToString();
            
        }
        private List<Giay_DTO> loadGiay()
        {
            List<Giay_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("Giay");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Giay_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenGiay.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên giày", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tengiay = txtTenGiay.Text;
            int soluong = Convert.ToInt32(txtSoLuong.Text);
            decimal dongia = Convert.ToDecimal(txtDonGia.Text);

            Giay_DTO giay = new Giay_DTO(tengiay, soluong, dongia);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Giay_DTO>("Giay", giay);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm giày thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Thêm giày không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Load();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtIDGiay.Text == "")
            {
                MessageBox.Show("phải chọn 1 giày để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn sửa giày : " + txtTenGiay.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    int idgiay = Convert.ToInt32(txtIDGiay.Text);
                    string tengiay = txtTenGiay.Text;
                    int soluong = Convert.ToInt32(txtSoLuong.Text);
                    decimal dongia = Convert.ToDecimal(txtDonGia.Text);

                    Giay_DTO giay = new Giay_DTO(idgiay, tengiay, soluong, dongia);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP PUT
                        var postTask = client.PutAsJsonAsync<Giay_DTO>("Giay", giay);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sửa giày thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Sửa giày không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
        }

        private void dgvGiay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDGiay.Text = dgvGiay.CurrentRow.Cells["IDGiay"].Value.ToString();
            txtTenGiay.Text = dgvGiay.CurrentRow.Cells["TenGiay"].Value.ToString();
            txtSoLuong.Text = dgvGiay.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGia.Text = dgvGiay.CurrentRow.Cells["DonGia"].Value.ToString();
           
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtIDGiay.Text = "";
            txtTenGiay.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tengiay = txtSearch.Text;
            List<Giay_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/Giay?TenGiay={TenGiay}
                var responseTask = client.GetAsync($"Giay?TenGiay={tengiay}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Giay_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listGiay = list;
            dgvGiay.DataSource = listGiay;
        }
        
    }
}
