//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROJ_RYAN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Agenda
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public System.DateTime DataHora { get; set; }
        public Nullable<System.TimeSpan> Hora { get; set; }
        public string Celular { get; set; }
    }
}
