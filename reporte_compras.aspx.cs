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

public partial class reporte_compras : System.Web.UI.Page
{
    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] claves_orden_compra, clientes, codigos_proveedor, productos, claves_cotizacion, estado_compra, fecha_orden, codigos_proveedor_orden;
    public int[] ids_clientes, cantidades, compras_anuales = new int[12], ventas_anuales = new int[12];
    public double[]  totales, comisiones, precios, precios_totales;
    public int n_compras, contador_ventas, busqueda, i, n_productos_venta, contador_productos_venta;
    public string cliente, fecha, estado;

    public void nu_productos_compra()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from lista_productos_orden_compra where clave_orden_compra='" + consulta.clave_orden_compra + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_venta = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            precios_totales = new double[n_productos_venta];
            claves_cotizacion = new string[n_productos_venta];
            productos = new string[n_productos_venta];
            estado_compra = new string[n_productos_venta];
            cantidades = new int[n_productos_venta];
            codigos_proveedor_orden = new string[n_productos_venta];
            
            
        }
        catch (System.Exception ex)
        {
        }
    }

    public void nu_compras()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from ordenes_compra";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_compras = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            claves_orden_compra = new string[n_compras];        
            estado_compra = new string[n_compras];
            totales = new double[n_compras];          
            fecha_orden = new string[n_compras];
            codigos_proveedor = new string[n_compras];
            

        }
        catch (System.Exception ex)
        {
        }
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
            objeto.cadena_comando_mysql = "select distinct date_format(fecha,'%Y-%m') from ordenes_compra";
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
    public void llenar_lista_years()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select distinct date_format(fecha,'%Y') from ordenes_compra";
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

    public void llenar_lista_proveedores()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre from proveedores";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                lista_fechas.Items.Add(new CultureInfo("en-US", false).TextInfo.ToTitleCase(registro[0]));
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void llenar_lista_por_estado()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT estado from ordenes_compra group by(estado)";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                lista_fechas.Items.Add(new CultureInfo("en-US", false).TextInfo.ToTitleCase(registro[0]));
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
    public void llenar_detalles_compra()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT lista_productos_orden_compra.precio_total,ordenes_compra.clave_cotizacion,lista_productos_orden_compra.producto,lista_productos_orden_compra.aprobacion,lista_productos_orden_compra.cantidad,ordenes_compra.proveedor FROM ordenes_compra inner join lista_productos_orden_compra ON lista_productos_orden_compra.clave_orden_compra = ordenes_compra.clave_orden_compra where  lista_productos_orden_compra.clave_orden_compra = '"+consulta.clave_orden_compra+"'";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[6];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                registro[5] = objeto.leer_comando.GetString(5);
                precios_totales[contador_productos_venta] = Convert.ToDouble(registro[0]);
                claves_cotizacion[contador_productos_venta] = registro[1];
                productos[contador_productos_venta] = registro[2];
                estado_compra[contador_productos_venta] = registro[3];
                cantidades[contador_productos_venta] = Convert.ToInt32(registro[4]);
                codigos_proveedor_orden[contador_productos_venta] = registro[5];
               
              
                contador_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public DataTable detalles_compra()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("precios_totales", typeof(double)));
        tabla.Columns.Add(new DataColumn("claves_cotizacion",typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("estado_compra", typeof(string)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("codigos_proveedor", typeof(string)));

        if (contador_productos_venta > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (claves_cotizacion[i] != null)
                {
                    fila["precios_totales"] = precios_totales[i];
                    fila["claves_cotizacion"] = claves_cotizacion[i];
                    fila["productos"] = productos[i];
                    fila["estado_compra"] = estado_compra[i];
                    fila["cantidades"] = cantidades[i];
                    fila["codigos_proveedor"] = codigos_proveedor_orden[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_productos_venta);
        }
        return tabla;
    }

    public void llenar_lista_compra()
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
                    objeto.cadena_comando_mysql = "select clave_orden_compra,estado,fecha,proveedor from ordenes_compra where estado= '"+ estado +"'  ";

                }
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "select clave_orden_compra,estado,fecha,proveedor from ordenes_compra where date_format(fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "'";
                    
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "select clave_orden_compra,estado,fecha,proveedor from ordenes_compra where date_format(fecha,'%Y')='" + fecha + "'";
                }
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = "select clave_orden_compra,estado,fecha,proveedor from ordenes_compra order by fecha desc";
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
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);            
                claves_orden_compra[contador_ventas] = registro[0];                
                estado_compra[contador_ventas] = registro[1];
                fecha_orden[contador_ventas] = Convert.ToString(registro[2]);
                codigos_proveedor[contador_ventas] = registro[3];
               
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
        tabla.Columns.Add(new DataColumn("precios_totales", typeof(double)));
        tabla.Columns.Add(new DataColumn("claves_cotizacion", typeof(string)));
        tabla.Columns.Add(new DataColumn("productos", typeof(string)));
        tabla.Columns.Add(new DataColumn("estado_compra", typeof(string)));
        tabla.Columns.Add(new DataColumn("cantidades", typeof(int)));
        tabla.Columns.Add(new DataColumn("codigos_proveedor", typeof(string)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (claves_cotizacion[i] != null)
                {
                    fila["precios_totales"] = precios_totales[i];
                    fila["claves_cotizacion"] = claves_cotizacion[i];
                    fila["productos"] = productos[i];
                    fila["estado_compra"] = estado_compra[i];
                    fila["cantidades"] = cantidades[i];
                    fila["codigos_proveedor"] = codigos_proveedor_orden[i];
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


    public DataTable datos_compra()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("claves_orden_compra", typeof(string)));
        tabla.Columns.Add(new DataColumn("estado_compra", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha_orden", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("codigos_proveedor", typeof(string)));
        
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (claves_orden_compra[i] != null)
                {
                    fila["claves_orden_compra"] = claves_orden_compra[i];
                    fila["estado_compra"] = estado_compra[i];
                    fila["fecha_orden"] = fecha_orden[i];
                    fila["codigos_proveedor"] = codigos_proveedor[i];
                    
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        return tabla;
    }

    public void extraer_compras_anuales()
    {
        enchufe.extraer_compras_anuales();
        compras_anuales = enchufe.compras_anuales;
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
            panelgrafica.Visible = true;
            extraer_compras_anuales();
            panel_busqueda.Visible = false;
            tipo_busqueda.SelectedIndex = 4;
            nu_compras();
            llenar_lista_compra();
            Page.DataBind();
            panel_datos.Visible = true;



        }
        if(IsPostBack)
        {
            panelgrafica.Visible = false;
            panel_busqueda.Visible = true;
        }
    }

    public void busqueda_reportes()
    {
        if (lista_fechas.SelectedIndex > 0)
        {
            panel_no_resultados_busqueda.Visible = false;
            panel_datos.Visible = false;
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            nu_compras();

            ///ventas x cliente
            if (tipo_busqueda.SelectedIndex == 1)
            {
                estado = lista_fechas.Text.ToLower();
                consulta.extraer_id_sucursal_nombre();
                llenar_lista_compra();
                
            }
            /// ventas x vendedor
            if (tipo_busqueda.SelectedIndex == 2)
            {
                consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
                llenar_lista_compra();
                
            }
            /// ventas x producto
            if (tipo_busqueda.SelectedIndex == 3)
            {
                fecha = lista_fechas.Text;
                llenar_lista_compra();
            }
            if (tipo_busqueda.SelectedIndex == 4)
            {
                llenar_lista_compra();
            }
           
        }
        Page.DataBind();
        if (contador_ventas > 0)
        {
            panel_datos.Visible = true;
            boton_exportar.Visible = true;
            //boton_imprimir.Visible = true;
        }
        else
        {
            boton_imprimir.Visible = false;
            boton_exportar.Visible = false;
            panel_no_resultados_busqueda.Visible = true;
        }
    }


    protected void tabla_compras_RowCommand(object sender, GridViewCommandEventArgs e)
     {
        
        if (e.CommandName == "Ver")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            consulta.clave_orden_compra = Convert.ToString(tabla_compras.DataKeys[index].Values["claves_orden_compra"]);
            tabla_compras.SelectedIndex = -1;
            panel_detalles_compra.Visible = true;
            nu_productos_compra();
            llenar_detalles_compra();           
            tabla_detalles_compra.DataBind();
        }
       
    }
    protected void tipo_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo_busqueda.SelectedIndex > 0)
        {
            /// x estado
            if (tipo_busqueda.SelectedIndex == 1)
            {            
                llenar_lista_por_estado();
            }
            /// x vendedores
            if (tipo_busqueda.SelectedIndex == 2)
            {
                llenar_lista_meses();
            }
            /// x producto
            if (tipo_busqueda.SelectedIndex == 3)
            {
                llenar_lista_years();
            }
            if (tipo_busqueda.SelectedIndex == 4)
            {
                nu_compras();
                llenar_lista_compra();
                Page.DataBind();
                panel_datos.Visible = true;

            }
            
        }
    }
    protected void lista_fechas_SelectedIndexChanged(object sender, EventArgs e)
    {
        busqueda_reportes();
    }
    protected void boton_exportar_Click(object sender, EventArgs e)
    {
        busqueda_reportes();
        exportar_excel();
    }
    protected void boton_imprimir_Click(object sender, EventArgs e)
    {

    }
    protected void boton_salir_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void boton_buscar_Click(object sender, EventArgs e)
    {
        /** 
        enchufe.codigo_proveedor = busca_producto.Text;
        enchufe.extraer_compras_producto();
        compras_anuales = enchufe.compras_anuales;
        enchufe.extraer_ventas_producto_compras();
        ventas_anuales = enchufe.ventas_anuales;
         **/
    }
    protected void boton_cerrar_detalles_venta_Click(object sender, EventArgs e)
    {
        panel_detalles_compra.Visible = false;
    }

}