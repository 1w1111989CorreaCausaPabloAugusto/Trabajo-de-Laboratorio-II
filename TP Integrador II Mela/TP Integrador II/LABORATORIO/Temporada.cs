using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    class Temporada
    {
        public string consulta()
        {
            string consultaSQL = "";
            consultaSQL = "select DISTINCT CL.Id_cliente ,nombre + ' ' + apellido 'Cliente', nro_documento 'Nro. Documento', temporada "+
                          "from CLIENTES CL, COMPROBANTES CO, TEMPORADAS TE "+
                          "where NOT EXISTS(select Id_cliente "+
                          "from COMPROBANTES "+
                          "where id_temporada != 1 "+
                          "and CL.Id_cliente = COMPROBANTES.Id_cliente) AND "+
                          "CL.Id_cliente = CO.Id_cliente AND CO.id_temporada = TE.id_temporada";
            return consultaSQL;
        }
        public string enunciado()
        {
            string txt = "";
            txt = "Listado de Clientes que solo vinieron durante las funciones de Verano";
            return txt;
        }
    }
}
