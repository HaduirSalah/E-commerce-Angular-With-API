using System.ComponentModel.DataAnnotations;

namespace API_PROJECT.DTO
{
    public class RegisterUserDto
    {
       
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public string Email { get; set; }

        public string NameOfRoleOFUserID { get; set; }

        public string? SSN { get; set;}

        public string? AccountNumber { get; set;}




    }
}

