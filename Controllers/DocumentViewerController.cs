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
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Services.FileSystem;
    using DotNetNuke.Web.Mvc.Framework.Controllers;

    using JB.Security.SecurityVulnerabilities.Models;

    public class DocumentViewerController : DnnController
    {

        // String Concat Sql Statement --Use for checking tables and displaying all module data.
        [HttpGet]
        public ActionResult Index()
        {
            var documentName = this.Request.QueryString["Document"];
            var filePath = $"{Globals.ApplicationMapPath}\\{documentName}";

            if (string.IsNullOrEmpty(documentName) || !System.IO.File.Exists(filePath))
            {
                var document = new Document();
                document.Contents = "File not available";
                return View(document);
            }
            
            var contentType = MimeMapping.GetMimeMapping(filePath);

            return File(filePath, contentType);

            // This is the Proper way.
            ////if (string.IsNullOrEmpty(documentName))
            ////{
            ////    var document = new Document();
            ////    document.Contents = "File not available";
            ////    return View(document);
            ////}

            ////var path = "images/";
            ////var folder = FolderManager.Instance.GetFolder(-1, path);
            ////var file = FileManager.Instance.GetFile(folder, documentName);
            ////return File(file.PhysicalPath, file.ContentType);
        }
    }
}