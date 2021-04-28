using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED1.Models
{
    public class PatientExtModel : PatientModel
    {
        public string NameKey { get; set; }
        public string LastNameKey { get; set; }

        public int Priority { get; set; }
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
            else if (1 < Age && Age <= 17)
            {
                Priority = 3;
            }
        }

        public void HospitalAssigment()
        {
            if (Department.Equals("Guatemala"))
            {
                if(Municipality.Equals("Zona 1"))
                {
                    Hospital = "Roosevelt";
                }else if(Municipality.Equals("Zona 2"))
                {
                    Hospital = "San Juan de Dios";
                }
            }else if(Department.Equals("Izabal")){

                if (Municipality.Equals("Zona 1"))
                {
                    Hospital = "Hospital del Carmen";
                }
                else if (Municipality.Equals("Zona 2"))
                {
                    Hospital = "Puesto de Salud, Los Angeles";
                }
            }
            else if (Department.Equals("Zacapa")){
                if (Municipality.Equals("Zona 1"))
                {
                    Hospital = "Hospital regional de Zacapa";
                }
                else if (Municipality.Equals("Zona 2"))
                {
                    Hospital = "Centro Medico De Zacapa";
                }
            }
        }
    }
}
