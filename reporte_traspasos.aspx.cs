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

public partial class reporte_traspasos : System.Web.UI.Page
{
    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public string[] proveedor, claves_venta, clientes, codigos_proveedor, productos, claves_pedido, estados, fechas, vendedores, productos_medina;
    public int[] cantidad_surtida, ids_clientes, cantidades, sucursales, id_sucursal_sistema_requiere, compras_anuales = new int[12], compras_anuales_cuarta = new int[12], compras_anuales_cbtis = new int[12], compras_anuales_cortez = new int[12];
    public double[] subtotales, ivas, totales, comisiones, precios, precios_totales;
    public int n_ventas, contador_ventas, busqueda, i, n_productos_venta, contador_productos_venta;
    public string cliente, fecha , estado , producto , sucursal;

    public void nu_productos_venta()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT count(*) from lista_productos_pedido where clave_pedido='" + consulta.clave_pedido + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_venta = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            precios_totales = new double[n_productos_venta];
            productos_medina = new string [n_productos_venta];
            proveedor = new string[n_productos_venta];
            cantidad_surtida = new int[n_productos_venta];
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
            objeto.cadena_comando_mysql = "select count(*) from pedidos inner join lista_productos_pedido on pedidos.clave_pedido = lista_productos_pedido.clave_pedido inner join transferencias_producto on transferencias_producto.clave_pedido = pedidos.clave_pedido";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_ventas = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            claves_pedido = new string[n_ventas];
            estados = new string[n_ventas];
            vendedores = new string[n_ventas];
            fechas = new string[n_ventas];
            sucursales = new int[n_ventas];
            codigos_proveedor = new string[n_ventas];
            id_sucursal_sistema_requiere = new int[n_ventas];
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


    public void llenar_lista_vendedores()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre from vendedores inner join pedidos on pedidos.id_vendedor = vendedores.id_vendedor group by nombre";
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
            objeto.cadena_comando_mysql = "SELECT codigo_proveedor FROM peticiones_transferencia_producto group by codigo_proveedor";
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

    public void llenar_por_estado()
    {
        try
        {
            lista_fechas.Items.Clear();
            lista_fechas.Items.Add("Seleccionar");
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select pedidos.estado from pedidos inner join lista_productos_pedido on pedidos.clave_pedido = lista_productos_pedido.clave_pedido inner join transferencias_producto on transferencias_producto.clave_pedido = pedidos.clave_pedido group by (pedidos.estado)";
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
    public void lista_detalles_producto()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select lista_productos_pedido.precio_total,inventario.producto,lista_productos_pedido.proveedor,lista_productos_pedido.cantidad_surtida from lista_productos_pedido inner join inventario on inventario.codigo_proveedor = lista_productos_pedido.codigo where clave_pedido='" + consulta.clave_pedido + "'";
            objeto.aplicar_comando_mysql_extraccion();
            contador_productos_venta = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                precios_totales[contador_productos_venta] = Convert.ToDouble(registro[0]);
                productos_medina[contador_productos_venta] = registro[1];
                proveedor[contador_productos_venta] = registro[2];
                cantidad_surtida[contador_productos_venta] = Convert.ToInt16(registro[3]);
                contador_productos_venta++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
    private void extraer_ventas_mensuales_matriz()
    {
        enchufe.id_sucursal_sistema = 1;
        enchufe.extraer_trasferencias_matriz();
        compras_anuales = enchufe.compras_anuales;

    }

    private void extraer_ventas_mensuales_cortez()
    {
        enchufe.id_sucursal_sistema = 2;
        enchufe.extraer_trasferencias_cortez();
        compras_anuales_cortez = enchufe.compras_anuales2;
    }
    private void extraer_ventas_mensuales_cuarta()
    {
        enchufe.id_sucursal_sistema = 3;
        enchufe.extraer_trasferencias_cuarta();
        compras_anuales_cuarta = enchufe.compras_anuales3;
    }
    private void extraer_ventas_mensuales_cbtis()
    {
        enchufe.id_sucursal_sistema = 4;
        enchufe.extraer_trasferencias_cbtis();
        compras_anuales_cbtis = enchufe.compras_anuales4;

    }
    public void llenar_lista_nombres_productos()
    {
        try
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
                    lista_fechas.Items.Add(new CultureInfo("en-US", false).TextInfo.ToTitleCase(registro[0]));
                }
                objeto.cerrar_conexion_mysql_extraccion();
                
            }
            
        
        catch (System.Exception ex)
        {
        }
    }

    public void llenar_lista_por_sucursales()
    {
        try
        {
                lista_fechas.Items.Clear();
                lista_fechas.Items.Add("Seleccionar");
                Conexion objeto = new Conexion();
                objeto.abrir_conexion_mysql();
                string[] registro;
                objeto.cadena_comando_mysql = "select pedidos.id_sucursal_sistema from pedidos inner join lista_productos_pedido on pedidos.clave_pedido = lista_productos_pedido.clave_pedido inner join transferencias_producto on transferencias_producto.clave_pedido = pedidos.clave_pedido group by (pedidos.id_sucursal_sistema)";
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

    public void llenar_todo()
    {
        try
        {
            
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor order by p.fecha desc";
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
                claves_pedido[contador_ventas] = registro[0];
                sucursales[contador_ventas] = Convert.ToInt16(registro[1]);
                id_sucursal_sistema_requiere[contador_ventas] = Convert.ToInt16(registro[2]);
                fechas[contador_ventas] = Convert.ToString(registro[3]);
                codigos_proveedor[contador_ventas] = Convert.ToString(registro[4]);
                vendedores[contador_ventas] = registro[5];
                estados[contador_ventas] = registro[6];
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch (System.Exception ex)
        {
        }
    }
    public void llenar_productos()
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
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v	on v.id_vendedor = p.id_vendedor where p.estado = '" + estado + "'";
                }
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor where v.id_vendedor='" + consulta.id_vendedor_cotizacion + "'";/// servira???
                }
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor  where  date_format(p.fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "'";/// servira???
                }
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    objeto.cadena_comando_mysql = " select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor where  date_format(fecha,'%Y')='" + fecha + "'";/// servira???                                                          
                }
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor where id_sucursal = '" + consulta.id_sucursal + "'";
                }
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor where pt.codigo_proveedor = '" + producto + "'";
                }
                if (tipo_busqueda.SelectedIndex == 7)
                {
                    Session["SelectedIndex"] = tipo_busqueda.SelectedIndex;
                    objeto.cadena_comando_mysql = "select pt.clave_pedido,pt.id_sucursal_sistema,pt.id_sucursal_sistema_requiere,p.fecha,pt.codigo_proveedor,v.nombre,tp.estado from peticiones_transferencia_producto pt inner join pedidos p on p.clave_pedido = pt.clave_pedido inner join transferencias_producto tp on tp.clave_transferencia = pt.clave_transferencia inner join vendedores v on v.id_vendedor = p.id_vendedor order by p.fecha desc ";
                }
               
            }
            else {
                /// x cliente
                if (tipo_busqueda.SelectedIndex == 1)
                {
                    //objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor_cotizacion + "' and id_sucursal='" + consulta.id_sucursal + "'";
                }
                /// x vendedor
                if (tipo_busqueda.SelectedIndex == 2)
                {
                    //objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor= '" + consulta.id_vendedor + "' ";/// servira???
                }
                /// x producto
                if (tipo_busqueda.SelectedIndex == 3)
                {
                    //objeto.cadena_comando_mysql = "select lista_productos_venta.clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas inner join lista_productos_venta on ventas.clave_venta = lista_productos_venta.clave_venta where lista_productos_venta.codigo = '" + enchufe.codigo_producto + "' ";
                }
                /// x categoria
                if (tipo_busqueda.SelectedIndex == 4)
                {
                    //objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor + "' and id_sucursal='" + consulta.id_sucursal + "'";
                }
                /// x mes
                if (tipo_busqueda.SelectedIndex == 5)
                {
                    //objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor + "' and date_format(fecha,'%Y-%m')='" + consulta.fecha.ToString("yyyy-MM") + "'";
                }
                /// x año
                if (tipo_busqueda.SelectedIndex == 6)
                {
                    //objeto.cadena_comando_mysql = "select clave_venta,subtotal,iva,total,comision_total_vendedor,id_sucursal from ventas where id_vendedor='" + consulta.id_vendedor + "' and date_format(fecha,'%Y')='" + fecha + "'";
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
                claves_pedido[contador_ventas] = registro[0];
                sucursales[contador_ventas] = Convert.ToInt16(registro[1]);
                id_sucursal_sistema_requiere[contador_ventas] = Convert.ToInt16(registro[2]);
                fechas[contador_ventas] = Convert.ToString(registro[3]);
                codigos_proveedor[contador_ventas] = Convert.ToString(registro[4]);
                vendedores[contador_ventas] = registro[5];
                estados[contador_ventas] = registro[6];                 
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
    public DataTable detalles_producto()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("total", typeof(double)));
        tabla.Columns.Add(new DataColumn("backorder", typeof(string)));
        tabla.Columns.Add(new DataColumn("proveedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("surtido", typeof(int)));
              
        if (contador_productos_venta > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (precios_totales[i] != null)
                {
                    fila["total"] = precios_totales[i];
                    fila["backorder"] = productos_medina[i];
                    fila["proveedor"] = proveedor[i];
                    fila["surtido"] = cantidad_surtida[i];
                    tabla.Rows.Add(fila);
                    i++;
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
        tabla.Columns.Add(new DataColumn("clave_pedido", typeof(string)));
        tabla.Columns.Add(new DataColumn("estado", typeof(string)));
        tabla.Columns.Add(new DataColumn("vendedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema", typeof(int)));
        tabla.Columns.Add(new DataColumn("codigo", typeof(string)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema_requiere", typeof(int)));
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (claves_pedido[i] != null)
                {
                    fila["clave_pedido"] = claves_pedido[i];
                    fila["estado"] = estados[i];
                    fila["vendedor"] = vendedores[i];
                    fila["fecha"] = fechas[i];
                    fila["id_sucursal_sistema"] = sucursales[i];
                    fila["codigo"] = codigos_proveedor[i];
                    fila["id_sucursal_sistema_requiere"] = id_sucursal_sistema_requiere[i];
                    tabla.Rows.Add(fila);
                    i++;
                }
            }
            while (i < contador_ventas);
        }
        ds.Tables.Add(tabla);
        return ds;
    }
    
    public DataTable datos_productos()
    {
        DataTable tabla = new DataTable();
        tabla.Columns.Add(new DataColumn("clave_pedido", typeof(string)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema", typeof(int)));
        tabla.Columns.Add(new DataColumn("id_sucursal_sistema_requiere", typeof(int)));
        tabla.Columns.Add(new DataColumn("fecha", typeof(DateTime)));    
        tabla.Columns.Add(new DataColumn("codigo", typeof(string)));
        tabla.Columns.Add(new DataColumn("vendedor", typeof(string)));
        tabla.Columns.Add(new DataColumn("estado", typeof(string)));
        
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla.NewRow();
                if (claves_pedido[i] != null)
                {
                    fila["clave_pedido"] = claves_pedido[i];
                    fila["id_sucursal_sistema"] = sucursales[i];
                    fila["id_sucursal_sistema_requiere"] = id_sucursal_sistema_requiere[i];
                    fila["fecha"] = fechas[i];
                    fila["codigo"] = codigos_proveedor[i];                   
                    fila["vendedor"] = vendedores[i];
                    fila["estado"] = estados[i];     
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
        ;
        if (!IsPostBack)
        {
            boton_exportar.Visible = false;
            boton_imprimir.Visible = false;
            nu_ventas();
            llenar_todo();
            tabla_productos.DataBind();
            panel_datos.Visible = true;
            encabezado.Text = "Trasferencias Realizadas";
            extraer_ventas_mensuales_matriz();
            extraer_ventas_mensuales_cortez();
            extraer_ventas_mensuales_cuarta();
            extraer_ventas_mensuales_cbtis();
        }
        else
        {
            extraer_ventas_mensuales_matriz();
            extraer_ventas_mensuales_cortez();
            extraer_ventas_mensuales_cuarta();
            extraer_ventas_mensuales_cbtis();
        }
    }

    
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
                llenar_por_estado();
            }
            /// x vendedores
            Session["SelectedIndex"] = tipo_busqueda.SelectedIndex;
            if (tipo_busqueda.SelectedIndex == 2)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_vendedores();
            }
            /// x producto
            if (tipo_busqueda.SelectedIndex == 3)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_meses();
            }
            if (tipo_busqueda.SelectedIndex == 4)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_years();
            }
            /// x sucursal
            if (tipo_busqueda.SelectedIndex == 5)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_por_sucursales();
            }
            /// x producto
            if (tipo_busqueda.SelectedIndex == 6)
            {
                consulta.extraer_nombre_vendedor();
                consulta.extraer_grupo_vendedor();
                llenar_lista_productos();
            }
            if (tipo_busqueda.SelectedIndex == 7)
            {
                encabezado.Text = "Trasferencias Realizadas";
                lista_fechas.Items.Clear();
                nu_ventas();
                llenar_productos();
                tabla_productos.DataBind();
                panel_no_resultados_busqueda.Visible = false;
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
            ///ventas x estado
            if (tipo_busqueda.SelectedIndex == 1)
            {
                encabezado.Text = "Trasferencias En Estado " + lista_fechas.Text.ToLower();
                estado = lista_fechas.Text.ToLower();
                llenar_productos();

            }
            /// ventas x vendedor
            if (tipo_busqueda.SelectedIndex == 2)
            {
                encabezado.Text = "Trasferencias Generadas Por " + lista_fechas.Text.ToLower();
                consulta.nombre_vendedor = lista_fechas.Text.ToLower();
                consulta.extraer_id_vendedor_nombre();
                llenar_productos();

            }
            /// ventas x mes
            if (tipo_busqueda.SelectedIndex == 3)
            {
                encabezado.Text = "Trasferencias " + lista_fechas.Text;
                consulta.fecha = Convert.ToDateTime(lista_fechas.Text);
                llenar_productos();

            }
            /// ventas x año
            if (tipo_busqueda.SelectedIndex == 4)
            {
                encabezado.Text = "Trasferencias " + lista_fechas.Text;
                fecha = lista_fechas.Text;
                llenar_productos();

            }
            ///ventas x sucursal
            if (tipo_busqueda.SelectedIndex == 5)
            {
                encabezado.Text = "Trasferencias Por La Sucursal ID " + lista_fechas.Text;
                consulta.id_sucursal = Convert.ToInt16(lista_fechas.Text.ToLower());
                
                llenar_productos();

            }
            ///ventas x producto
            if (tipo_busqueda.SelectedIndex == 6)
            {
                encabezado.Text = "Trasferencias " + lista_fechas.Text;
                
                producto = lista_fechas.Text.ToLower();
                llenar_productos();
            }
            if (tipo_busqueda.SelectedIndex == 7)
            {

                
               
            }
        }
        tabla_productos.DataBind();
        tabla_productos.UseAccessibleHeader = true;
        tabla_productos.HeaderRow.TableSection = TableRowSection.TableHeader;
        TableCellCollection cells = tabla_productos.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-hide", "phone,tablet");
        cells[3].Attributes.Add("data-hide", "phone,tablet");
        cells[4].Attributes.Add("data-hide", "phone");
        cells[5].Attributes.Add("data-hide", "phone");
        cells[6].Attributes.Add("data-hide", "phone");
        
    
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
    protected void tabla_productos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      //  if (Session["usuario_vendedor"] != null)
   //     {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                consulta.clave_pedido = Convert.ToString(tabla_productos.DataKeys[index].Values["clave_pedido"]);
                tabla_productos.SelectedIndex = -1;
                panel_detalles_compra.Visible = true;
                nu_productos_venta();
                lista_detalles_producto();
                tabla_detalles_producto.DataBind();
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
        tipo_busqueda.SelectedIndex = Convert.ToInt16(Session["SelectedIndex"]);
        if (tipo_busqueda.SelectedIndex == 7)
        {

            nu_ventas();
            llenar_productos();

        }
        else
        {
            busqueda_reportes();
        }
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
}
