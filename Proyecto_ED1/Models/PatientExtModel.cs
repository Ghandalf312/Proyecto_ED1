using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED1.Models
{
    public class PatientExtModel : PatientModel
    {
        /// <summary>
        /// Declaración de variables
        /// </summary>
        public string NameKey { get; set; }
        public string LastNameKey { get; set; }
        public string Hospital { get; set; }

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
            else if (1 <= Age && Age <= 17)
            {
                Priority = 3;
            }
        }
    }
}
