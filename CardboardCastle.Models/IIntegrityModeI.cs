using System;

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
}
