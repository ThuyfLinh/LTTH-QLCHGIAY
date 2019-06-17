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
    public partial class fHoaDonBan : MetroFramework.Forms.MetroForm
    {
        public fHoaDonBan()
        {
            InitializeComponent();
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<HoaDonBan_DTO> listHDB = null;

        private void Load()
        {
            listHDB = loadHDB();
            dgvHoaDonBan.DataSource = listHDB;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaHD.Text = dgvHoaDonBan.CurrentRow.Cells["MaHD"].Value.ToString();
            txtTenNV.Text = dgvHoaDonBan.CurrentRow.Cells["TenNV"].Value.ToString();
            txtTenKH.Text = dgvHoaDonBan.CurrentRow.Cells["TenKH"].Value.ToString();
            txtKM.Text = dgvHoaDonBan.CurrentRow.Cells["TenCT"].Value.ToString();
            dtNgay.Text = dgvHoaDonBan.CurrentRow.Cells["Ngay"].Value.ToString();
            txtSDT.Text = dgvHoaDonBan.CurrentRow.Cells["SDT"].Value.ToString();
        }
        private List<HoaDonBan_DTO> loadHDB()
        {
            List<HoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("HoaDonBan");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HoaDonBan_DTO>>();
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
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string search = txtTimKiem.Text;
            List<HoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/HoaDonBan?search={search}
                var responseTask = client.GetAsync($"HoaDonBan?search={search}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HoaDonBan_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listHDB = list;
            dgvHoaDonBan.DataSource = listHDB;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("phải chọn 1 HD để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn sửa giày : " + txtMaHD.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    int maHD;
                    Int32.TryParse(txtMaHD.Text, out maHD);
                    string tenNV = txtTenNV.Text;
                    string tenKH = txtTenKH.Text;
                    string sdt = txtSDT.Text;
                    string tenCT = txtKM.Text;
                    DateTime Ngay;
                    DateTime.TryParse(dtNgay.Text, out Ngay);

                    HoaDonBan_DTO HD = new HoaDonBan_DTO(maHD, tenNV, tenKH, tenCT,Ngay,sdt);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP PUT
                        var postTask = client.PutAsJsonAsync<HoaDonBan_DTO>("HoaDonBan", HD);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sửa HD thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Sửa HD không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            if (rbKH_old.Checked)
            {
                if (txtTenKH.Text == "" || txtTenNV.Text == "" || txtKM.Text == "" || txtSDT.Text == "" || dtNgay.Text == "")
                {
                    MessageBox.Show("Sai hoặc thiếu thông tin");
                    Load();
                }
                else
                {
                    string tenNV = txtTenNV.Text;
                    string tenKH = txtTenKH.Text;

                    string sdt = txtSDT.Text;
                    string tenCT = txtKM.Text;
                    DateTime Ngay;
                    DateTime.TryParse(dtNgay.Text, out Ngay);

                    HoaDonBan_DTO HD = new HoaDonBan_DTO(tenNV, tenKH, tenCT, Ngay, sdt);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP POST api/HoaDonBan?hoadon={hoadon}
                        var postTask = client.PostAsJsonAsync<HoaDonBan_DTO>($"HoaDonBan?hoadon={HD}",HD);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Thêm HD thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Thêm HD không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
            if (rbKH_new.Checked)
            {
                if (txtTenKH.Text == "" || txtTenNV.Text == "" || txtKM.Text == "" || txtSDT.Text == "" || dtNgay.Text == "")
                {
                    MessageBox.Show("Sai hoặc thiếu thông tin");
                    Load();
                }
                else
                {
                    string tenNV = txtTenNV.Text;
                    string tenKH = txtTenKH.Text;
                    string sdt = txtSDT.Text;
                    string tenCT = txtKM.Text;
                    DateTime Ngay;
                    DateTime.TryParse(dtNgay.Text, out Ngay);
                    HoaDonBan_DTO HD = new HoaDonBan_DTO(tenNV, tenKH, tenCT, Ngay, sdt);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP POST api/HoaDonBan?hoadon={hoadon}
                        var postTask = client.PostAsJsonAsync<HoaDonBan_DTO>("HoaDonBan", HD);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Thêm HD thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Thêm HD không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
            if (!rbKH_old.Checked && !rbKH_new.Checked)
            {
                MessageBox.Show("Vui lòng chọn loại khách hàng!");
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            fCTHoaDonBan f = new fCTHoaDonBan(int.Parse(txtMaHD.Text));
            f.ShowDialog();
            this.Show();
        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dgvHoaDonBan.CurrentRow.Cells["MaHD"].Value.ToString();
            txtTenNV.Text = dgvHoaDonBan.CurrentRow.Cells["TenNV"].Value.ToString();
            txtTenKH.Text = dgvHoaDonBan.CurrentRow.Cells["TenKH"].Value.ToString();
            txtKM.Text = dgvHoaDonBan.CurrentRow.Cells["TenCT"].Value.ToString();
            dtNgay.Text = dgvHoaDonBan.CurrentRow.Cells["Ngay"].Value.ToString();
            txtSDT.Text = dgvHoaDonBan.CurrentRow.Cells["SDT"].Value.ToString();
        }
    }
}
