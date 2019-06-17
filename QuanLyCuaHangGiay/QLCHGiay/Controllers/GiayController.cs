using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLCHGiay.Controllers
{
    public class GiayController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Giay_DTO> item = GiayDAO.Instance.GetGiay();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(Giay_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                GiayDAO.Instance.InsertGiay(x.TenGiay, x.SoLuong, x.DonGia);
              
            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(Giay_DTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                GiayDAO.Instance.UpdateGiay(x.IDGiay,x.TenGiay, x.SoLuong, x.DonGia);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            //try
            //{
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                GiayDAO.Instance.DELETEGIAY(id);

            //}
            //catch (Exception)
            //{

            //}
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiemGiay(string TenGiay)
        {
            List<Giay_DTO> item = GiayDAO.Instance.SEARCHGIAY(TenGiay);
            return Ok(item);
        }
    }
}
