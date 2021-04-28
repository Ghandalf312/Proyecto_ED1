using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomGenerics.Estructuras;
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
                case "Busqueda":
                    return RedirectToAction("Registro");
                case "Lista espera":
                    return RedirectToAction("Registro");
                case "Hospitales":
                    return RedirectToAction("Registro");
                case "% vacunados":
                    return RedirectToAction("Registro");
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
                  
                
                };
                newPatientModel.PriorityAssignment();
                newPatientModel.HospitalAssigment();
                






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
                    Singleton.Instance.patientsByName.Add(newPatientModel, newPatientModel.Name);
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
                    Singleton.Instance.patientsByLastName.Add(newPatientModel, newPatientModel.LastName);
                }

                Singleton.Instance.patientsByDPI.Add(newPatientModel, newPatientModel.DPI);
                Singleton.Instance.patientsHash.Insert(newPatientModel, newPatientModel.DPI);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Department", "Por favor, asegurese de haber llenado todo los campos correctamente.");
            }
            return View();
        }

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
        #endregion

        private void LoadHospitalsByDepartment()
        {
            AddHospital("Guatemala");
            AddHospital("Izabal");
            AddHospital("Zacapa");

        }


        private void AddHospital(string hospital)
        {
            var newHospital = new Hospital()
            {
                HospitalName = hospital,
                PatientQueue = new CustomGenerics.PriorityQueue<PatientExtModel>(),
            
            };
            newHospital.GetDepartments();
            Singleton.Instance.Hospitals.Add(newHospital);
        }

    }


}