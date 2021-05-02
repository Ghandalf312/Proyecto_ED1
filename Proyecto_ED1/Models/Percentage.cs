using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED1.Models
{
    public class Percentage
    {
        /// <summary>
        /// Declaración de variables
        /// </summary>
        [Display(Name = "Cantidad de personas sin vacunar")]
        public int NotVaccinated { get; set; }
        [Display(Name = "Cantidad de personas vacuandas")]
        public int Vaccinated { get; set; }
        [Display(Name = "Porcentage de vacunados")]
        public double PositivePercentage { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Percentage()
        {
            NotVaccinated = 0;
            Vaccinated = 0;
            PositivePercentage = 0;
        }
        /// <summary>
        /// Consigue el porcentaje con base a la cantidad de persona registradas en la lista de espera y los que ya han sido vacunados
        /// </summary>
        public void GetPercentage()
        {
            if (NotVaccinated > 0)
            {
                PositivePercentage = Math.Round(((double)Vaccinated / (NotVaccinated + Vaccinated)) * 100, 3);
            }
            else
            {
                if (NotVaccinated == 0 && Vaccinated > 0)
                {
                    PositivePercentage = 100;
                }
            }
        }
    }
}
