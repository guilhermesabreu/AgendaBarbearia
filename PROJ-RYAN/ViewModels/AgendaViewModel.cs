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
        [Required(ErrorMessage = "Preencha o Campo Nome")]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Data do Agendamento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Preencha o Campo Data")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Preencha o Campo Hora")]    
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Horário")]        
        public TimeSpan? Hora { get; set; }
        [Display(Name ="Celular")] 
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0: (###) ###-####}")]
        [Required(ErrorMessage = "Preencha o Campo Celular")]
        public string Celular { get; set; }
    }
}