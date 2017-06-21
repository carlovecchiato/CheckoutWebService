using CheckoutWebService.ActionFilter;
using CheckoutWebService.BAL;
using CheckoutWebService.Models;
using CheckoutWebService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckoutWebService.Controllers
{
    public class ShoppingListController : ApiController
    {
        private ShoppingListRepository _repository = new ShoppingListRepository();

        [HttpPost]
        public IHttpActionResult AddItem(Drink item)
        {
            if (ModelState.IsValid)
            {
                int newQuantity = _repository.Add(item);
                return Ok(newQuantity);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(Drink item)
        {
            if (ModelState.IsValid)
            {
                if (_repository.Update(item))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(string itemName)
        {
            if (_repository.Delete(itemName))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult GetItem(string id)
        {
            Drink result = _repository.GetDrink(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [BasicAuthentication("ApiUsername", "ApiPassword")]
        public IHttpActionResult GetList(int pageNo = 1, int pageSize = 50)
        {
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;

            // Get total number of records
            int total = _repository.List.Count();

            // Select the records based on paging parameters
            var items = _repository.List
                .OrderBy(c => c.Key)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // Get the page links
            var linkBuilder = new PageLinkBuilder(Url, "DefaultApi", null, pageNo, pageSize, total);

            // Return the list of customers
            return Ok(new
            {
                Data = items,
                Paging = new
                {
                    First = linkBuilder.FirstPage,
                    Previous = linkBuilder.PreviousPage,
                    Next = linkBuilder.NextPage,
                    Last = linkBuilder.LastPage
                }
            });
        }
    }
}
