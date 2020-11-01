using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    class ConsultasxEnunciados
    {
        string nacActor;
        string nacDirector;
        string gen;
        DateTime fecha1;
        DateTime fecha2;
        double importeMin;

        public ConsultasxEnunciados()
        {
            nacActor = "";
            nacDirector = "";
            gen = "";
            fecha1 = DateTime.Today;
            fecha2 = DateTime.Today;
            importeMin = 0;
        }

        public string pNacActor { get => nacActor; set => nacActor = value; }
        public string pNacDirector { get => nacDirector; set => nacDirector = value; }
        public string pGen { get => gen; set => gen = value; }
        public DateTime pFecha1 { get => fecha1; set => fecha1 = value; }
        public DateTime pFecha2 { get => fecha2; set => fecha2 = value; }
        public double pImporteMin { get => importeMin; set => importeMin = value; }

        public string consultaActores()
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
        public string enunciadoActores()
        {
            string txt = "";
            txt = "Lista de todos los actores de " + pNacActor + " y los directores de " + pNacDirector + " que hayan tenido participación" +
                "en las peliculas presentadas por nuestro cine";
            return txt;
        }
        public string consultaReservas()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT COUNT(id_reservas) 'Reservas hechas'," +
                            "SUM(CAST(pagado AS INT)) 'Reservas Pagadas'," +
                            "COUNT(id_reservas) - SUM(CAST(pagado AS INT)) 'Reservas Canceladas'" +
                            "FROM RESERVAS";
            return consultaSQL;
        }
        public string enunciadoReservas()
        {
            string txt = "";
            txt = "Reservas Realizadas, Reservas Pagas y Reservas Canceladas ";
            return txt;
        }
        public string consultaTemporada()
        {
            string consultaSQL = "";
            consultaSQL = "select DISTINCT CL.Id_cliente ,nombre + ' ' + apellido 'Cliente', nro_documento 'Nro. Documento', temporada " +
                          "from CLIENTES CL, COMPROBANTES CO, TEMPORADAS TE " +
                          "where NOT EXISTS(select Id_cliente " +
                          "from COMPROBANTES " +
                          "where id_temporada != 1 " +
                          "and CL.Id_cliente = COMPROBANTES.Id_cliente) AND " +
                          "CL.Id_cliente = CO.Id_cliente AND CO.id_temporada = TE.id_temporada";
            return consultaSQL;
        }
        public string enunciadoTemporada()
        {
            string txt = "";
            txt = "Listado de Clientes que solo vinieron durante las funciones de Verano";
            return txt;
        }
        public string consultaGeneros()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT titulo, genero " +
                          "FROM TICKETS TI FULL JOIN FUNCIONES FU ON TI.id_funcion = FU.id_funcion  FULL JOIN " +
                          "PELICULAS PE ON FU.Id_pelicula = PE.Id_pelicula FULL JOIN GENEROS GE ON PE.Id_genero = GE.Id_genero " +
                          "WHERE TI.id_ticket IS NULL AND GE.genero LIKE '" + gen + "'";
            return consultaSQL;
        }
        public string enunciadoGeneros()
        {
            string txt = "";
            txt = "Listado de peliculas de genero " + pGen + " que no tuvieron publico";
            return txt;
        }
        public string consultaPeriodo()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT CO.Id_comprobante, FORMAT(fecha, 'MM/dd/yy') Fecha, SUM(TI.precio) 'Importe Total' " +
                          "FROM COMPROBANTES CO JOIN TICKETS TI ON CO.Id_comprobante = TI.Id_comprobante " +
                          "WHERE fecha BETWEEN '"+pFecha1.ToShortDateString()+"' AND '"+pFecha2.ToShortDateString()+"' " +
                          "GROUP BY CO.Id_comprobante, fecha " +
                          "HAVING SUM(TI.precio) >= "+pImporteMin+" " +
                          "ORDER BY 2";
            return consultaSQL;
        }
        public string enunciadoPeriodo()
        {
            string txt = "";
            txt = "Listado de los comprobantes con sus respectivas fechas que hayan sido generados en en un periodo. Donde el importe" +
                  "de dicho comprobante sea mayor al importe mínimo ingresado";
            return txt;
        }
        public string consultarTickets()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT titulo, temporada, COUNT(TI.id_ticket) 'Tickets' "+
                          "FROM COMPROBANTES CO JOIN TICKETS TI ON CO.Id_comprobante = TI.Id_comprobante JOIN "+
                          "FUNCIONES FU ON TI.id_funcion = FU.id_funcion JOIN PELICULAS PE ON FU.Id_pelicula = PE.Id_pelicula JOIN TEMPORADAS TE ON TE.id_temporada = CO.id_temporada "+
                          "GROUP BY temporada, titulo "+
                          "ORDER BY 2, 3 desc";
            return consultaSQL;
        }
        public string enunciadoTickets()
        {
            string txt = "";
            txt = "Listado de tickets que se vendieron por temporada y por pelicula";
            return txt;
        }


        public string consultaComprobantes()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT CO.Id_comprobante, FORMAT(fecha, 'dd/MM/yy') Fecha, SUM(TI.precio) 'Importe Total', 'Importe Maximo' 'Extremos' " +
                            " FROM COMPROBANTES CO JOIN TICKETS TI ON CO.Id_comprobante = TI.Id_comprobante " +
                            "GROUP BY CO.Id_comprobante, fecha " +
                            "HAVING SUM(TI.precio) >= ALL " +
                            "(SELECT SUM(precio) " +
                            "FROM TICKETS " +
                            "GROUP BY Id_comprobante) " +
                            "UNION " +
                            "SELECT CO.Id_comprobante, FORMAT(fecha, 'dd/MM/yy') Fecha, SUM(TI.precio) 'Importe Total', 'Importe Minimo' " +
                            "FROM COMPROBANTES CO JOIN TICKETS TI ON CO.Id_comprobante = TI.Id_comprobante " +
                            "GROUP BY CO.Id_comprobante, fecha " +
                            "HAVING SUM(TI.precio) <= ALL " +
                            "(SELECT SUM(precio) " +
                            "FROM TICKETS " +
                            "GROUP BY Id_comprobante) ";
                 return consultaSQL;
        }
        public string enunciadoComprobantes()
        {
            string txt = "";
            txt = "Comprobantes de mayor importe y los de menor importe";
            return txt;
        }


        public string consultaPagos()
        {
            string consultaSQL = "";
            consultaSQL = "SELECT CO.id_comprobante,CO.fecha, CL.nombre+space(2)+LOWER(CL.apellido) Cliente, titulo " +
                           "FROM CLIENTES CL JOIN COMPROBANTES CO ON CL.Id_cliente = CO.Id_cliente JOIN TICKETS TI ON CO.Id_comprobante = TI.Id_comprobante JOIN " +
                           "FUNCIONES FU ON TI.id_funcion = FU.id_funcion JOIN PELICULAS PE ON PE.Id_pelicula = FU.Id_pelicula " +
                           "WHERE " + pImporteMin + " < ( " +
                                        "SELECT SUM(precio) " +

                                        "FROM COMPROBANTES COM JOIN TICKETS TIC ON COM.Id_comprobante = TIC.Id_comprobante " +

                                        "WHERE COM.Id_comprobante = CO.Id_comprobante) " +
                                        "ORDER BY 1 ";
            return consultaSQL;
        }
        public string enunciadoPagos()
        {
            string txt = "";
            txt = "Número de comprobante, fecha, cliente y pelicula para los casos en que  los montos del comprobante de pago sea mayor al importe mínimo ingresado ";
            return txt;
        }





    }

}
