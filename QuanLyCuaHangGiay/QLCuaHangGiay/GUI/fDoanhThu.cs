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
    public partial class fDoanhThu : MetroFramework.Forms.MetroForm
    {
        public fDoanhThu()
        {
            InitializeComponent();
            Load();
            LoadDateTimePicker();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<ThongKe_DTO> list = null;
        
        void Load()
        {
            list = load();
            dgvThongKe.DataSource = list;
            TongDoanhThu();
        }

        private List<ThongKe_DTO> load()
        {
            DateTime tungay = Convert.ToDateTime(dtpTuNgay.Value);
            DateTime denngay = Convert.ToDateTime(dtpDenNgay.Value);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                // GET api/DoanhThu?tungay={tungay}&denngay={denngay}                
                var responseTask = client.GetAsync($"DoanhThu?tungay={tungay}&denngay={denngay}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ThongKe_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else 
                {
                    
                }
            }
            return list;
        }
        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpTuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
        }
        void TongDoanhThu()
        {
            double tongtien = 0;
            int n = dgvThongKe.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                double y;
                string s = dgvThongKe.Rows[i].Cells["TongTien"].Value.ToString();
                double.TryParse(s, out y);
                tongtien = tongtien + y;
            }
            lblTongTien.Text = tongtien.ToString();
            lblTongHoaDon.Text = n.ToString();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            Load();
        }
    }
}
