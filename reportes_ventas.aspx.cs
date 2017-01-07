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

public partial class reportes_ventas : System.Web.UI.Page
{
    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] claves_venta, clientes, codigos_proveedor, productos, top_productos = new string[10], fechas;
    public int[] ids_clientes, cantidades, top_cantidades = new int[10], compras_anuales = new int[12];
    public double[] subtotales, ivas, totales, comisiones, precios, precios_totales;
    public int n_ventas, contador_ventas, busqueda, i, n_productos_venta, contador_productos_venta;
    public string cliente, fecha;
    public void nu_productos_venta()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from lista_productos_venta where clave_venta='" + consulta.clave_venta + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_venta = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            codigos_proveedor = new string[n_productos_venta];
            precios = new double[n_productos_venta];
            precios_totales = new double[n_productos_venta];
            cantidades = new int[n_productos_venta];
            productos = new string[n_productos_venta];
        }
        catch (System.Exception ex)
        {
        }
    }
    public void extraer_compras_anuales()
    {
        enchufe.extraer_compras_anuales();
        compras_anuales = enchufe.compras_anuales;
    }
    public void nu_ventas()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from ventas";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_ventas = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            claves_venta = new string[n_ventas];
            subtotales = new double[n_ventas];
            ivas = new double[n_ventas];
            totales = new double[n_ventas];
            comisiones = new double[n_ventas];
            ids_clientes = new int[n_ventas];
            clientes = new string[n_ventas];
            fechas = new string[n_ventas];
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
    public void extraer_ventas_mensuales_producto()
    {
        enchufe.extraer_top_10_productos_vendidos();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
    }
    public void extraer_clientes_compras()
    {
        enchufe.extraer_top_10_clientes_consumo();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
    }
    public void extraer_top_ventas_vendedor()
    {
        enchufe.extraer_top_10_ventas_vendedor();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
    }
    public void extraer_top_ventas_producto()
    {
        enchufe.extraer_top_10_ventas_productos();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
    }
    public void extraer_top_ventas_pasado()
    {
        enchufe.extraer_top_10_productos_vendidos_pasado();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
    }
    public void extraer_top_ventas_last_year()
    {
        enchufe.extraer_top_10_ventas_last_year();
        top_cantidades = enchufe.top_cantidades;
        top_productos = enchufe.top_productos;
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
            objeto.cadena_comando_mysql = "select distinct date_format(fecha,'%Y') from ventas";
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
    public void llenar_lista_clientes()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre from sucursales where grupo='" + consulta.grupo + "'";
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

    /// <summary>
    /// 
    /// </summary>
    /// 

    public void llenar_lista_vendedores()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre from vendedores where grupo='" + consulta.grupo + "'";
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

    public void llenar_lista_productos()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select inventario.producto from inventario inner join lista_productos_venta  on lista_productos_venta.codigo = inventario.codigo_proveedor where inventario.categoria != 'servicios' group by(inventario.producto) ";
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

    public void llenar_lista_categorias()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select categoria from categorias where categoria != 'servicios'";
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
    /// <summary>
    /// 
    /// </summary>
    public void llenar_detalles_venta()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select codigo,cantidad,precio,precio_total from lista_productos_venta where clave_venta='" + consulta.clave_venta + "'";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                codigos_proveedor[contador_productos_venta] = registro[0];
                cantidades[contador_productos_venta] = Convert.ToInt32(registro[1]);
                precios[contador_productos_venta] = Convert.ToDouble(registro[2]);
                precios_totales[contador_productos_venta] = Convert.ToDouble(registro[3]);
                contador_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
    public void llenar_lista_nombres_productos()
    {
        try
        {
            i = 0;
            do
            {
                Conexion objeto = new Conexion();
                objeto.abrir_conexion_mysql();
                string[] registro;
                objeto.cadena_comando_mysql = "select producto from inventario where codigo_proveedor='" + codigos_proveedor[i] + "'";
                objeto.aplicar_comando_mysql_extraccion();
                while (objeto.leer_comando.Read())
                {
                    registro = new string[1];
                    registro[0] = objeto.leer_comando.GetString(0);
                    productos[i] = registro[0];
                    
                }
                objeto.cerrar_conexion_mysql_extraccion();
                i++;
            }
            while (i < contador_productos_venta);
        }
        catch (System.Exception ex)
        {
        }
    }
    public void llenar_lista_ventas()
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
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where  id_sucursal='" + consulta.id_sucursal + "'";
                    extraer_clientes_compras();
                    mensage.Text = "Ventas A "+ lista_fechas.Text;
                }
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where id_vendedor='" + consulta.id_vendedor_cotizacion + "' order by fecha desc ";/// servira???
                    enchufe.id_vendedor = consulta.id_vendedor_cotizacion;
                    mensage.Text = "Ventas De " + lista_fechas.Text;                                                                                                                                                              
                    extraer_top_ventas_vendedor();                                                                                                                                                                              
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "select lista_productos_venta.clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas inner join lista_productos_venta on ventas.clave_venta = lista_productos_venta.clave_venta where lista_productos_venta.codigo = '" + enchufe.codigo_producto + "' ";/// servira???
                    extraer_top_ventas_producto();
                    mensage.Text = "Ventas " + lista_fechas.Text;                                                                                                                                                                                                                                                                                         
                }
                /// x top 10 de mes
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where date_format(ventas.fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "' order by total desc limit 10";
                    enchufe.fecha = consulta.fecha.AddMonths(-1);
                    mensage.Text = "Mejores 10 Ventas " + lista_fechas.Text;
                    extraer_top_ventas_pasado();
                }
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where  date_format(fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "'";
                    enchufe.fecha = consulta.fecha.AddMonths(-1);
                    extraer_top_ventas_pasado();
                    mensage.Text = "Ventas Del Mes " + lista_fechas.Text;
                }
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where  date_format(fecha,'%Y')='" + fecha + "'";
                    enchufe.fecha = Convert.ToDateTime("01/01/" + fecha).AddYears(-1);
                    extraer_top_ventas_last_year();
                    mensage.Text = "Ventas " + lista_fechas.Text;
                }
                if (tipo_busqueda.SelectedIndex == 7)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas order by fecha desc";
                    
                    mensage.Text = "Ventas InkLaser";
                }
            }
            else {
                /// x cliente
                if (tipo_busqueda.SelectedIndex == 1)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where id_vendedor='" + consulta.id_vendedor_cotizacion + "' and id_sucursal='" + consulta.id_sucursal + "'";
                }
                /// x vendedor
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal,fecha from ventas where id_vendedor= '" + consulta.id_vendedor + "' ";/// servira???
                }
                /// x producto
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "select lista_productos_venta.clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas inner join lista_productos_venta on ventas.clave_venta = lista_productos_venta.clave_venta where lista_productos_venta.codigo = '" + enchufe.codigo_producto + "' ";
                }
                /// x top 10 de mes
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where date_format(ventas.fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "' order by total desc limit 10";
                }
                /// x mes
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor + "' and date_format(fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "'";
                }
                /// x año
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor + "' and date_format(fecha,'%Y')='" + fecha + "'";
                }            
            }

            
            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
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
                claves_venta[contador_ventas] = registro[0];
                subtotales[contador_ventas] = Convert.ToDouble(registro[1]);
                ivas[contador_ventas] = Convert.ToDouble(registro[2]);
                totales[contador_ventas] = Convert.ToDouble(registro[3]);
                comisiones[contador_ventas] = Convert.ToDouble(registro[4]);
                ids_clientes[contador_ventas] = Convert.ToInt32(registro[5]);
                fechas[contador_ventas] = registro[6];
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
    

    public void llenar_lista_clientes_ventas()
    {
        try
        {
            i = 0;
            do
            {
                Conexion objeto = new Conexion();
                objeto.abrir_conexion_mysql();
                string[] registro;
                objeto.cadena_comando_mysql = "select nombre from sucursales where id_sucursal='" + ids_clientes[i] + "'";
                objeto.aplicar_comando_mysql_extraccion();
                while (objeto.leer_comando.Read())
                {
                    registro = new string[1];
                    registro[0] = objeto.leer_comando.GetString(0);
                    clientes[i] = registro[0];
                }
                objeto.cerrar_conexion_mysql_extraccion();
                i++;
            }
            while (i < contador_ventas);
        }
        catch (System.Exception ex)
        {
        }
    }
    public DataTable detalles_venta()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("producto", typeof(string)));
        tabla.Columns.Add(new DataColumn("cantidad", typeof(Int32)));
        tabla.Columns.Add(new DataColumn("precio", typeof(double)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        if (contador_productos_venta > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["producto"] = productos[i];
                    fila["cantidad"] = cantidades[i];
                    fila["precio"] = precios[i];
                    fila["total"] = precios_totales[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
                else
                {
                    
                }
            }
            while (i < contador_productos_venta);
        }
        return tabla;
    }
    public DataSet datos_ventas_excel()
    {
        DataSet ds = new DataSet();
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("venta", typeof(string)));
        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("subtotal", typeof(double)));
        tabla.Columns.Add(new DataColumn("iva", typeof(double)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("comision", typeof(double)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {
                    fila["venta"] = claves_venta[i];
                    fila["cliente"] = clientes[i];
                    fila["subtotal"] = subtotales[i];
                    fila["iva"] = ivas[i];
                    fila["total"] = totales[i];
                    fila["comision"] = comisiones[i];
                    fila["fecha"] = fechas[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        ds.Tables.Add(tabla);
        return ds;
    }
    public DataTable datos_ventas()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("venta", typeof(string)));
        tabla.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla.Columns.Add(new DataColumn("subtotal", typeof(double)));
        tabla.Columns.Add(new DataColumn("iva", typeof(double)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("comision", typeof(double)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (clientes[i] != null)
                {
                    fila["venta"] = claves_venta[i];
                    fila["cliente"] = clientes[i];
                    fila["subtotal"] = subtotales[i];
                    fila["iva"] = ivas[i];
                    fila["total"] = totales[i];
                    fila["comision"] = comisiones[i];
                    fila["fecha"] = fechas[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        return tabla;
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
            consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
            consulta.extraer_nombre_vendedor();
            consulta.extraer_grupo_vendedor();

            boton_exportar.Visible = false;
            boton_imprimir.Visible = false;
            extraer_compras_anuales();
            texto_cabezera.Text = "Grafica Ventas Productos Mes Pasado";
            extraer_ventas_mensuales_producto();
            tipo_busqueda.SelectedIndex = 7;
            nu_ventas();
            llenar_lista_ventas();
            llenar_lista_clientes_ventas();
            tabla_ventas.DataBind();
            panel_datos.Visible = true;
            boton_exportar.Visible = true;

        }
        if (IsPostBack)
        {
            texto_cabezera.Text = "Grafica Ventas Productos Mes Pasado";
            extraer_ventas_mensuales_producto();
 
        }
    }

    /// <summary>
    /// droplist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tipo_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo_busqueda.SelectedIndex > 0)
        {
            /// x clientes
            if (tipo_busqueda.SelectedIndex == 1)
            {
                consulta.usuario = Convert.ToString(Session["usuario_vendedor"]);
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_clientes();
            }
            /// x vendedores
            if (tipo_busqueda.SelectedIndex == 2)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                //llenar_lista_clientes();
                llenar_lista_vendedores();
            }
            /// x producto
            if (tipo_busqueda.SelectedIndex == 3)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                //llenar_lista_clientes();
                llenar_lista_productos();
            }
            /// x producto mas vendido
            if (tipo_busqueda.SelectedIndex == 4)
            {
                llenar_lista_meses();
            }
            /// x meses
            if (tipo_busqueda.SelectedIndex == 5)
            {
                llenar_lista_meses();
            }
            /// x años
            if (tipo_busqueda.SelectedIndex == 6)
            {
                llenar_lista_years();
            }
            if (tipo_busqueda.SelectedIndex == 7)
            {
                nu_ventas();
                llenar_lista_ventas();
                tabla_ventas.DataBind();
                panel_datos.Visible = true;
                boton_exportar.Visible = true;

            }
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
            nu_ventas();
            ///ventas x cliente
            if (tipo_busqueda.SelectedIndex == 1)
            {
                consulta.nombre_sucursal = lista_fechas.Text.ToLower();
                consulta.extraer_id_sucursal_nombre();
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = "Grafica Consumo Clientes";
            }
            /// ventas x vendedor
            if (tipo_busqueda.SelectedIndex == 2)
            {
                consulta.nombre_vendedor = lista_fechas.Text.ToLower();
                consulta.extraer_id_vendedor_nombre();
                
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = ("Mayores Ventas De "+Convert.ToString(consulta.nombre_vendedor));
            }
            /// ventas x producto
            if (tipo_busqueda.SelectedIndex == 3)
            {
                enchufe.producto = lista_fechas.Text.ToLower();
                enchufe.extraer_id_producto_nombre();
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = "Grafica Productos Mas Vendidos";
            }
            /// ventas x producto mas vendido
            if (tipo_busqueda.SelectedIndex == 4)
            {
                consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = ("Mejores Ventas De " + lista_fechas.Text);
            }
            ///ventas x mes
            if (tipo_busqueda.SelectedIndex == 5)
            {
                consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = "Grafica Ventas Del Mes Pasado";
            }
            ///ventas x año
            if (tipo_busqueda.SelectedIndex == 6)
            {
                fecha = lista_fechas.Text;
                llenar_lista_ventas();
                llenar_lista_clientes_ventas();
                texto_cabezera.Text = "Grafica Ventas Del Año Pasado";
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
    protected void tabla_ventas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      //  if (Session["usuario_vendedor"] != null)
   //     {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                consulta.clave_venta = Convert.ToString(tabla_ventas.DataKeys[index].Values["venta"]);
                tabla_ventas.SelectedIndex = -1;
                panel_detalles_venta.Visible = true;
                nu_productos_venta();
                llenar_detalles_venta();
                llenar_lista_nombres_productos();
                tabla_detalles_venta.DataBind();
            }
      //  }
      //  if (Session["usuario_vendedor"] == null)
      //  {
      //      ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "salir();", true);
      //  }

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
    protected void boton_cerrar_detalles_venta_Click(object sender, EventArgs e)
    {
        panel_detalles_venta.Visible = false;
    }
}