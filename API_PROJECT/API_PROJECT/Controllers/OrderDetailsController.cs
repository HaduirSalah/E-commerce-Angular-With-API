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
    public class OrderDetailsController : ControllerBase
    {
        public Ireposatory<OrderDetailes> OrderDetailesRepo;
        public OrderDetailsController(Ireposatory<OrderDetailes> OrderDetailesRepo)
        {
            this.OrderDetailesRepo = OrderDetailesRepo;
        }
        // GET: api/<OrderDetailsController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<OrderDetailes> temp = OrderDetailesRepo.getall();
            List<OrderDetailsResponseDTO> newTemp = new List<OrderDetailsResponseDTO>();
            foreach (OrderDetailes order in temp)
            {
                OrderDetailsResponseDTO OrderDTO = new OrderDetailsResponseDTO();
                OrderDTO.ID = order.ID;
                OrderDTO.OrderID = order.OrderID;
                OrderDTO.TotalPrice = order.TotalPrice;
                OrderDTO.ProductID = order.ProductID;
                OrderDTO.Quntity = order.Quntity;
                newTemp.Add(OrderDTO);
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


        [HttpGet("OrderByOrderId/{id:int}")]
        public ActionResult<ResultDTO> GetOrderByOrderId(int id)
        {


            ResultDTO result = new ResultDTO();

            List<OrderDetailes> orders = OrderDetailesRepo.getall().Where(e => e.OrderID == id).ToList();

            List<OrderDetailsResponseDTO> newTemp = new List<OrderDetailsResponseDTO>();
            foreach (OrderDetailes order in orders)
            {
                OrderDetailsResponseDTO OrderDTO = new OrderDetailsResponseDTO();
                OrderDTO.ID = order.ID;
                OrderDTO.OrderID = order.OrderID;
                OrderDTO.TotalPrice = order.TotalPrice;
                OrderDTO.ProductID = order.ProductID;
                OrderDTO.Quntity = order.Quntity;

                newTemp.Add(OrderDTO);
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



























        // GET api/<OrderDetailsController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                OrderDetailsResponseDTO OrderDTO = new OrderDetailsResponseDTO();
                OrderDetailes order = OrderDetailesRepo.getbyid(id);
                OrderDTO.ID = order.ID;
                OrderDTO.OrderID = order.OrderID;
                OrderDTO.TotalPrice = order.TotalPrice;
                OrderDTO.ProductID = order.ProductID;
                OrderDTO.Quntity = order.Quntity;
                result.Data = OrderDTO;
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

        // POST api/<OrderDetailsController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] OrderDetailsRequestDTO orderDetailsRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    OrderDetailes order = new OrderDetailes();

                 
                    order.OrderID = orderDetailsRequest.OrderID;
                    order.TotalPrice = orderDetailsRequest.TotalPrice;
                    order.ProductID = orderDetailsRequest.ProductID;
                    order.Quntity = orderDetailsRequest.Quntity;

                    OrderDetailesRepo.create(order);
                    result.Data = orderDetailsRequest;
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

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, OrderDetailsRequestDTO orderDetailsRequest)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    OrderDetailes oldOrder = OrderDetailesRepo.getbyid(id);


                    oldOrder.OrderID = orderDetailsRequest.OrderID;
                    oldOrder.TotalPrice = orderDetailsRequest.TotalPrice;
                    oldOrder.ProductID = orderDetailsRequest.ProductID;
                    oldOrder.Quntity = orderDetailsRequest.Quntity;
                    result.Data = orderDetailsRequest;
                    result.Statescode = 200;
                    OrderDetailesRepo.update(oldOrder);
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


        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

            ResultDTO result = new ResultDTO();
            try
            {
                OrderDetailes order = OrderDetailesRepo.getbyid(id);

                OrderDetailesRepo.delete(order);
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
