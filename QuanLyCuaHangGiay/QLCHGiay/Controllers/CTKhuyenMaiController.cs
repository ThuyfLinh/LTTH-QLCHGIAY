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
    public class CTKhuyenMaiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<CTKhuyenMai_DTO> item = CTKhuyenMaiDAO.Instance.GetListCTKM();
            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult GetCTKM(int idkm)
        {
            List<CTKhuyenMai_DTO> item = CTKhuyenMaiDAO.Instance.ListCTKM(idkm);
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(CTKhuyenMai_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");
               
                CTKhuyenMaiDAO.Instance.InsertCTKM(x.MaKM,x.MaGiay,x.CK);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int idkm,int idgiay)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                CTKhuyenMaiDAO.Instance.DeleteCTKM(idkm,idgiay);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
    }
}
