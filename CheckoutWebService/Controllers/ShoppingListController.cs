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
            try
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
            catch(System.Exception e)
            {
                //a log here may be usefull
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(Drink item)
        {
            try
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
            catch (System.Exception e)
            {
                //a log here may be usefull
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(string itemName)
        {
            try
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
            catch (System.Exception e)
            {
                //a log here may be usefull
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public IHttpActionResult GetItem(string id)
        {
            try
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
            catch (System.Exception e)
            {
                //a log here may be usefull
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [BasicAuthentication("ApiUsername", "ApiPassword")]
        public IHttpActionResult GetList(int pageNo = 1, int pageSize = 50)
        {
            try
            {
                // Determine the number of itmes to skip
                int skip = (pageNo - 1) * pageSize;

                // Get total number of items
                int total = _repository.List.Count();

                // Select the items based on paging parameters
                var items = _repository.List
                    .OrderBy(c => c.Key)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                // Get the page links
                var linkBuilder = new PageLinkBuilder(Url, "DefaultApi", null, pageNo, pageSize, total);

                // Return the list of items
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
            catch (System.Exception e)
            {
                //a log here may be usefull
                return InternalServerError(e);
            }
        }
    }
}
