namespace JB.Security.SecurityVulnerabilities.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Caching;

    using DotNetNuke.ComponentModel.DataAnnotations;

    [TableName("SecurityVulnerabilities_Orders")]
    //setup the primary key for table
    [PrimaryKey("OrderId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Order", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("UserId")]
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string ItemIds { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public decimal Total { get; set; }

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int CreatedByUserId { get; set; } = -1;

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int LastModifiedByUserId { get; set; } = -1;

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;
    }
}