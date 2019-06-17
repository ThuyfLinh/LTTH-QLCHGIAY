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
    public class CTHoaDonBanController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            List<CTHoaDonBan_DTO> item = CTHoaDonBan_DAO.Instance.GetCTHoaDonBan(id);
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(CTHoaDonBan_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                CTHoaDonBan_DAO.Instance.Insert_CTHoaDonBan(x.MaGiay,x.TenGiay,x.SoLuong);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
        public IHttpActionResult Delete(int idhd, int idgiay)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                CTHoaDonBan_DAO.Instance.Delete_CTHoaDonBan(idhd,idgiay);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult TimKiem(int id,string search)
        {
            List<CTHoaDonBan_DTO> item = CTHoaDonBan_DAO.Instance.SearchCTHD(id,search);
            return Ok(item);
        }
    }
}
