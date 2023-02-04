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
        public IActionResult Create(HotelBooking booking)
        {
            // id is null in json so we add to context
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, booking);
            }
            else
            {
                return NotFound(booking);
            }
        }

        [HttpPut]
        public IActionResult Update(HotelBooking booking)
        {
            var currentBooking = _context.Bookings.Find(booking.Id);
            if (currentBooking == null)
            {
                return NotFound(booking);
            }

            currentBooking.RoomNumber = booking.RoomNumber;
            currentBooking.ClientName = booking.ClientName;

            _context.SaveChanges();
            return Ok(currentBooking);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _context.Bookings.Find(id);

            if (res == null)
            {
                return NotFound(res);
            }
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _context.Bookings.Find(id);
            if (res == null)
            {
                return NotFound(res);
            }
            _context.Bookings.Remove(res);
            _context.SaveChanges();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _context.Bookings.ToList();

            return Ok(res);
        }
    }
}
