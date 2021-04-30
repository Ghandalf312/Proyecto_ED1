﻿using System;
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
        public static bool FirstTime = true;
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

        public ActionResult Busqueda()
        {
            Singleton.Instance.miBuqueda.Clear();
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
        {//pendiente para ver
            Singleton.Instance.Location.Clear();
            int currentPatientCount = 0;
            int patientCount = 3;
            foreach (var patient in Singleton.Instance.patientsH1)
            {
                if (patient.Hospital == "Alta Verapaz" && currentPatientCount < patientCount)
                {
                    Singleton.Instance.Location.Add(patient);
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
                if (patient.Value.Hospital == "Guatemala" && currentPatientCount < patientCount)
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
                if (patient.Value.Hospital == "Totonicapán" && currentPatientCount < patientCount)
                {
                    Singleton.Instance.Location.Add(patient.Value);
                    currentPatientCount++;
                }
            }
            Singleton.Instance.Location.Sort();
            return View(Singleton.Instance.Location);
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
            }
            return View();
        }
        [HttpPost]
        public ActionResult Registro(IFormCollection collection)
        {
            try
            {
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
                    Singleton.Instance.patientsH1.AddPatient(newPatientModel.DPI, newPatientModel.Age, newPatientModel, newPatientModel.Priority);
                }
                else if (newPatientModel.Hospital == "Totonicapán")
                {
                    Singleton.Instance.patientsH1.AddPatient(newPatientModel.DPI, newPatientModel.Age, newPatientModel, newPatientModel.Priority);
                }
                Singleton.Instance.patientsByDPI.Add(newPatientModel, newPatientModel.DPI);
                Singleton.Instance.patientsHash.Insert(newPatientModel, newPatientModel.DPI);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Municipality", "Por favor, asegurese de haber llenado todo los campos correctamente.");
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
                        Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByDPI.Buscar(Convert.ToString(busqueda)));

                    }
                    catch
                    {
                        TempData["Error"] = "Favor ingrese numero de DPI válido";
                    }
                    break;
                case "Nombre":
                    try
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
                    catch
                    {
                        Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByName.Buscar(busqueda));
                    }
                    break;
                case "Apellido":
                    try
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
                    catch
                    {
                        Singleton.Instance.miBuqueda.Add(Singleton.Instance.patientsByLastName.Buscar(busqueda));
                    }
                    break;
                default:
                    break;
            }
            return View(Singleton.Instance.miBuqueda);
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
            return View();
        }
        [HttpPost]
        public ActionResult Hospital2s(IFormCollection collection)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Hospital3s(IFormCollection collection)
        {
            return View();
        }

        #endregion
        #region Metodos_ayuda
        /// <summary>
        /// Si el string tiene un numero, retorna verdadero, si no, retorna falso.
        /// </summary>
        /// <param name="data"></param> representa el input que ingreso el usuario.
        /// <returns></returns>
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
        private void LoadHospitalsByDepartment()
        {
            AddHospital("Alta Verapaz");
            AddHospital("Guatemala");
            AddHospital("Totonicapán");

        }

        private void AddHospital(string hospital)
        {
            var newHospital = new Hospital()
            {
                HospitalName = hospital,
                PatientQueue = new PriorityQueue<PatientModel>(),
            
            };
            newHospital.GetDepartments();
            Singleton.Instance.Hospitals.Add(newHospital);
        }

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
        public ActionResult ListaPersonas()
        {
            if (Singleton.Instance.miBuqueda.Count() == 0)
            {
                TempData["Vacio"] = "No se encontró a la persona solicitada";
            }
            return View(Singleton.Instance.miBuqueda);
        }
        public ActionResult DetailsBusc(String id)
        {

            return View(Singleton.Instance.patientsByDPI.Buscar(id));
        }





        #endregion
    }



}