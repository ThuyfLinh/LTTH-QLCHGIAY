using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class KhuyenMaiDAO
    {
        private static KhuyenMaiDAO instance;

        public static KhuyenMaiDAO Instance
        {
            get { if (instance == null) instance = new KhuyenMaiDAO(); return instance; }
            private set { instance = value; }
        }
        
        public List<KhuyenMaiDTO> GetKhuyenMai()
        {
            List<KhuyenMaiDTO> list = new List<KhuyenMaiDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from KhuyenMai ");
            foreach (DataRow item in data.Rows)
            {
                KhuyenMaiDTO KM = new KhuyenMaiDTO(item);
                list.Add(KM);
            }
            return list;
        }
        public List<KhuyenMaiDTO> SearchKM(string str)
        {
            List<KhuyenMaiDTO> KMList = new List<KhuyenMaiDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_SearchKM @str ", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                KhuyenMaiDTO tenKM = new KhuyenMaiDTO(item);
                KMList.Add(tenKM);
            }
            return KMList;
        }
       
        public int UpdateKM(int idkm, string tenct, string mota, DateTime ngaybd, DateTime ngaykt, float chietkhau)
        {
            string query = "Update KHUYENMAI set TenCT = " +
                "N'" + tenct + "'," +
                "MoTa = N'" + mota + "'," +
                "NgayBD = '" + ngaybd + "'," +
                "NgayKT = '" + ngaykt + "'," +
                "ChietKhau = " + chietkhau +
                " where IDKhuyenMai = " + idkm;
            return DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int DeleteKM(int maKM)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_DeleteKM @maKM ", new object[] { maKM });

            return result;
        }

        public int InsertKM(string tenct, string mota, DateTime ngaybd, DateTime ngaykt, float chietkhau)
        {
            
            string query = "Insert into KHUYENMAI(TenCT,MoTa,NgayBD,NgayKT,ChietKhau) values (" +
                "N'" + tenct + "',"+
                "N'" + mota + "'," +
                "'" + ngaybd + "'," +
                "'" + ngaykt + "'," +
                 chietkhau +
                ")";
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public int LayIDKM()
        {
            int id = 0;
            string query = string.Format("select max(IDKhuyenMai) as IDKhuyenMai from KHUYENMAI");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if(data.Rows.Count <= 0)
            {
                return 0;
            }
            else
            {
                foreach(DataRow item in data.Rows)
                {
                    id = Convert.ToInt32(item["IDKhuyenMai"]);
                }
            }
            return id;
        }

        public int ThemCTKM(List<CTKhuyenMai_DTO> CTKM, int IDKM)
        {
            foreach (CTKhuyenMai_DTO ct in CTKM)
            {
                int insert = DataProvider.Instance.ExecuteNonQuery("EXEC USP_ThemCTKM @IDKM , @MaGiay , @ChietKhau ", new object[] { IDKM, ct.MaGiay, ct.CK });
            }
            return 1;
        }

        public int HoanThanh(List<CTKhuyenMai_DTO> CTKM, string TenCT, string MoTa, DateTime NgayBD, DateTime NgayKT)
        {
            InsertKM(TenCT, MoTa, NgayBD, NgayKT, 0);
            int IDKMMoi = LayIDKM();
            ThemCTKM(CTKM, IDKMMoi);
            return 1;
        }

    }
}
