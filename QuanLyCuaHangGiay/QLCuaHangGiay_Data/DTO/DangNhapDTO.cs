using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DTO
{
    public class DangNhapDTO
    {
        private int idnv;
        private string name;
        private string pass;
        private int phanquyen;
        private string nameold;
     
        public int IDNV { get => idnv; set => idnv = value; }
        public string Name { get => name; set => name = value; }
        public string Pass { get => pass; set => pass = value; }
        public int PhanQuyen { get => phanquyen; set => phanquyen = value; }
        public string NameOld { get => nameold; set => nameold = value; }
        public DangNhapDTO()
        {

        }
        public DangNhapDTO(string NameOld,string Name, string Pass)
        {
            this.nameold = NameOld;
            this.name = Name;
            this.pass = Pass;
        }
        public DangNhapDTO(string Name, string Pass)
        {
            this.name = Name;
            this.pass = Pass;
        }
        public DangNhapDTO(int IDNV, string Name, string Pass, int PhanQuyen)
        {
            this.idnv = IDNV;
            this.name = Name;
            this.pass = Pass;
            this.phanquyen = PhanQuyen;
        }
        public DangNhapDTO(DataRow row)
        {
            Int32.TryParse(row["IDNhanVien"].ToString(), out this.idnv);
            this.name = row["Name"].ToString();
            this.pass = row["Pass"].ToString();
            Int32.TryParse(row["PhanQuyen"].ToString(), out this.phanquyen);
        }
    }
}
