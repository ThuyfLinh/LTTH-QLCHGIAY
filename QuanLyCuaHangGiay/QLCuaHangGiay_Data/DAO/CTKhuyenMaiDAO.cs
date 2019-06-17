using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class CTKhuyenMaiDAO
    {
        private static CTKhuyenMaiDAO instance;

        public static CTKhuyenMaiDAO Instance
        {
            get { if (instance == null) instance = new CTKhuyenMaiDAO(); return instance; }
            private set { instance = value; }
        }

        public List<CTKhuyenMai_DTO> GetListCTKM()
        {
            List<CTKhuyenMai_DTO> ListCTKM = new List<CTKhuyenMai_DTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT IDKHUYENMAI,IDGIAY,CHIETKHAU FROM CTKHUYENMAI");
            foreach (DataRow item in data.Rows)
            {
                CTKhuyenMai_DTO MaGiay = new CTKhuyenMai_DTO(item);
                ListCTKM.Add(MaGiay);
            }
            return ListCTKM;
        }

        public List<CTKhuyenMai_DTO> ListCTKM(int ID)
        {
            List<CTKhuyenMai_DTO> ListCTKM = new List<CTKhuyenMai_DTO>();
            //DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_ListCTKM @ma ", new object[] { ID });
            string query = "select * from CTKHUYENMAI where IDKhuyenMai = " + ID;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CTKhuyenMai_DTO MaKM = new CTKhuyenMai_DTO(item);
                ListCTKM.Add(MaKM);
            }
            return ListCTKM;
        }

        public int InsertCTKM(int IDKM,int IDGiay, float ChietKhau)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("Insert into CTKHUYENMAI(IDKhuyenMai,IDGiay,ChietKhau) Values(" +
                              "N'" + IDKM + "'," +
                              "N'" + IDGiay + "'," +
                              "N'" + ChietKhau + "'" +
                               ")" );
            return result;
        }
        public List<CTKhuyenMai_DTO> SearchKM(string str)
        {
            List<CTKhuyenMai_DTO> KMList = new List<CTKhuyenMai_DTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_SearchKM @str ", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                CTKhuyenMai_DTO tenKM = new CTKhuyenMai_DTO(item);
                KMList.Add(tenKM);
            }
            return KMList;
        }

        public int DeleteCTKM(int idkm, int idgiay)
        {
            string query = "delete CTKHUYENMAI where IDGiay = " + idgiay + " and IDKhuyenMai = " + idkm;
            //int result = DataProvider.Instance.ExecuteNonQuery("exec USP_DeleteCTKM @maGiay , @maKM ", new object[] { idgiay , idkm });
            return DataProvider.Instance.ExecuteNonQuery(query);
        }


        public int UpdateCTKM(int idkm, int idgiay, int chietkhau)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateCT @idkm , @idgiay , @chietkhau ", new object[] { idkm, idgiay,chietkhau });
            return result;
        }
    }
}
