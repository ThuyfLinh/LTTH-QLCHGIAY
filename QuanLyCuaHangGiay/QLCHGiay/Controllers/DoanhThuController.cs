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
    public class DoanhThuController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(DateTime tungay, DateTime denngay)
        {
            List<ThongKe_DTO> item = ThongKe_DAO.Instance.ThongKe(tungay,denngay);
            return Ok(item);
        }

    }
}
