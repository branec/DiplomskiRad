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
    
    public partial class TicketInventory
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public Nullable<int> StockQuantity { get; set; }
        public Nullable<int> ReservedQuantity { get; set; }
    
        public virtual Ticket Ticket { get; set; }
    }
}
