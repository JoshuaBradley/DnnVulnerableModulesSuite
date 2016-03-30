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

namespace JB.Security.SecurityVulnerabilities.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using DotNetNuke.Entities.Users;
    using DotNetNuke.Framework.JavaScriptLibraries;
    using DotNetNuke.Web.Mvc.Framework.ActionFilters;
    using DotNetNuke.Web.Mvc.Framework.Controllers;

    using JB.Security.SecurityVulnerabilities.Components;
    using JB.Security.SecurityVulnerabilities.Models;

    public class DisplayItemController : DnnController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var displayItem = DisplayItemManager.Instance.GetDisplayItem(ModuleContext.ModuleId) ?? new DisplayItem();
            // A whitelist of valid tags and a parser of the HTML would be required to protect this from XSS.
            return View(displayItem);
        }
        
        public ActionResult Edit()
        {
            var displayItem = DisplayItemManager.Instance.GetDisplayItem(this.ModuleContext.ModuleId) ?? new DisplayItem() {ModuleId = ModuleContext.ModuleId};
            return View(displayItem);
        }

        [HttpPost]
        public ActionResult Edit(DisplayItem displayItem)
        {
            var existingItem = DisplayItemManager.Instance.GetDisplayItem(displayItem.ModuleId);
            if (existingItem == null)
            {
                displayItem.ModuleId = this.ActiveModule.ModuleID;
                displayItem.CreatedByUserId = User.UserID;
                displayItem.CreatedOnDate = DateTime.UtcNow;
                displayItem.LastModifiedByUserId = User.UserID;
                displayItem.LastModifiedOnDate = DateTime.UtcNow;

                DisplayItemManager.Instance.CreateDisplayItem(displayItem);
            }
            else
            {
                existingItem.LastModifiedByUserId = User.UserID;
                existingItem.LastModifiedOnDate = DateTime.UtcNow;
                existingItem.Title = displayItem.Title;
                existingItem.Display = displayItem.Display;

                DisplayItemManager.Instance.UpdateDisplayItem(existingItem);
            }
            
            return RedirectToDefaultRoute();
        }
        
        public ActionResult Delete()
        {
            DisplayItemManager.Instance.DeleteDisplayItem(ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }
    }
}