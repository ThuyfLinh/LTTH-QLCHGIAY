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
    public class DangNhapController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDN(string name,string pass)
        {
            List<DangNhapDTO> item = DangNhapDAO.Instance.DangNhap(name, pass);
            return Ok(item);
        }
        [HttpPut]
        public IHttpActionResult Put(DangNhapDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                DangNhapDAO.Instance.Update(x.NameOld,x.Name,x.Pass);

            }
            catch (Exception)
            {

            }
            return Ok();
        }
    }
}
