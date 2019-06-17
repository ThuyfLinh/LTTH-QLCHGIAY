using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace QLCHGiay.Controllers
{
    public class KhuyenMaiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<KhuyenMaiDTO> item = KhuyenMaiDAO.Instance.GetKhuyenMai();
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(KhuyenMaiDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                KhuyenMaiDAO.Instance.InsertKM(x.TenCT, x.MoTa, x.NgayBD, x.NgayKT, x.ChietKhau);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(KhuyenMaiDTO x)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");
                //int idkm, string tenct, string mota, DateTime ngaybd, DateTime ngaykt, float chietkhau
                KhuyenMaiDAO.Instance.UpdateKM(x.MaKM,x.TenCT,x.MoTa,x.NgayBD,x.NgayKT,x.ChietKhau);

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

                KhuyenMaiDAO.Instance.DeleteKM(id);

            }
            catch (Exception)
            {

            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult TimKiemKM(string TenCT)
        {
            List<KhuyenMaiDTO> item = KhuyenMaiDAO.Instance.SearchKM(TenCT);
            return Ok(item);
        }

    }
}
