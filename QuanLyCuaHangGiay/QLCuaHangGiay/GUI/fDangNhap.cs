using QLCuaHangGiay_Data.DAO;
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
    public partial class fDangNhap : MetroFramework.Forms.MetroForm
    {
        public fDangNhap()
        {
            InitializeComponent();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<DangNhapDTO> listGiay = null;
        public string tenDN;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string name = txtTenDN.Text;
            string pass = txtMK.Text;

            DangNhapDTO DN = new DangNhapDTO(name, pass);
            List<DangNhapDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET api/DangNhap?name={name}&pass={pass}
                var responseTask = client.GetAsync($"DangNhap?name={name}&pass={pass}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<DangNhapDTO>>();
                    readTask.Wait();

                    list = readTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        fMain f = new fMain();
                        if (list.Count() != 0)
                        {
                            f.CheckPhanQuyen(list.SingleOrDefault<DangNhapDTO>());
                            f.Show();
                            
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else  
                {
                }
            }
           
        }
    }
}
