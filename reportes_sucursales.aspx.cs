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

public partial class reportes_sucursales : System.Web.UI.Page
{

    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] clientes, codigos_proveedor, productos, top_productos = new string[10], nombres, fechas, sucursales;
    public int[] ids_clientes, cantidades, top_cantidades = new int[10], compras_anuales_matriz = new int[12], cantidades_compras, numero_compras, compras_anuales_cuarta = new int[12], compras_anuales_cbtis = new int[12], compras_anuales_cortez = new int[12], ids_vendedor;
    public double[] subtotales, ivas, totales, comisiones, precios, precios_totales;
    public int n_ventas, contador_ventas, busqueda, i, n_productos_venta, contador_productos_venta,ventas_detalle;
    public string cliente, fecha, nombre_cliente, nombre_sucursal, fechaletra;
    public double ventas_acumuladas, prediccion_ventas;
    DateTime fecha_inicio, fecha_final,fechapolar;
    /// <summary>
    /// /
    /// </summary>

    

    public void nu_productos_venta()
    {
        try
        {


            codigos_proveedor = new string[ventas_detalle];
            fechas = new string[ventas_detalle];
            precios = new double[ventas_detalle];
            ids_vendedor = new int[ventas_detalle];
        }
        catch (System.Exception ex)
        {

        }
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

            ids_clientes = new int[n_ventas];
            sucursales = new string[n_ventas];
            numero_compras = new int[n_ventas];
            precios_totales = new double[n_ventas];
            codigos_proveedor = new string[n_ventas];
            fechas = new string[n_ventas];
            precios = new double[n_ventas];
            ids_vendedor = new int[n_ventas];
            contador_ventas++;
            
        }
        catch (System.Exception ex)
        {
        }
    }


    public void llenar_lista_sucursales()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre from sucursales_sistema";
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

    

    public void llenar_lista_fechas_pro()
    {
        
        try
        {
            lista_fechas.Items.Clear();
        lista_fechas.Items.Add("Seleccionar");
        Conexion objeto = new Conexion();
        objeto.abrir_conexion_mysql();
        string[] registro;
        objeto.cadena_comando_mysql = "select distinct date_format(fecha,'%Y-%m-%d') from ventas order by fecha";
        objeto.aplicar_comando_mysql_extraccion();
        while (objeto.leer_comando.Read())
        {
            registro = new string[1];
            registro[0] = objeto.leer_comando.GetString(0);
            lista_fechas.Items.Add(registro[0]);

        }
        objeto.cerrar_conexion_mysql_extraccion();
        }
        catch 
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

    public void llenar_turnos()
    {
        lista_fechas.Items.Clear();
        lista_fechas.Items.Add("Seleccionar");
        lista_fechas.Items.Add("Turno Matutino");
        lista_fechas.Items.Add("Turno Vespertino");
        lista_fechas.Items.Add("Turno Completo");

    }

    private void extraer_ventas_mensuales_matriz()
    {
        enchufe.id_sucursal_sistema = 1;
        enchufe.extraer_compras_anuales_matriz();
        compras_anuales_matriz = enchufe.compras_anuales;
            
    }

    private void extraer_ventas_mensuales_cortez()
    {
        enchufe.id_sucursal_sistema = 2;
        enchufe.extraer_compras_anuales_cortez();
        compras_anuales_cortez = enchufe.compras_anuales2;
    }
    private void extraer_ventas_mensuales_cuarta()
    {
        enchufe.id_sucursal_sistema = 3;
        enchufe.extraer_compras_anuales_cuarta();
        compras_anuales_cuarta = enchufe.compras_anuales3;
    }
    private void extraer_ventas_mensuales_cbtis()
    {
        enchufe.id_sucursal_sistema = 4;
        enchufe.extraer_compras_anuales_cbtis();
        compras_anuales_cbtis = enchufe.compras_anuales4;
        
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


    public void llenar_lista_sucursales_grid()
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
                    objeto.cadena_comando_mysql = "SELECT ventas.id_sucursal,sucursales.nombre ,COUNT( ventas.clave_venta ) as Numero_Compras,sum(ventas.total) FROM ventas join sucursales on ventas.id_sucursal = sucursales.id_sucursal where ventas.id_sucursal_sistema = '" + enchufe.id_sucursal_sistema + "' group by sucursales.nombre order by Numero_compras desc";
                    Session["id_para_detalles_medina"] = enchufe.id_sucursal_sistema;
                    
                }
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "SELECT ventas.id_sucursal,sucursales.nombre ,COUNT( ventas.clave_venta ) as Numero_Compras,sum(ventas.total) FROM ventas join sucursales  on ventas.id_sucursal = sucursales.id_sucursal where Date_format(fecha,'%Y-%m-%d')= '" + consulta.fecha.ToString("yyyy-MM-dd") + "' group by sucursales.nombre order by Numero_compras desc";
                    Session["fecha_para_detalles_medina"] = consulta.fecha.ToString("yyyy-MM-dd");
                   
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "SELECT ventas.id_sucursal,sucursales.nombre ,COUNT( ventas.clave_venta ) as Numero_Compras,sum(ventas.total) FROM ventas join sucursales  on ventas.id_sucursal = sucursales.id_sucursal where Date_format(fecha,'%Y-%m')= '" + consulta.fecha.ToString("yyyy-MM") + "' group by sucursales.nombre order by Numero_compras desc";
                    Session["fecha_para_detalles_medina"] = consulta.fecha.ToString("yyyy-MM");
                }
               
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = "SELECT ventas.id_sucursal,sucursales.nombre ,COUNT( ventas.clave_venta ) as Numero_Compras,sum(ventas.total) FROM ventas join sucursales  on ventas.id_sucursal = sucursales.id_sucursal where Date_format(fecha,'%Y')= '" + fecha + "' group by sucursales.nombre order by Numero_compras desc";
                    Session["fecha_para_detalles_medina"] = fecha;
                    
                }
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    objeto.cadena_comando_mysql = "SELECT ventas.id_sucursal_sistema,sucursales_sistema.nombre ,COUNT( ventas.clave_venta ) as Numero_Compras,sum(ventas.total)  FROM ventas  join sucursales_sistema  on ventas.id_sucursal_sistema = sucursales_sistema.id_sucursal_sistema  where ventas.fecha  BETWEEN '" + fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "'  and  '" + fecha_final.ToString("yyyy-MM-dd HH:mm:ss") + "' group by ventas.id_sucursal_sistema order by Numero_compras desc";
                    Session["id_para_detalles_medina"] = enchufe.id_sucursal_sistema;
                    Session["fecha_inicial_medina"] = fecha_inicio;
                    Session["fecha_final_medina"] = fecha_final;

                }
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    ///objeto.cadena_comando_mysql = "";
        
                }
            }
            else
            {
                /// x cliente
                if (tipo_busqueda.SelectedIndex == 1)
                {
                    
                }
                /// x vendedor
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    
                }
                /// x producto
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    
                }
                /// x top 10 de mes
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    
                }
                /// x mes
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    
                }
                /// x año
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    
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
                ids_clientes[contador_ventas] = Convert.ToInt32(registro[0]);
                sucursales[contador_ventas] = registro[1];
                numero_compras[contador_ventas] = Convert.ToInt32(registro[2]);
                precios_totales[contador_ventas] = Convert.ToDouble(registro[3]);
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void llenar_detalles_venta()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            if (tipo_busqueda.SelectedIndex == 1)
            {
                objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor from ventas inner join sucursales  on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal_sistema = '" + consulta.id_sucursal_sistema + "' and ventas.id_sucursal = '" + consulta.id_sucursal + "' order by ventas.fecha desc";
            }
            if (tipo_busqueda.SelectedIndex == 2)
            {
                objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor from ventas inner join sucursales  on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal = '" + consulta.id_sucursal + "' and Date_format(fecha,'%Y-%m-%d')= '" + enchufe.fecha_para_detalles + "' ";
            }
            if (tipo_busqueda.SelectedIndex == 3)
            {
                objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor from ventas inner join sucursales  on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal = '" + consulta.id_sucursal + "' and Date_format(fecha,'%Y-%m')= '" + enchufe.fecha_para_detalles + "'";
            }
            if (tipo_busqueda.SelectedIndex == 4)
            {
                objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor from ventas inner join sucursales  on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal = '" + consulta.id_sucursal + "' and Date_format(fecha,'%Y')= '" + enchufe.fecha_para_detalles + "'";
            }
            if (tipo_busqueda.SelectedIndex == 5)
            {
                objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor  FROM ventas  join sucursales_sistema  on ventas.id_sucursal_sistema = sucursales_sistema.id_sucursal_sistema  where (ventas.fecha  BETWEEN '" + fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "'  and  '" + fecha_final.ToString("yyyy-MM-dd HH:mm:ss") + "') and ventas.id_sucursal_sistema = '" + consulta.id_sucursal + "'  ";
            }
            if (tipo_busqueda.SelectedIndex == 6)
            {
                
            }
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                codigos_proveedor[contador_productos_venta] = registro[0]; /// clave venta
                fechas[contador_productos_venta] = registro[1];
                precios[contador_productos_venta] = Convert.ToDouble(registro[2]);
                ids_vendedor[contador_productos_venta] = Convert.ToInt32(registro[3]); /// id vendedores
                contador_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }


    public void llenar_detalles_sucursal()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select ventas.clave_venta , ventas.fecha ,ventas.total,ventas.id_vendedor from ventas inner join sucursales  on sucursales.id_sucursal = ventas.id_sucursal where ventas.id_sucursal_sistema = '" + enchufe.id_sucursal_sistema + "' order by ventas.fecha desc";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                codigos_proveedor[contador_productos_venta] = registro[0]; /// clave venta
                fechas[contador_productos_venta] = registro[1];
                precios[contador_productos_venta] = Convert.ToDouble(registro[2]);
                ids_vendedor[contador_productos_venta] = Convert.ToInt32(registro[3]); /// id vendedores
                contador_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    
    public DataTable detalles_venta()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("codigo_proveedor", typeof(string))); ///claves venta
        tabla.Columns.Add(new DataColumn("fechas", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("id_clientes", typeof(Int32)));///id vendedores

        
        if (contador_productos_venta > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (codigos_proveedor[i] != null)
                {
                    fila["codigo_proveedor"] = codigos_proveedor[i];
                    fila["fechas"] = fechas[i];
                    fila["total"] = precios[i];
                    fila["id_clientes"] = ids_vendedor[i];
                    
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
        tabla.Columns.Add(new DataColumn("id_sucursal", typeof(Int32)));
        tabla.Columns.Add(new DataColumn("nombre", typeof(string)));
        tabla.Columns.Add(new DataColumn("compras", typeof(Int32)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (ids_clientes[i] != null)
                {
                    fila["id_sucursal"] = ids_clientes[i];
                    fila["nombre"] = sucursales[i];
                    fila["compras"] = numero_compras[i];
                    fila["total"] = precios_totales[i];
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
        tabla.Columns.Add(new DataColumn("id_sucursal", typeof(Int32)));
        tabla.Columns.Add(new DataColumn("nombre", typeof(string)));
        tabla.Columns.Add(new DataColumn("compras", typeof(Int32)));
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (ids_clientes[i] != null)
                {
                    fila["id_sucursal"] = ids_clientes[i];
                    fila["nombre"] = sucursales[i];
                    fila["compras"] = numero_compras[i];
                    fila["total"] = precios_totales[i];
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
    public void prediccion()
    {
        Conexion objeto = new Conexion();
        string[] registro;
        enchufe.id_sucursal_sistema = Convert.ToInt16(Session["id_sucursal_sistema_medina"]);

        if (DateTime.TryParseExact(Convert.ToString(Session["fecha_inicial_medina_predictivo"]) , "mm/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None ,out fecha_inicio))
        {
            if (DateTime.TryParseExact(Convert.ToString(Session["fecha_final_medina_predictivo"]) , "mm/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None ,out fecha_final))
            {
                TimeSpan diferencia = fecha_final - fecha_inicio;
                int dias_trascurridos = diferencia.Days;
                if (dias_trascurridos > 1 && dias_trascurridos < 30)
                {
                    try
                    {
                        objeto.abrir_conexion_mysql();
                        objeto.cadena_comando_mysql = "SELECT sum(ventas.total) as ventas_acumuladas FROM ventas  where (Date_format(ventas.fecha,'%Y-%m-%d')  BETWEEN '" + fecha_inicio.ToString("yyyy-MM-dd") + "'  and  '" + fecha_final.ToString("yyyy-MM-dd") + "') and ventas.id_sucursal_sistema = '" + enchufe.id_sucursal_sistema + "'";
                        objeto.aplicar_comando_mysql_extraccion();
                        while (objeto.leer_comando.Read())
                        {
                            registro = new string[1];
                            registro[0] = objeto.leer_comando.GetString(0);
                            ventas_acumuladas = Convert.ToDouble(registro[0]);
                        }
                        objeto.cerrar_conexion_mysql_extraccion();
                    }

                    catch (System.Exception ex) { }

                    if (ventas_acumuladas != null && dias_trascurridos != null)
                    {
                        prediccion_ventas = (ventas_acumuladas / dias_trascurridos) * 30;
                        cantidad_calculo.Text = Convert.ToString(Math.Round(prediccion_ventas, 2));
                        cantidad_calculo.Visible = true;
                    }
                }
                else 
                {
                    Response.Write("<script language=javascript>alert('Error En Intervalo De Dias');</script>");
                }
            }
        }
       

        
       
       
       

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Session["usuario_vendedor"] = "jmedina@inklaser.mx";
        //Session["fecha_inicial_medina_predictivo"] = "";
        //Session["fecha_final_medina_predictivo"] = ""; 

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
            panel_busqueda.Visible = false;
            extraer_ventas_mensuales_matriz();
            extraer_ventas_mensuales_cortez();
            extraer_ventas_mensuales_cuarta();
            extraer_ventas_mensuales_cbtis();
            
            
          
        }
        if (IsPostBack)
        {
           
            panelgrafica.Visible = false;
            
        }
    }

    protected void tipo_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        panel_detalles_venta.Visible = false;
        titulo.Text = "Reportes InkLaser";
        if (tipo_busqueda.SelectedIndex > 0)
        {
            /// x sucursales
            if (tipo_busqueda.SelectedIndex == 1)
            {
                llenar_lista_sucursales();
            }
            /// x dia
            if (tipo_busqueda.SelectedIndex == 2)
            {
                llenar_lista_fechas_pro();
            }
            /// x meses
            if (tipo_busqueda.SelectedIndex == 3)
            {
                llenar_lista_meses();               
            }
            /// x años
            if (tipo_busqueda.SelectedIndex == 4)
            {
                llenar_lista_years();
            }
            /// x turno
            if (tipo_busqueda.SelectedIndex == 5)
            {
                llenar_turnos();
            }
            /// x pronostico
            if (tipo_busqueda.SelectedIndex == 6)
            {
                
                panel_busqueda.Visible = false;
                llenar_lista_sucursales();
                panel_busqueda.Visible = false;
                panel_datos.Visible = false;

            }
            panelpredictivo.Visible = false;
        }
    }
    public void busqueda_reportes()
    {
        if (lista_fechas.SelectedIndex > 0)
        {
            panel_no_resultados_busqueda.Visible = false;
            panel_datos.Visible = false;
            panelpredictivo.Visible = false;
            nu_ventas();
            
            ///ventas x cliente
            if (tipo_busqueda.SelectedIndex == 1)
            {
                cabezera.Text = "Reporte De " + lista_fechas.Text;
                nombre_sucursal = lista_fechas.Text;
                enchufe.nombre_sucursal_sistema = nombre_sucursal;
                enchufe.extraer_id_sucursal_sistema_con_nombre();
                llenar_lista_sucursales_grid();
                llenar_detalles_sucursal();
                tabla_detalles_venta.DataBind();
                panel_detalles_venta.Visible = true;
                titulo.Text = "Ventas " + lista_fechas.Text;
                panel_busqueda.Visible = true;
                
            }
            /// ventas x dias
            if (tipo_busqueda.SelectedIndex == 2)
            {
                cabezera.Text = "Reporte De " + lista_fechas.Text;
                consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
                llenar_lista_sucursales_grid();
            }
            /// ventas x mes
            if (tipo_busqueda.SelectedIndex == 3)
            {
                cabezera.Text = "Reporte De " + lista_fechas.Text;
                consulta.fecha =Convert.ToDateTime(lista_fechas.Text);
                llenar_lista_sucursales_grid();
            }
            /// ventas x año
            if (tipo_busqueda.SelectedIndex == 4)
            {
                cabezera.Text = "Reporte De " + lista_fechas.Text;
                fecha = lista_fechas.Text;
                llenar_lista_sucursales_grid();
            }
            ///ventas x turno
            if (tipo_busqueda.SelectedIndex == 5)
            {
                cabezera.Text = "Reporte " + lista_fechas.Text;
                fecha = lista_fechas.Text;
                fechapolar = DateTime.Now;
                fechaletra = fechapolar.ToString("yyyy-MM-dd");
                if (fecha == "Turno Matutino")
                {

                    fecha_inicio = Convert.ToDateTime(fechaletra+" 08:00:00");
                    fecha_final = Convert.ToDateTime(fechaletra + " 15:00:00");
                    llenar_lista_sucursales_grid();
                }
                if (fecha == "Turno Vespertino")
                {
                    fecha_inicio = Convert.ToDateTime(fechaletra +" 14:00:00");
                    fecha_final = Convert.ToDateTime(fechaletra + " 21:10:00");
                    llenar_lista_sucursales_grid();
                }
                if (fecha == "Turno Completo")
                {
                    fecha_inicio = Convert.ToDateTime(fechaletra + " 07:00:00");
                    fecha_final = Convert.ToDateTime(fechaletra + " 21:10:00");
                    llenar_lista_sucursales_grid();
                }
            }
            ///pronostico
            if (tipo_busqueda.SelectedIndex == 6)
            {
                enchufe.nombre_sucursal_sistema = lista_fechas.Text;
                enchufe.extraer_id_sucursal_sistema_con_nombre();
                Session["id_sucursal_sistema_medina"] = enchufe.id_sucursal_sistema;
                panelpredictivo.Visible = true;
                panel_datos.Visible = false;
                
               
                

            }

        }
        tabla_ventas.DataBind();
        if (contador_ventas > 0 && tipo_busqueda.SelectedIndex != 6)
        {
            panel_datos.Visible = true;
            boton_exportar.Visible = true;
            //boton_imprimir.Visible = true;
        }
        else if (tipo_busqueda.SelectedIndex != 6)
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
            enchufe.fecha_para_detalles = Convert.ToString(Session["fecha_para_detalles_medina"]);
            consulta.id_sucursal_sistema = Convert.ToInt32(Session["id_para_detalles_medina"]);     
            fecha_inicio = Convert.ToDateTime(Session["fecha_inicial_medina"]);
            fecha_final = Convert.ToDateTime(Session["fecha_final_medina"]);
            int index = Convert.ToInt32(e.CommandArgument);
            consulta.id_sucursal = Convert.ToInt32(tabla_ventas.DataKeys[index].Values["id_sucursal"]);
            ventas_detalle = Convert.ToInt32(tabla_ventas.DataKeys[index].Values["compras"]);
            enchufe.nombre_sucursal = (string)this.tabla_ventas.DataKeys[index]["nombre"];
            tabla_ventas.SelectedIndex = -1;
            panel_detalles_venta.Visible = true;
            nu_productos_venta();
            llenar_detalles_venta();
            tabla_detalles_venta.DataBind();
            titulo.Text = "Ventas A " + enchufe.nombre_sucursal;
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
    protected void prediccion_Click(object sender, EventArgs e)
    {
        if (buscarfecha.Text != "" )
        {
            Session["fecha_inicial_medina_predictivo"] = buscarfecha.Text;
            Session["fecha_final_medina_predictivo"] = buscarfecha2.Text;
            prediccion();
            pronosticoDiv.Visible = true;
        }
      else { }
        
    }
}