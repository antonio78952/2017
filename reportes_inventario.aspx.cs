using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;



public partial class reportes_inventario : System.Web.UI.Page
{

    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] codigo_producto, clientes, codigos_proveedor, productos, marca, modelo, costo, codigos_proveedor_orden,fechas,comentarios;
    public int[] ids_clientes, cantidades, compras_anuales = new int[12],minimo,maximo,id_inspeccion,cantidad_inventario,cantidad_fisica,id_sucursal_sistema,id_vendedor;
    public double[] totales, comisiones, precios, precios_totales,costos,precio_mayoreo,precio_contado,precio_gobierno,precio_credito;
    public int n_productos, contador_ventas, busqueda, i, n_productos_detalle, contador_productos;
    public string cliente, fecha, estado, id_control;

    public void nu_productos_detalle()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from inventario where codigo_proveedor='" + consulta.codigo_proveedor + "' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_detalle = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            precio_credito = new double[n_productos_detalle];
            precio_contado = new double[n_productos_detalle];
            precio_gobierno = new double[n_productos_detalle];
            precio_mayoreo = new double[n_productos_detalle];


        }
        catch (System.Exception ex)
        {
        }
    }


    public void nu_productos_inspeccion()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from inspecciones_inventario ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_detalle = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            id_inspeccion = new int[n_productos_detalle];
            codigos_proveedor = new string[n_productos_detalle];
            cantidad_inventario = new int[n_productos_detalle];
            cantidad_fisica = new int[n_productos_detalle];
            fechas = new string[n_productos_detalle];
            comentarios = new string[n_productos_detalle];
            id_sucursal_sistema = new int[n_productos_detalle];
            id_vendedor = new int[n_productos_detalle];


        }
        catch (System.Exception ex)
        {
        }
    }

    public void nu_productos_reporte()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from inventario where categoria != 'servicios' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            codigos_proveedor = new string[n_productos];
            productos = new string[n_productos];
            marca = new string[n_productos];
            costos = new double[n_productos];
            cantidades = new int[n_productos];
            minimo = new int[n_productos];
            maximo = new int[n_productos];
            precios_totales = new double[n_productos];
            fechas = new string[n_productos];




        }
        catch (System.Exception ex)
        {
        }
    }

     

    
    public void llenar_detalles_producto()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select precio_credito,precio_contado,precio_gobierno,precio_mayoreo from inventario where codigo_proveedor='"+consulta.codigo_proveedor +"' ";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                precio_credito[contador_productos] = Convert.ToDouble(registro[0]);
                precio_contado[contador_productos] = Convert.ToDouble(registro[1]);
                precio_gobierno[contador_productos] = Convert.ToDouble(registro[2]);
                precio_mayoreo[contador_productos] = Convert.ToDouble(registro[3]);

                contador_productos++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

   

    public DataTable detalles_producto()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("precio_credito", typeof(double)));
        tabla.Columns.Add(new DataColumn("precio_contado", typeof(double)));
        tabla.Columns.Add(new DataColumn("precio_gobierno", typeof(double)));
        tabla.Columns.Add(new DataColumn("precio_mayoreo", typeof(double)));
       

        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (precio_credito[i] != null)
                {
                    fila["precio_credito"] = precio_credito[i];
                    fila["precio_contado"] = precio_contado[i];
                    fila["precio_gobierno"] = precio_gobierno[i];
                    fila["precio_mayoreo"] = precio_mayoreo[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        return tabla;
    }

    public void llenar_lista_productos()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            if (consulta.departamento == "GERENCIA" || consulta.departamento == "ADMINISTRACION" || consulta.departamento == "SISTEMAS")
            {
                if (tipo_busqueda.SelectedIndex == 1)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo from inventario where (cantidad <= minimo) and categoria != 'servicios'  ";
                    mensagito.Text = "Productos En Minimo";
                }
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo from inventario where (cantidad = maximo) and categoria != 'servicios'";
                    mensagito.Text = "Productos En Maximo";
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo from inventario where (cantidad > maximo) and categoria != 'servicios'";
                    mensagito.Text = "Productos Exedentes";
                }
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo from inventario where estado_requisicion_automatica = 1 and categoria != 'servicios'";
                    mensagito.Text = "Productos Que Generaron Una Requisision Automatica";
                }
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    objeto.cadena_comando_mysql = "select inventario.codigo_proveedor,inventario.producto,inventario.marca,inventario.costo,count(lista_productos_venta.cantidad),inventario.minimo,inventario.maximo,round(sum(lista_productos_venta.precio_total),2) from inventario inner join lista_productos_venta   on inventario.codigo_proveedor = lista_productos_venta.codigo inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta where Date_format(ventas.fecha,'%Y-%m')= '" + consulta.fecha.ToString("yyyy-MM") + "' group by inventario.producto order by count(lista_productos_venta.cantidad) desc";
                    
                }
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo  from inventario  group by producto order by cantidad desc";
                    mensagito.Text = "Cantidades En Inventario";
                }
                if (tipo_busqueda.SelectedIndex == 7)
                {
                    objeto.cadena_comando_mysql = "select codigo_proveedor,producto,marca,costo,cantidad,minimo,maximo  from inventario  group by producto order by cantidad desc";

                }
                if (tipo_busqueda.SelectedIndex == 8)
                {
                    objeto.cadena_comando_mysql = "select lista_productos_venta.codigo,inventario.producto,lista_productos_venta.proveedor,max(ventas.fecha) from lista_productos_venta inner join ventas on lista_productos_venta.clave_venta = ventas.clave_venta inner join inventario on inventario.codigo_proveedor = lista_productos_venta.codigo group by lista_productos_venta.codigo order by max(ventas.fecha) asc";
                    mensage2.Text = "Ultima Fecha De Venta";
                }
                if (tipo_busqueda.SelectedIndex == 9)
                {
                    objeto.cadena_comando_mysql = "SELECT id_inspeccion,codigo_proveedor,cantidad_inventario,cantidad_fisica,fecha,comentarios,id_sucursal_sistema,id_vendedor FROM inspecciones_inventario";
                    texto_inspeccion.Text = "Inspeccion Inventario";
                }
            }
            else
            {

                if (tipo_busqueda.SelectedIndex == 1)
                {
                    objeto.cadena_comando_mysql = "";
                }

                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "";
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "";
                }

            }


            objeto.aplicar_comando_mysql_extraccion();
            contador_productos = 0;
            if (tipo_busqueda.SelectedIndex == 5)
            {
                while (objeto.leer_comando.Read())
                {
                    registro = new string[8];
                    registro[0] = objeto.leer_comando.GetString(0);
                    registro[1] = objeto.leer_comando.GetString(1);
                    registro[2] = objeto.leer_comando.GetString(2);
                    registro[3] = objeto.leer_comando.GetString(3);
                    registro[4] = objeto.leer_comando.GetString(4);
                    registro[5] = objeto.leer_comando.GetString(5);
                    registro[6] = objeto.leer_comando.GetString(6);
                    registro[7] = objeto.leer_comando.GetString(7);
                    codigos_proveedor[contador_productos] = registro[0];
                    productos[contador_productos] = registro[1];
                    marca[contador_productos] = registro[2];
                    costos[contador_productos] = Convert.ToDouble(registro[3]);
                    cantidades[contador_productos] = Convert.ToInt16(registro[4]);
                    minimo[contador_productos] = Convert.ToInt16(registro[5]);
                    maximo[contador_productos] = Convert.ToInt16(registro[6]);
                    precios_totales[contador_productos] = Convert.ToDouble(registro[7]);



                    contador_productos++;
                }
            }
            else if (tipo_busqueda.SelectedIndex == 8)
            {
                while (objeto.leer_comando.Read())
                {
                    registro = new string[4];
                    registro[0] = objeto.leer_comando.GetString(0);
                    registro[1] = objeto.leer_comando.GetString(1);
                    registro[2] = objeto.leer_comando.GetString(2);
                    registro[3] = objeto.leer_comando.GetString(3);
                    
                    codigos_proveedor[contador_productos] = registro[0];
                    productos[contador_productos] = registro[1];
                    marca[contador_productos] = registro[2];
                    fechas[contador_productos] = registro[3];
                    



                    contador_productos++;
                }
 
            }
            else if (tipo_busqueda.SelectedIndex == 9)
            {
                while (objeto.leer_comando.Read())
                {
                    registro = new string[8];
                    registro[0] = objeto.leer_comando.GetString(0);
                    registro[1] = objeto.leer_comando.GetString(1);
                    registro[2] = objeto.leer_comando.GetString(2);
                    registro[3] = objeto.leer_comando.GetString(3);
                    registro[4] = objeto.leer_comando.GetString(4);
                    registro[5] = objeto.leer_comando.GetString(5);
                    registro[6] = objeto.leer_comando.GetString(6);
                    registro[7] = objeto.leer_comando.GetString(7);
                   
                    id_inspeccion[contador_productos] = Convert.ToInt16(registro[0]);
                    codigos_proveedor[contador_productos] = registro[1];
                    cantidad_inventario[contador_productos] = Convert.ToInt16(registro[2]);
                    cantidad_fisica[contador_productos] = Convert.ToInt16(registro[3]);
                    fechas[contador_productos] = registro[4];
                    comentarios[contador_productos] = registro[5];
                    id_sucursal_sistema[contador_productos] = Convert.ToInt16(registro[6]);
                    id_vendedor[contador_productos] = Convert.ToInt16(registro[7]);
                    contador_productos++;
                }

            }
            else
            {
                while (objeto.leer_comando.Read())
                {
                    registro = new string[7];
                    registro[0] = objeto.leer_comando.GetString(0);
                    registro[1] = objeto.leer_comando.GetString(1);
                    registro[2] = objeto.leer_comando.GetString(2);
                    registro[3] = objeto.leer_comando.GetString(3);
                    registro[4] = objeto.leer_comando.GetString(4);
                    registro[5] = objeto.leer_comando.GetString(5);
                    registro[6] = objeto.leer_comando.GetString(6);
                    codigos_proveedor[contador_productos] = registro[0];
                    productos[contador_productos] = registro[1];
                    marca[contador_productos] = registro[2];
                    costos[contador_productos] = Convert.ToDouble(registro[3]);
                    cantidades[contador_productos] = Convert.ToInt16(registro[4]);
                    minimo[contador_productos] = Convert.ToInt32(registro[5]);
                    maximo[contador_productos] = Convert.ToInt16(registro[6]);



                    contador_productos++;
                }
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public DataTable ultimas_ventas()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if(codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["fecha"] = fechas[i];
                    tabla.Rows.Add(fila);
                    i++;

                }
            }
            while (i < contador_productos);
        }
        return tabla;
    }


    public DataSet ultimas_ventas_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(string)));
        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["fecha"] = fechas[i];
                    tabla.Rows.Add(fila);
                    i++;

                }
            }
            while (i < contador_productos);
        }
        ds.Tables.Add(tabla);
        return ds;
    }




    public DataTable datos_productos()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("costos", typeof(double)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("minimo", typeof(int)));
        tabla.Columns.Add(new DataColumn("maximo", typeof(int)));

        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["costos"] = costos[i];
                    fila["cantidades"] = cantidades[i];
                    fila["minimo"] = minimo[i];
                    fila["maximo"] = maximo[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        return tabla;
    }

    public DataSet datos_productos_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("costos", typeof(double)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("minimo", typeof(int)));
        tabla.Columns.Add(new DataColumn("maximo", typeof(int)));
        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["costos"] = costos[i];
                    fila["cantidades"] = cantidades[i];
                    fila["minimo"] = minimo[i];
                    fila["maximo"] = maximo[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        ds.Tables.Add(tabla);
        return ds;
    }


   

    public DataTable datos_ventas()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("costos", typeof(double)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("minimo", typeof(int)));
        tabla.Columns.Add(new DataColumn("maximo", typeof(int)));

        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["costos"] = costos[i];
                    fila["total"] = precios_totales[i];
                    fila["cantidades"] = cantidades[i];
                    fila["minimo"] = minimo[i];
                    fila["maximo"] = maximo[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        return tabla;
    }

    public DataSet datos_ventas_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("marca", typeof(string)));
        tabla.Columns.Add(new DataColumn("costos", typeof(double)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("minimo", typeof(int)));
        tabla.Columns.Add(new DataColumn("maximo", typeof(int)));
        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["productos"] = productos[i];
                    fila["marca"] = marca[i];
                    fila["costos"] = costos[i];
                    fila["total"] = precios_totales[i];
                    fila["cantidades"] = cantidades[i];
                    fila["minimo"] = minimo[i];
                    fila["maximo"] = maximo[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        ds.Tables.Add(tabla);
        return ds;
    }


    public void exportar_excel()
    {
        using (XLWorkbook libro_trabajo = new XLWorkbook())
        {
            if (tipo_busqueda.SelectedIndex == 5)
            {
                DataSet ps = datos_ventas_excel();
                libro_trabajo.Worksheets.Add(ps);
            }
            if (tipo_busqueda.SelectedIndex == 8)
            {
                DataSet ps = ultimas_ventas_excel();
                libro_trabajo.Worksheets.Add(ps);
            }
            if (tipo_busqueda.SelectedIndex == 9)
            {
                DataSet ps = inspeccion_excel();
                libro_trabajo.Worksheets.Add(ps);
            }
            else if ((tipo_busqueda.SelectedIndex != 5 && tipo_busqueda.SelectedIndex != 8)&& tipo_busqueda.SelectedIndex != 9)
            {
                DataSet ps = datos_productos_excel();
                libro_trabajo.Worksheets.Add(ps);
            }
            libro_trabajo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            libro_trabajo.Style.Font.Bold = true;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte" + ".xlsx");
            using (MemoryStream memoria = new MemoryStream())
            {
                libro_trabajo.SaveAs(memoria);
                memoria.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }


    public DataSet inspeccion_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("id_inspeccion", typeof(int)));
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("cantidad_inventario", typeof(int)));
        tabla.Columns.Add(new DataColumn("cantidad_fisica", typeof(int)));
        tabla.Columns.Add(new DataColumn("fechas", typeof(string)));
        tabla.Columns.Add(new DataColumn("comentarios", typeof(string)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema", typeof(int)));
        tabla.Columns.Add(new DataColumn("id_vendedor", typeof(int)));
        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["id_inspeccion"] = id_inspeccion[i];
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["cantidad_inventario"] = cantidad_inventario[i];
                    fila["cantidad_fisica"] = cantidad_fisica[i];
                    fila["fechas"] = fechas[i];
                    fila["comentarios"] = comentarios[i];
                    fila["id_sucursal_sistema"] = id_sucursal_sistema[i];
                    fila["id_vendedor"] = id_vendedor[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        ds.Tables.Add(tabla);
        return ds;
    }


    public DataTable datos_inspeccion()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("id_inspeccion", typeof(int)));
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("cantidad_inventario", typeof(int)));
        tabla.Columns.Add(new DataColumn("cantidad_fisica", typeof(int)));
        tabla.Columns.Add(new DataColumn("fechas", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("comentarios", typeof(string)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema", typeof(int)));
        tabla.Columns.Add(new DataColumn("id_vendedor", typeof(int)));

        if (contador_productos > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["id_inspeccion"] = id_inspeccion[i];
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["cantidad_inventario"] = cantidad_inventario[i];
                    fila["cantidad_fisica"] = cantidad_fisica[i];
                    fila["fechas"] = fechas[i];
                    fila["comentarios"] = comentarios[i];
                    fila["id_sucursal_sistema"] = id_sucursal_sistema[i];
                    fila["id_vendedor"] = id_vendedor[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos);
        }
        return tabla;
    }

    public void extraer_compras_anuales()
    {
        enchufe.extraer_compras_anuales();
        compras_anuales = enchufe.compras_anuales;
    }

    public void llenar_lista_meses()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select distinct date_format(fecha,'%Y-%m') from ventas";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                lista_fechas.Items.Add(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    


    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Session["usuario_vendedor"] = "jmedina@inklaser.mx";
        if (Session["usuario_vendedor"] == null)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "salir();", true);
        }
        consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
        consulta.extraer_id_vendedor();
        consulta.verificar_tipo_sesion();
        extraer_compras_anuales();
        caja_cantidad_surtir_maximos.Attributes.Add("onkeypress", "javascript:return numeros(event); ");
        caja_cantidad_surtir_minimos.Attributes.Add("onkeypress", "javascript:return numeros(event); ");
        
        if (!IsPostBack)
        {
            boton_exportar.Visible = false;
            boton_imprimir.Visible = false;
            extraer_compras_anuales();
            panelbuscar.Visible = true;
            tipo_busqueda.SelectedIndex = 1;
            nu_productos_reporte();
            llenar_lista_productos();
            tabla_productos.DataBind();
            Panel_Completo.Visible = true;
        }
        if (IsPostBack)
        {
            id_control = Convert.ToString(verificar_id_control_postback());
            panelgrafica.Visible = false;
            panelbuscar.Visible = true;
            
        }
    }

   
   
    protected void tipo_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo_busqueda.SelectedIndex > 0)
        {
            lista_fechas.Items.Clear();
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_productos_reporte();
            llenar_lista_productos();

            if (tipo_busqueda.SelectedIndex == 5)
            {
                llenar_lista_meses();
                
            }
            
            
            
        }
        if (tipo_busqueda.SelectedIndex == 7)
        {
            boton_exportar.Visible = true;
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_productos_reporte();
            llenar_lista_productos();
            tabla_modificar.DataBind();
            panel_maximos_minimos.Visible = true;
            panel_no_resultados_busqueda.Visible = false;
            Panel_Ventas.Visible = false;
            Panel_Ventas_Pasado.Visible = false;
            Panel_Completo.Visible = false;
            Ultima_venta.Visible = false;
            panel_inspeccion.Visible = false;
        }
        else if (tipo_busqueda.SelectedIndex == 8)
        {
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_productos_reporte();
            llenar_lista_productos();
            tabla_ultima_fecha.DataBind();
            Ultima_venta.Visible = true;
            boton_exportar.Visible = true;
            panel_no_resultados_busqueda.Visible = false;
            panel_maximos_minimos.Visible = false;
            Panel_Ventas.Visible = false;
            Panel_Ventas_Pasado.Visible = false;
            Panel_Completo.Visible = false;
            panel_inspeccion.Visible = false;
            
            

        }
        else if (tipo_busqueda.SelectedIndex == 9)
        {
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_productos_inspeccion();
            llenar_lista_productos();
            tabla_inspeccion.DataBind();
            Ultima_venta.Visible = false;
            boton_exportar.Visible = true;
            panel_no_resultados_busqueda.Visible = false;
            panel_maximos_minimos.Visible = false;
            Panel_Ventas.Visible = false;
            Panel_Ventas_Pasado.Visible = false;
            Panel_Completo.Visible = false;
            panel_inspeccion.Visible = true;


        }
        else 
        {

            tabla_productos.DataBind();

            if (contador_productos > 0)
            {
                boton_exportar.Visible = true;
                Panel_Completo.Visible = true;
                panel_no_resultados_busqueda.Visible = false;
                panel_maximos_minimos.Visible = false;
                Panel_Ventas.Visible = false;
                Panel_Ventas_Pasado.Visible = false;   
                Ultima_venta.Visible = false;
                panel_inspeccion.Visible = false;
                
            }
            else
            {
                boton_imprimir.Visible = false;
                boton_exportar.Visible = false;
                if (tipo_busqueda.SelectedIndex != 5)
                {
                    panel_no_resultados_busqueda.Visible = true;
                }



            }
        }

    }

    public void llenar_lista_productos_pasado()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select inventario.codigo_proveedor,inventario.producto,inventario.marca,inventario.costo,count(lista_productos_venta.cantidad),inventario.minimo,inventario.maximo,round(sum(lista_productos_venta.precio_total),2) from inventario inner join lista_productos_venta   on inventario.codigo_proveedor = lista_productos_venta.codigo inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta where Date_format(ventas.fecha,'%Y-%m')= '" + consulta.fecha.AddMonths(-1).ToString("yyyy-MM") + "' group by inventario.producto order by count(lista_productos_venta.cantidad) desc";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[8];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                registro[5] = objeto.leer_comando.GetString(5);
                registro[6] = objeto.leer_comando.GetString(6);
                registro[7] = objeto.leer_comando.GetString(7);
                codigos_proveedor[contador_productos] = registro[0];
                productos[contador_productos] = registro[1];
                marca[contador_productos] = registro[2];
                costos[contador_productos] = Convert.ToDouble(registro[3]);
                cantidades[contador_productos] = Convert.ToInt16(registro[4]);
                minimo[contador_productos] = Convert.ToInt16(registro[5]);
                maximo[contador_productos] = Convert.ToInt16(registro[6]);
                precios_totales[contador_productos] = Convert.ToDouble(registro[7]);
                contador_productos++;
            }
        }
        catch 
        {

        }
    
    }

    private string verificar_id_control_postback()
    {
        Control control = null;
        string nombre_control = Page.Request.Params["__EVENTTARGET"]; //se captura en la variable control el id que hizo el postback
        if (nombre_control != null && nombre_control != String.Empty) //si el control es un boton el valor de retorno sera null debido al metodo summit por lo tanto se haran iteraciones para verificar el nombre del control
        {
            control = Page.FindControl(nombre_control);
            if (control == null)
            {
                nombre_control = Page.Request.Params["__EVENTTARGET"];
                control = Page.FindControl(nombre_control);
            }
        }
        else
        {
            string cadena_control = String.Empty; //cadena que verificara los elementos en el formulario
            Control c = null;
            foreach (string control_pulsado in Page.Request.Form)
            {
                if (control_pulsado.EndsWith(".x") || control_pulsado.EndsWith(".y")) //se capturar las coordenadas del boton que fue pulsado
                {
                    cadena_control = control_pulsado.Substring(0, control_pulsado.Length - 2);
                    c = Page.FindControl(cadena_control);
                }
                else
                {
                    c = Page.FindControl(control_pulsado);
                }
                if (c is System.Web.UI.WebControls.Button ||
                         c is System.Web.UI.WebControls.ImageButton || c is System.Web.UI.WebControls.CheckBox)
                {
                    control = c;
                    break;
                }
            }
            control = Page.FindControl("boton_oculto_modificar_maximo");
        }
        return control.ID;
    }


    protected void tabla_productos_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Ver")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            consulta.codigo_proveedor = Convert.ToString(tabla_productos.DataKeys[index].Values["codigo_proveedor"]);
            tabla_productos.SelectedIndex = -1;
            panel_detalles_compra.Visible = true;
            nu_productos_detalle();
            llenar_detalles_producto();
            tabla_detalles_producto.DataBind();
        }

    }

    protected void tabla_modificar_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Modificar_minimos")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            enchufe.codigo_proveedor = Convert.ToString(tabla_modificar.DataKeys[index].Values["codigo_proveedor"]);
            tabla_modificar.SelectedIndex = -1;
            Session["codigo_proveedor_modificar_maximos_minimos_medina"] = enchufe.codigo_proveedor;
        
            GridViewRow row = tabla_modificar.Rows[index];
            caja_nombre_producto_minimos.Text = row.Cells[1].Text;
            caja_numero_productos_minimos.Text = row.Cells[4].Text;
            cantidad_actual_minimo.Text = row.Cells[5].Text;
            panel_modificar_minimo.Visible = true;
            
        }

        if (e.CommandName == "Modificar_maximos")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            enchufe.codigo_proveedor = Convert.ToString(tabla_modificar.DataKeys[index].Values["codigo_proveedor"]);
            tabla_modificar.SelectedIndex = -1;
            Session["codigo_proveedor_modificar_maximos_minimos_medina"] = enchufe.codigo_proveedor;
            
            GridViewRow row = tabla_modificar.Rows[index];
            caja_nombre_producto_maximos.Text = row.Cells[1].Text;
            caja_numero_productos_maximos.Text = row.Cells[4].Text;
            cantidad_actual_maximos.Text = row.Cells[6].Text;
            panel_modificar_maximos.Visible = true;
            
        }
    }

    
    
    protected void boton_exportar_Click(object sender, EventArgs e)
    {

        consulta.fecha = Convert.ToDateTime(Session["fecha_medina"]);
        nu_productos_reporte();
        nu_productos_inspeccion();
        llenar_lista_productos();
        tabla_productos.DataBind();
        exportar_excel();


    }
    protected void boton_imprimir_Click(object sender, EventArgs e)
    {

    }
    protected void boton_salir_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void boton_cerrar_detalles_venta_Click(object sender, EventArgs e)
    {
        panel_detalles_compra.Visible = false;
    }
    protected void boton_cerrar_panel_minimo_Click(object sender, EventArgs e)
    {
        panel_modificar_minimo.Visible = false;
    }

    protected void boton_cerrar_panel_maximo_Click(object sender, EventArgs e)
    {
        panel_modificar_maximos.Visible = false;
    }
    protected void boton_modificar_minimos_Click(object sender, EventArgs e)
    { 
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "cargar('" + this.id_control + "');", true);
    }
    protected void boton_modificar_maximos_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "cargar('" + this.id_control + "');", true);
    }
    protected void lista_fechas_SelectedIndexChanged(object sender, EventArgs e)
    {
        consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
        cabecera_fecha.Text = Convert.ToString(consulta.fecha.ToString("yyyy-MM"));
        Session["fecha_medina"] = Convert.ToString(consulta.fecha.ToString("yyyy-MM"));
        cabecera_fecha2.Text = Convert.ToString(consulta.fecha.AddMonths(-1).ToString("yyyy-MM"));
        consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
        consulta.extraer_nombre_vendedor();
        nu_productos_reporte();
        llenar_lista_productos();
        ventas.DataBind();
        llenar_lista_productos_pasado();
        ventas_pasadas.DataBind();
        Panel_Ventas.Visible = true;
        Panel_Ventas_Pasado.Visible = true;
        panel_no_resultados_busqueda.Visible = false;
        boton_exportar.Visible = true;
        panel_no_resultados_busqueda.Visible = false;
        panel_maximos_minimos.Visible = false;
        panel_inspeccion.Visible = false;
        Panel_Completo.Visible = false;
        Ultima_venta.Visible = false;
        

    }



    protected void boton_oculto_modificar_maximo_Click(object sender, EventArgs e)
    {
        if (caja_cantidad_surtir_maximos.Text == "")
        {
            ///mensaje de error
        }
        else
        {

            if (Convert.ToInt32(caja_cantidad_surtir_maximos.Text) > 0)
            {

                enchufe.cantidad_modificar_maximo = Convert.ToInt32(caja_cantidad_surtir_maximos.Text);
                enchufe.codigo_proveedor = Convert.ToString(Session["codigo_proveedor_modificar_maximos_minimos_medina"]);
                enchufe.modificar_maximos_inventario();
                panel_modificar_maximos.Visible = false;
                tipo_busqueda.SelectedIndex = 7;
                nu_productos_reporte();
                llenar_lista_productos();
                tabla_modificar.DataBind();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "cerrar_mensaje();", true);
                
                
            }
            else
            {
                ///mensaje 

            }
        }

    }

    protected void boton_oculto_modificar_minimo_Click(object sender, EventArgs e)
    {
        if (caja_cantidad_surtir_minimos.Text == "")
        {
            ///mensaje de error
        }
        else
        {

            if (Convert.ToInt32(caja_cantidad_surtir_minimos.Text) > 0)
            {

                enchufe.cantidad_modificar_minimo = Convert.ToInt32(caja_cantidad_surtir_minimos.Text);
                enchufe.codigo_proveedor = Convert.ToString(Session["codigo_proveedor_modificar_maximos_minimos_medina"]);
                enchufe.modificar_minimos_inventario();
                panel_modificar_minimo.Visible = false;
                tipo_busqueda.SelectedIndex = 7;
                nu_productos_reporte();
                llenar_lista_productos();
                tabla_modificar.DataBind();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "cerrar_mensaje();", true);
                

            }
            else
            {
                ///mensaje 

            }
        }

    }


}