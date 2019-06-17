using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLyCuaHangGiay.Controllers
{
    public class GiayController : ApiController
    {
        public IHttpActionResult Get()
        {
            List<Giay_DTO> item = GiayDAO.Instance.GetGiay();
            if (item.Count == 0)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
