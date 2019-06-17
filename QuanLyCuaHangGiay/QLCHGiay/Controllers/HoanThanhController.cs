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
    public class HoanThanhController : ApiController
    {

        [HttpPost]
        public IHttpActionResult HoanThanh(KhuyenMaiDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                KhuyenMaiDAO.Instance.HoanThanh(x.List, x.TenCT, x.MoTa, x.NgayBD, x.NgayKT);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
    }
}
