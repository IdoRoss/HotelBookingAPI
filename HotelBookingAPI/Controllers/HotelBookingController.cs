using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            // id is null in json so we addto context
            if(booking.Id== 0) 
            { 
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.FirstOrDefault(b=> b.Id== booking.Id);
                if(bookingInDb==null)
                {
                    return new JsonResult(NotFound(booking));
                }
                bookingInDb = booking;
            }

            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }
        [HttpGet] public JsonResult Get(int id)
        {
            var res =  _context.Bookings.Find(id);

            if(res == null) 
            { 
                return new JsonResult(NotFound(res));
            }
            return new JsonResult(res);
        }

        [HttpDelete] public JsonResult Delete(int id)
        {
            var res = _context.Bookings.Find(id);
            if(res == null)
            {
                return new JsonResult(NotFound(res));
            }
            _context.Bookings.Remove(res);
            _context.SaveChanges();
            return new JsonResult(Ok(res));
        }
        [HttpGet("/getall")]
        public JsonResult GetAll(string id)
        {
            var res = _context.Bookings.ToList();

            return new JsonResult(res);
        }
    }
}
