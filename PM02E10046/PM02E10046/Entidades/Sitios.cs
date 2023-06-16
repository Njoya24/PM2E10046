using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM02E10304.Entidades
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double latitud { get; set; }
        public double longitud{ get; set; }
        public string descripcion { get; set; }
        public string image { get; set; }


    }
}

