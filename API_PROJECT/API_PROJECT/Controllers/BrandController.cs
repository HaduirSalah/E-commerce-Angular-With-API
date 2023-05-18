using API_PROJECT.DTO;
using API_PROJECT.DTO.Request_DTO;
using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public Ireposatory<Brand> BrandRepo;
        public BrandController(Ireposatory<Brand> BrandRepo)
        {
            this.BrandRepo = BrandRepo;
        }

        // GET: api/<BrandController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

                ResultDTO result = new ResultDTO();

                List<Brand> temp = BrandRepo.getall();
                List<BrandResponseDTO> newTemp = new List<BrandResponseDTO>();
                foreach (Brand brand in temp)
                {
                    BrandResponseDTO barndDTO = new BrandResponseDTO();
                    barndDTO.ID = brand.ID;
                    barndDTO.Name = brand.Name;

                    newTemp.Add(barndDTO);
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

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
                ResultDTO result = new ResultDTO();
                try
                {
                    BrandResponseDTO brandResponseDTO = new BrandResponseDTO();
                    Brand brand = BrandRepo.getbyid(id);
                    brandResponseDTO.ID = brand.ID;
                    brandResponseDTO.Name = brand.Name;
                    result.Data = brandResponseDTO;
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

        // POST api/<BrandController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] BrandRequestDTO brandRequestDTO)
        {
            
                ResultDTO result = new ResultDTO();

                if (ModelState.IsValid)
                {
                    try
                    {
                        Brand brand = new Brand();
                        brand.ID = brandRequestDTO.ID;
                        brand.Name = brandRequestDTO.Name;

                        BrandRepo.create(brand);
                        result.Data = brandRequestDTO.Name;
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

        // PUT api/<BrandController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, [FromBody] BrandRequestDTO newBrand)
        {

                ResultDTO result = new ResultDTO();

                if (ModelState.IsValid)
                {
                    try
                    {
                        Brand oldBrand = BrandRepo.getbyid(id);
                        newBrand.ID = oldBrand.ID;
                        oldBrand.Name = newBrand.Name;

                        result.Data = newBrand;
                        result.Statescode = 200;

                        BrandRepo.update(oldBrand);
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

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

                ResultDTO result = new ResultDTO();
                try
                {
                    Brand brand = BrandRepo.getbyid(id);

                    BrandRepo.delete(brand);
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
