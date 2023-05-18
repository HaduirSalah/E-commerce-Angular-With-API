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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public Ireposatory<Cart> CartRepo;
        public CartController(Ireposatory<Cart> CartRepo)
        {
            this.CartRepo = CartRepo;
        }
        // GET: api/<CartController>
        [HttpGet("AllByuserID/{id}")]
		public ActionResult<ResultDTO> GetAll([FromRoute]int id)
        {

            ResultDTO result = new ResultDTO();
            List<Cart> temp = CartRepo.getall("Product").Where(e=>e.CustomerID==id).ToList();
            List<CartResponseDTO> newTemp = new List<CartResponseDTO>();
            foreach (Cart cart in temp)
            {
                CartResponseDTO cartResponse = new CartResponseDTO();
                cartResponse.ID = cart.ID;
                cartResponse.ProductID = cart.ProductID;
                cartResponse.CustomerID = cart.CustomerID;
                cartResponse.Quantity = cart.Quantity;
                cartResponse.TotalPrice = cart.TotalPrice;
                cartResponse.ImageOfProduct = cart.Product.ImageName;
                cartResponse.productName = cart.Product.Name;
                cartResponse.itemPrice = cart.Product.Price;
                newTemp.Add(cartResponse);
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
        /// <summary>
        /// ///////////////////////////////////////////if use it take care  you will use it if you want to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<CartController>/5
        [HttpGet("GetitemById/{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                CartResponseDTO cartResponse = new CartResponseDTO();
                Cart cart = CartRepo.getall("Product").FirstOrDefault(e=>e.ID==id);
                cartResponse.ID = cart.ID;
                cartResponse.ProductID = cart.ProductID;
                cartResponse.CustomerID = cart.CustomerID;
                cartResponse.Quantity = cart.Quantity;
                cartResponse.TotalPrice = cart.TotalPrice;
				cartResponse.ImageOfProduct = cart.Product.ImageName;
				cartResponse.productName = cart.Product.Name;
				cartResponse.itemPrice = cart.Product.Price;
				
                result.Data = cartResponse;
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

		

		// POST api/<CartController>
		[HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] CartRequestDTO cartRequest)
            {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {   
                try
                {
                    Cart cart = new Cart();
                    cart.ProductID = cartRequest.ProductID;
                    cart.Quantity = cartRequest.Quantity;
                    cart.TotalPrice = cartRequest.TotalPrice;
                    cart.CustomerID = cartRequest.CustomerID;
                    Cart carttemp = CartRepo.getall().FirstOrDefault(x => x.CustomerID == cartRequest.CustomerID && x.ProductID == cartRequest.ProductID);
                   if (carttemp != null)
                    {
                        carttemp.Quantity = cartRequest.Quantity;
                        carttemp.TotalPrice = cartRequest.TotalPrice;
                        CartRepo.update(carttemp);

                    }
                    else
                    {
                        CartRepo.create(cart);
                    }
					result.Data = cartRequest;
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

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, CartRequestDTO cartRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Cart oldCart = CartRepo.getbyid(id);
                    cartRequest.ID = oldCart.ID;
                    oldCart.ProductID = cartRequest.ProductID;
                    oldCart.Quantity = cartRequest.Quantity;
                    oldCart.TotalPrice = cartRequest.TotalPrice;
                    oldCart.CustomerID = cartRequest.CustomerID;
                    result.Data = cartRequest;
                    result.Statescode = 200;

                    CartRepo.update(oldCart);
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


        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                Cart cart = CartRepo.getbyid(id);

                CartRepo.delete(cart);
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
