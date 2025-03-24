using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    public class UserRoleDTO
    {
        [Required(ErrorMessage = "User Id is Required")]
        public int UsertId { get; set; }
        [Required(ErrorMessage = "Role Id is Required")]
        public string RoleId { get; set; }
    }
}
