using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLCHGiay.Controllers
{
    public class HoaDonBanController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<HoaDonBan_DTO> item = HoaDonBan_DAO.Instance.GetHoaDonBan();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult PostKHnew(HoaDonBan_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                HoaDonBan_DAO.Instance.Insert_HoaDonBan_KHnew(x.TenNV,x.TenKH,x.TenCT,x.Ngay,x.SDT);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult PostKHold(HoaDonBan_DTO x,bool hoadon = true)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                HoaDonBan_DAO.Instance.Insert_HoaDonBan_KHold(x.TenNV, x.TenKH, x.TenCT, x.Ngay, x.SDT);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(HoaDonBan_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                HoaDonBan_DAO.Instance.Update_HoaDonBan(x.MaHD,x.TenNV, x.TenKH, x.TenCT, x.Ngay, x.SDT);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiem(string search)
        {
            List<HoaDonBan_DTO> item = HoaDonBan_DAO.Instance.SearchHD(search);
            return Ok(item);
        }
    }
}
