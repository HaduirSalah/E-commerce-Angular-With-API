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
   
    [Route("api/[controller]")]
    [ApiController]
    public class Category_BrandController : ControllerBase
    {
        public Ireposatory<Category_Brand> Category_BrandRepo;
        public Category_BrandController(Ireposatory<Category_Brand> Category_BrandRepo)
        {
            this.Category_BrandRepo = Category_BrandRepo;
        }
        // GET: api/<Category_BrandController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<Category_Brand> temp = Category_BrandRepo.getall();
            List<CategoryBrandResponseDTO> newTemp = new List<CategoryBrandResponseDTO>();
            foreach (Category_Brand category_Brand in temp)
            {
                CategoryBrandResponseDTO barndDTO = new CategoryBrandResponseDTO();
                barndDTO.ID = category_Brand.ID;
                barndDTO.BrandID = category_Brand.BrandID;
                barndDTO.CategoryID = category_Brand.CategoryID;

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

        // GET api/<Category_BrandController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                CategoryBrandResponseDTO categoryBrand = new CategoryBrandResponseDTO();
                Category_Brand category_Brand = Category_BrandRepo.getbyid(id);
                categoryBrand.ID = category_Brand.ID;
                categoryBrand.BrandID = category_Brand.BrandID;
                categoryBrand.CategoryID = category_Brand.CategoryID;
                result.Data = categoryBrand;
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

        // POST api/<Category_BrandController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] CategoryBrandRequestDTO categoryBrandRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Category_Brand category_Brand = new Category_Brand();
                    category_Brand.BrandID = categoryBrandRequest.BrandID;
                    category_Brand.CategoryID = categoryBrandRequest.CategoryID;

                    Category_BrandRepo.create(category_Brand);
                    result.Data = categoryBrandRequest;
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


        // PUT api/<Category_BrandController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, [FromBody] CategoryBrandRequestDTO categoryBrandRequest)
        {

            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Category_Brand oldBrand = Category_BrandRepo.getbyid(id);
                    categoryBrandRequest.ID = oldBrand.ID;
                    oldBrand.CategoryID = categoryBrandRequest.CategoryID;
                    oldBrand.BrandID = categoryBrandRequest.BrandID;

                    result.Data = categoryBrandRequest;
                    result.Statescode = 200;

                    Category_BrandRepo.update(oldBrand);
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

        // DELETE api/<Category_BrandController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

            ResultDTO result = new ResultDTO();
            try
            {
                Category_Brand brand = Category_BrandRepo.getbyid(id);

                Category_BrandRepo.delete(brand);
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
