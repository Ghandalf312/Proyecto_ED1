using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_ED1.Models
{
    public class PatientModel : IComparable
    {
        /// <summary>
        /// Declaracion de variables
        /// </summary>
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        public string DPI { get; set; }
        public int Priority { get; set; }
        public int Age { get; set; }
        public string Hospital { get; set; }
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return this.DPI.CompareTo(((PatientModel)obj).DPI);
            }
        }
        /// <summary>
        /// Asigna la prioridad del paciente con base a su edad.
        /// </summary>
        public void PriorityAssignment()
        {
            if (Age >= 60)
            {
                Priority = 1;
            }
            else if (17 < Age && Age < 60)
            {
                Priority = 2;
            }
            else if (1 < Age && Age <= 17)
            {
                Priority = 3;
            }
        }
    }
}
