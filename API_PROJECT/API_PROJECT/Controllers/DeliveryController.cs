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
    public class DeliveryController : ControllerBase
    {
        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        public DelivaryReposatory delivaryReposatory;
        public Ireposatory<Delivary> DelivaryRepo;
        public DeliveryController(Ireposatory<Delivary> DelivaryRepo, UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager , DelivaryReposatory delivaryReposatory )
        {
            this.DelivaryRepo = DelivaryRepo;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.delivaryReposatory = delivaryReposatory;

        }
        
        
        
        
        
        
        // GET: api/<DeliveryController>
        [HttpGet("GetDelivaryByID/{ApplidicationUserID}")]
        public async Task<ActionResult<ResultDTO>> GetDelivaryByID( string ApplidicationUserID)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                   ApplicationUser applicationUser = await userManager.FindByIdAsync(ApplidicationUserID);
                    Delivary delivary = delivaryReposatory.GetDelivaryByID(ApplidicationUserID);
                    DeliveryResponseDTO DeliveryRespons = new DeliveryResponseDTO();
                    DeliveryRespons.ID=delivary.ID;
                    DeliveryRespons.ApplicationUserId = delivary.ApplicationUserId;
                    DeliveryRespons.SSN = delivary.SSN;
                    DeliveryRespons.IsBusy= delivary.IsBusy;
                    DeliveryRespons.AccountNumber = delivary.AccountNumber;
                    result.Data = DeliveryRespons;
                    result.Statescode = 200;
            }
            catch (Exception ex)
            {
                result.Message = "Erroe in try OF GET DELIVARY ID ";
                result.Statescode = 404;
            }
                return result;

        }


        [Authorize]
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<Delivary> temp = DelivaryRepo.getall();
            List<DeliveryResponseDTO> newTemp = new List<DeliveryResponseDTO>();
            foreach (Delivary delivary in temp)
            {
                DeliveryResponseDTO deliveryResponse = new DeliveryResponseDTO();
                deliveryResponse.ID = delivary.ID;
                deliveryResponse.SSN = delivary.SSN;
                deliveryResponse.IsBusy = delivary.IsBusy;
                deliveryResponse.ApplicationUserId = delivary.ApplicationUserId;
                deliveryResponse.AccountNumber = delivary.AccountNumber;

                newTemp.Add(deliveryResponse);
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

        [Authorize]
        // GET api/<DeliveryController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                DeliveryResponseDTO deliveryResponse = new DeliveryResponseDTO();
                Delivary delivary = DelivaryRepo.getbyid(id);
                deliveryResponse.ID = delivary.ID;
                deliveryResponse.SSN = delivary.SSN;
                deliveryResponse.IsBusy = delivary.IsBusy;
                deliveryResponse.ApplicationUserId = delivary.ApplicationUserId;
                deliveryResponse.AccountNumber = delivary.AccountNumber;
                result.Data = deliveryResponse;
                result.Statescode = 200;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Erroe Not Find";
                result.Statescode = 404;
                return result;
            }
        }



        [Authorize]
        // POST api/<DeliveryController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] DeliveryRequestDTO deliveryRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Delivary delivary = new Delivary();

                    delivary.ApplicationUserId = deliveryRequest.ApplicationUserId;
                    delivary.IsBusy = deliveryRequest.IsBusy;
                    delivary.ID = deliveryRequest.ID;
                    delivary.AccountNumber = deliveryRequest.AccountNumber;
                    delivary.SSN = deliveryRequest.SSN;

                    if (DelivaryRepo.getall().FirstOrDefault(p => p.ApplicationUserId == deliveryRequest.ApplicationUserId) == null)
                    {
                        DelivaryRepo.create(delivary);
                        result.Data = deliveryRequest;
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
        // PUT api/<DeliveryController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, DeliveryRequestDTO deliveryRequest) //[FromBody] string value)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Delivary delivary = DelivaryRepo.getbyid(id);

                    deliveryRequest.ID = delivary.ID;
                    delivary.ApplicationUserId = deliveryRequest.ApplicationUserId;
                    delivary.IsBusy = deliveryRequest.IsBusy;
                    delivary.AccountNumber = deliveryRequest.AccountNumber;
                    delivary.SSN = deliveryRequest.SSN;

                    result.Data = deliveryRequest;
                    result.Statescode = 200;

                    DelivaryRepo.update(delivary);
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
        // DELETE api/<DeliveryController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                Delivary delivary = DelivaryRepo.getbyid(id);

                DelivaryRepo.delete(delivary);
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
