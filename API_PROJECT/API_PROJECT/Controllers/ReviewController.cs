using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using API_PROJECT.DTO.Request_DTO;
using API_PROJECT.reposatory;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public Ireposatory<Review> ReviewRepo;
        public IReviewRepository ReviewRepository;
        public ReviewController(Ireposatory<Review> ReviewRepo,IReviewRepository _reviewRepository)
        {
            this.ReviewRepo = ReviewRepo;
            this.ReviewRepository = _reviewRepository;
        }
        // GET: api/<ReviewController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<Review> temp = ReviewRepo.getall();
            List<ReviewResponseDTO> newTemp = new List<ReviewResponseDTO>();
            foreach (Review review in temp)
            {
                ReviewResponseDTO reviewResponse = new ReviewResponseDTO();
                reviewResponse.ID = review.ID;
                reviewResponse.ReviewText = review.ReviewText;
                reviewResponse.CustomerId = review.CustomerId;
                reviewResponse.ProductID = review.ProductID;
                reviewResponse.Date = review.Date;

                newTemp.Add(reviewResponse);
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

        [HttpGet("Product/{productID}")]
        public ActionResult<ResultDTO> GetReviewsByProductID(int productID)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                List<ReviewsByProductIdResponseDTO> reviewsResponse = new List<ReviewsByProductIdResponseDTO>();
                List<Review> reviews = ReviewRepository.GetReviewsByProductID(productID);
                foreach(var review in reviews)
                {
                    ReviewsByProductIdResponseDTO NewReview = new ReviewsByProductIdResponseDTO();
                    NewReview.ReviewText = review.ReviewText;
                    NewReview.Date = review.Date;
                    NewReview.CustomerName = review.Customer.ApplicationUser.UserName;
                    reviewsResponse.Add(NewReview);
                }
                result.Data = reviewsResponse;
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

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                ReviewResponseDTO reviewResponse = new ReviewResponseDTO();
                Review review = ReviewRepo.getbyid(id);
                reviewResponse.ID = review.ID;
                reviewResponse.ReviewText = review.ReviewText;
                reviewResponse.CustomerId = review.CustomerId;
                reviewResponse.ProductID = review.ProductID;
                reviewResponse.Date = review.Date;
                result.Data = reviewResponse;
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

        // POST api/<ReviewController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] ReviewRequestDTO reviewRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Review review = new Review();

                    review.ReviewText = reviewRequest.ReviewText;
                    review.CustomerId = reviewRequest.CustomerId;
                    review.ProductID = reviewRequest.ProductID;
                    review.Date = reviewRequest.Date;
                    
                    ReviewRepo.create(review);
                    result.Data = reviewRequest;
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

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, ReviewRequestDTO reviewRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Review Oldreview = ReviewRepo.getbyid(id);
                    reviewRequest.ID = Oldreview.ID;
                    Oldreview.ProductID = reviewRequest.ProductID;
                    Oldreview.CustomerId = reviewRequest.CustomerId;
                    Oldreview.ReviewText = reviewRequest.ReviewText;
                    Oldreview.Date = reviewRequest.Date ;
                    result.Data = reviewRequest;
                    result.Statescode = 200;

                    ReviewRepo.update(Oldreview);
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

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

            ResultDTO result = new ResultDTO();
            try
            {
                Review review = ReviewRepo.getbyid(id);

                ReviewRepo.delete(review);
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
