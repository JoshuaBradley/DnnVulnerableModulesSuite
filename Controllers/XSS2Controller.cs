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
    using System.Web.Mvc;

    using DotNetNuke.Web.Mvc.Framework.Controllers;

    using JB.Security.SecurityVulnerabilities.Models;

    public class XSS2Controller : DnnController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var displayItem = new DisplayItem() { Display = this.Request.QueryString["name"] };
            return View(displayItem);
        }
    }
}