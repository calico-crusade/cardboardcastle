﻿using System;

namespace CardboardCastle.Models
{
    public interface IIntegrityModel
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        string ObsoletedBy { get; set; }
        DateTime? ObsoletedOn { get; set; }
    }

    /// <summary>
    /// Represents an object that requires some form of an audit trail
    /// </summary>
    public abstract class IntegrityModel : IIntegrityModel
    {
        /// <summary>
        /// The date the object was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Who the object was created by
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The date the object was last modified
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// Who the object was last modified by
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// When the object was deleted (null = not deleted)
        /// </summary>
        public DateTime? ObsoletedOn { get; set; }
        /// <summary>
        /// Who the object was deleted by
        /// </summary>
        public string ObsoletedBy { get; set; }
    }
}
