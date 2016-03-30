namespace JB.Security.SecurityVulnerabilities.Models
{
    using System;
    using System.Web.Caching;

    using DotNetNuke.ComponentModel.DataAnnotations;

    [TableName("SecurityVulnerabilities_DisplayItems")]
    //setup the primary key for table
    [PrimaryKey("DisplayItemId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("DisplayItem", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class DisplayItem
    {
        /// <summary>
        /// Gets or sets the display item identifier.
        /// </summary>
        public int DisplayItemId { get; set; } = -1;
     
        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int ModuleId { get; set; } = -1;

        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        public string Title { get; set; }

        ///<summary>
        /// A string with the description of the object
        ///</summary>
        public string Display { get; set; }

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