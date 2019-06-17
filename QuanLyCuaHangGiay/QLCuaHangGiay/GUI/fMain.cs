using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangGiay_Data.DTO;

namespace QLCuaHangGiay.GUI
{
    public partial class fMain : MetroFramework.Forms.MetroForm
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void mtNhanVien_Click(object sender, EventArgs e)
        {
            fNhanVien nv = new fNhanVien();
            this.Hide();
            nv.ShowDialog();
            this.Show();
        }

        private void mtKhachHang_Click(object sender, EventArgs e)
        {
            fKhachHang form = new fKhachHang();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtGiay_Click(object sender, EventArgs e)
        {
            fGiay form = new fGiay();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtKhuyenMai_Click(object sender, EventArgs e)
        {
            fKhuyenMai form = new fKhuyenMai();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtHoaDonNhap_Click(object sender, EventArgs e)
        {
            fHoaDonNhap form = new fHoaDonNhap();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtHoaDonBan_Click(object sender, EventArgs e)
        {
            fHoaDonBan form = new fHoaDonBan();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtDoanhThu_Click(object sender, EventArgs e)
        {
            fDoanhThu form = new fDoanhThu();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        public void CheckPhanQuyen(DangNhapDTO x)
        {
            if(x.PhanQuyen == 2)
            {
                mtNhanVien.Enabled = false;
                mtGiay.Enabled = false;
                mtKhuyenMai.Enabled = false;
                mtDoanhThu.Enabled = false;
                mtPhanQuyen.Enabled = false;
                mtHoaDonBan.Enabled = true;
                mtHoaDonNhap.Enabled = true;
                mtKhachHang.Enabled = true;
                mtDoiMK.Enabled = true;
            }
            else if (x.PhanQuyen == 1)
            {
                mtNhanVien.Enabled = true;
                mtGiay.Enabled = true;
                mtKhuyenMai.Enabled = true;
                mtDoanhThu.Enabled = true;
                mtHoaDonBan.Enabled = false;
                mtHoaDonNhap.Enabled = false;
                mtKhachHang.Enabled = false;
                mtPhanQuyen.Enabled = false;
                mtDoiMK.Enabled = true;
            }
            else if (x.PhanQuyen == 3)
            {
                mtNhanVien.Enabled = true;
                mtGiay.Enabled = true;
                mtKhuyenMai.Enabled = true;
                mtDoanhThu.Enabled = true;
                mtHoaDonBan.Enabled = true;
                mtHoaDonNhap.Enabled = true;
                mtKhachHang.Enabled = true;
                mtPhanQuyen.Enabled = true;
                mtDoiMK.Enabled = true;
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void mtPhanQuyen_Click(object sender, EventArgs e)
        {
            fPhanQuyen form = new fPhanQuyen();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void mtDoiMK_Click(object sender, EventArgs e)
        {
            fDoiMK form = new fDoiMK();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
