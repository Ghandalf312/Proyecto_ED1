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
        /// Variable declaration
        /// </summary>
        [Display(Name = "Hospital")]
        public string HospitalName { get; set; }
        public List<string> Departments { get; set; }
        public PriorityQueue<PatientExtModel> PatientQueue { get; set; }
        public List<PatientModel> VacunedList { get; set; }


 
        public void GetDepartments()
        {
            Departments = new List<string>();
            switch (HospitalName)
            {
                case "Guatemala":
                    Departments.Add("Guatemala, Amatitlan, Chinautla, Chuarrancho, Fraijanes, Mixco, Palencia, San Jose del Golfo, San Jose Pinula, San Juan Sacatepequez, San Miguel Petapa, San Pedro Ayampuc ");
                    Departments.Add("San Pedro Sacatepequez, San Raymundo, Santa Catarina Pinula, Villa Canales, Villa nueva");
                    break;
                case "Izabal":
                    Departments.Add("El Estor. Puerto Barrios, Livingston");
                    Departments.Add("Morales, Los Amates");
                    
                    break;
                case "Zacapa":
                    Departments.Add("Zacapa, Cabañas, Estanzuela, Gualan, Huite, La Unión");
                    Departments.Add("San Jorge, Teculutan, Usumatlan,Rio Hondo, San Diego ");
                    Departments.Add("Baja verapaz");
                    Departments.Add("Sololá");
                    Departments.Add("Quiché");
                    break;
                
            }
        }

        public bool PatientQueueFull()
        {
            return PatientQueue.IsFull();
        }

        
    }
}
