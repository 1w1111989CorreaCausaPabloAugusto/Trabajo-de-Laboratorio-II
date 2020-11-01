using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    class ActoresxDirectores
    {

        string nacActor;
        string nacDirector;

        public string pNacActor { get => nacActor; set => nacActor = value; }
        public string pNacDirector { get => nacDirector; set => nacDirector = value; }

        public ActoresxDirectores(string nacActor, string nacDirector)
        {
            this.nacActor = nacActor;
            this.nacDirector = nacDirector;
        }
        public ActoresxDirectores()
        {
            nacActor = "";
            nacDirector = "";
        }
        public string consulta()
        {
            string consultaSQL = "";
            consultaSQL = " select  Id_actor ,nombre + ' ' + apellido 'Nombre Completo', nacionalidad , 'ACTOR'  Rol " +
                              " from ACTORES AC join NACIONALIDADES NA on AC.Id_nacionalidad = NA.Id_nacionalidad " +
                                " where NA.nacionalidad LIKE '" + nacActor + "%'" +
                                "union" +
                                " select Id_directorers, nombre + ' ' + apellido 'Nombre Completo',nacionalidad, 'DIRECTOR' " +
                                " from DIRECTORES DI join NACIONALIDADES NAC on DI.Id_nacionalidad = NAC.Id_nacionalidad " +
                                "where NAC.nacionalidad LIKE '" + nacDirector + "%' ORDER BY 4";
            return consultaSQL;
        }
        public string enunciado()
        {
            string txt = "";
            txt = "Lista de todos los actores de " + nacActor + " y los directores de " + nacDirector + " que hayan tenido participación" +
                "en las peliculas presentadas por nuestro cine";
            return txt;
        }
    }
}
