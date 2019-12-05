using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJ_RYAN.ViewModels
{

    public partial class AgendaViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int ClienteId { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Data do Agendamento")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Appointment Time is required")]    
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Horário")]        
        public TimeSpan? Hora { get; set; }
    }
}