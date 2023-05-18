using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_PROJECT.DTO;
using API_PROJECT.Models;
using System.Data;
using API_PROJECT.Model;

namespace API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly Ireposatory<Delivary> DelivaryRepo;
        private readonly Ireposatory<Customer> CustomerRepo;

        private readonly IConfiguration config;


        public AccountController(Ireposatory<Delivary> DelivaryRepo, Ireposatory<Customer> CustomerRepo, RoleManager<IdentityRole> RoleManager,UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.RoleManager = RoleManager;
            this.userManager = userManager;
            this.config = config;
            this.CustomerRepo = CustomerRepo;
            this.DelivaryRepo= DelivaryRepo;
        }



        [HttpPost("register")]//api/account/register
        public async Task<ResultDTO> Register(RegisterUserDto userDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {   
                bool ErrorinmakeSupopject=false;
                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = userDTO.Email;
                userModel.UserName = userDTO.UserName;
                IdentityResult resultOfUser = await userManager.CreateAsync(userModel, userDTO.Password);
                IdentityResult resultOfRole = await userManager.AddToRoleAsync(userModel, userDTO.NameOfRoleOFUserID);
                try
                {
                    if (userDTO.NameOfRoleOFUserID == "Delivery")
                    {
                        Delivary delivary = new Delivary()
                        {
                            IsBusy = false,
                            AccountNumber = userDTO.AccountNumber,
                            ApplicationUserId = userModel.Id,
                            SSN = userDTO.SSN,
                        };
                        DelivaryRepo.create(delivary);
                    }
                    else if (userDTO.NameOfRoleOFUserID == "Customer")
                    {
                        Customer customer = new Customer() { TotalPoint = 0, ApplicationUserId = userModel.Id };
                        CustomerRepo.create(customer);
                     
                    }

                }
                catch (Exception)
                {
                    ErrorinmakeSupopject = true;
                }
                   
                if (resultOfUser.Succeeded && resultOfRole.Succeeded && !ErrorinmakeSupopject)
                {
                    resultDTO.Message = "succes";
                    resultDTO.Statescode = 200;
                }

                resultDTO.Message = "Failore";
                resultDTO.Statescode = 400;
                resultDTO.Data = ModelState;
            }

            return resultDTO;
        }

        [HttpPost("login")]//api/account/login
        public async Task<IActionResult> Login(LoginDTO userDto)
        {
            if (ModelState.IsValid)
            {
                //check 
                ApplicationUser userModel= await  userManager.FindByNameAsync(userDto.UserName);
                if (userModel != null && await userManager.CheckPasswordAsync(userModel,userDto.Password))
                {
                    List<Claim> myClaims = new List<Claim>();
                    myClaims.Add(new Claim(type: "ID", value: userModel.Id));
                    myClaims.Add(new Claim(type: "Name", value: userModel.UserName));
                    myClaims.Add(new Claim(type: "Email", value: userModel.Email));
                    myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                    List<string> roles =(List<string>)await userManager.GetRolesAsync(userModel);
                    if (roles != null) {
                        foreach (var item in roles)
                        {
                            myClaims.Add(new Claim(type: "Role", value: item));
                        }
                    }
                    var authSecritKey = 
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecrytKey"]));//asdZXCZX!#!@342352
                    
                    SigningCredentials credentials = 
                        new SigningCredentials(authSecritKey, SecurityAlgorithms.HmacSha256);
                    
                    //craete token
                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: config["JWT:ValidIss"],
                        audience: config["JWT:ValidAud"],
                        expires:DateTime.Now.AddHours(2),
                        claims:myClaims,
                        signingCredentials: credentials
                        );//signed token "resprest Toke"

                    return Ok(new
                    {
                        message = "Success",
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration=mytoken.ValidTo
                    }) ;

                }
                //craete toke
                return BadRequest("Invalid Login Account");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("getRoles")]//api/account/login
        public async Task<ResultDTO> getRoles()
        {
            ResultDTO ResultDTO = new ResultDTO();
            try
            {
                ResultDTO.Statescode = 200;
                ResultDTO.Data= RoleManager.Roles.ToList();
                

            }
            catch (Exception)
            {
                ResultDTO.Statescode = 400;
                ResultDTO.Data = RoleManager.Roles.ToList();
                ResultDTO.Message = "error in get roles";

            }
            return ResultDTO;
        }

        [HttpGet("CreateRoles")]//api/account/login
        public async Task<ResultDTO> CreateRoles(string RoleName )
        {
            ResultDTO ResultDTO = new ResultDTO();
            try
            {
                IdentityRole temp = new IdentityRole();
                temp.Name = RoleName;
                IdentityResult result = await RoleManager.CreateAsync(temp);
                if (result.Succeeded)
                {
                    ResultDTO.Statescode = 200;
                    ResultDTO.Message = "Successed";

                }
                else
                {
                    ResultDTO.Statescode = 400;
                    ResultDTO.Message = "Creation not succes";
                }
            }
            catch (Exception)
            {
                ResultDTO.Statescode = 400;
                ResultDTO.Data = null;
                ResultDTO.Message = "error in get roles";

            }
            return ResultDTO;
        }


    }
}
