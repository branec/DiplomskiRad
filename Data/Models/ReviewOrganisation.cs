//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReviewOrganisation
    {
        public int Id { get; set; }
        public Nullable<int> StarsService { get; set; }
        public Nullable<int> StarsAmbience { get; set; }
        public Nullable<int> StarsCleanless { get; set; }
        public string Comment { get; set; }
        public int IdOrganisation { get; set; }
    
        public virtual Organisation Organisation { get; set; }
    }
}
