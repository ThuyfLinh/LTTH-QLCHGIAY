using QLCuaHangGiay_Data.DAO;
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
    public partial class fKhuyenMai : MetroFramework.Forms.MetroForm
    {
        public fKhuyenMai()
        {
            InitializeComponent();
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<KhuyenMaiDTO> listKM = null;
        List<CTKhuyenMai_DTO> listCTKM = null;

        private void Load()
        {
            listKM = loadKM();
            dgvKhuyenMai.DataSource = listKM;
            dgvDSKM.DataSource = listCTKM;
            AddBinding();
        }

        void AddBinding()
        {
            txtIDKM.Text = dgvKhuyenMai.CurrentRow.Cells["MaKM"].Value.ToString();
            txtTenCT.Text = dgvKhuyenMai.CurrentRow.Cells["TenCT"].Value.ToString();
            txtMoTa.Text = dgvKhuyenMai.CurrentRow.Cells["MoTa"].Value.ToString();
            txtChietKhau.Text = dgvKhuyenMai.CurrentRow.Cells["ChietKhau"].Value.ToString();
            dtpDenNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayKT"].Value.ToString();
            dtpTuNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayBD"].Value.ToString();
        }

        private List<KhuyenMaiDTO> loadKM()
        {
            List<KhuyenMaiDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("KhuyenMai");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhuyenMaiDTO>>();
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
            string tenct = txtTimKiem.Text;
            List<KhuyenMaiDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/KhuyenMai?TenCT={TenCT}
                var responseTask = client.GetAsync($"KhuyenMai?TenCT={tenct}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhuyenMaiDTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listKM = list;
            dgvKhuyenMai.DataSource = listKM;
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            fCTKhuyenMai KM = new fCTKhuyenMai();
            this.Hide();
            KM.ShowDialog();
            this.Show();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtTenCT.Text == null || dtpTuNgay.Text == null || dtpDenNgay == null || txtChietKhau.Text==null)
            {
                MessageBox.Show("Thêm đầy đủ thông tin khuyến mại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            int id = Convert.ToInt32(txtIDKM.Text);
            string tenct = txtTenCT.Text;
            string mota = txtMoTa.Text;
            float chietkhau = float.Parse(txtChietKhau.Text);
            DateTime ngaybd = Convert.ToDateTime(dtpTuNgay.Text);
            DateTime ngaykt = Convert.ToDateTime(dtpDenNgay.Text);

            KhuyenMaiDTO KM = new KhuyenMaiDTO(id, tenct, mota, ngaybd, ngaykt, chietkhau);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP PUT
                var postTask = client.PutAsJsonAsync<KhuyenMaiDTO>("KhuyenMai", KM);

                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Load();
            }

        }

        private void dgvDSKM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                txtMaGiay.Text = dgvDSKM.CurrentRow.Cells["MaGiay"].Value.ToString();
                txtCK.Text = dgvDSKM.CurrentRow.Cells["CK"].Value.ToString();
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (txtMaGiay.Text == null || txtCK.Text == null)
            {
                MessageBox.Show("Chọn ít nhất 1 chi tiết khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết khuyến mãi ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int maKM = Convert.ToInt32(txtIDKM.Text);
                    int maGiay = Convert.ToInt32(txtMaGiay.Text);
                    float CK = float.Parse(txtCK.Text);

                    CTKhuyenMai_DTO ctkm = new CTKhuyenMai_DTO(maKM, maGiay, CK);
                    CTKhuyenMai_DTO CTKM = new CTKhuyenMai_DTO(maKM, maGiay);
                    List<CTKhuyenMai_DTO> list = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        
                        var url = baseAddress + "CTKhuyenMai?idkm="+maKM+"&idgiay="+maGiay;
                        //HTTP PUT api / CTKhuyenMai?idkm ={idkm}&idgiay ={ idgiay}
                        var postTask = client.DeleteAsync(url);
                        postTask.Wait();
                        
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa thành công");
                        }

                        var responseTask = client.GetAsync($"CTKhuyenMai?idkm={maKM}");
                        responseTask.Wait();

                        var result1 = responseTask.Result;
                        if (result1.IsSuccessStatusCode)
                        {
                            var readTask = result1.Content.ReadAsAsync<List<CTKhuyenMai_DTO>>();
                            readTask.Wait();

                            list = readTask.Result;

                        }
                    }
                    listCTKM = list;
                    dgvDSKM.DataSource = listCTKM;
                    AddBinding();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnThemCTKM_Click(object sender, EventArgs e)
        {
            if (txtMaGiay.Text == null || txtCK.Text == null)
            {
                MessageBox.Show("Thêm thông tin chi tiết khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn thêm chi tiết khuyến mãi ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int maKM = Convert.ToInt32(txtIDKM.Text);
                    int maGiay = Convert.ToInt32(txtMaGiay.Text);
                    float CK = float.Parse(txtCK.Text);

                    CTKhuyenMai_DTO ctkm = new CTKhuyenMai_DTO(maKM,maGiay,CK);
                    List<CTKhuyenMai_DTO> list = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);
                        
                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<CTKhuyenMai_DTO>("CTKhuyenMai", ctkm);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Thêm thành công");
                        }

                        var responseTask = client.GetAsync($"CTKhuyenMai?idkm={maKM}");
                        responseTask.Wait();

                        var result1 = responseTask.Result;
                        if (result1.IsSuccessStatusCode)
                        {
                            var readTask = result1.Content.ReadAsAsync<List<CTKhuyenMai_DTO>>();
                            readTask.Wait();

                            list = readTask.Result;

                        }
                    }
                    listCTKM = list;
                    dgvDSKM.DataSource = listCTKM;
                    AddBinding();
                }
                else
                {
                    return;
                }
            }
        }

        private void dgvKhuyenMai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //dgvKhuyenMai.CurrentRow.Selected = true;

                int id = Convert.ToInt32(txtIDKM.Text);
                List<CTKhuyenMai_DTO> list = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    // GET api/CTKhuyenMai?idkm={idkm}
                    var responseTask = client.GetAsync($"CTKhuyenMai?idkm={id}");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<CTKhuyenMai_DTO>>();
                        readTask.Wait();

                        list = readTask.Result;

                    }
                    else //web api sent error response 
                    {
                        //log response status here..    

                    }
                }
                listCTKM = list;
                dgvDSKM.DataSource = listCTKM;
                AddBinding();
            }
            catch { }
        }
    }
}
