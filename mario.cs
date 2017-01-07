using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class mario
{
    // para editar_usuario
    public string nombre_vendedor, usuario, password, telefono, comprobar_vendedor;
    public int id_vendedor, llave_usuario;

    // para inventario_activo_fijo
    public string clave_cotizacion, codigo_proveedor, producto, clave_requisicion;
    public int  cantidad;
 
    // para inventario_inspecciones
    public string departamento, grupo, clave_inspeccion;
    public int id_sucursal, id_sucursal_sistema;
    public DateTime fecha_inspeccion;

    // para prospectos
    public string nombre_prospecto, comentarios, telefono_prospecto;
    public int id_prospecto, configuracion_agenda, id_vendedor2;
    public DateTime fecha_llamada;
    
    public void actualizar_datos_usuario()
    {
        Conexion comando = new Conexion();
        comando.abrir_conexion_mysql();
        comando.cadena_comando_mysql = "update vendedores set nombre='" + nombre_vendedor + "',usuario='" + usuario + "',password='" + password + "',telefono='" + telefono + "' where id_vendedor='" + id_vendedor + "'";
        comando.aplicar_comando_mysql();
        comando.cerrar_conexion_mysql();
    }

    public void extraer_datos_usuario()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre,usuario,password,telefono from vendedores where id_vendedor='" + id_vendedor + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    registro[i] = objeto.leer_comando.GetString(i);
                }
                nombre_vendedor = registro[0];
                usuario = registro[1];
                password = registro[2];
                telefono = registro[3];
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void verificar_existencia_vendedor()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            comprobar_vendedor = null;
            objeto.cadena_comando_mysql = "select nombre from vendedores where usuario='" + usuario + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                comprobar_vendedor = registro[0];
            }
            if (comprobar_vendedor == null)
            {
                llave_usuario = 1;
            }
            if (comprobar_vendedor != null)
            {
                llave_usuario = 0;
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void extraer_id_vendedor()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select id_vendedor from vendedores where usuario='" + usuario + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                id_vendedor = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void extraer_pass_vendedor()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select password from vendedores where usuario='" + usuario + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                id_vendedor = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void verificar_tipo_sesion()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select departamento,grupo from vendedores where usuario='" + usuario + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[2];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                departamento = registro[0];
                grupo = registro[1];
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void extraer_id_sucursal_sistema_vendedor()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select id_sucursal_sistema from vendedores where id_vendedor='" + id_vendedor + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[1];
                registro[0] = objeto.leer_comando.GetString(0);
                id_sucursal_sistema = Convert.ToInt32(registro[0]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }

    public void agendar_llamada()
    {
        Conexion comando = new Conexion();
        comando.abrir_conexion_mysql();
        comando.cadena_comando_mysql = "update prospectos set status='AGENDADA',configuracion_agenda='" + configuracion_agenda + "',fecha_llamada='" + fecha_llamada.ToString("yyyy-MM-dd HH:mm:ss") + "',id_vendedor='" + id_vendedor + "'where id_prospecto='" + id_prospecto + "'";
        comando.aplicar_comando_mysql();
        comando.cerrar_conexion_mysql();
    }

    public void extraer_datos_prospecto()
    {
        try
        {
            Conexion objeto = new Conexion();
            objeto.abrir_conexion_mysql();
            string[] registro;
            objeto.cadena_comando_mysql = "select nombre,telefono,comentarios, id_vendedor from prospectos where id_prospecto='" + id_prospecto + "'";
            objeto.aplicar_comando_mysql_extraccion();
            while (objeto.leer_comando.Read())
            {
                registro = new string[4];
                registro[0] = objeto.leer_comando.GetString(0);
                registro[1] = objeto.leer_comando.GetString(1);
                registro[2] = objeto.leer_comando.GetString(2);
                registro[2] = objeto.leer_comando.GetString(3);
                nombre_prospecto = registro[0];
                telefono_prospecto = registro[1];
                comentarios = registro[2];
                id_vendedor2 = Convert.ToInt32(registro[3]);
            }
            objeto.cerrar_conexion_mysql_extraccion();
        }
        catch (System.Exception ex)
        {
        }
    }
}