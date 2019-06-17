using QLCuaHangGiay_Data.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLCuaHangGiay_Data.DTO;

namespace QuanLyCuaHangGiay.Controllers
{
    public class KhuyenMaiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<KhuyenMaiDTO> item = KhuyenMaiDAO.Instance.GetKhuyenMai();
            if (item.Count == 0)
            {
                return NotFound();
            }
            return Ok(item);
        }
        public IHttpActionResult Post([FromBody]  KhuyenMaiDTO x)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            KhuyenMaiDAO.Instance.InsertKM(x.TenCT,x.MoTa,x.NgayBD,x.NgayKT,x.ChietKhau);
            return Ok();
        }

        public IHttpActionResult Put([FromBody] KhuyenMaiDTO x)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            KhuyenMaiDAO.Instance.UpdateKM(x.TenCT,x.MoTa,x.NgayBD,x.NgayKT,x.ChietKhau);
            return Ok();
        }

        public IHttpActionResult Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");
            KhuyenMaiDAO.Instance.DeleteKM(id);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiemSinhvienThiLai([FromUri]string TenCT)
        {
            List<KhuyenMaiDTO> item = KhuyenMaiDAO.Instance.SearchKM(TenCT);
            if (item.Count == 0)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
