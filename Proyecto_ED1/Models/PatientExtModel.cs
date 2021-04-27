using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED1.Models
{
    public class PatientExtModel : PatientModel
    {
        [Display(Name = "Departamento")]
        public string Department { get; set; }
        [Display(Name = "Municipio")]
        public string Municipality { get; set; }

    }
}
