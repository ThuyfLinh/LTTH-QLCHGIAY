﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DTO
{
    public class ThongKe_DTO
    {
        private int idHoaDon;
        private string tenNV;
        private int soLuong;
        private decimal tongTien;

        public int IdHoaDon { get => idHoaDon; set => idHoaDon = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }

        public ThongKe_DTO()
        {

        }

        public ThongKe_DTO(int idHoaDon, string tenNV, int soLuong, decimal tongTien)
        {
            this.idHoaDon = idHoaDon;
            this.tenNV = tenNV;
            this.soLuong = soLuong;
            this.tongTien = tongTien;
        }

        public ThongKe_DTO(DataRow row)
        {
            Int32.TryParse(row["IDHoaDon"].ToString(), out idHoaDon);
            this.tenNV = row["TenNV"].ToString();
            Int32.TryParse(row["SoLuongSP"].ToString(), out soLuong);
            decimal.TryParse(row["TongDoanhThu"].ToString(), out tongTien);
        }
    }
}
