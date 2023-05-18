using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using API_PROJECT.DTO.Request_DTO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        public Ireposatory<Supplier> SupplierRepo;
        public SupplierController(Ireposatory<Supplier> SupplierRepo)
        {
            this.SupplierRepo = SupplierRepo;
        }
        // GET: api/<SupplierController>
        [HttpGet]
       public ActionResult<ResultDTO> GetAll()
       {
                ResultDTO result = new ResultDTO();
                
                List<Supplier> temp = SupplierRepo.getall();
                List<SupplierResponseDTO> newTemp = new List<SupplierResponseDTO>();
                foreach (Supplier supplier in temp)
                {
                    SupplierResponseDTO supplierResponseDTO = new SupplierResponseDTO();
                    supplierResponseDTO.ID = supplier.ID;
                    supplierResponseDTO.Name = supplier.Name;
                    supplierResponseDTO.SSN = supplier.SSN;
                    supplierResponseDTO.AccountNumber = supplier.AccountNumber;
                    supplierResponseDTO.TotalSales = supplier.TotalSales   ;
                    supplierResponseDTO.VerifecationState = supplier.VerifecationState;
                    newTemp.Add(supplierResponseDTO);
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
        

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                SupplierResponseDTO supplierResponse = new SupplierResponseDTO();
                Supplier supplier = SupplierRepo.getbyid(id);
                supplierResponse.ID = supplier.ID;
                supplierResponse.Name = supplier.Name;
                supplierResponse.SSN = supplier.SSN;
                supplierResponse.AccountNumber = supplier.AccountNumber;
                supplierResponse.TotalSales = supplier.TotalSales;
                supplierResponse.VerifecationState = supplier.VerifecationState;
                result.Data = supplierResponse;
                result.Statescode = 200;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Message = "Erroe Not Find";
                result.Statescode = 404;
                return result;
            }
        }

        // POST api/<SupplierController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] SupplierRequestDTO supplierRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Supplier supplier = new Supplier();
                    supplier.ID = supplierRequest.ID;
                    supplier.Name = supplierRequest.Name;
                    supplier.SSN = supplierRequest.SSN;
                    supplier.AccountNumber = supplierRequest.AccountNumber;
                    supplier.TotalSales = supplierRequest.TotalSales;
                    supplier.VerifecationState = supplierRequest.VerifecationState;
                    SupplierRepo.create(supplier);
                    result.Data = supplierRequest;
                    result.Statescode = 200;
                }
                catch (Exception ex)
                {
                    result.Message = "Error in inserting";
                    result.Statescode = 400;

                }


            }
            return result;
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, SupplierRequestDTO supplierRequest) //[FromBody] string value)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Supplier supplier = SupplierRepo.getbyid(id);
                    supplierRequest.ID = supplier.ID;
                    supplierRequest.Name = supplier.Name;
                    supplier.AccountNumber = supplierRequest.AccountNumber;
                    supplier.SSN = supplierRequest.SSN;
                    supplier.TotalSales = supplierRequest.TotalSales;
                    supplier.VerifecationState = supplierRequest.VerifecationState;

                    result.Data = supplierRequest;
                    result.Statescode = 200;

                    SupplierRepo.update(supplier);
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

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                Supplier supplier = SupplierRepo.getbyid(id);

                SupplierRepo.delete(supplier);
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
