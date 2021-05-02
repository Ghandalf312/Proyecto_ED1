using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomGenerics.Estructuras;
using CustomGenerics;
using Proyecto_ED1.Models;
using Proyecto_ED1.Models.Storage;

namespace Proyecto_ED1.Controllers
{
    public class HospitalController : Controller
    {
        public int contador =0;
        public static bool FirstTime = true;
        public Node<PatientExtModel> Temp1;

        #region Metodos GET
        public ActionResult Index()
        {
            if (FirstTime)
            {
                LoadHospitalsByDepartment();
                FirstTime = false;
            }
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }
        public ActionResult Espera()
        {
            return View();
        }

        public ActionResult Vacunados()
        {
            return View(Singleton.Instance.Vacunados);
        }

        public ActionResult Busqueda()
        {
            Singleton.Instance.miBuqueda.Clear();
            return View();
        }

        public ActionResult BusquedaVacunados()
        {
            Singleton.Instance.miBuquedaVacunados.Clear();
            return View();
        }

        public ActionResult Hospital1()
        {
            Singleton.Instance.Location.Clear();
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Alta Verapaz")
                {
                    Singleton.Instance.Location.Add(patient.Value);
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }

        public ActionResult Hospital2()
        {
            Singleton.Instance.Location.Clear();
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Guatemala")
                {
                    Singleton.Instance.Location.Add(patient.Value);
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }

        public ActionResult Hospital3()
        {
            Singleton.Instance.Location.Clear();
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Totonicapán")
                {
                    Singleton.Instance.Location.Add(patient.Value);
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }

        public ActionResult Simulacion()
        {
            return View();
        }
        public ActionResult Hospital1s()
        {
            Singleton.Instance.Location.Clear();
            int currentPatientCount = 0;
            int patientCount = 3;

            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Alta Verapaz" && currentPatientCount < patientCount && patient.Value.Priority == 1)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Alta Verapaz" && currentPatientCount < patientCount && patient.Value.Priority == 2)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Alta Verapaz" && currentPatientCount < patientCount && patient.Value.Priority == 3)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }
        public ActionResult Hospital2s()
        {
            Singleton.Instance.Location.Clear();
            int currentPatientCount = 0;
            int patientCount = 3;
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Guatemala" && currentPatientCount < patientCount && patient.Value.Priority == 1)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Guatemala" && currentPatientCount < patientCount && patient.Value.Priority == 2)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Guatemala" && currentPatientCount < patientCount && patient.Value.Priority == 3)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }
        public ActionResult Hospital3s()
        {
            Singleton.Instance.Location.Clear();
            int currentPatientCount = 0;
            int patientCount = 3;
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Totonicapán" && currentPatientCount < patientCount && patient.Value.Priority == 1)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Totonicapán" && currentPatientCount < patientCount && patient.Value.Priority == 2)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
            {
                if (patient.Value.Hospital == "Totonicapán" && currentPatientCount < patientCount && patient.Value.Priority == 3)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
        }
        public ActionResult Percentage()
        {
            Singleton.Instance.vaccinatedPercentage.GetPercentage();
            if (Singleton.Instance.vaccinatedPercentage != null)
            {
                return View(Singleton.Instance.vaccinatedPercentage);
            }
            else
            {
                return View(new Percentage());
            }
        }
        #endregion

        #region Metodos HTTPOST
        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            var option = collection["Option"];
            switch (option)
            {
                case "Registro":
                    return RedirectToAction("Registro");
                case "Espera":
                    return RedirectToAction("Espera");
                case "Simulacion":
                    return RedirectToAction("Simulacion");
                case "Vacunados":
                    return RedirectToAction("Vacunados");
                case "Porcentaje":
                    return RedirectToAction("Percentage");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Registro(IFormCollection collection)
        {
            try
            {
                string a = collection["DPI"].ToString();
                
                if (HasIncorrectCharacter(collection["Name"]) || HasIncorrectCharacter(collection["LastName"]) || HasIncorrectCharacter(collection["Municipality"]))
                {
                    ModelState.AddModelError("Name", "Por favor, ingrese datos no numericos en los campos pertenecientes.");
                    return View("Registro");
                }
                if (int.Parse(collection["Age"]) < 0 || int.Parse(collection["Age"]) > 120)
                {
                    ModelState.AddModelError("Age", "Por favor, una edad valida.");
                    return View("Registro");
                }
                else if (collection["Department"] == "Seleccionar Departamento")
                {
                    ModelState.AddModelError("Department", "Por favor seleccione un departamento");
                    return View("Registro");
                }else if (a.Length != 13)
                {
                    ModelState.AddModelError("DPI", "Por favor introduzca un DPI/CUI válido (13 carácteres)");
                    return View("Registro");
                }
              
                foreach (var patient in Singleton.Instance.patientsHash.GetAsNodes())
                {
                    if (patient.Value.DPI == collection["DPI"])
                    {
                        ModelState.AddModelError("DPI", "Un paciente con el mismo DPI ya ha sido ingresado en el sistema. Ingrese otro paciente.");
                        return View("Registro");
                    }
                }
                var newPatient = new PatientModel()
                {
                    Name = collection["Name"],
                    LastName = collection["LastName"],
                    DPI = collection["DPI"],
                    Age = int.Parse(collection["Age"]),
                    Department = collection["Department"],
                    Municipality = collection["Municipality"],

                   
                };

                var newPatientModel = new PatientExtModel
                {
                    Name = newPatient.Name,
                    LastName = newPatient.LastName,
                    NameKey = newPatient.Name,
                    LastNameKey = newPatient.LastName,
                    DPI = newPatient.DPI,
                    Age = newPatient.Age,
                    Department = newPatient.Department,
                    Municipality = newPatient.Municipality,
                    Hospital = GetHospital(collection["Department"])
                
                };
                newPatientModel.PriorityAssignment();
             

                //Agregar al arbol

                if (Singleton.Instance.patientsByName.Buscar(newPatientModel.Name) != null)
                {
                    var searchName = Singleton.Instance.repeatedNames.Find(x => x.value == newPatientModel.Name);
                    if (searchName == null)
                    {
                        var newRepeated = new RepeatedModel();
                        newRepeated.value = newPatient.Name;
                        newRepeated.numberRepeats++;
                        Singleton.Instance.repeatedNames.Add(newRepeated);
                        newPatientModel.NameKey = newPatientModel.Name + newRepeated.numberRepeats.ToString();
                        Singleton.Instance.patientsByName.Add(newPatientModel, newPatientModel.NameKey);
                    }
                    else
                    {
                        searchName.numberRepeats++;
                        newPatientModel.NameKey = newPatientModel.Name + searchName.numberRepeats.ToString();
                        Singleton.Instance.patientsByName.Add(newPatientModel, newPatientModel.NameKey);
                    }
                }
                else
                {
                    newPatientModel.NameKey = newPatientModel.Name;
                    Singleton.Instance.patientsByName.Add(newPatientModel, newPatientModel.NameKey);
                }


                if (Singleton.Instance.patientsByLastName.Buscar(newPatientModel.LastName) != null)
                {
                    var searchName = Singleton.Instance.repeatedLastNames.Find(x => x.value == newPatientModel.LastName);
                    if (searchName == null)
                    {
                        var newRepeated = new RepeatedModel();
                        newRepeated.value = newPatient.LastName;
                        newRepeated.numberRepeats++;
                        Singleton.Instance.repeatedLastNames.Add(newRepeated);
                        newPatientModel.LastNameKey = newPatientModel.LastName + newRepeated.numberRepeats.ToString();
                        Singleton.Instance.patientsByLastName.Add(newPatientModel, newPatientModel.LastNameKey);
                    }
                    else
                    {
                        searchName.numberRepeats++;
                        newPatientModel.LastNameKey = newPatientModel.LastName + searchName.numberRepeats.ToString();
                        Singleton.Instance.patientsByLastName.Add(newPatientModel, newPatientModel.LastNameKey);
                    }
                }
                else
                {
                    newPatientModel.LastNameKey = newPatientModel.LastName;
                    Singleton.Instance.patientsByLastName.Add(newPatientModel, newPatientModel.LastNameKey);
                }

                //Agregar a la cola de prioridad
                if (newPatientModel.Hospital == "Alta Verapaz")
                {
                    Singleton.Instance.patientsH1.AddPatient(newPatientModel.DPI, newPatientModel.Age, newPatientModel, newPatientModel.Priority);
                }
                else if (newPatientModel.Hospital == "Guatemala")
                {
                    Singleton.Instance.patientsH2.AddPatient(newPatientModel.DPI, newPatientModel.Age, newPatientModel, newPatientModel.Priority);
                }
                else if (newPatientModel.Hospital == "Totonicapán")
                {
                    Singleton.Instance.patientsH3.AddPatient(newPatientModel.DPI, newPatientModel.Age, newPatientModel, newPatientModel.Priority);
                }
                Singleton.Instance.patientsByDPI.Add(newPatientModel, newPatientModel.DPI);
                Singleton.Instance.patientsHash.Insert(newPatientModel, newPatientModel.DPI, GetMultiplier(newPatientModel.Hospital));
                contador++;
                Singleton.Instance.vaccinatedPercentage.NotVaccinated++;
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Por favor, asegurese de haber llenado todo los campos correctamente.");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Espera(IFormCollection collection)
        {
            var option = collection["Option"];
            switch (option)
            {
                case "Hospital1":
                    return RedirectToAction("Hospital1");
                case "Hospital2":
                    return RedirectToAction("Hospital2");
                case "Hospital3":
                    return RedirectToAction("Hospital3");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Hospital1(IFormCollection collection)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Hospital2(IFormCollection collection)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Hospital3(IFormCollection collection)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Simulacion(IFormCollection collection)
        {
            var option = collection["Option"];
            switch (option)
            {
                case "Hospital1s":
                    return RedirectToAction("Hospital1s");
                case "Hospital2s":
                    return RedirectToAction("Hospital2s");
                case "Hospital3s":
                    return RedirectToAction("Hospital3s");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Hospital1s(IFormCollection collection)
        {
            Temp1 = Singleton.Instance.patientsH1.Root;
            
            var patient1 = collection["Patient 1"];
            string patient = patient1.ToString();
            if (patient != "")
            {
                string patientDPI = patient.Substring(11, patient.Length - 11);
                var patientValue = patient.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH1.GetFirst();


                        }
                    }
                }
            }
            var patient2 = collection["Patient 2"];
            String patient2V = patient2.ToString();
            if (patient2V != "")
            {
                
                string patientDPI = patient2V.Substring(11, patient2V.Length - 11);
                var patientValue = patient2V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH1.GetFirst();


                        }
                    }
                }
            }
            var patient3 = collection["Patient 3"];
            String patient3V = patient3.ToString();
            if (patient3V != "")
            {
                string patientDPI = patient3V.Substring(11, patient3V.Length - 11);
                var patientValue = patient3V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH1.GetFirst();


                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Busqueda(IFormCollection collection)
        {
            var x = collection["select"];
            var busqueda = collection["search"];
            Singleton.Instance.miBuqueda.Clear();
            switch (x)
            {
                case "DPI":
                    try
                    {
                        for (int j = 0; j <= contador; j++)
                        {
                            Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByDPI.Buscar(Convert.ToString(busqueda)));
                        }
                    }
                    catch
                    {
                        TempData["Error"] = "Favor ingrese numero de DPI válido";
                    }
                    break;
                case "Nombre":
                    try
                    {
                       for (int j=0; j <= contador; j++)
                        {


                            int cant = Singleton.Instance.repeatedNames.Find(h => h.value == busqueda).numberRepeats;

                            for (int i = 0; i <= cant; i++)
                            {
                                if (i != 0)
                                {
                                    Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByName.Buscar(busqueda + i.ToString()));
                                }
                                else
                                {
                                    Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByName.Buscar(busqueda));
                                }
                            }
                        }

                    }
                    catch
                    {
                        for (int j = 0; j <= contador; j++)
                        {
                            Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByName.Buscar(busqueda));
                        }
                    }
                    break;
                case "Apellido":
                    try
                    {
                        for (int j = 0; j <= contador; j++)
                        {
                            int cant = Singleton.Instance.repeatedLastNames.Find(h => h.value == busqueda).numberRepeats;

                            for (int i = 0; i <= cant; i++)
                            {
                                if (i != 0)
                                {
                                    Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByLastName.Buscar(busqueda + i.ToString()));
                                }
                                else
                                {
                                    Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByLastName.Buscar(busqueda));
                                }
                            }
                        }
                    }
                    catch
                    {
                        for (int j = 0; j <= contador; j++)
                        {
                            Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByLastName.Buscar(busqueda));
                        }
                    }
                    break;
                default:
                    break;
            }
            return View(Singleton.Instance.miBuqueda);
        }

        //[HttpPost]
        //public ActionResult BusquedaVacunados(IFormCollection collection)
        //{
        //    var x = collection["select"];
        //    var busqueda = collection["search"];
        //    Singleton.Instance.miBuquedaVacunados.Clear();
        //    switch (x)
        //    {
        //        case "Nombre":
        //            try
        //            {
        //                for (int j = 0; j <= contador; j++)
        //                {


        //                    int cant = Singleton.Instance.repeatedNames.Find(h => h.value == busqueda).numberRepeats;

        //                    for (int i = 0; i <= cant; i++)
        //                    {
        //                        if (i != 0)
        //                        {
        //                            Singleton.Instance.miBuquedaVacunados.Add(Singleton.Instance.patientsByName.Buscar(busqueda + i.ToString()));
        //                        }
        //                        else
        //                        {
        //                            Singleton.Instance.miBuquedaVacunados.Add(Singleton.Instance.patientsByName.Buscar(busqueda));
        //                        }
        //                    }
        //                }

        //            }
        //            catch
        //            {
        //                for (int j = 0; j <= contador; j++)
        //                {
        //                    Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByName.Buscar(busqueda));
        //                }
        //            }
        //            break;
        //    }
        //    return View(Singleton.Instance.miBuqueda);
        //}













        [HttpPost]
        public ActionResult Hospital2s(IFormCollection collection)
        {
            Temp1 = Singleton.Instance.patientsH1.Root;

            var patient1 = collection["Patient 1"];
            string patient = patient1.ToString();
            if (patient != "")
            {
                string patientDPI = patient.Substring(11, patient.Length - 11);
                var patientValue = patient.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH2.GetFirst();


                        }
                    }
                }
                else
                {

                }
            }
            var patient2 = collection["Patient 2"];
            String patient2V = patient2.ToString();
            if (patient2V != "")
            {

                string patientDPI = patient2V.Substring(11, patient2V.Length - 11);
                var patientValue = patient2V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH2.GetFirst();


                        }
                    }
                }
            }
            var patient3 = collection["Patient 3"];
            String patient3V = patient3.ToString();
            if (patient3V != "")
            {
                string patientDPI = patient3V.Substring(11, patient3V.Length - 11);
                var patientValue = patient3V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH2.GetFirst();


                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Hospital3s(IFormCollection collection)
        {
            Temp1 = Singleton.Instance.patientsH1.Root;

            var patient1 = collection["Patient 1"];
            string patient = patient1.ToString();
            if (patient != "")
            {
                string patientDPI = patient.Substring(11, patient.Length - 11);
                var patientValue = patient.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH3.GetFirst();


                        }
                    }
                }
                else
                {

                }
            }
            var patient2 = collection["Patient 2"];
            String patient2V = patient2.ToString();
            if (patient2V != "")
            {

                string patientDPI = patient2V.Substring(11, patient2V.Length - 11);
                var patientValue = patient2V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH3.GetFirst();


                        }
                    }
                }
            }
            var patient3 = collection["Patient 3"];
            String patient3V = patient3.ToString();
            if (patient3V != "")
            {
                string patientDPI = patient3V.Substring(11, patient3V.Length - 11);
                var patientValue = patient3V.Substring(0, 10);
                if (patientValue == "Vaccinated")
                {
                    foreach (var patientV in Singleton.Instance.Location)
                    {

                        if (patientV.DPI.Equals(patientDPI))
                        {
                            Singleton.Instance.Vacunados.Add(patientV);
                            Singleton.Instance.vaccinatedPercentage.Vaccinated++;
                            Singleton.Instance.vaccinatedPercentage.NotVaccinated--;
                            Singleton.Instance.patientsByName.Remove(patientV.NameKey);
                            Singleton.Instance.patientsByLastName.Remove(patientV.LastNameKey);
                            Singleton.Instance.patientsByDPI.Remove(patientV.DPI);
                            Singleton.Instance.patientsHash.Delete(patientV, patientV.DPI, GetMultiplier(patientV.Hospital));
                            Singleton.Instance.patientsH3.GetFirst();


                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Metodos_ayuda
        /// <summary>
        /// Verifica que la persona esté vacunada
        /// </summary>
        public void Vacunado(IFormCollection collection)
        {
            //Singleton.Instance.Vacunados.Clear();
            bool x = Convert.ToBoolean(collection["checkbox"]);
            if (x == true)
            {
                foreach (var item in Singleton.Instance.Location)
                {
                    Singleton.Instance.Vacunados.Add(item);
                }

            } else { }
         
            RedirectToAction("Hospitals1", "Hospital");
        }
        /// <summary>
        /// Si el string tiene un numero, retorna verdadero, si no, retorna falso.
        /// </summary>
        public bool HasIncorrectCharacter(string data)
        {
            try
            {
                var num = int.Parse(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Agrega los hospitales
        /// </summary>
        private void LoadHospitalsByDepartment()
        {
            AddHospital("Alta Verapaz");
            AddHospital("Guatemala");
            AddHospital("Totonicapán");

        }
        /// <summary>
        /// Consigue l multiplicador para el rango de la tabla hash,con base al hospital ingresado
        /// </summary>
        private int GetMultiplier(string hospital)
        {
            switch (hospital)
            {
                case "Alta Verapaz":
                    return 1;
                case "Guatemala":
                    return 2;
                case "Totonicapán":
                    return 3;
                
            }
            return -1;
        }
        /// <summary>
        /// Crea un nuevo hospital
        /// </summary>
        private void AddHospital(string hospital)
        {
            var newHospital = new Hospital()
            {
                HospitalName = hospital,
            };
            newHospital.GetDepartments();
            Singleton.Instance.Hospitals.Add(newHospital);
        }
        /// <summary>
        /// Consigue el nombre del hospital con base al departamento que esté ubicado.
        /// </summary>
        private string GetHospital(string department)
        {
            foreach (var hospital in Singleton.Instance.Hospitals)
            {
                if (hospital.Departments.Contains(department))
                {
                    return hospital.HospitalName;
                }
            }
            return null;
        }
        #endregion
    }



}