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
    public class KhachHangController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<KhachHang_DTO> item = KhachHang_DAO.Instance.GetDSKH();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(KhachHang_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                KhachHang_DAO.Instance.InsertKH(x.Tenkh, x.Sdt);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(KhachHang_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                KhachHang_DAO.Instance.UpdateKH(x.Makh,x.Tenkh,x.Sdt);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                KhachHang_DAO.Instance.DeleteKhachHang(id);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult TimKiem(string TenKH)
        {
            List<Giay_DTO> item = GiayDAO.Instance.SEARCHGIAY(TenKH);
            return Ok(item);
        }
    }
}
