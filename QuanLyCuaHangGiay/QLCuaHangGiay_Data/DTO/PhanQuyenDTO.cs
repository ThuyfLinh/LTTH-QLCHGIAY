using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DTO
{
    public class PhanQuyenDTO
    {
        private int idnv;
        private string name;
        private string pass;
        private int phanquyen;

        public int IDNV
        {
            get { return idnv; }
            set { idnv = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public int PhanQuyen
        {
            get { return phanquyen; }
            set { phanquyen = value; }
        }

        public PhanQuyenDTO()
        {
            
        }
        public PhanQuyenDTO(int idnv, string name,string pass, int PhanQuyen)
        {
            this.idnv = idnv;
            this.name = name;
            this.pass = pass;
            this.phanquyen = PhanQuyen;
        }
        
        public PhanQuyenDTO(DataRow row)
        {
            Int32.TryParse(row["IDNhanVien"].ToString(), out this.idnv);
            this.name = row["Name"].ToString();
            this.pass = row["Pass"].ToString();
            Int32.TryParse(row["PhanQuyen"].ToString(), out this.phanquyen);
        }
    }
}
