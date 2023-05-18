using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using API_PROJECT.DTO.Request_DTO;
using System.Numerics;
using Microsoft.Identity.Client;
using API_PROJECT.reposatory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public Ireposatory<Order> OrderRepo;
        public Ireposatory<Cart> CartRepo;
        public Ireposatory<Delivary> DelivaryRepo;
        public UserManager<ApplicationUser> UserManager;

        public OrderController(UserManager<ApplicationUser> UserManager , Ireposatory<Order> OrderRepo, Ireposatory<Cart> CartRepo, Ireposatory<Delivary> DelivaryRepo)
        {
            this.OrderRepo = OrderRepo;
            this.CartRepo = CartRepo;
            this.DelivaryRepo = DelivaryRepo;
            this.UserManager= UserManager;
        }

      

        // GET: api/<OrderController>

        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<Order> temp = OrderRepo.getall();
            List<OrderResponseDTO> newTemp = new List<OrderResponseDTO>();
            foreach (Order order in temp)
            {
                OrderResponseDTO OrderDTO = new OrderResponseDTO();
                OrderDTO.ID = order.ID;
                OrderDTO.CustomerID = order.CustomerID;
                OrderDTO.TotalPrice = order.TotalPrice;
                //OrderDTO.DelivaryID = order.DelivaryID;
                OrderDTO.CrediteNumber = order.CrediteNumber;
                OrderDTO.OrderState = order.OrderState;
                OrderDTO.PaidMethod = order.PaidMethod;
                OrderDTO.Date = order.Date;

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
        
        [HttpGet("waiting_to_delivred")]
        public ActionResult<ResultDTO> Getwaiting_to_delivred()
        {
            ResultDTO result = new ResultDTO();
            List<Order> temp = OrderRepo.getall().Where(e=>e.OrderState=="waiting to delivred" && e.DelivaryID==null).ToList();
            List<OrderResponseDTO> newTemp = new List<OrderResponseDTO>();
            foreach (Order order in temp)
            {
                OrderResponseDTO OrderDTO = new OrderResponseDTO();
                OrderDTO.ID = order.ID;
                OrderDTO.CustomerID = order.CustomerID;
                OrderDTO.TotalPrice = order.TotalPrice;
                //OrderDTO.DelivaryID = order.DelivaryID;
                OrderDTO.CrediteNumber = order.CrediteNumber;
                OrderDTO.OrderState = order.OrderState;
                OrderDTO.PaidMethod = order.PaidMethod;
                OrderDTO.Date = order.Date;

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

        [HttpGet("ASSigined_to_delivred/{OrderID}/{delivaryid}")]
        public ActionResult<ResultDTO> ASSigined_to_delivred(int OrderID,int delivaryid)
        {
            ResultDTO result = new ResultDTO(); 
                try
                {
                    Order oldOrder = OrderRepo.getbyid(OrderID);
                    oldOrder.DelivaryID = delivaryid;
                    oldOrder.OrderState = "Wating to Arrive";
                    OrderRepo.update(oldOrder);
                    Delivary delivary = DelivaryRepo.getbyid(delivaryid);
                    delivary.IsBusy = true;
                    DelivaryRepo.update(delivary);
                    
                    result.Data = oldOrder;
                    result.Statescode = 200;
                }
                catch (Exception ex)
                {
                    result.Message = "Error in Updating";
                    result.Statescode = 400;
                }
            return result;
        }


        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                OrderResponseDTO OrderDTO = new OrderResponseDTO();
                Order order = OrderRepo.getall("OrderDetailes").FirstOrDefault(e=>e.ID==id);
                OrderDTO.ID = order.ID;
                OrderDTO.CustomerID = order.CustomerID;
                OrderDTO.TotalPrice = order.TotalPrice;
                //OrderDTO.DelivaryID = order.DelivaryID;
                OrderDTO.CrediteNumber = order.CrediteNumber;
                OrderDTO.OrderState = order.OrderState;
                OrderDTO.PaidMethod = order.PaidMethod;
                OrderDTO.Date = order.Date;
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


        [HttpGet("OrderByCustomer/{id:int}")]
        public ActionResult<ResultDTO> GetOrderByCustomerId(int id)
        {

            ResultDTO result = new ResultDTO();
            List<Order> orders = OrderRepo.getall().Where(e => e.CustomerID == id).ToList();
            List<OrderResponseDTO> newTemp = new List<OrderResponseDTO>();
            foreach (Order order in orders)
            {
                OrderResponseDTO OrderDTO = new OrderResponseDTO();
                OrderDTO.ID = order.ID;
                OrderDTO.CustomerID = order.CustomerID;
                OrderDTO.TotalPrice = order.TotalPrice;
                //OrderDTO.DelivaryID = order.DelivaryID;
                OrderDTO.CrediteNumber = order.CrediteNumber;
                OrderDTO.OrderState = order.OrderState;
                OrderDTO.PaidMethod = order.PaidMethod;
                OrderDTO.Date = order.Date;

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

        [HttpGet("OrderByDelivary/{id:int}")]
        public ActionResult<ResultDTO> OrderByDelivary(int id)
        {
            ResultDTO result = new ResultDTO();
            Order orders = OrderRepo.getall().FirstOrDefault(e => e.DelivaryID == id);
            if (orders != null)
            {
                result.Statescode = 200;
                result.Data = orders;
            }
            result.Statescode = 404;
            result.Message = "data not found";
            return result;
        }


        // POST api/<OrderController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] OrderRequestDTO orderRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    double totalPrice = 0;
                    int itemQuantity = 0;
                    var cartlist = CartRepo.getall().Where(p => p.CustomerID == orderRequest.CustomerID).ToList();
                    foreach (var item in cartlist)
                    {
                        totalPrice += item.TotalPrice;
                        itemQuantity += item.Quantity;
                        CartRepo.delete(item);
                    }

                    Order order = new Order();
                    // order.ID = orderRequest.ID;
                    order.Date = orderRequest.Date;
                    order.CustomerID = orderRequest.CustomerID;
                    order.TotalPrice = totalPrice;

                    //order.DelivaryID = orderRequest.DelivaryID;
                    order.CrediteNumber = orderRequest.CrediteNumber;
                    order.OrderState = "waiting to delivred";
                    order.PaidMethod = orderRequest.PaidMethod;
                    order.OrderDetailes = null;

                    OrderRepo.create(order);

                    order.OrderDetailes = new List<OrderDetailes>();
                    foreach (var item in cartlist) {
                        order.OrderDetailes.Add(new OrderDetailes

                        { OrderID = order.ID,
                            ProductID = item.ProductID,
                            Quntity = item.Quantity,
                            TotalPrice = item.TotalPrice
                        });

                    }
                    OrderRepo.update(order);

                    result.Data = order;
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


        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, OrderRequestDTO orderRequest)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    Order oldOrder = OrderRepo.getbyid(id);
                    

                    oldOrder.CustomerID = orderRequest.CustomerID;
                    oldOrder.TotalPrice = orderRequest.TotalPrice.Value;
                    oldOrder.DelivaryID = orderRequest.DelivaryID;
                    oldOrder.CrediteNumber = orderRequest.CrediteNumber;
                    oldOrder.OrderState = orderRequest.OrderState;
                    oldOrder.PaidMethod = orderRequest.PaidMethod;
                    oldOrder.Date = orderRequest.Date;
                    result.Data = orderRequest;
                    result.Statescode = 200;
                    OrderRepo.update(oldOrder);
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


        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

            ResultDTO result = new ResultDTO();
            try
            {
                Order order = OrderRepo.getbyid(id);

                OrderRepo.delete(order);
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
