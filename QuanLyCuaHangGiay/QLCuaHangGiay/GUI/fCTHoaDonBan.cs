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
    public partial class fCTHoaDonBan : MetroFramework.Forms.MetroForm
    {
        private int MaHD;
        public fCTHoaDonBan(int id)
        {
            InitializeComponent();
            MaHD = id;
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<CTHoaDonBan_DTO> listCTHDB = null;

        private void Load()
        {
            listCTHDB = loadCTHDB();
            dgvCTHD.DataSource = listCTHDB;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaHD.Text = dgvCTHD.CurrentRow.Cells["MaHD"].Value.ToString();
            txtMaGiay.Text = dgvCTHD.CurrentRow.Cells["MaGiay"].Value.ToString();
            txtSoLuong.Text = dgvCTHD.CurrentRow.Cells["SoLuong"].Value.ToString();
            
        }
        private List<CTHoaDonBan_DTO> loadCTHDB()
        {
            int id = Convert.ToInt32(txtMaGiay.Text);
            List<CTHoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET api/CTHoaDonBan/{id}
                var responseTask = client.GetAsync($"CTHoaDonBan/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CTHoaDonBan_DTO>>();
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
            int id = Convert.ToInt32(txtMaGiay.Text);
            string search = txtTimKiem.Text;
            List<CTHoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/CTHoaDonBan/{id}?search={search}
                var responseTask = client.GetAsync($"CTHoaDonBan/{id}?search={search}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CTHoaDonBan_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listCTHDB = list;
            dgvCTHD.DataSource = listCTHDB;
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (txtMaGiay.Text == null || txtSoLuong.Text == null)
            {
                MessageBox.Show("Chọn 1 chi tiết hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết khuyến mãi ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(txtMaHD.Text);
                    int idgiay = Convert.ToInt32(txtMaGiay.Text);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        var url = baseAddress + "CTHoaDonBan?idhd=" + id + "&idgiay=" + idgiay;
                        //HTTP DELETE api/CTHoaDonBan?idhd={idhd}&idgiay={idgiay}
                        var postTask = client.DeleteAsync(url);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa CTHD thành công!", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Xóa CTHD không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnThemCTKM_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Sai hoặc thiếu thông tin");
                Load();
            }
            else
            {
                int MaHD;
                Int32.TryParse(txtMaHD.Text, out MaHD);
                int MaGiay;
                Int32.TryParse(txtMaGiay.Text, out MaGiay);
                int SoLuong;
                Int32.TryParse(txtSoLuong.Text, out SoLuong);


                CTHoaDonBan_DTO HD = new CTHoaDonBan_DTO(MaHD, MaGiay, SoLuong);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    //HTTP POST api/HoaDonBan?hoadon={hoadon}
                    var postTask = client.PostAsJsonAsync<CTHoaDonBan_DTO>("CTHoaDonBan", HD);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Thêm CTHD thành công", "Thông báo", MessageBoxButtons.OK);

                    }
                    else
                    {
                        MessageBox.Show("Thêm CTHD không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Load();
                }
            }
        }
    }
}
