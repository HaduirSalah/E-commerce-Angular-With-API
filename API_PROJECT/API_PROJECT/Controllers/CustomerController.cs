using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using API_PROJECT.DTO.Request_DTO;
using API_PROJECT.reposatory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public Ireposatory<Customer> CustomerRepo;
        public ICustomerRepository CustomerRepository;
        public UserManager<ApplicationUser> UserManager;
        
        public CustomerController(UserManager<ApplicationUser> _UserManager, Ireposatory<Customer> CustomerRepo, ICustomerRepository _CustomerRepo)
        {
            this.CustomerRepo = CustomerRepo;
            this.CustomerRepository = _CustomerRepo;
            this.UserManager = _UserManager;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        [Authorize]
        public ActionResult<ResultDTO> GetAll()
        {
            ResultDTO result = new ResultDTO();

            List<Customer> temp = CustomerRepo.getall("ApplicationUser");
            List<CustomerResponsDTO> newTemp = new List<CustomerResponsDTO>();
            foreach (Customer customer in temp)
            {
                CustomerResponsDTO customerRespons = new CustomerResponsDTO();
                customerRespons.ID = customer.ID;
                customerRespons.ApplicationUserId = customer.ApplicationUserId;
                customerRespons.TotalPoint = customer.TotalPoint;
                
                newTemp.Add(customerRespons);
                //result.Data = prod;
            }
            if (newTemp != null)
            {

                result.Statescode = 200;
                result.Data = newTemp;

                return result;
            }

            result.Statescode = 404;
            result.Message = "data not found";
            return result;

        }


        [HttpGet("{ApplicationUserID}")]
        public async Task<ActionResult<ResultDTO>> GetCustomerID(string ApplicationUserID)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                ApplicationUser applicationUser = await UserManager.FindByIdAsync(ApplicationUserID);
                IList<string> rolelistOfuser = await UserManager.GetRolesAsync(applicationUser);        
                if (rolelistOfuser.Contains("Customer"))
                { 
                    Customer customer = CustomerRepository.GetCustomerByID(ApplicationUserID);
                    CustomerResponsDTO customerRespons = new CustomerResponsDTO();
                    customerRespons.ID = customer.ID;
                    customerRespons.ApplicationUserId = customer.ApplicationUserId;
                    customerRespons.TotalPoint = customer.TotalPoint;
                    result.Data = customerRespons;
                    result.Statescode = 200;
                    return result;
                }
                else
                {
                    result.Statescode = 200;
                    result.Message = "USER ID not found as Customer Not Find";
                }
            }
            catch (Exception ex)
            {
                result.Message = "Erroe Not Find";
                result.Statescode = 404;
                
            }
            return result;
        }

        //[HttpGet("AppUserID")]
        //public ActionResult<ResultDTO> GetCustomerName(string ApplicationUserID)
        //{
        //    ResultDTO result = new ResultDTO();
        //    try
        //    {
        //        var UserName = UserManager.Users.Where(d => d.Id == ApplicationUserID).Select(d=>d.UserName).FirstOrDefault();
        //        result.Data = UserName;
        //        result.Statescode = 200;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = "Error Not Find";
        //        result.Statescode = 404;
        //        return result;
        //    }
        //}

        // GET api/<CustomerController>/5
        [Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                CustomerResponsDTO customerRespons = new CustomerResponsDTO();
                Customer customer = CustomerRepo.getbyid(id);
                customerRespons.ID = customer.ID;
                customerRespons.ApplicationUserId = customer.ApplicationUserId;
                customerRespons.TotalPoint = customer.TotalPoint;
                result.Data = customerRespons;
                result.Statescode = 200;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Error Not Find";
                result.Statescode = 404;
                return result;
            }
        }

        [Authorize]
        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] CustomerRequestDTO customerRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Customer customer = new Customer();

                    customer.ApplicationUserId = customerRequest.ApplicationUserId;
                    customer.TotalPoint = customerRequest.TotalPoint;



                    if (CustomerRepo.getall().FirstOrDefault(p => p.ApplicationUserId == customerRequest.ApplicationUserId) == null)
                    {
                        CustomerRepo.create(customer);
                        result.Data = customerRequest;
                        result.Statescode = 200;

                    }
                    else
                    {
                        result.Message = "User is Already used";
                        result.Statescode = 300;
                    }

                }
                catch (Exception ex)
                {
                    result.Message = "Error in inserting";
                    result.Statescode = 400;

                }


            }
            return result;
        }

        [Authorize]
        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, CustomerRequestDTO customerRequest) //[FromBody] string value)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Customer customer = CustomerRepo.getbyid(id);

                    customerRequest.ID = customer.ID;
                    customer.ApplicationUserId = customerRequest.ApplicationUserId;
                    customer.TotalPoint = customerRequest.TotalPoint;

                    result.Data = customerRequest;
                    result.Statescode = 200;

                    CustomerRepo.update(customer);
                    return result;
                }
                catch (Exception ex)
                {
                    result.Message = "Error in Updating";
                    result.Statescode = 400;
                    return result;
                }


            }
            return BadRequest(ModelState);
        }

        [Authorize]
        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                Customer customer = CustomerRepo.getbyid(id);

                CustomerRepo.delete(customer);
                result.Statescode = 200;
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Statescode = 400;
                result.Message = "Error in Delete";
                return result;
            }
        }
    }
}
