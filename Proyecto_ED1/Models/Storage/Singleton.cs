using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomGenerics.Estructuras;
using Proyecto_ED1.Models;

namespace Proyecto_ED1.Models.Storage
{
    public class Singleton
    {
        public static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }

        public Hash<PatientExtModel> patientsHash = new Hash<PatientExtModel>(150);
    }
}
