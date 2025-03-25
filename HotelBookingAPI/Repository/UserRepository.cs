using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.UserDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    public class UserRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public UserRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        // Add User
        public async Task<CreateUserResponseDTO>AddUserAsync(CreateUserDTO user)
        {
            CreateUserResponseDTO createUserResponseDTO = new CreateUserResponseDTO();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spAddUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", user.Email);
            // Convert password to hash here
            command.Parameters.AddWithValue("@PasswordHash", user.Password);
            command.Parameters.AddWithValue("@CreatedBy", "System");
            var userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(userIdParam);
            command.Parameters.Add(errorMessageParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            var UserId = (int)userIdParam.Value;

            if(UserId != -1)
            {
                createUserResponseDTO.UserId = UserId;
                createUserResponseDTO.IsCreated = true;
                createUserResponseDTO.Message = "User Created Successfully";
                return createUserResponseDTO;
            }

            var message = errorMessageParam.Value?.ToString();
            createUserResponseDTO.IsCreated = false;
            createUserResponseDTO.Message = message ?? "An unknown error occurred while creating the user.";
            return createUserResponseDTO;
        }


        //Assign Role To User
        //public async Task<UserRoleResponseDTO>AssignRoleToUserAsync(UserRoleResponseDTO userRole)
        //{
        //    UserRoleResponseDTO userRoleResponseDTO = new UserRoleResponseDTO();

        //    using var connection = _connectionFactory.CreateConnection();
        //    using var command = new SqlCommand("spAssignUserRole", connection);
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    command.Parameters.AddWithValue("@UserID", userRole.UserId);
        //}
    }
}
