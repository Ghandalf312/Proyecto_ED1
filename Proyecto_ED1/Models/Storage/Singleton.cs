﻿using System;
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

        public Hash<PatientExtModel> patientsHash = new Hash<PatientExtModel>(100);
        public AVLTree<PatientModel, string> patientsByName = new AVLTree<PatientModel, string>();
        public List<RepeatedModel> repeatedNames = new List<RepeatedModel>();
        public AVLTree<PatientModel, string> patientsByLastName = new AVLTree<PatientModel, string>();
        public List<RepeatedModel> repeatedLastNames = new List<RepeatedModel>();
        public AVLTree<PatientModel, string> patientsByDPI = new AVLTree<PatientModel, string>();
    }
}