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


public partial class reporte_clientes : System.Web.UI.Page
{
    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] telefonos, nombre_vendedores, claves_venta, clientes, codigos_proveedor, productos, lista_productos, numero_ventas, lista_productos_cliente, lista_descripcion;
    public int[] ids_clientes, cantidades;
    public double[] subtotales, ivas, totales, comisiones, precios, precios_totales;
    public int n_ventas, contador_ventas, busqueda, i, n_productos_venta, contador_productos_venta, n_productos, contador_lista_productos, checale;
    public string cliente, fecha;
    public DateTime[] fecha_alta;
    
    public void nu_datos()
    {
        try
        {
            
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = "select  sucursales.nombre,count(*) as numero_ventas,sum(ventas.total) as suma_total_ventas from ventas inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal group by sucursales.nombre order by sum(ventas.total) desc";
            objeto.aplicar_comando_mysql_extraccion();
            n_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                n_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clientes = new string[n_productos_venta];
            numero_ventas = new string[n_productos_venta];
            totales = new double[n_productos_venta];
        }
        catch
        {

        }
    }

    public void nu_productos()
    {
        try
        {

            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = "select lista_productos_venta.codigo,inventario.producto from lista_productos_venta inner join inventario on inventario.codigo_proveedor = lista_productos_venta.codigo group by codigo";
            objeto.aplicar_comando_mysql_extraccion();
            n_productos = 0;
            while (objeto.leer_comando.Read())
            {
                n_productos++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
            lista_productos = new string[n_productos];
            lista_descripcion = new string[n_productos];
            
        }
        catch
        {

        }
    }


    public void lista_productos_autocompleta()
    {
        try
        {

            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = " select lista_productos_venta.codigo,inventario.producto from lista_productos_venta inner join inventario on inventario.codigo_proveedor = lista_productos_venta.codigo group by codigo ";
            objeto.aplicar_comando_mysql_extraccion();
            string[] registro;
            contador_lista_productos = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[2];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                lista_productos[contador_lista_productos] = registro[0];
                lista_descripcion[contador_lista_productos] = registro[1];
                contador_lista_productos++;
            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch (System.Exception ex)
        {

        }
    }

    public void nu_productos_cliente()
    {
        try
        {

            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = "select sucursales.nombre from sucursales inner join ventas on ventas.id_sucursal = sucursales.id_sucursal group by sucursales.nombre";
            objeto.aplicar_comando_mysql_extraccion();
            n_productos = 0;
            while (objeto.leer_comando.Read())
            {
                n_productos++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
            lista_productos_cliente = new string[n_productos];

        }
        catch (System.Exception ex)
        {

        }
    }


    public void lista_productos_autocompleta_cliente()
    {
        try
        {

            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = "select sucursales.nombre from sucursales inner join ventas on ventas.id_sucursal = sucursales.id_sucursal group by sucursales.nombre";
            objeto.aplicar_comando_mysql_extraccion();
            string[] registro;
            contador_lista_productos = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                lista_productos_cliente[contador_lista_productos] = registro[0];
                contador_lista_productos++;
            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch (System.Exception ex)
        {

        }
    }


    public void llenar_lista_clientes()
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
                    objeto.cadena_comando_mysql = "select  sucursales.nombre,count(*) as numero_ventas,sum(ventas.total) as suma_total_ventas from ventas inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal group by sucursales.nombre order by sum(ventas.total) desc";
                }
                if (checale == 2)
                {
                    objeto.cadena_comando_mysql = "select sucursales.nombre,lista_productos_venta.codigo,count(lista_productos_venta.cantidad) from lista_productos_venta inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal where lista_productos_venta.codigo = '" + consulta.codigo_proveedor + "' group by sucursales.nombre order by count(lista_productos_venta.cantidad) desc";
                    Session["imprimir_medina"] = 2;
                    
                }
                if (checale == 3)
                {
                    objeto.cadena_comando_mysql = "select sucursales.nombre,lista_productos_venta.codigo,count(lista_productos_venta.cantidad) from lista_productos_venta inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal = '" + enchufe.id_sucursal + "' group by lista_productos_venta.codigo order by count(lista_productos_venta.cantidad) desc";
                    Session["imprimir_medina"] = 3;
                }
            }
            else
            {
                /// x cliente
                if (tipo_busqueda.SelectedIndex == 1)
                {
                    objeto.cadena_comando_mysql = "select  sucursales.nombre,count(*) as numero_ventas,sum(ventas.total) as suma_total_ventas from ventas inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal group by sucursales.nombre order by sum(ventas.total) desc";
                }
                if (checale == 2)
                {
                    objeto.cadena_comando_mysql = "select sucursales.nombre,lista_productos_venta.codigo,count(lista_productos_venta.cantidad) from lista_productos_venta inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal where lista_productos_venta.codigo = '" + consulta.codigo_proveedor + "' group by sucursales.nombre order by count(lista_productos_venta.cantidad) desc";
                    Session["imprimir_medina"] = 2;
                }
                if (checale == 3)
                {
                    objeto.cadena_comando_mysql = "select sucursales.nombre,lista_productos_venta.codigo,count(lista_productos_venta.cantidad) from lista_productos_venta inner join ventas on ventas.clave_venta = lista_productos_venta.clave_venta inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal = '" + enchufe.id_sucursal + "' group by lista_productos_venta.codigo order by count(lista_productos_venta.cantidad) desc";
                    Session["imprimir_medina"] = 3;
                }
                
            }


            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[3];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                clientes[contador_ventas] = Convert.ToString(registro[0]);
                numero_ventas[contador_ventas] = Convert.ToString(registro[1]);
                totales[contador_ventas] = Convert.ToDouble(registro[2]);


                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
    public DataSet datos_ventas_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();

        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("numero_ventas", typeof(string)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {

                    fila["cliente"] = clientes[i];
                    fila["numero_ventas"] = numero_ventas[i];
                    fila["total"] = totales[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        ds.Tables.Add(tabla);
        return ds;
    }
    public void exportar_excel()
    {
        using (XLWorkbook libro_trabajo = new XLWorkbook())
        {
            DataSet ps = datos_ventas_excel();
            libro_trabajo.Worksheets.Add(ps);
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

    public DataTable datos_clientes()
    {
        DataTable tabla = new DataTable();
        
        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("numero_ventas", typeof(string)));        
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {
                    
                    fila["cliente"] = clientes[i];
                    fila["numero_ventas"] = numero_ventas[i];                   
                    fila["total"] = totales[i];
                    
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        return tabla;
    }

    public DataTable alta_clientes_grilla()
    {
        DataTable tabla = new DataTable();

        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha_alta", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("nombre_vendedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("telefono", typeof(string)));

        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {

                    fila["cliente"] = clientes[i];
                    fila["fecha_alta"] = fecha_alta[i];
                    fila["nombre_vendedor"] = nombre_vendedores[i];
                    fila["telefono"] = telefonos[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        return tabla;
    }

    public DataSet datos_altas_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();

        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha_alta", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("nombre_vendedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("telefono", typeof(string)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {

                    fila["cliente"] = clientes[i];
                    fila["fecha_alta"] = fecha_alta[i];
                    fila["nombre_vendedor"] = nombre_vendedores[i];
                    fila["telefono"] = telefonos[i];

                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        ds.Tables.Add(tabla);
        return ds;
    }

    public void exportar_excel_altas()
    {
        using (XLWorkbook libro_trabajo = new XLWorkbook())
        {
            DataSet ps = datos_altas_excel();
            libro_trabajo.Worksheets.Add(ps);
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
        if (!IsPostBack)
        {
            boton_exportar.Visible = false;
            boton_imprimir.Visible = false;
            tipo_busqueda.SelectedIndex = 1;
            nu_datos();
            llenar_lista_clientes();
            tabla_ventas.DataBind();
            panel_datos.Visible = true;
            encabezado.Text = "Consumo De Clientes";

        }
        else 
        {
            nu_productos_cliente();
            lista_productos_autocompleta_cliente();
            nu_productos();
            lista_productos_autocompleta();

        }
    }

    public void nu_alta_clientes()
    {
        Conexion objeto = new Conexion();
        objeto.abrir_conexion_mysql();
        objeto.cadena_comando_mysql = "select count(*) from sucursales";
        objeto.aplicar_comando_mysql_extraccion();
        n_productos_venta = 0;
        string[] registro;
        while (objeto.leer_comando.Read())
        {
            registro = new string[1];
            registro[0] = objeto.leer_comando.GetString(0);
            n_productos_venta = Convert.ToInt32(registro[0]);
        }
        objeto.cerrar_conexion_mysql_extraccion();
        clientes = new string[n_productos_venta];
        fecha_alta = new DateTime[n_productos_venta];
        nombre_vendedores = new string[n_productos_venta];
        telefonos = new string[n_productos_venta];
    }


    public void alta_clientes()
    {
        string[] registro;
        Conexion objeto = new Conexion();
        objeto.abrir_conexion_mysql();
        objeto.cadena_comando_mysql = "select sucursales.nombre,sucursales.fecha_alta,vendedores.nombre,telefono_principal from sucursales inner join vendedores on vendedores.id_vendedor = sucursales.id_vendedor";
        objeto.aplicar_comando_mysql_extraccion();
        contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                clientes[contador_ventas] = Convert.ToString(registro[0]);
                fecha_alta[contador_ventas] = Convert.ToDateTime(registro[1]);
                nombre_vendedores[contador_ventas] = Convert.ToString(registro[2]);
                telefonos[contador_ventas] = Convert.ToString(registro[3]);
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
 
    

    protected void tipo_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo_busqueda.SelectedIndex > 0)
        {
            panel_busqueda_cliente.Visible = false;
            panel_busqueda.Visible = false;
            panel_no_resultados_busqueda.Visible = false;
            panel_datos.Visible = false;
            panel_alta_clientes.Visible = false;
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_datos();
            if (tipo_busqueda.SelectedIndex == 1)
            {
                consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_clientes();
                Session["imprimir_medina"] = 0;
                encabezado.Text = "Consumo De Clientes";
                panel_alta_clientes.Visible = false;
                panel_datos.Visible = true;
                Busqueda_principal.Visible = true;

                
            }
            if (tipo_busqueda.SelectedIndex == 2)
            {
                encabezado.Text = "Consumo De Productos" + busqueda_cliente.Text;
                consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                nu_productos();
                lista_productos_autocompleta();
                panel_busqueda.Visible = true;
                panel_busqueda_cliente.Visible = false;
                panel_alta_clientes.Visible = false;
                Busqueda_principal.Visible = false;
                
            }
            if (tipo_busqueda.SelectedIndex == 3)
            {
                encabezado.Text = "Productos Que Consume "+busqueda_cliente.Text;
                consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                nu_productos_cliente();
                lista_productos_autocompleta_cliente();
                panel_busqueda_cliente.Visible = true;
                panel_busqueda.Visible = false;
                panel_alta_clientes.Visible = false;
                Busqueda_principal.Visible = false;
                
            }
            if (tipo_busqueda.SelectedIndex == 4)
            {
                encabezado.Text = "Alta Clientes ";
                consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                nu_alta_clientes();
                alta_clientes();
                tabla_alta_clientes.DataBind();
                panel_alta_clientes.Visible = true;
                panel_busqueda_cliente.Visible = false;
                panel_busqueda.Visible = false;
                panel_datos.Visible = false;
                Session["imprimir_medina"] = 4;

            }


         
                      
        }
        else if (tipo_busqueda.SelectedIndex != 4)
        {
            tabla_ventas.DataBind();
            panel_datos.Visible = true;
        }
        if (contador_ventas > 0)
        {
            
            boton_exportar.Visible = true;
            //boton_imprimir.Visible = true;
        }
        else if (tipo_busqueda.SelectedIndex != 3 && tipo_busqueda.SelectedIndex != 2)
        {
            boton_imprimir.Visible = false;
            boton_exportar.Visible = false;
            panel_no_resultados_busqueda.Visible = true;
        }

    }
    protected void tabla_ventas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

    }

    protected void lista_fechas_SelectedIndexChanged(object sender, EventArgs e)
    {
        llenar_lista_clientes();
    }
    protected void boton_exportar_Click(object sender, EventArgs e)
    {
        nu_datos();
        nu_alta_clientes();
        checale = Convert.ToInt16(Session["imprimir_medina"]);
        enchufe.id_sucursal = Convert.ToInt16(Session["nombre_sucursal_para_imprimir"]);
        consulta.codigo_proveedor = Convert.ToString(Session["codigo_proveedor_para_imprimir"]);
        if (checale != 4)
        {
            llenar_lista_clientes();
            exportar_excel();
        }
        else if (checale == 4)
        {
            alta_clientes();
            exportar_excel_altas();
        }
    }
    protected void boton_imprimir_Click(object sender, EventArgs e)
    {

    }
    protected void boton_buscar_Click(object sender, EventArgs e)
    {
        nu_datos();
        checale = 2;
        consulta.codigo_proveedor = busqueda_medina.Text;
        encabezado.Text = "Clienetes Que Consumen " + busqueda_medina.Text;
        Session["codigo_proveedor_para_imprimir"] = busqueda_medina.Text;
        llenar_lista_clientes();
        tabla_ventas.DataBind();
        panel_datos.Visible = true;
        boton_exportar.Visible = true;

    }
    protected void boton_buscar_cliente_Click(object sender, EventArgs e)
    {
        nu_datos();
        checale = 3;
        enchufe.nombre_sucursal = busqueda_cliente.Text;       
        enchufe.extraer_id_sucursal_con_nombre();
        Session["nombre_sucursal_para_imprimir"] = enchufe.id_sucursal;
        llenar_lista_clientes();
        tabla_ventas.DataBind();
        encabezado.Text = "Productos Que Consume " + busqueda_cliente.Text;
        panel_datos.Visible = true;
        boton_exportar.Visible = true;


    }
    protected void boton_buscar_producto_Click(object sender, EventArgs e)
    {
        nu_datos();
        checale = 2;
        enchufe.producto_descripcion = busqueda_producto.Text;
        enchufe.extraer_codigo_proveedor_con_descripcion();
        consulta.codigo_proveedor = enchufe.codigo_proveedor;
        encabezado.Text = "Clienetes Que Consumen " + busqueda_producto.Text;
        Session["codigo_proveedor_para_imprimir"] = consulta.codigo_proveedor;
        llenar_lista_clientes();
        tabla_ventas.DataBind();
        panel_datos.Visible = true;
        boton_exportar.Visible = true;
    }
    protected void boton_salir_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void boton_cerrar_detalles_venta_Click(object sender, EventArgs e)
    {
        panel_detalles_venta.Visible = false;
    }


 }
