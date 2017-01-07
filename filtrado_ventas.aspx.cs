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

public partial class filtrado_ventas : System.Web.UI.Page
{
    Funciones consulta = new Funciones();
    medina enchufe = new medina();
    public int j,i,n_ventas, n_requisiciones, n_productos_lista_requisicion, contador_ventas, n_ordenes_compra, n_listas_orden,n_pedidos,n_lista_pedidos;
    public int[] cantidad_lista_requisicion, cantidad_lista_venta, cantidad_backorder_lista_venta, cantidad_trasferencia,id_sucursal_requiere,id_sucursal_trasfer,
                 cantidad_surtida_pedido, cantidad_backorder_lista_pedido, cantidad_lista_pedido,
                 numero_factura,
                 cantidad_orden_compra_lista;

    public string[] clientes, clave_venta, clave_cotizacion,clave_pedido, nombre_vendedor, sucursal, estado_venta,
                    clave_cotizacion_en_requisicion, clave_requisicion,estado_requisicion,
                    clave_requisicion_lista,codigo_proveedor_requisiscion,producto_requisicion,aprobacion_requisicion,proveedor_lista_requisicion,
                    clave_venta_lista,codigo_proveedor_lista_venta,proveedor_lista_venta,
                    clave_cotizacion_trasferencia,clave_venta_trasferencia,codigo_trasferencia,
                    clave_cotizacion_ordenes_compra,clave_orden_compra,estado_orden_compra,listo,almacen,
                    clave_orden_lista,codigo_lista_orden,aprobacion_listas_orden,producto_lista_orden,
                    clave_cotizacion_pedido,estado_pedido,
                    clave_pedido_lista,codigo_lista_pedido
                    
                    ;

    public double[] iva, total, comision_vendedor, comision_negocio, subtotal,
                    iva_requisicion,subtotal_requisicion,total_requisicion,
                    precio_lista_requisicion, precio_total_lista_requisicion, precio_lista_venta;

    public DateTime[] fecha_venta, fecha_cotizacion, fecha_pedido, fecha_requisicion,fecha_orden_compra,
                      fecha_factura;
    public DateTime fecha_inicio, fecha_fin;
    public string clave_cotizacion_tabla, clave_pedido_tabla, clave_venta_tabla, clave_orden_compra_tabla, clave_requisicion_tabla;

    public void numero_registros_ventas()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select count(*) from ventas";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_ventas = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clientes = new string[n_ventas];
            clave_cotizacion = new string[n_ventas];
            clave_pedido = new string[n_ventas];
            clave_venta = new string[n_ventas];
            iva = new double[n_ventas];
            subtotal = new double[n_ventas];
            total = new double[n_ventas];
            nombre_vendedor = new string[n_ventas];
            sucursal = new string[n_ventas];
            comision_vendedor = new double[n_ventas];
            comision_negocio = new double[n_ventas];
            estado_venta = new string[n_ventas];
            fecha_cotizacion = new DateTime[n_ventas];
            fecha_pedido = new DateTime[n_ventas];
            fecha_venta = new DateTime[n_ventas];
        }
        catch (System.Exception ex)
        {
        }
    }

    public void generar_n_requisiciones()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select count(*) from requisiciones where clave_cotizacion = '"+clave_cotizacion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_requisiciones = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clave_cotizacion_en_requisicion = new string[n_requisiciones];
            clave_requisicion = new string[n_requisiciones];
            subtotal_requisicion = new double[n_requisiciones];
            iva_requisicion = new double[n_requisiciones];
            total_requisicion = new double[n_requisiciones];
            estado_requisicion = new string[n_requisiciones];
        }
        catch { }


    }

    public void generar_n_ordenes_compra()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            n_ordenes_compra = 0;
            objeto.cadena_comando_mysql = "select count(*) from ordenes_compra where clave_cotizacion = '"+clave_cotizacion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_ordenes_compra = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clave_cotizacion_ordenes_compra = new string[n_ordenes_compra];
            clave_orden_compra = new string[n_ordenes_compra];
            estado_orden_compra = new string[n_ordenes_compra];
            listo = new string[n_ordenes_compra];
            fecha_orden_compra = new DateTime[n_ordenes_compra];
            almacen = new string[n_ordenes_compra];

        }
        catch 
        {

        }
        
    }

    public void generar_n_pedidos()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select count(*) from pedidos where clave_cotizacion='"+clave_cotizacion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_pedidos = Convert.ToInt32(registro[0]);
               
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clave_cotizacion_pedido =  new string[n_pedidos];
            estado_pedido = new string[n_pedidos];
        }
        catch
        {
 
        }
    }

    public void generar_n_productos_lista_requisicion()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select count(*) from lista_productos_requisicion where clave_requisicion = '"+clave_requisicion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_lista_requisicion = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();

            clave_requisicion_lista = new string[n_productos_lista_requisicion];
            codigo_proveedor_requisiscion = new string[n_productos_lista_requisicion];
            producto_requisicion = new string[n_productos_lista_requisicion];
            aprobacion_requisicion = new string[n_productos_lista_requisicion];
            cantidad_lista_requisicion = new int[n_productos_lista_requisicion];
            precio_lista_requisicion = new double[n_productos_lista_requisicion];
            precio_total_lista_requisicion = new double[n_productos_lista_requisicion];
            proveedor_lista_requisicion = new string[n_productos_lista_requisicion];
        }
        catch { }
    }

    public void generar_n_productos_lista_venta()
    { 
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            n_productos_lista_requisicion = 0;
            objeto.cadena_comando_mysql = "select count(*) from lista_productos_venta where clave_venta = '"+clave_venta_tabla+"' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_lista_requisicion = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clave_venta_lista = new string[n_productos_lista_requisicion];
            codigo_proveedor_lista_venta = new string[n_productos_lista_requisicion];
            proveedor_lista_venta = new string[n_productos_lista_requisicion];
            cantidad_backorder_lista_venta = new int[n_productos_lista_requisicion];
            cantidad_lista_venta = new int[n_productos_lista_requisicion];
            precio_lista_venta = new double[n_productos_lista_requisicion];
        }
        catch { }
    }

    public void generar_n_productos_lista_trasferencia()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            n_productos_lista_requisicion = 0;
            objeto.cadena_comando_mysql = "select count(*) from peticiones_transferencia_producto where clave_pedido='" + clave_pedido_tabla + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_productos_lista_requisicion = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            clave_venta_trasferencia = new string[n_productos_lista_requisicion];
            codigo_trasferencia = new string[n_productos_lista_requisicion];
            clave_cotizacion_trasferencia = new string[n_productos_lista_requisicion];
            cantidad_trasferencia = new int[n_productos_lista_requisicion];
            id_sucursal_requiere = new int[n_productos_lista_requisicion];
            id_sucursal_trasfer = new int[n_productos_lista_requisicion];
            
            
        }
        catch { }
    }

    public void generar_n_lista_productos_ordenes()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            n_listas_orden = 0;
            objeto.cadena_comando_mysql = "select count(*) from lista_productos_orden_compra where clave_orden_compra = '" + clave_orden_compra_tabla + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_listas_orden = Convert.ToInt32(registro[0]);

            } 
            objeto.cerrar_conexion_mysql_extraccion();
            codigo_lista_orden = new string[n_listas_orden];
            cantidad_orden_compra_lista = new int[n_listas_orden];
            producto_lista_orden = new string[n_listas_orden];
            aprobacion_listas_orden = new string[n_listas_orden];
            
        }
        catch 
        {

        }
        
    }

    public void generar_n_lista_productos_pedido()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select count(*) from lista_productos_pedido where clave_pedido = '"+clave_pedido_tabla+"' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_lista_pedidos = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            codigo_lista_pedido = new string[n_lista_pedidos];
            clave_pedido_lista = new string[n_lista_pedidos];
            cantidad_lista_pedido = new Int32[n_lista_pedidos];
            cantidad_backorder_lista_pedido = new Int32[n_lista_pedidos];
            cantidad_surtida_pedido = new Int32[n_lista_pedidos];
        }
        catch
        {
 
        }
    }

    public void generar_n_facturas()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            n_lista_pedidos = 0;
            objeto.cadena_comando_mysql = "select count(*) from facturas where clave_cotizacion = '"+clave_cotizacion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                n_lista_pedidos = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
            fecha_factura = new DateTime[n_lista_pedidos];
            numero_factura = new int[n_lista_pedidos];

        }
        catch
        {
 
        }
    }

    public void extraer_ventas()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select sucursales.nombre as cliente,ventas.clave_cotizacion as clave_cotizacion,ventas.clave_pedido as clave_pedido,ventas.clave_venta,ventas.iva,ventas.subtotal,ventas.total,vendedores.nombre as Nombre_vendedor,sucursales_sistema.nombre as Sucursal,ventas.comision_total_vendedor,ventas.comision_total_negocio,ventas.estado as Estado_venta,cotizaciones.fecha as fecha_cotizacion,pedidos.fecha as fecha_pedido,ventas.fecha as fecha_venta from ventas inner join cotizaciones on cotizaciones.clave_cotizacion = ventas.clave_cotizacion  inner join pedidos on pedidos.clave_pedido = ventas.clave_pedido inner join sucursales on sucursales.id_sucursal = ventas.id_sucursal inner join sucursales_sistema on sucursales_sistema.id_sucursal_sistema = ventas.id_sucursal_sistema inner join vendedores on vendedores.id_vendedor = ventas.id_vendedor order by fecha_venta desc limit 100";
            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[15];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                registro[5] = objeto.leer_comando.GetString(5);
                registro[6] = objeto.leer_comando.GetString(6);
                registro[7] = objeto.leer_comando.GetString(7);
                registro[8] = objeto.leer_comando.GetString(8);
                registro[9] = objeto.leer_comando.GetString(9);
                registro[10] = objeto.leer_comando.GetString(10);
                registro[11] = objeto.leer_comando.GetString(11);
                registro[12] = objeto.leer_comando.GetString(12);
                registro[13] = objeto.leer_comando.GetString(13);
                registro[14] = objeto.leer_comando.GetString(14);

               
                clientes[contador_ventas] = registro[0];
                clave_cotizacion[contador_ventas] = registro[1];
                clave_pedido[contador_ventas] = registro[2];
                clave_venta[contador_ventas] = registro[3];
                iva[contador_ventas] = Convert.ToDouble(registro[4]);
                subtotal[contador_ventas] = Convert.ToDouble(registro[5]);
                total[contador_ventas] = Convert.ToDouble(registro[6]);
                nombre_vendedor[contador_ventas] = registro[7];
                sucursal[contador_ventas] = registro[8];
                comision_vendedor[contador_ventas] = Convert.ToDouble(registro[9]);
                comision_negocio[contador_ventas] = Convert.ToDouble(registro[10]);
                estado_venta[contador_ventas] = registro[11];
                fecha_cotizacion[contador_ventas] = Convert.ToDateTime(registro[12]);
                fecha_pedido[contador_ventas] = Convert.ToDateTime(registro[13]);
                fecha_venta[contador_ventas] = Convert.ToDateTime(registro[14]);

                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void extraer_requisisciones()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            contador_ventas = 0;
            objeto.cadena_comando_mysql = "select clave_requisicion,subtotal,iva,total,estado from requisiciones where clave_cotizacion = '"+clave_cotizacion_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[5];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
   
                clave_requisicion[contador_ventas] = registro[0];
                subtotal_requisicion[contador_ventas] = Convert.ToDouble(registro[1]);
                iva_requisicion[contador_ventas] = Convert.ToDouble(registro[2]);
                total_requisicion[contador_ventas] = Convert.ToDouble(registro[3]);
                estado_requisicion[contador_ventas] = registro[4];
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch
        {
 
        }
    }

    public void extraer_lista_productos_venta()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select clave_venta,codigo,proveedor,cantidad_backorder,cantidad,precio_total from lista_productos_venta  where clave_venta = '"+clave_venta_tabla+"'";
            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[6];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                registro[5] = objeto.leer_comando.GetString(5);

                clave_venta_lista[contador_ventas] = registro[0];
                codigo_proveedor_lista_venta[contador_ventas] = registro[1];
                proveedor_lista_venta[contador_ventas] = registro[2];
                cantidad_backorder_lista_venta[contador_ventas] = Convert.ToInt32(registro[3]);
                cantidad_lista_venta[contador_ventas] = Convert.ToInt32(registro[4]);
                precio_lista_venta[contador_ventas] = Convert.ToDouble(registro[5]);
                contador_ventas++;
            }

            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {

        }
    }

    public void extraer_lista_productos_requisicion()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select codigo,producto,aprobacion,cantidad,precio,precio_total,proveedor from lista_productos_requisicion  inner join requisiciones  on requisiciones.clave_requisicion = lista_productos_requisicion.clave_requisicion  where lista_productos_requisicion.clave_requisicion = '"+clave_requisicion_tabla+"' ";
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
                
               

                
                codigo_proveedor_requisiscion[contador_ventas] = registro[0];
                producto_requisicion[contador_ventas] = registro[1];
                aprobacion_requisicion [contador_ventas]= registro[2];
                cantidad_lista_requisicion[contador_ventas] = Convert.ToInt32(registro[3]);
                precio_lista_requisicion[contador_ventas] = Convert.ToDouble(registro[4]);
                precio_total_lista_requisicion[contador_ventas] = Convert.ToDouble(registro[5]);
                proveedor_lista_requisicion[contador_ventas] = registro[6];
                contador_ventas++;
            }

            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {

        }
    }

    public void extraer_lista_productos_trasferencia()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select cotizaciones.clave_cotizacion,peticiones_transferencia_producto.id_sucursal_sistema,peticiones_transferencia_producto.id_sucursal_sistema_requiere,peticiones_transferencia_producto.codigo_proveedor,peticiones_transferencia_producto.cantidad from peticiones_transferencia_producto inner join ventas on ventas.clave_pedido = peticiones_transferencia_producto.clave_pedido inner join cotizaciones on cotizaciones.clave_cotizacion = ventas.clave_cotizacion where cotizaciones.clave_cotizacion = '" + clave_cotizacion_tabla + "'";
            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[5];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                

                clave_cotizacion_trasferencia[contador_ventas] = registro[0];
                id_sucursal_trasfer[contador_ventas] = Convert.ToInt32(registro[1]);
                id_sucursal_requiere[contador_ventas] = Convert.ToInt32(registro[2]);
                codigo_trasferencia[contador_ventas] = registro[3];               
                cantidad_trasferencia[contador_ventas] = Convert.ToInt32(registro[4]);
                contador_ventas++;
            }

            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {

        }
    }

    public void extraer_ordenes_compra()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            contador_ventas = 0;
            objeto.cadena_comando_mysql = "select clave_cotizacion,clave_orden_compra,estado,fecha,listo,almacen  from ordenes_compra where clave_cotizacion = '"+clave_cotizacion_tabla+"' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[6];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                registro[5] = objeto.leer_comando.GetString(5);

                clave_cotizacion_ordenes_compra[contador_ventas] = registro[0];
                clave_orden_compra[contador_ventas] = registro[1];
                estado_orden_compra[contador_ventas] = registro[2];
                fecha_orden_compra[contador_ventas] = Convert.ToDateTime(registro[3]);
                listo[contador_ventas] = registro[4];
                almacen[contador_ventas] = registro[5];
                contador_ventas++;

            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch 
        {
 
        }
    }

    public void extraer_productos_lista_ordenes_compra()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            contador_ventas = 0;
            objeto.cadena_comando_mysql = "select codigo,cantidad,producto,aprobacion from lista_productos_orden_compra where clave_orden_compra = '" + clave_orden_compra_tabla +"'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                
                codigo_lista_orden[contador_ventas] = registro[0];
                cantidad_orden_compra_lista[contador_ventas] = Convert.ToInt32(registro[1]);
                producto_lista_orden[contador_ventas] = registro[2];
                aprobacion_listas_orden[contador_ventas] = registro[3];
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch 
        { }
    }

    public void extraer_pedidos()
    {
        try 
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "SELECT clave_cotizacion,estado FROM pedidos where clave_cotizacion ='"+ clave_cotizacion_tabla +"' ";
            objeto.aplicar_comando_mysql_extraccion();
            contador_ventas = 0;
            while (objeto.leer_comando.Read())
            {
                registro = new string[2];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                clave_cotizacion_pedido[contador_ventas] = registro[0];
                estado_pedido[contador_ventas] = registro[1];
                contador_ventas++;

            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch { }
    }

    public void extraer_listas_pedidos()
    {
        try
        {
            Conexion objeto = new Conexion();
            string[] registro;
            contador_ventas = 0;
            objeto.abrir_conexion_mysql();
            objeto.cadena_comando_mysql = "SELECT clave_pedido,codigo,cantidad,cantidad_surtida,cantidad_backorder FROM lista_productos_pedido where clave_pedido = '"+ clave_pedido_tabla +"' limit 20";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[5];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[3] = objeto.leer_comando.GetString(3);
                registro[4] = objeto.leer_comando.GetString(4);
                clave_pedido_lista[contador_ventas] = registro[0];
                codigo_lista_pedido[contador_ventas] = registro[1];
                cantidad_lista_pedido[contador_ventas] = Convert.ToInt32(registro[2]);
                cantidad_surtida_pedido[contador_ventas] = Convert.ToInt32(registro[3]);
                cantidad_backorder_lista_pedido[contador_ventas] = Convert.ToInt32(registro[4]);
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch 
        {
            
        }
    }

    public void extraer_facturas()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            contador_ventas = 0;
            objeto.cadena_comando_mysql = "select factura, fecha from facturas where clave_cotizacion = '"+clave_cotizacion_tabla+"' ";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[2];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                numero_factura[contador_ventas] = Convert.ToInt32(registro[0]);
                fecha_factura[contador_ventas] = Convert.ToDateTime(registro[1]);
                contador_ventas++;
            }
            objeto.cerrar_conexion_mysql_extraccion();

        }
        catch 
        {

        }
    }

    public DataTable pedidos_estado()
    {
        DataTable tabla_pedidos = new DataTable();
        //tabla_pedidos.Columns.Add(new DataColumn("estado", typeof(string)));
        tabla_pedidos.Columns.Add(new DataColumn("codigo_lista_pedido", typeof(string)));
        tabla_pedidos.Columns.Add(new DataColumn("cantidad_lista_pedido", typeof(Int32)));
        tabla_pedidos.Columns.Add(new DataColumn("cantidad_surtida_lista_pedido", typeof(Int32)));
        tabla_pedidos.Columns.Add(new DataColumn("cantidad_backorder_lista_pedido", typeof(Int32)));
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_pedidos.NewRow();
                if (codigo_lista_pedido[j] != null)
                {
                    //fila["estado"] = estado_pedido[enchufe.enchufado];
                    fila["codigo_lista_pedido"] = codigo_lista_pedido[j];
                    fila["cantidad_lista_pedido"] = cantidad_lista_pedido[j];
                    fila["cantidad_surtida_lista_pedido"] = cantidad_surtida_pedido[j];
                    fila["cantidad_backorder_lista_pedido"] = cantidad_backorder_lista_pedido[j];
                    tabla_pedidos.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }
        enchufe.enchufado = 0;
        return tabla_pedidos;   
    }

    public DataTable ventas()
    {
        DataTable tabla_ventas = new DataTable();
        
        tabla_ventas.Columns.Add(new DataColumn("cliente", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("clave_cotizacion", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("clave_pedido", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("clave_venta", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("iva", typeof(double)));
        tabla_ventas.Columns.Add(new DataColumn("subtotal", typeof(double)));
        tabla_ventas.Columns.Add(new DataColumn("total", typeof(double)));
        tabla_ventas.Columns.Add(new DataColumn("vendedor", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("sucursal", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("comision_negocio", typeof(double)));
        tabla_ventas.Columns.Add(new DataColumn("comision_vendedor", typeof(double)));
        tabla_ventas.Columns.Add(new DataColumn("estado_venta", typeof(string)));
        tabla_ventas.Columns.Add(new DataColumn("fecha_cotizacion", typeof(DateTime)));
        tabla_ventas.Columns.Add(new DataColumn("fecha_pedido", typeof(DateTime)));
        tabla_ventas.Columns.Add(new DataColumn("fecha_venta", typeof(DateTime)));
        
        if (contador_ventas > 0)
        {
            i = 0;
            do
            {
                DataRow fila = tabla_ventas.NewRow();
                if (clientes[i] != null)
                {
                    fila["cliente"] = clientes[i];
                    fila["clave_cotizacion"] = clave_cotizacion[i];
                    fila["clave_pedido"] = clave_pedido[i];
                    fila["clave_venta"] = clave_venta[i];
                    fila["iva"] = iva[i];
                    fila["subtotal"] = subtotal[i];
                    fila["total"] = total[i];
                    fila["vendedor"] = nombre_vendedor[i];
                    fila["sucursal"] = sucursal[i];
                    fila["comision_negocio"] = comision_negocio[i];
                    fila["comision_vendedor"] = comision_vendedor[i];
                    fila["estado_venta"] = estado_venta[i];
                    fila["fecha_cotizacion"] = fecha_cotizacion[i];
                    fila["fecha_pedido"] = fecha_pedido[i];
                    fila["fecha_venta"] = fecha_venta[i];
                    tabla_ventas.Rows.Add(fila);
                    i++;
                }
                else
                {

                }
            }
            while (i < contador_ventas);
        }
        enchufe.enchufado = 0;
        return tabla_ventas;
    }

    public DataTable requisiciones()
    { 
        DataTable tabla_requisiciones = new DataTable();
        tabla_requisiciones.Columns.Add(new DataColumn("clave_requisicion", typeof(string)));
        tabla_requisiciones.Columns.Add(new DataColumn("subtotal_requisicion", typeof(double)));
        tabla_requisiciones.Columns.Add(new DataColumn("iva_requisicion", typeof(double)));
        tabla_requisiciones.Columns.Add(new DataColumn("total_requisicion", typeof(double)));
        tabla_requisiciones.Columns.Add(new DataColumn("estado_requisicion", typeof(string)));
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_requisiciones.NewRow();
                if (clave_requisicion[j] != null)
                {

                    fila["clave_requisicion"] = clave_requisicion[j];
                    fila["subtotal_requisicion"] = subtotal_requisicion[j];
                    fila["iva_requisicion"] = iva_requisicion[j];
                    fila["total_requisicion"] = total_requisicion[j];
                    fila["estado_requisicion"] = estado_requisicion[j];
                    tabla_requisiciones.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }

        return tabla_requisiciones;
    }

    public DataTable lista_productos_venta()
    {
        DataTable tabla_ventas_lista = new DataTable();

        tabla_ventas_lista.Columns.Add(new DataColumn("clave_venta", typeof(string)));
        tabla_ventas_lista.Columns.Add(new DataColumn("codigo_proveedor_lista_venta", typeof(string)));
        tabla_ventas_lista.Columns.Add(new DataColumn("proveedor_lista_venta", typeof(string)));
        tabla_ventas_lista.Columns.Add(new DataColumn("cantidad_backorder_lista_venta", typeof(Int32)));
        tabla_ventas_lista.Columns.Add(new DataColumn("cantidad_lista_venta", typeof(Int32)));
        tabla_ventas_lista.Columns.Add(new DataColumn("precio_lista_venta", typeof(double)));
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_ventas_lista.NewRow();
                if (codigo_proveedor_lista_venta[j] != null)
                {
                    fila["clave_venta"] = clave_venta_lista[j];
                    fila["codigo_proveedor_lista_venta"] = codigo_proveedor_lista_venta[j];
                    fila["proveedor_lista_venta"] = proveedor_lista_venta[j];
                    fila["cantidad_backorder_lista_venta"] = cantidad_backorder_lista_venta[j];
                    fila["cantidad_lista_venta"] = cantidad_lista_venta[j];
                    fila["precio_lista_venta"] = precio_lista_venta[j];
                    tabla_ventas_lista.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }
        
        return tabla_ventas_lista;   
    }

    public DataTable lista_productos_requisiscion()
    {
        DataTable tabla_requisiciones = new DataTable();
        //tabla_requisiciones.Columns.Add(new DataColumn("clave_requisicion", typeof(string)));
        tabla_requisiciones.Columns.Add(new DataColumn("codigo_proveedor_requisicion", typeof(string)));
        tabla_requisiciones.Columns.Add(new DataColumn("producto_requisicion", typeof(string)));
        tabla_requisiciones.Columns.Add(new DataColumn("aprobacion_requisicion", typeof(string)));
        tabla_requisiciones.Columns.Add(new DataColumn("cantidad_requisicion", typeof(int)));
        tabla_requisiciones.Columns.Add(new DataColumn("precio_requisicion", typeof(double)));
        tabla_requisiciones.Columns.Add(new DataColumn("precio_total_requisicon", typeof(double)));
        tabla_requisiciones.Columns.Add(new DataColumn("proveedor_requisicion", typeof(string)));
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_requisiciones.NewRow();
                if (codigo_proveedor_requisiscion[j] != null)
                {

                    //fila["clave_requisicion"] = clave_requisicion_lista[j];
                    fila["codigo_proveedor_requisicion"] = codigo_proveedor_requisiscion[j];
                    fila["producto_requisicion"] = producto_requisicion[j];
                    fila["aprobacion_requisicion"] = aprobacion_requisicion[j];
                    fila["cantidad_requisicion"] = cantidad_lista_requisicion[j];
                    fila["precio_requisicion"] = precio_lista_requisicion[j];
                    fila["precio_total_requisicon"] = precio_total_lista_requisicion[j];
                    fila["proveedor_requisicion"] = proveedor_lista_requisicion[j];
                    tabla_requisiciones.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }

        return tabla_requisiciones;
    }

    public DataTable lista_productos_trasferencia()
    {
        DataTable tabla_trasferencia = new DataTable();
        tabla_trasferencia.Columns.Add(new DataColumn("id_sucursal_transfer", typeof(int)));
        tabla_trasferencia.Columns.Add(new DataColumn("id_sucursal_trasfer_requiere", typeof(int)));
        tabla_trasferencia.Columns.Add(new DataColumn("codigo_trasfer", typeof(string)));
        tabla_trasferencia.Columns.Add(new DataColumn("cantidad_trasfer", typeof(int)));
        
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_trasferencia.NewRow();
                if (id_sucursal_trasfer[j] != null)
                {

                    fila["id_sucursal_transfer"] = id_sucursal_trasfer[j];
                    fila["id_sucursal_trasfer_requiere"] = id_sucursal_requiere[j];
                    fila["codigo_trasfer"] = codigo_trasferencia[j];
                    fila["cantidad_trasfer"] = cantidad_trasferencia[j];
                    
                    tabla_trasferencia.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }

        return tabla_trasferencia;
    }

    public DataTable ordenes_compra ()
    {
        DataTable tabla_ordenes_compra = new DataTable();
        tabla_ordenes_compra.Columns.Add(new DataColumn("clave_orden_compra", typeof(string)));
        tabla_ordenes_compra.Columns.Add(new DataColumn("estado_orden_compra", typeof(string)));
        tabla_ordenes_compra.Columns.Add(new DataColumn("fecha_orden_compra", typeof(DateTime)));
        tabla_ordenes_compra.Columns.Add(new DataColumn("listo", typeof(string)));
        tabla_ordenes_compra.Columns.Add(new DataColumn("almacen", typeof(string)));

        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_ordenes_compra.NewRow();
                if (clave_orden_compra[j] != null)
                {

                    fila["clave_orden_compra"] = clave_orden_compra[j];
                    fila["estado_orden_compra"] = estado_orden_compra[j];
                    fila["fecha_orden_compra"] = fecha_orden_compra[j];
                    fila["listo"] = listo[j];
                    fila["almacen"] = almacen[j];

                    tabla_ordenes_compra.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }
        return tabla_ordenes_compra;

    }

    public DataTable facturas()
    {
        DataTable tabla_facturas = new DataTable();
        tabla_facturas.Columns.Add(new DataColumn("factura", typeof(string)));
        tabla_facturas.Columns.Add(new DataColumn("fecha_factura", typeof(DateTime)));
  
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_facturas.NewRow();
                if (numero_factura[j] != null)
                {

                    fila["factura"] = numero_factura[j];
                    fila["fecha_factura"] = fecha_factura[j];
                   

                    tabla_facturas.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }
        return tabla_facturas;
    }

    public DataTable lista_ordenes_compra()
    {
        DataTable tabla_lista_ordenes_compra = new DataTable();
        tabla_lista_ordenes_compra.Columns.Add(new DataColumn("codigo", typeof(string)));
        tabla_lista_ordenes_compra.Columns.Add(new DataColumn("cantidad", typeof(Int32)));
        tabla_lista_ordenes_compra.Columns.Add(new DataColumn("producto", typeof(string)));
        tabla_lista_ordenes_compra.Columns.Add(new DataColumn("aprobacion", typeof(string)));
        
        if (contador_ventas > 0)
        {
            j = 0;
            do
            {
                DataRow fila = tabla_lista_ordenes_compra.NewRow();
                if (codigo_lista_orden[j] != null)
                {

                    fila["codigo"] = codigo_lista_orden[j];
                    fila["cantidad"] = cantidad_orden_compra_lista[j];
                    fila["producto"] = producto_lista_orden[j];
                    fila["aprobacion"] = aprobacion_listas_orden[j];
                    
                    tabla_lista_ordenes_compra.Rows.Add(fila);
                    j++;
                }
                else
                {

                }
            }
            while (j < contador_ventas);
        }
        return tabla_lista_ordenes_compra;
    }

  //****************************************** Exportar **************************************************//
    public override void VerifyRenderingInServerForm(Control control) { }

    protected void boton_exportar_Click(object sender, EventArgs e)
    {
        
        exportar_grilla_mamalona();
    }

    private void exportar_grilla_mamalona()
    {
       
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Reporte " + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        tabla_principal.GridLines = GridLines.Both;
        tabla_principal.HeaderStyle.Font.Bold = true;
        tabla_principal.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }  

   //******************************************  FIN **************************************************//

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["usuario_vendedor"] = "jmedina@inklaser.mx";
       
        
        if (Session["usuario_vendedor"] == null)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocar_funcion", "salir();", true);
        }
        if (!IsPostBack)
        {
            numero_registros_ventas();
            extraer_ventas();
            tabla_principal.DataBind();
            /*tabla_principal.UseAccessibleHeader = true;
            tabla_principal.HeaderRow.TableSection = TableRowSection.TableHeader;
            TableCellCollection cells = tabla_principal.HeaderRow.Cells;

            cells[9].Attributes.Add("data-priority", "2");
            cells[6].Attributes.Add("data-priority", "3");
            cells[7].Attributes.Add("data-priority", "4");
            cells[8].Attributes.Add("data-priority", "5");
            cells[11].Attributes.Add("data-priority", "6");
            cells[12].Attributes.Add("data-priority", "7");
            cells[13].Attributes.Add("data-priority", "8");
            cells[14].Attributes.Add("data-priority", "9");
            cells[15].Attributes.Add("data-priority", "2");
            cells[1].Attributes.Add("data-priority", "1");*/
            
           

        }
        else 
        {
           
        }
    }


    protected void boton_imprimir_Click(object sender, EventArgs e)
    {

    }

    protected void boton_salir_Click(object sender, EventArgs e)
    {

    }

    protected void llenado_tabla_principal(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            clave_cotizacion_tabla = tabla_principal.DataKeys[e.Row.RowIndex].Values["clave_cotizacion"].ToString();
            clave_pedido_tabla = tabla_principal.DataKeys[e.Row.RowIndex].Values["clave_pedido"].ToString();
            clave_venta_tabla = tabla_principal.DataKeys[e.Row.RowIndex].Values["clave_venta"].ToString();
            generar_n_lista_productos_pedido();
            extraer_listas_pedidos();
            GridView tabla_pedido = e.Row.FindControl("tabla_pedido") as GridView;
            tabla_pedido.DataSource = pedidos_estado();
            tabla_pedido.DataBind();
            //tabla_pedido.UseAccessibleHeader = true;
            //tabla_pedido.HeaderRow.TableSection = TableRowSection.TableHeader;
            //TableCellCollection fila = tabla_pedido.HeaderRow.Cells;
            //fila[0].Attributes.Add("data-breakpoints", "all");
            //fila[1].Attributes.Add("data-breakpoints", "all");
            //fila[2].Attributes.Add("data-breakpoints", "all");
            //fila[3].Attributes.Add("data-breakpoints", "all");
            

            generar_n_productos_lista_venta();
            extraer_lista_productos_venta();
            GridView tabla_ventas_lista = e.Row.FindControl("tabla_ventas_lista") as GridView; 
            tabla_ventas_lista.DataSource = lista_productos_venta();  
            tabla_ventas_lista.DataBind();
            

            generar_n_facturas();
            if (n_lista_pedidos != 0)
            {
                extraer_facturas();
                GridView tabla_facturas = e.Row.FindControl("tabla_facturas") as GridView;
                Panel panel_factura = e.Row.FindControl("panel_factura") as Panel;
                tabla_facturas.DataSource = facturas();
                tabla_facturas.DataBind();
                panel_factura.Visible = true;
            }

            generar_n_ordenes_compra();
            if (n_ordenes_compra != 0)
            {
                extraer_ordenes_compra();
                Panel panel_ordenes_compra = e.Row.FindControl("panel_ordenes_compra") as Panel;
                GridView tabla_ordenes_compra = e.Row.FindControl("tabla_ordenes_compra") as GridView;
                tabla_ordenes_compra.DataSource = ordenes_compra();
                tabla_ordenes_compra.DataBind();
                panel_ordenes_compra.Visible = true;
            }

            generar_n_productos_lista_trasferencia();
            if (n_productos_lista_requisicion != 0)
            {
                extraer_lista_productos_trasferencia();
                Panel panel_trasferencias = e.Row.FindControl("panel_trasferencias") as Panel;
                GridView tabla_trasferencias = e.Row.FindControl("tabla_trasferencias") as GridView;
                tabla_trasferencias.DataSource = lista_productos_trasferencia();
                tabla_trasferencias.DataBind();
                panel_trasferencias.Visible = true;
            }

            generar_n_requisiciones();
            if (n_requisiciones != 0)
            {
                extraer_requisisciones();
                Panel panel_requisiciones = e.Row.FindControl("panel_requisiciones") as Panel;
                GridView tabla_requisiciones = e.Row.FindControl("tabla_requisiciones") as GridView;
                tabla_requisiciones.DataSource = requisiciones();
                tabla_requisiciones.DataBind();
                panel_requisiciones.Visible = true;
            }         

        }
    }

    protected void llenado_tabla_requisiciones(object sender, GridViewRowEventArgs e)
    {
        GridView tabla_requisiciones = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            clave_requisicion_tabla = tabla_requisiciones.DataKeys[e.Row.RowIndex].Values["clave_requisicion"].ToString();
            generar_n_productos_lista_requisicion();
            extraer_lista_productos_requisicion();
            GridView tabla_lista_productos_requisiciones = e.Row.FindControl("tabla_lista_productos_requisiciones") as GridView;
            tabla_lista_productos_requisiciones.DataSource = lista_productos_requisiscion();
            tabla_lista_productos_requisiciones.DataBind();
        }
    }

    protected void llenado_tabla_ordenes_compra(object sender, GridViewRowEventArgs e)
    {
        
        GridView tabla_ordenes_compra = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            clave_orden_compra_tabla = tabla_ordenes_compra.DataKeys[e.Row.RowIndex].Values["clave_orden_compra"].ToString();
            generar_n_lista_productos_ordenes();
            extraer_productos_lista_ordenes_compra();
            GridView tabla_lista_productos_ordenes_compra = e.Row.FindControl("tabla_lista_productos_ordenes_compra") as GridView;
            tabla_lista_productos_ordenes_compra.DataSource = lista_ordenes_compra();
            tabla_lista_productos_ordenes_compra.DataBind();
            

        }
    }

   

    

}