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
    public class CategoryController : ControllerBase
    {
        public Ireposatory<Category> CategoryRepo;
        public CategoryController(Ireposatory<Category> CategoryRepo)
        {
            this.CategoryRepo = CategoryRepo;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

                ResultDTO result = new ResultDTO();

                List<Category> temp = CategoryRepo.getall();
                List<CategoryDTO> newTemp = new List<CategoryDTO>();
                foreach (Category category in temp)
                {
                CategoryDTO categoryDTO = new CategoryDTO();
                categoryDTO.ID = category.ID;
                categoryDTO.Name = category.Name;
                   
                    newTemp.Add(categoryDTO);
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

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
           
                ResultDTO result = new ResultDTO();
                try
                {
                    CategoryDTO categoryDTO = new CategoryDTO();
                    Category category = CategoryRepo.getbyid(id);
                    categoryDTO.ID = category.ID;
                    categoryDTO.Name = category.Name;
                    result.Data = categoryDTO;
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

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] CategoryDTO categoryDTO)
        {
            
                ResultDTO result = new ResultDTO();

                if (ModelState.IsValid)
                {
                    try
                    {
                        Category category = new Category();
                        category.ID = categoryDTO.ID;
                        category.Name = categoryDTO.Name;
                      
                        CategoryRepo.create(category);
                        result.Data = categoryDTO.Name;
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

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, CategoryDTO newCategory)
        {
           
                ResultDTO result = new ResultDTO();

                if (ModelState.IsValid)
                {
                    try
                    {
                        Category oldCategory = CategoryRepo.getbyid(id);
                        newCategory.ID = oldCategory.ID;
                        oldCategory.Name = newCategory.Name;
                       
                        result.Data = newCategory;
                        result.Statescode = 200;

                        CategoryRepo.update(oldCategory);
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

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            
                ResultDTO result = new ResultDTO();
                try
                {
                    Category category = CategoryRepo.getbyid(id);

                    CategoryRepo.delete(category);
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
