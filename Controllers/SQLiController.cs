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
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Framework.JavaScriptLibraries;
    using DotNetNuke.Web.Mvc.Framework.ActionFilters;
    using DotNetNuke.Web.Mvc.Framework.Controllers;

    using JB.Security.SecurityVulnerabilities.Components;
    using JB.Security.SecurityVulnerabilities.Models;

    public class SQLiController : DnnController
    {
        // String Concat Sql Statement
        [HttpGet]
        public ActionResult Index()
        {
            var keyword = this.Request.QueryString["keyword"] ?? "-1";
            if (keyword.Equals("-1"))
            {
                var display = new DisplayItem() { Display = "Please Search!" };
                return View(display);
            }

            var sql = $"Select Display from SecurityVulnerabilities_DisplayItems where Display like '%{keyword}%'";
            var result = new StringBuilder(string.Empty);
            using (var connection = new SqlConnection(Config.GetConnectionString()))
            {
                var cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();

                ExecuteAndRead(cmd, result);
            }

            var displayItem = new DisplayItem() { Display = result.ToString() };
            return View(displayItem);
        }


        //String Concat Stored Proc.
        ////[HttpGet]
        ////public ActionResult Index()
        ////{
        ////    var keyword = this.Request.QueryString["keyword"] ?? "-1";
        ////    if (keyword.Equals("-1"))
        ////    {
        ////        var display = new DisplayItem() { Display = "Please Search!" };
        ////        return View(display);
        ////    }

        ////        var sql = $"Exec sp_GetDisplay {keyword}";
        ////    var result = new StringBuilder(string.Empty);
        ////    using (var connection = new SqlConnection(Config.GetConnectionString()))
        ////    {
        ////        var cmd = new SqlCommand(sql);
        ////        cmd.CommandType = CommandType.Text;
        ////        cmd.Connection = connection;
        ////        connection.Open();

        ////        ExecuteAndRead(cmd, result);
        ////    }

        ////    var displayItem = new DisplayItem() { Display = result.ToString() };
        ////    return View(displayItem);
        ////}

        private static void ExecuteAndRead(SqlCommand cmd, StringBuilder result)
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        result.Append(reader[i]);
                    }
                }
            }
        }
    }
}