using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCuaHangGiay_Data.DTO;

namespace QLCuaHangGiay_Data.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }

        public List<NhanVienDTO> GetNV()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from NhanVien");
            foreach (DataRow item in data.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO(item);
                list.Add(nv);
            }
            return list;

        }

        public int InsertNv(string hoten, DateTime ngsinh, string diachi)
        {
            //int result = DataProvider.Instance.ExecuteNonQuery(" EXEC dbo.USP_InsertNhanVien @tennv , @ngaysinh , @diachi ", new object[] { hoten , ngsinh , diachi });
            string query = "Insert NHANVIEN(TenNV,NgSinh,DiaChi) values (" +
                "N'" + hoten + "'," +
                "'" + ngsinh + "'," +
                "N'" + diachi + "'" +
                ")";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int UpdateNv(int manv, string hoten, DateTime ngsinh, string diachi)
        {
            string query = "Update NHANVIEN set TenNV = " +
                "N'" + hoten + "'," +
                " NgSinh = '" + ngsinh + "'," +
                " DiaChi = N'" + diachi + "'" +
                " where IDNhanVien = " + manv;
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int DeleteNv(int manv)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(" EXEC dbo.USP_DeleteNhanVien @manv ", new object[] { manv });

            return result;
        }

        public List<NhanVienDTO> SearchNv(string str)
        {
            List<NhanVienDTO> NvList = new List<NhanVienDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_SearchNhanVien @search ", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                NhanVienDTO NhanVien = new NhanVienDTO(item);
                NvList.Add(NhanVien);
            }
            return NvList;
        }
    }
}
