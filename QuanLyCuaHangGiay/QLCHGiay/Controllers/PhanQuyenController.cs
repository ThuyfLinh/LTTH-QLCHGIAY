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
    public class PhanQuyenController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<PhanQuyenDTO> item = PhanQuyenDAO.Instance.GetDN();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(PhanQuyenDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                PhanQuyenDAO.Instance.Insert(x.IDNV,x.Name,x.Pass,x.PhanQuyen);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(PhanQuyenDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                PhanQuyenDAO.Instance.Update(x.IDNV, x.Name, x.Pass, x.PhanQuyen);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        public IHttpActionResult Delete(string name)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                PhanQuyenDAO.Instance.Delete(name);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiem(string name)
        {
            List<PhanQuyenDTO> item = PhanQuyenDAO.Instance.Search(name);
            return Ok(item);
        }
    }
}
