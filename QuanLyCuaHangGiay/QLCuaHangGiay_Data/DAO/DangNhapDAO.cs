using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class DangNhapDAO
    {
        private static DangNhapDAO instance;

        public static DangNhapDAO Instance
        {
            get { if (instance == null) instance = new DangNhapDAO(); return instance; }
            private set { instance = value; }
        }
       
        public List<DangNhapDTO> DangNhap(string name, string pass)
        {
            List<DangNhapDTO> list = new List<DangNhapDTO>();
            string query = string.Format("SELECT COUNT(*) AS SoLuong  FROM DANGNHAP WHERE Name = '{0}' AND Pass = '{1}'", name, pass);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if ((int)data.Rows[0]["SoLuong"] > 0)
            {
                string query1 = string.Format("SELECT * FROM DANGNHAP WHERE Name = '{0}' AND Pass = '{1}'", name, pass);
                DataTable data1 = DataProvider.Instance.ExecuteQuery(query1);
                foreach (DataRow item in data1.Rows)
                {
                    DangNhapDTO DN = new DangNhapDTO(item);
                    list.Add(DN);
                }
            }
            
            return list;
        }

        public int Update(string namold, string name, string pass)
        {
            string query = "Update DANGNHAP set Name = " +
                "N'" + name + "'," +
                " Pass = N'" + pass + "'" +
                " where Name = N'" + namold+"'";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
