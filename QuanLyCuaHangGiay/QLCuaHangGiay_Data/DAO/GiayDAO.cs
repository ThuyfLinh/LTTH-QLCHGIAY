 using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class GiayDAO
    {
        private static GiayDAO instance;
          
        public static GiayDAO Instance
        {
            get { if (instance == null) instance = new GiayDAO(); return instance; }
            private set { instance = value; }
        }
        

        public List<Giay_DTO> GetGiay()
        {
            List<Giay_DTO> list = new List<Giay_DTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Giay ");
            foreach (DataRow item in data.Rows)
            {
                Giay_DTO Giay = new Giay_DTO(item);
                list.Add(Giay);
            }
            return list;
        }

        public int InsertGiay(string Tengiay, int Soluong, decimal Dongia)
        {
            string query = "Insert GIAY(TenGiay,SoLuong,DonGia) values (" +
                "N'" + Tengiay + "'," +
                Soluong + ","+
                Dongia +
                ")";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int UpdateGiay(int idGiay, string tenGiay, int soLuong, decimal donGia)
        {
            string query = "Update GIAY set TenGiay = " +
                "N'" + tenGiay + "'," +
                " SoLuong = " + soLuong + "," +
                " DonGia = " + donGia +
                " where IDGiay = " + idGiay;
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int DELETEGIAY(int idGiay)
        {
            //string query = "delete CTKHUYENMAI where IDGiay = " + idGiay +
            //               " update CTHOADONBAN set IDGiay = null where IDGiay = " + idGiay +
            //               " update CTHOADONNHAP set IDGiay = null where IDGiay = " + idGiay +
            //               " delete GIAY where IDGiay = " + idGiay;
            int result = DataProvider.Instance.ExecuteNonQuery(" EXEC USP_DeleteGiay @id ", new object[] { idGiay });

            return result;
        }
        public List<Giay_DTO> SEARCHGIAY(string str)
        {
            List<Giay_DTO> GiayList = new List<Giay_DTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_SEARCHGIAY @search ", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                Giay_DTO Giay = new Giay_DTO(item);
                GiayList.Add(Giay);
            }
            return GiayList;
        }
    }
}
