using API_PROJECT.DTO;
using API_PROJECT.DTO.Request_DTO;
using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using API_PROJECT.PageResult;
using API_PROJECT.reposatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public Ireposatory<Product> ProductRepo;
        public IProductRepository ProdRepo;
        public ProductsController(Ireposatory<Product> ProductRepo, IProductRepository _prodcutRepo)
        {
            this.ProductRepo = ProductRepo;
            this.ProdRepo = _prodcutRepo;
        }


        [HttpGet("ByPage/{page:int}")]
		public PageResult<ProductResponeDTO> GettAllProductsByPage(int? page, int pagesize = 6)
		{
            
			ResultDTO result = new ResultDTO();
			List<Product> temp = ProductRepo.getall();
			List<ProductResponeDTO> newTemp = new List<ProductResponeDTO>();
			foreach (Product product in temp)
			{
				ProductResponeDTO prod = new ProductResponeDTO();
				prod.ID = product.ID;
				prod.ImageName = product.ImageName;
				prod.Name = product.Name;
				prod.Description = product.Description;
				prod.BrandID = product.BrandID;
				prod.CategoryID = product.CategoryID;
				prod.Quantity = product.Quantity;
				prod.Price = product.Price;
				prod.OfferID = product.OfferID;
				prod.SupplierID = product.SupplierID;
				newTemp.Add(prod);
			}

			float countDetails = ProductRepo.getall().Count();
			var resultt = new PageResult<ProductResponeDTO>
			{
				Count =(int)Math.Ceiling(countDetails / pagesize) ,
				PageIndex = page ?? 1,
				PageSize = pagesize,
				Items = newTemp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
			};
			return resultt;
		}

        // GET: api/<ProductsController>
        [HttpGet]
        //[Authorize]
        public ActionResult<ResultDTO> GetAll ()
        {
            ResultDTO result = new ResultDTO();
            List< Product> temp = ProductRepo.getall();
            List< ProductResponeDTO > newTemp= new List< ProductResponeDTO >();
            foreach (Product product in temp)
            {
                ProductResponeDTO prod = new ProductResponeDTO();
                prod.ID = product.ID;
                prod.ImageName = product.ImageName;
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.BrandID = product.BrandID;
                prod.CategoryID = product.CategoryID;
                prod.Quantity = product.Quantity;
                prod.Price = product.Price;
                prod.OfferID = product.OfferID;
                prod.SupplierID = product.SupplierID;
                newTemp.Add( prod );
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

        // GET api/<ProductsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try { 
            ProductResponeDTO prod = new ProductResponeDTO();
            Product product = ProductRepo.getbyid(id);
            prod.ID = product.ID;
            prod.ImageName = product.ImageName;
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.BrandID = product.BrandID;
            prod.CategoryID = product.CategoryID;
            prod.Quantity = product.Quantity;
            prod.Price = product.Price;
            prod.OfferID = product.OfferID;
            prod.SupplierID = product.SupplierID;
            result.Data = prod;
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

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromForm] Product_DTO product)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try {
                    Product prod = new Product();
                    prod.ID = default;
                    prod.ImageName = ImagesHelper.uploadImg(product.ImageOfProduct, "ProductIMG");
                    prod.Name= product.Name;
                    prod.Description = product.Description;
                    prod.BrandID    = product.BrandID;
                    prod.CategoryID = product.CategoryID;
                    prod.Quantity = product.Quantity;
                    prod.Price = product.Price;
                    prod.OfferID = product.OfferID;
                    prod.SupplierID = product.SupplierID;
                    ProductRepo.create(prod);
                    result.Data = product;
                    result.Statescode=200;
                    
                  }
                catch (Exception ex)
                {
                    result.Message = "Error in inserting";
                    result.Statescode = 400;
                    
                }
              

            }
            return result;
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id:int}")]
        public ActionResult<ResultDTO> Put(int id, Product_DTO newProd) //[FromBody] string value)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Product orgDept = ProductRepo.getbyid(id);
                    newProd.ID= orgDept.ID;
                    orgDept.Name = newProd.Name;
                    orgDept.BrandID = newProd.BrandID;
                    orgDept.ImageName = newProd.ImageName;
                    orgDept.CategoryID = newProd.CategoryID;
                    orgDept.Description = newProd.Description;
                    orgDept.Quantity = newProd.Quantity;
                    orgDept.Price = newProd.Price;
                    orgDept.OfferID = newProd.OfferID;
                    orgDept.SupplierID = newProd.SupplierID;
                    result.Data = newProd;
                    result.Statescode=200;

                    ProductRepo.update(orgDept);
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

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                Product orgDept = ProductRepo.getbyid(id);

                ProductRepo.delete(orgDept);
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


        [HttpGet("{Name:alpha}")]
        public ActionResult<ResultDTO> GetWithCategory(string Name)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                List<ProductResponeDTO> prodList = new List<ProductResponeDTO>();
                List<Product> products = ProdRepo.GetWithCategory(Name);
                foreach(var product in products)
                {
                    ProductResponeDTO prod = new ProductResponeDTO();
                    prod.ID = product.ID;
                    prod.ImageName = product.ImageName;
                    prod.Name = product.Name;
                    prod.Description = product.Description;
                    prod.BrandID = product.BrandID;
                    prod.CategoryID = product.CategoryID;
                    prod.Quantity = product.Quantity;
                    prod.Price = product.Price;
                    prod.OfferID = product.OfferID;
                    prod.SupplierID = product.SupplierID;
                    prodList.Add(prod);
                }

                result.Data = prodList;
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
    }
}
