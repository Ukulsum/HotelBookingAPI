namespace HotelBookingAPI.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public int UsertId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public int RoleID { get; set; }
    }
}
