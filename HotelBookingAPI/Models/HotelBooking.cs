using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class HotelBooking
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string? ClientName { get; set; }
    }
}
