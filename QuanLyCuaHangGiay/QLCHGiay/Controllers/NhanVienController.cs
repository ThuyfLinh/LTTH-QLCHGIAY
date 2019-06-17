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
    public class NhanVienController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<NhanVienDTO> item = NhanVienDAO.Instance.GetNV();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(NhanVienDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                NhanVienDAO.Instance.InsertNv(x.TenNv, x.NgaySinh, x.DiaChi);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Put(NhanVienDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                NhanVienDAO.Instance.UpdateNv(x.IdNv, x.TenNv, x.NgaySinh, x.DiaChi);

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

                NhanVienDAO.Instance.DeleteNv(id);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiemNhanVien(string TenNV)
        {
            List<NhanVienDTO> item = NhanVienDAO.Instance.SearchNv(TenNV);
            return Ok(item);
        }
    }
}
