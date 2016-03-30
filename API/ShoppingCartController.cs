namespace JB.Security.SecurityVulnerabilities.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web;
    using System.Web.Http;

    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Api;
    using DotNetNuke.Web.Mvp;

    using JB.Security.SecurityVulnerabilities.Components;
    using JB.Security.SecurityVulnerabilities.Models;
    
    public class ShoppingCartController : DnnApiController
    {
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage PurchaseOrder([FromBody]ShoppingCart shoppingCart)
        {
            try
            {
                var order = new Order()
                        { 
                            UserId = UserController.Instance.GetCurrentUserInfo().UserID,
                            Name = shoppingCart.Name,
                            Address = shoppingCart.Address,
                            City = shoppingCart.City,
                            State = shoppingCart.State,
                            ZipCode = shoppingCart.ZipCode,
                            Country = shoppingCart.Country,
                            ItemIds = string.Join(",", shoppingCart.ItemIds),
                            Total = shoppingCart.Total,
                        };

                OrderManager.Instance.CreateOrder(order);


                // TODO: Credit Card Processing would go here...

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Exceptions.LogException(e);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpGet]
        ////[HttpPost]
        ////[DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        [AllowAnonymous]
        public HttpResponseMessage UpdateAddress(string address, string city, string state, string zipCode, string country)
        {
            try
            {
                var orderManager = OrderManager.Instance;
                
                var orders = orderManager.GetOrders(UserController.Instance.GetCurrentUserInfo().UserID);

                foreach (var order in orders)
                {
                    order.Address = address;
                    order.City = city;
                    order.State = state;
                    order.ZipCode = zipCode;
                    order.Country = country;

                    orderManager.UpdateOrder(order);
                }

                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                ////httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                ////httpResponseMessage.Headers.Add("'Access-Control-Allow-Methods'", "POST");

                return httpResponseMessage;
            }
            catch(Exception exc)
            {
                Exceptions.LogException(exc);
                var httpResponseMessage = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                httpResponseMessage.Headers.Add("'Access-Control-Allow-Methods'", "POST");
                return httpResponseMessage;
            }
        }
        
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            var orderManager = OrderManager.Instance;
            var orders = orderManager.GetOrders(UserController.Instance.GetCurrentUserInfo().UserID);

            return orders;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var orderManager = OrderManager.Instance;
            var items = orderManager.GetItems();

            return items;
        }
    }
}