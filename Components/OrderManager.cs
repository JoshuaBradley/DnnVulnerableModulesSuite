/*
' Copyright (c) 2016 Joshua Bradley
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using JB.Security.SecurityVulnerabilities.Models;

namespace JB.Security.SecurityVulnerabilities.Components
{
    using System.Data.SqlClient;
    using System.Linq;

    using DotNetNuke.Web.InternalServices;

    using JB.Security.SecurityVulnerabilities.Models;

    interface IOrderManager
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        IEnumerable<Order> GetOrders(int userId);
        IEnumerable<Item> GetItems();
    }

    class OrderManager : ServiceLocator<IOrderManager, OrderManager>, IOrderManager
    {
        public void CreateOrder(Order order)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Order>();
                rep.Insert(order);
            }
        }

        public void UpdateOrder(Order order)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Order>();
                rep.Update(order);
            }
        }

        public IEnumerable<Order> GetOrders(int userId)
        {
            using (var dataContext = DataContext.Instance())
            {
                var repository = dataContext.GetRepository<Order>();
                return repository.Find($"WHERE UserId = {userId}");
            }
        }

        public IEnumerable<Item> GetItems()
        {
            using (var dataContext = DataContext.Instance())
            {
                var repository = dataContext.GetRepository<Item>();
                return repository.Get();
            }
        } 

        protected override System.Func<IOrderManager> GetFactory()
        {
            return () => new OrderManager();
        }
    }
}
