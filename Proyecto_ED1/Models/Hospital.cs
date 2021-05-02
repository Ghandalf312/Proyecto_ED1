using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using CustomGenerics;
using CustomGenerics.Estructuras;


using System.ComponentModel.DataAnnotations;

using Proyecto_ED1.Models.Storage;

namespace Proyecto_ED1.Models
{
    public class Hospital
    {

        /// <summary>
        /// Declaración de variables
        /// </summary>
        [Display(Name = "Hospital")]
        public string HospitalName { get; set; }
        public List<string> Departments { get; set; }

        /// <summary>
        /// Procedimiento que le asigna los repectivos departamentos a cada hospital.
        /// </summary>
        public void GetDepartments()
        {
            Departments = new List<string>();
            switch (HospitalName)
            {
                case "Alta Verapaz":
                    Departments.Add("Petén");
                    Departments.Add("Alta Verapaz");
                    Departments.Add("Baja Verapaz");
                    Departments.Add("El Progreso");
                    Departments.Add("Chiquimula");
                    Departments.Add("Zacapa");
                    Departments.Add("Izabal");
                    break;
                case "Guatemala":
                    Departments.Add("Chimaltenango");
                    Departments.Add("Guatemala");
                    Departments.Add("Sacatepéquez");
                    Departments.Add("Jalapa");
                    Departments.Add("Santa Rosa");
                    Departments.Add("Escuintla");
                    Departments.Add("Jutiapa");
                    break;
                case "Totonicapán":
                    Departments.Add("Huehuetenango");
                    Departments.Add("Quiché");
                    Departments.Add("San Marcos");
                    Departments.Add("Totonicapán");
                    Departments.Add("Sololá");
                    Departments.Add("Suchitepéquez");
                    Departments.Add("Retalhuleu");
                    Departments.Add("Quetzaltenango");
                    break;
                
            }
        }

    }
}
