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
    public partial class fDoiMK : MetroFramework.Forms.MetroForm
    {
        
        public fDoiMK()
        {
            InitializeComponent();
            
        }
        public string baseAddress = "http://localhost:63920/api/";
        private void btnDoi_Click(object sender, EventArgs e)
        {
            if (txtMKold.Text == "" || txtMKnew.Text == "" || txtTennew.Text ==""||txtTenold.Text=="")
            {
                MessageBox.Show("phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var xacnhan = MessageBox.Show("bạn có chắc chắn muốn sửa MK : " + txtTenold.Text, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacnhan == DialogResult.Yes)
                {
                    string nameold = txtTenold.Text;
                    string namenew = txtTennew.Text;
                    string mknew = txtMKnew.Text;

                    DangNhapDTO MK = new DangNhapDTO(nameold,namenew,mknew);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseAddress);

                        //HTTP PUT
                        var postTask = client.PutAsJsonAsync<DangNhapDTO>("DangNhap", MK);

                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sửa MK thành công", "Thông báo", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Sửa MK không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       
                    }
                }
            }
        }
    }
}
