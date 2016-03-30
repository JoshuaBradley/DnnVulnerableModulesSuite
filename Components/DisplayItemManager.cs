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

    using JB.Security.SecurityVulnerabilities.Models;

    interface IDisplayItemManager
    {
        void CreateDisplayItem(DisplayItem t);
        void DeleteDisplayItem(int moduleId);
        void DeleteDisplayItem(DisplayItem t);
        DisplayItem GetDisplayItem(int moduleId);

        IEnumerable<DisplayItem> GetDisplayItems(); 
        void UpdateDisplayItem(DisplayItem t);
    }

    class DisplayItemManager : ServiceLocator<IDisplayItemManager, DisplayItemManager>, IDisplayItemManager
    {
        public void CreateDisplayItem(DisplayItem displayItem)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<DisplayItem>();
                rep.Insert(displayItem);
            }
        }

        public void DeleteDisplayItem(int moduleId)
        {
            var displayItem = this.GetDisplayItem(moduleId);
            this.DeleteDisplayItem(displayItem);
        }

        public void DeleteDisplayItem(DisplayItem displayItem)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<DisplayItem>();
                rep.Delete(displayItem);
            }
        }

        public DisplayItem GetDisplayItem(int moduleId)
        {
            using (var dataContext = DataContext.Instance())
            {
                var repository = dataContext.GetRepository<DisplayItem>();
                return repository.Find($"WHERE ModuleId = {moduleId}").FirstOrDefault();
            }
        }

        public IEnumerable<DisplayItem> GetDisplayItems()
        {
            using (var dataContext = DataContext.Instance())
            {
                var repository = dataContext.GetRepository<DisplayItem>();
                return repository.Get();
            }
        }

        public void UpdateDisplayItem(DisplayItem displayItem)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<DisplayItem>();
                rep.Update(displayItem);
            }
        }

        protected override System.Func<IDisplayItemManager> GetFactory()
        {
            return () => new DisplayItemManager();
        }
    }
}
