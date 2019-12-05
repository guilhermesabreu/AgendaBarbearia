using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJ_RYAN.ViewModels
{
    public class loginViewModel
    {
        [Display(Name = "Id")]
        public int id_login { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "Senha")]
        public string senha { get; set; }
        [Display(Name = "Data da alteração")]
        public Nullable<System.DateTime> data_cadastro { get; set; }
        [Display(Name = "Login")]
        public string login_email { get; set; }
        // Esta propriedade descriptografa a senha no banco 
        public string senhaDecrypt {
            get
            {   //Atribui à variável senhaBytes a conversão da senha do banco para bytes
                var senhaBytes = System.Convert.FromBase64String(this.senha);
                //Atribui à variável senhaString a conversão da senha em bytes para string
                var senhaString = System.Text.Encoding.UTF8.GetString(senhaBytes);

                return senhaString;
            }
        }
    }
}