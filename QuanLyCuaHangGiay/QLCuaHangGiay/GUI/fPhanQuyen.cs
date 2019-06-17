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
    public partial class fPhanQuyen : MetroFramework.Forms.MetroForm
    {
        public fPhanQuyen()
        {
            InitializeComponent();
            Load();
        }

        public string baseAddress = "http://localhost:63920/api/";
        List<PhanQuyenDTO> listDN = null;

        private void Load()
        {
            listDN = load();
            dgvDN.DataSource = listDN;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaNV.Text = dgvDN.CurrentRow.Cells["IDNV"].Value.ToString();
            txtPhanQuyen.Text = dgvDN.CurrentRow.Cells["PhanQuyen"].Value.ToString();
            txtName.Text = dgvDN.CurrentRow.Cells["Name"].Value.ToString();
            txtPass.Text = dgvDN.CurrentRow.Cells["Pass"].Value.ToString();

        }
        private List<PhanQuyenDTO> load()
        {
            List<PhanQuyenDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("PhanQuyen");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PhanQuyenDTO>>();
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
            string name = txtSearch.Text;
            List<PhanQuyenDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/PhanQuyen?name={name}
                var responseTask = client.GetAsync($"PhanQuyen?name={name}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PhanQuyenDTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listDN = list;
            dgvDN.DataSource = listDN;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtName.Text = "";
            txtPass.Text = "";
            txtPhanQuyen.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtMaNV.Text==""||txtPass.Text==""||txtPhanQuyen.Text=="")
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = Convert.ToInt32(txtMaNV.Text);
            string name = txtName.Text;
            string pass = txtPass.Text;
            int phanquyen = Convert.ToInt32(txtPhanQuyen.Text);
            
            PhanQuyenDTO PQ = new PhanQuyenDTO(id,name,pass,phanquyen);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PhanQuyenDTO>("PhanQuyen", PQ);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm TK thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Thêm TK không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Load();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("phải chọn 1 TK để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn sửa TK : " + txtName.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(txtMaNV.Text);
                    string name = txtName.Text;
                    string pass = txtPass.Text;
                    int phanquyen = Convert.ToInt32(txtPhanQuyen.Text);

                    PhanQuyenDTO PQ = new PhanQuyenDTO(id, name, pass, phanquyen);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP PUT
                        var postTask = client.PutAsJsonAsync<PhanQuyenDTO>("PhanQuyen", PQ);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sửa TK thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Sửa TK không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Load();
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //HTTP DELETE api/PhanQuyen?name={name}
            if (MessageBox.Show("Bạn có thật sự muốn xóa TK là: " + txtName.Text, "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                string ten = txtName.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    var url = baseAddress + "PhanQuyen?name=" + ten;
                    //HTTP PUT
                    var postTask = client.DeleteAsync(url);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Xóa TK thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Xóa TK không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Load();
                }
            }
        }

        private void dgvDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dgvDN.CurrentRow.Cells["IDNV"].Value.ToString();
            txtPhanQuyen.Text = dgvDN.CurrentRow.Cells["PhanQuyen"].Value.ToString();
            txtName.Text = dgvDN.CurrentRow.Cells["Name"].Value.ToString();
            txtPass.Text = dgvDN.CurrentRow.Cells["Pass"].Value.ToString();
        }
    }
}
