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
    public partial class fCTKhuyenMai : MetroFramework.Forms.MetroForm
    {
        public fCTKhuyenMai()
        {
            InitializeComponent();
            Load();
        }

        public string baseAddress = "http://localhost:63920/api/";
        List<Giay_DTO> listGiay = null;
        List<CTKhuyenMai_DTO> listCTKM = null;
        private void Load()
        {
            listGiay = loadGiay();
            dgvDsGiay.DataSource = listGiay;
            AddBinding();
        }
        void AddBinding()
        {
            txtIDGiay.Text = dgvDsGiay.CurrentRow.Cells["IDGiay"].Value.ToString();
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
        private void radKMSP_CheckedChanged(object sender, EventArgs e)
        {
            txtCK.Enabled = false;
            dgvDsGiay.Enabled = true;
            dgvDSKM.Enabled = true;
            txtChietKhau.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void radKMDH_CheckedChanged(object sender, EventArgs e)
        {
            txtCK.Enabled = true;
            dgvDsGiay.Enabled = false;
            dgvDSKM.Enabled = false;
            txtChietKhau.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn hủy các thao tác vừa nhập ", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                txtChietKhau.Text = null;
            }
        }

        private void dgvDsGiay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDGiay.Text = dgvDsGiay.CurrentRow.Cells["IDGiay"].Value.ToString();
        }
        BindingSource listCTKhuyenMai = new BindingSource();
        List<CTKhuyenMai_DTO> listCT = new List<CTKhuyenMai_DTO>();

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtIDGiay.Text == "" || txtChietKhau.Text == "")
            {
                MessageBox.Show("Mã giày không tồn tại hoặc không có chiết khấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (DataGridViewRow row in dgvDSKM.Rows)
                {
                    if (dgvDSKM.Rows.Count > 1 && row.Cells["MaGiay"].Value != null && txtIDGiay.Text.Equals(row.Cells["MaGiay"].Value.ToString()))
                    {
                        MessageBox.Show("Mã giày đã có trong danh sách khuyến mãi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                int check;
                if (Int32.TryParse(txtChietKhau.Text, out check))
                {
                    CTKhuyenMai_DTO ChiTietKM = new CTKhuyenMai_DTO();
                    ChiTietKM.MaGiay = Convert.ToInt32(txtIDGiay.Text);
                    ChiTietKM.CK = Convert.ToInt32(txtChietKhau.Text);
                    listCTKhuyenMai.Add(ChiTietKM);
                    listCT.Add(ChiTietKM);
                    dgvDSKM.DataSource = listCTKhuyenMai;
                    dgvDSKM.Columns["MAKM"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Nhập sai chiết khấu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public string tempCTKM;
        int tempIndex;

        private void btnXoa_Click(object sender, EventArgs e)
        {
            tempCTKM = dgvDSKM.CurrentRow.Cells["MaGiay"].Value.ToString();
            if (tempCTKM == null)
            {
                MessageBox.Show("Chọn ít nhất 1 chi tiết khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết khuyến mãi là: " + tempCTKM, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    listCTKhuyenMai.RemoveAt(tempIndex);
                    listCT.RemoveAt(tempIndex);
                    tempIndex = 0;
                    tempCTKM = null;
                    dgvDSKM.DataSource = listCTKhuyenMai;
                }
                else
                {
                    return;
                }
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (txtTenCT.Text == "" || dtpTuNgay == null || dtpDenNgay == null)
            {
                MessageBox.Show("Nhập đầy đủ thông tin chương trình khuyến mãi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (radKMDH.Checked == true)
            {
                string tenct = txtTenCT.Text;
                string mota = txtMoTa.Text;
                DateTime ngaybd = Convert.ToDateTime(dtpTuNgay.Text);
                DateTime ngaykt = Convert.ToDateTime(dtpDenNgay.Text);
                float chietkhau = float.Parse(txtCK.Text);

                KhuyenMaiDTO CTKM = new KhuyenMaiDTO(tenct, mota, ngaybd, ngaykt, chietkhau);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<KhuyenMaiDTO>("KhuyenMai", CTKM);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("thêm thành công 1 phiếu đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            else
            {
                //KhuyenMaiDAO.Instance.HoanThanh(listCTKM, txtTenCT.Text, txtMoTa.Text, Convert.ToDateTime(dtpTuNgay.Text), Convert.ToDateTime(dtpDenNgay.Text));
                string tenct = txtTenCT.Text;
                string mota = txtMoTa.Text;
                DateTime ngaybd = Convert.ToDateTime(dtpTuNgay.Text);
                DateTime ngaykt = Convert.ToDateTime(dtpDenNgay.Text);

                KhuyenMaiDTO CTKM = new KhuyenMaiDTO(listCT,tenct, mota, ngaybd, ngaykt);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<KhuyenMaiDTO>("HoanThanh", CTKM);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("thêm thành công 1 phiếu đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }

            }

            fKhuyenMai KM = new fKhuyenMai();
            this.Hide();
            KM.ShowDialog();
            this.Show();
        }
    }
}
