using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    class Genero
    {
        string gen;

        public Genero()
        {
            gen = "";
        }

        public string pGen { get => gen; set => gen = value; }
        public string consulta()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT titulo, genero " +
                          "FROM TICKETS TI FULL JOIN FUNCIONES FU ON TI.id_funcion = FU.id_funcion  FULL JOIN " +
                          "PELICULAS PE ON FU.Id_pelicula = PE.Id_pelicula FULL JOIN GENEROS GE ON PE.Id_genero = GE.Id_genero " +
                          "WHERE TI.id_ticket IS NULL AND GE.genero LIKE '"+gen+"'";
            return consultaSQL;
        }
        public string enunciado()
        {
            string txt = "";
            txt = "Listado de peliculas de genero "+gen+" que no tuvieron publico";
            return txt;
        }
    }
}
