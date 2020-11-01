using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    class Reservas
    {
        public string consulta()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT COUNT(id_reservas) 'Reservas hechas'," +
                            "SUM(CAST(pagado AS INT)) 'Reservas Pagadas'," +
                            "COUNT(id_reservas) - SUM(CAST(pagado AS INT)) 'Reservas Canceladas'" +
                            "FROM RESERVAS";
            return consultaSQL;
        }
        public string enunciado()
        {
            string txt = "";
            txt = "Reservas Realizadas, Reservas Pagas y Reservas Canceladas ";
            return txt;
        }
    }
}
