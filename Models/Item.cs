namespace JB.Security.SecurityVulnerabilities.Models
{
    using System;
    using System.Web.Caching;

    using DotNetNuke.ComponentModel.DataAnnotations;

    [TableName("SecurityVulnerabilities_Items")]
    //setup the primary key for table
    [PrimaryKey("ItemId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Item", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ItemId")]
    public class Item
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Photo { get; set; }

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