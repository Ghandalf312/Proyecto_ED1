using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED1.Models
{
    public class Percentage
    {
        [Display(Name = "Cantidad de personas sin vacunar")]
        public int NotVaccinated { get; set; }
        [Display(Name = "Cantidad de personas vacuandas")]
        public int Vaccinated { get; set; }
        [Display(Name = "Porcentage de vacunados")]
        public double PositivePercentage { get; set; }

        public Percentage()
        {
            NotVaccinated = 0;
            Vaccinated = 0;
            PositivePercentage = 0;
        }

        public void GetPercentage()
        {
            if (NotVaccinated > 0)
            {
                PositivePercentage = Math.Round(((double)Vaccinated / NotVaccinated) * 100, 3);
            }
        }
    }
}
