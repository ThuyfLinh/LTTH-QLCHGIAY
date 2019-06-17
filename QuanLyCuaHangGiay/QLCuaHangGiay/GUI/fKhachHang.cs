using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangGiay.GUI
{
    public partial class fKhachHang : MetroFramework.Forms.MetroForm
    {
        public fKhachHang()
        {
            InitializeComponent();
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<KhachHang_DTO> listKH = null;

        private void Load()
        {
            listKH = loadKH();
            dgvKhachHang.DataSource = listKH;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["Makh"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["Tenkh"].Value.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells["Sdt"].Value.ToString();
        }
        private List<KhachHang_DTO> loadKH()
        {
            List<KhachHang_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("KhachHang");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhachHang_DTO>>();
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
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("phải chọn 1 giày để khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn sửa Khách hàng : " + txtTenKH.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    int idkh = Convert.ToInt32(txtMaKH.Text);
                    string tenkh = txtTenKH.Text;
                    int sdt = Convert.ToInt32(txtSDT.Text);

                    KhachHang_DTO kh = new KhachHang_DTO(idkh, tenkh, sdt);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP PUT
                        var postTask = client.PutAsJsonAsync<KhachHang_DTO>("KhachHang", kh);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Khách hàng thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Sửa khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int sdt = Convert.ToInt32(txtSDT.Text);
            string tenkh = txtTenKH.Text;
           
            KhachHang_DTO kh = new KhachHang_DTO(tenkh, sdt);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KhachHang_DTO>("KhachHang", kh);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Thêm khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Load();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("phải chọn 1 khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn xóa khách hàng : " + txtTenKH.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    int idkh = Convert.ToInt32(txtMaKH.Text);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        var url = baseAddress + "KhachHang/" + idkh;
                        //HTTP PUT 
                        var postTask = client.DeleteAsync(url);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Xóa khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["Makh"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["Tenkh"].Value.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells["Sdt"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenkh = txtTenKH.Text;
            List<KhachHang_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/KhachHang?TenKH={TenKH}
                var responseTask = client.GetAsync($"KhachHang?TenKH={tenkh}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhachHang_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listKH = list;
            dgvKhachHang.DataSource = listKH;
        }
    }
}
