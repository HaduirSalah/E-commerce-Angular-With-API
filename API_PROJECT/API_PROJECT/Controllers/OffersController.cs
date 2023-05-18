using API_PROJECT.DTO;
using API_PROJECT.DTO.Request_DTO;
using API_PROJECT.DTO.Response_DTO;
using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        public Ireposatory<Offer> OfferRepo;
        public OffersController(Ireposatory<Offer> OfferRepo)
        {
            this.OfferRepo = OfferRepo;
        }
        // GET: api/<OffersController>
        [HttpGet]
        public ActionResult<ResultDTO> GetAll()
        {

            ResultDTO result = new ResultDTO();

            List<Offer> temp = OfferRepo.getall();
            List<OfferResponseDTO> newTemp = new List<OfferResponseDTO>();
            foreach (Offer offer in temp)
            {
                OfferResponseDTO offerResponse = new OfferResponseDTO();
                offerResponse.ID = offer.ID;
                offerResponse.Name = offer.Name;
                offerResponse.StartDate = offer.StartDate;
                offerResponse.EndDate = offer.EndDate;
                offerResponse.OfferPersentage = offer.OfferPersentage;
                newTemp.Add(offerResponse);
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

        // GET api/<OffersController>/5
        [HttpGet("{id}")]
        public ActionResult<ResultDTO> GetById(int id)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                OfferResponseDTO offerResponse = new OfferResponseDTO();
                Offer offer = OfferRepo.getbyid(id);
                offerResponse.ID = offer.ID;
                offerResponse.Name = offer.Name;
                offerResponse.StartDate = offer.StartDate;
                offerResponse.EndDate = offer.EndDate;
                offerResponse.OfferPersentage = offer.OfferPersentage;
                result.Data = offerResponse;
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

        // POST api/<OffersController>
        [HttpPost]
        public ActionResult<ResultDTO> Post([FromBody] OfferRequestDTO offerRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Offer offer = new Offer();
                    offer.ID = offerRequest.ID;
                    offer.Name = offerRequest.Name;
                    offer.StartDate = offerRequest.StartDate;
                    offer.EndDate = offerRequest.EndDate;
                    offer.OfferPersentage = offerRequest.OfferPersentage;
                    OfferRepo.create(offer);
                    result.Data = offerRequest;
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

        // PUT api/<OffersController>/5
        [HttpPut("{id}")]
        public ActionResult<ResultDTO> Put(int id, OfferRequestDTO offerRequest)
        {
            ResultDTO result = new ResultDTO();

            if (ModelState.IsValid)
            {
                try
                {
                    Offer oldOffer = OfferRepo.getbyid(id);
                    offerRequest.ID = oldOffer.ID;
                    oldOffer.Name = offerRequest.Name;
                    oldOffer.StartDate = offerRequest.StartDate;
                    oldOffer.EndDate = offerRequest.EndDate;
                    oldOffer.OfferPersentage = offerRequest.OfferPersentage;
                    result.Data = offerRequest;
                    result.Statescode = 200;

                    OfferRepo.update(oldOffer);
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

        // DELETE api/<OffersController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResultDTO> Delete(int id)
        {

            ResultDTO result = new ResultDTO();
            try
            {
                Offer offer = OfferRepo.getbyid(id);

                OfferRepo.delete(offer);
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
