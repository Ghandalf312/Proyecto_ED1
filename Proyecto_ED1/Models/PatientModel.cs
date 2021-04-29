using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_ED1.Models
{
    public class PatientModel : IComparable<PatientModel>
    {
        /// <summary>
        /// Declaracion de variables
        /// </summary>
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        
        public string DPI { get; set; }

        [Display(Name = "Departamento")]
        public string Department { get; set; }
        [Display(Name = "Municipio")]
        public string Municipality { get; set; }
        public int Age { get; set; }

        public int Priority { get; set; }

        public int CompareTo(PatientModel obj)
        {
            return Priority.CompareTo(obj.Priority);
        }



       
    }
}
