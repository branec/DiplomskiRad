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
    
    public partial class CreditCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CreditCard()
        {
            this.TicketReservations = new HashSet<TicketReservation>();
        }
    
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public System.DateTime ExpDate { get; set; }
        public string CVV { get; set; }
        public string NameSurname { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketReservation> TicketReservations { get; set; }
    }
}
