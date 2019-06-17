using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class PhanQuyenDAO
    {
        private static PhanQuyenDAO instance;

        public static PhanQuyenDAO Instance
        {
            get { if (instance == null) instance = new PhanQuyenDAO(); return instance; }
            private set { instance = value; }
        }

        public List<PhanQuyenDTO> GetDN()
        {
            List<PhanQuyenDTO> ListDN = new List<PhanQuyenDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from DANGNHAP");
            foreach (DataRow item in data.Rows)
            {
                PhanQuyenDTO dn = new PhanQuyenDTO(item);
                ListDN.Add(dn);
            }
            return ListDN;
        }

        public List<PhanQuyenDTO> Search(string str)
        {
            List<PhanQuyenDTO> List = new List<PhanQuyenDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_SearchDN @ten ", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                PhanQuyenDTO dn = new PhanQuyenDTO(item);
                List.Add(dn);
            }
            return List;
        }
        public int Update(int ID, string name, string pass, int phanquyen)
        {
            string query = "Update DANGNHAP set IDNhanVien = " + ID +"," +
                " Pass = N'" + pass + "'," +
                " PhanQuyen = "+ phanquyen +
                " where Name = N'" + name + "'";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int Insert(int ID, string name, string pass,int phanquyen)
        {
            string query = "Insert DANGNHAP(IDNhanVien,Name,Pass,PhanQuyen) values (" + ID + ","+
               "N'" + name + "'," +
                "N'" + pass + "'," +
               phanquyen +
               ")";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int Delete(string name)
        {
            string query = "delete DANGNHAP where Name = N'" + name +"'";
                          
            return DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
