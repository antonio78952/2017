<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codigo/reportes_inventario.aspx.cs" Inherits="reportes_inventario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="js/Chart.js"></script>
<script src="http://code.jquery.com/jquery-latest.min.js"></script> 
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<script type="text/javascript" language="javascript">
    (function () {
        var mouse = false, keyboard = false;
        document.onmousemove = function () {
            mouse = true;
        };
        document.onkeydown = function () {
            keyboard = true;
        };
       
        setInterval(function () {
            if (!mouse && !keyboard) {
                //  var boton = window.opener.document.getElementById('actualizar');
                // boton.click();
            }
            else {
                mouse = false;
                keyboard = false;
                window.opener.document.getElementById('caja_contador').value = "0";
            }
        }, 1000);
    })()
    function abrir() {
        window.moveTo(0, 0);
        window.resizeTo(screen.availWidth, screen.availHeight);
    }
    onload = abrir();


    function salir() {

        window.close();
    }
    function graficar() {
        var compras_anuales = document.getElementById("grafica_compras_anuales").getContext("2d");
        window.compras_anuales_grafica = new Chart(compras_anuales).Line(datos_anuales, {
            responsive: true
        });
    }
    window.onload = function () {
        graficar();
    }


	</script>
    <script type="text/javascript">
        <%  var serializer =  new  System . Web . Script . Serialization . JavaScriptSerializer ();  %>   

       var compras_anuales =  <%= serializer . Serialize ( compras_anuales )  %>;
       var datos_anuales = {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio","Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        datasets: [
                				{
                				    label: "My Second dataset",
                				    fillColor: "rgba(87,11,175,0.5)",
                                    strokeColor: "rgba(22,6,40,1)",
                                    pointColor: "rgba(79,193,233,0.5)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "#fff",
                                    pointHighlightStroke: "rgba(220,220,220,1)",
                				    data: compras_anuales
                				}
			]
    }

   

   
 </script>

 <script type="text/javascript">
     $(function () {
         var $rows = $('#tabla_modificar tr[class!="cabecera_tabla"]');
         var $rows2 = $('#tabla_productos tr[class!="cabecera_tabla"]');
         var $rows3 = $('#ventas_productos tr[class!="cabecera_tabla"]');
         var $rows4 = $('#ventas_productos_pasado tr[class!="cabecera_tabla"]');
         var $rows5 = $('#tabla_ultima_fecha tr[class!="cabecera_tabla"]');
         var $rows6 = $('#tabla_inspeccion tr[class!="cabecera_tabla"]');
         $('#buscale').click(function () {
             var val = $.trim($("#buscar").val()).replace(/ +/g, ' ').toLowerCase();

             $rows.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
             $rows2.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
             $rows3.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
             $rows4.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
             $rows5.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
             $rows6.show().filter(function () {
                 var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                 return ! ~text.indexOf(val);
             }).hide();
         });
     });

     function cargar(boton) 
     {
         document.getElementById("<%= panel_cargando.ClientID %>").style.display = "inline";

         if (boton.toString() == "boton_modificar_minimo") 
         {
             setTimeout(function () 
             {
                 var boton = document.getElementById("boton_oculto_modificar_minimo");
                 boton.click();
             }, 500);
         }
         if (boton.toString() == "boton_modificar_maximo") 
         {
             setTimeout(function () 
             {
                 var boton = document.getElementById("boton_oculto_modificar_maximo");
                 boton.click();
             }, 500);
         }

     }

    </script>
    <script type="text/javascript">
        $(function () {
            $("#buscarfecha").datepicker({ dateFormat: 'dd/mm/yy', maxDate: "0D" }).val();
        });

        $(function () {
            
            var $rows = $('#tabla_ultima_fecha tr[class!="cabecera_tabla"]');
            var $rows2 = $('#tabla_inspeccion tr[class!="cabecera_tabla"]');
            $('#buscarfecha').change(function () {
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return ! ~text.indexOf(val);
                }).hide();

                $rows2.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return ! ~text.indexOf(val);
                }).hide();
            });
        });
  </script>
    <script type="text/javascript" >
        $(function () {
            $("#mensajes_sistema").dialog();
        });
        $(function menu() {

            $('#boton_menu').click(function () {
                $(window).scrollTop(0);
                $('#submenu').css('visibility', 'visible');
                $('#submenu').css('height', '300px');
                if (document.getElementById("submenu").style.visibility == "visible") {
                    setTimeout(function () {
                        $('#elementos').css('display', 'inline');
                        //  $('#main').css('display', 'none');
                    }, 200);
                }
                document.getElementById("<%= boton_menu_cerrar.ClientID %>").style.display = "inline";
                document.getElementById("<%= boton_menu.ClientID %>").style.display = "none";
                return false;
            });

            $('#boton_menu_cerrar').click(function () {
                $(window).scrollTop(0);
                $('#elementos').css('display', 'none');
                $('#submenu').css('height', '0px');
                $('#submenu').css('visibility', 'hidden');
                $('#main').css('display', 'inline-block');
                document.getElementById("<%= boton_menu.ClientID %>").style.display = "inline";
                document.getElementById("<%= boton_menu_cerrar.ClientID %>").style.display = "none";
                return false;
            });
        });
    </script>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Reporte Inventario - Inklaser</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/custom.css" />
    <link rel="stylesheet" href="css/dashboard.css" />
    <script src="js/Chart.js"></script>
    <script type="text/javascript" src="js/vendor/modernizr.js"></script>
    <script type="text/javascript" src="javascript/funciones.js"></script>

      <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,700,800' rel='stylesheet' type='text/css'>
      <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet">
      <link rel="stylesheet" href="css/material-design-font.css">
    </head>
    <body bgcolor="#f0f0f0">
    <form id="formulario" runat="server" >
    <div id="menu">
    <div id="menu_mobiles">
      <asp:LinkButton ID="boton_menu" runat="server"  ToolTip="Menu de opciones"><i class="fa fa-bars fa-2x"></i></asp:LinkButton>
      <asp:LinkButton ID="boton_menu_cerrar" runat="server"  ToolTip="Cerrar menu de opciones"><i class="fa fa-bars fa-2x"></i></asp:LinkButton>
      </div>
      <i class="fa fa-dashboard fa-2x blanco"></i><asp:Label ID="vendedor" runat="server" CssClass="nombre_vendedor">Inventario</asp:Label>
      <div class="botones_menu">
      <asp:LinkButton ID="boton_exportar" runat="server" CssClass="boton medina" ToolTip="Exportar" onclick="boton_exportar_Click" ><img id="d" class="icono" src="png/z.png"></asp:LinkButton>               
      <asp:LinkButton ID="boton_imprimir" runat="server" CssClass="boton medina" ToolTip="Imprimir" onclick="boton_imprimir_Click" ><img id="e" class="icono" src="png/e.png"></asp:LinkButton>               
      <asp:LinkButton ID="salir" runat="server" CssClass="boton medina" ToolTip="Salir" onclick="boton_salir_Click"><img id="f" class="icono" src="png/f.png"></asp:LinkButton>
      </div> 
      </div>  
      <div id="submenu">
      <div id="elementos">
                                    
         <asp:LinkButton CssClass="elemento_menu" ID="m_exportar" runat="server"  ToolTip="Exportar" onclick="boton_exportar_Click" Enabled="False"><p class="texto_m">Exportar</p></asp:LinkButton>                
         <asp:LinkButton CssClass="elemento_menu" ID="m_salir" runat="server"  ToolTip="Salir" onclick="boton_salir_Click"><p class="texto_m">Salir</p></asp:LinkButton>
      </div>
      </div>   
        
        
        <div class="main medina">                
         <div id="main_derecho">  
          <div class="paneles_graficas">
          <div class="controles_busqueda">
           <ul>
            <li class="cabecera_informacion"><p class="texto_cabecera">Filtrar</p></li>
            <li>
            <asp:DropDownList ID="tipo_busqueda" runat="server" CssClass="lista_categoria_medina " AutoPostBack="True" Width="150px" onselectedindexchanged="tipo_busqueda_SelectedIndexChanged">
            <asp:ListItem>Seleccionar</asp:ListItem>
                <asp:ListItem>Minimos</asp:ListItem>
                <asp:ListItem>Maximos</asp:ListItem>
                <asp:ListItem>Exedentes</asp:ListItem>
                <asp:ListItem>Productos surtidos automaticamente</asp:ListItem>
                <asp:ListItem>Cantidad De Productos Vendidos x Mes</asp:ListItem>
                <asp:ListItem>Cantidades En Inventario</asp:ListItem>
                <asp:ListItem>Modificar Maximos Minimos</asp:ListItem>
                <asp:ListItem>Ultima Fecha De Venta X Producto</asp:ListItem>
                <asp:ListItem>Inspeccion Inventario</asp:ListItem>
             </asp:DropDownList>
            </li>
            <li class="cabecera_informacion"><p class="texto_cabecera">Opciones</p></li>
            <li>
            <asp:DropDownList ID="lista_fechas" runat="server" CssClass="lista_categoria_medina"  AutoPostBack="True" Width="200px" onselectedindexchanged="lista_fechas_SelectedIndexChanged">
            </asp:DropDownList>
            </li>    
            </ul> 
            </div> 
            <div class="espacio"></div>
          </div>
        </div>

        <div id="main_izquierdo"> 
            <div class="paneles_graficas">
            <asp:Panel ID="panelgrafica" runat="server">       
                <div class="cabecera_informacion"><p class="texto_cabecera">Top Ventas</p></div>
                <div class="grafica puntos">
                <canvas id="grafica_compras_anuales" class="graficas"></canvas>
                </div>
            </asp:Panel>
            <div class="controles_busqueda">
            <asp:Panel ID="panelbuscar" runat="server" Visible = "false">
            <div class="cabecera_informacion"><p class="texto_cabecera">Buscar</p></div>
            <asp:TextBox ID="buscar" runat="server" CssClass="cajas_chicas" Enabled="true" ></asp:TextBox>
            <asp:Label ID="buscale" runat="server"  ToolTip="Buscar" Style="text-decoration: none" class="boton_buscar" >Buscar</asp:Label>
            <div class="espacio"></div>
            <asp:Label ID="Label8" runat="server" Text="Buscar Por Fecha"></asp:Label>
            <asp:TextBox type="text" id="buscarfecha" runat="server" Width="120px" class="cajas_chicas"></asp:TextBox>
            <asp:Label ID="mensajes_sistema" runat="server" Visible = "false" />
            </asp:Panel>
            </div>
            </div>
        </div> 

        <div class="espacio"></div>
        <div id="main_centro" runat = "server">
        <div class="paneles_graficas">
        <div class="controles_busqueda">
        <asp:Panel ID="Panel_Completo" runat="server"  Visible="False">
        <div class="cabecera_informacion" id="panel3"  runat="server" ><p class="texto_cabecera" ><asp:Label ID="mensagito" runat="server"></asp:Label></p></div>
        <asp:Panel ID="panel_datos" class="datos_ventas" runat="server" >
        <asp:GridView ID="tabla_productos"  runat="server" AutoGenerateColumns="False" DataSource='<%# datos_productos() %>'  CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_productos_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
        <HeaderStyle CssClass="cabecera_tabla"/>
		<PagerSettings Visible="False" />
		<AlternatingRowStyle CssClass="renglon_tabla" />
        <Columns>
           <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor">
           </asp:BoundField>
           <asp:BoundField HeaderText="Descripcion" DataField="productos" >
           </asp:BoundField>
           <asp:BoundField HeaderText="Marca" DataField="marca">
           </asp:BoundField>
           <asp:BoundField HeaderText="Costo" DataField="costos" >
           </asp:BoundField>
           <asp:BoundField HeaderText="Cantidad" DataField="cantidades" >
           </asp:BoundField>
           <asp:BoundField HeaderText="Minimo" DataField="minimo" >
           </asp:BoundField>
           <asp:BoundField HeaderText="Maximo" DataField="maximo" >
           </asp:BoundField>
           <asp:TemplateField HeaderText="" ShowHeader="True">
             <ItemTemplate>
             <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ToolTip="Ver detalles de la venta" CommandName="Ver" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"><i class="fa fa-eye control-tabla-verde"></i></asp:LinkButton>
             </ItemTemplate>
             <ItemStyle CssClass="control" />
           </asp:TemplateField>
        </Columns>          
        </asp:GridView>
        </asp:Panel>
        </asp:Panel>

        

            <asp:Panel ID="Panel_Ventas" runat="server"  Visible="False"> 
            <div class="cabecera_informacion"  style="margin-bottom:0px;"><p class="texto_cabecera"><asp:Label ID="cabecera_fecha" runat="server"></asp:Label></p></div>
            <asp:Panel ID="ventas_productos" class="datos_ventas" runat="server" style="margin-top:0px;" >
            <asp:GridView ID="ventas"  runat="server" AutoGenerateColumns="False" DataSource='<%# datos_ventas() %>'  CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_productos_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor">
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripcion" DataField="productos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Marca" DataField="marca">
                </asp:BoundField>
                 <asp:BoundField HeaderText="Costo" DataField="costos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio Total" DataField="total" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Cantidad Vendida" DataField="cantidades" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Minimo" DataField="minimo" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Maximo" DataField="maximo" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ShowHeader="True">
                <ItemStyle CssClass="control" />
                </asp:TemplateField>
            </Columns>          
            </asp:GridView>
            </asp:Panel>
            </asp:Panel>
      
      
         <asp:Panel ID="Panel_Ventas_Pasado" runat="server"  Visible="False"> 
         <div class="cabecera_informacion" style="margin-bottom:0px;"><p class="texto_cabecera" > <asp:Label ID="cabecera_fecha2" runat="server"></asp:Label></p></div>
         <asp:Panel ID="ventas_productos_pasado" class="datos_ventas" runat="server" style="margin:0px;" > 
            <asp:GridView ID="ventas_pasadas"  runat="server"  AutoGenerateColumns="False" DataSource='<%# datos_ventas() %>'   CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_productos_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor">
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripcion" DataField="productos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Marca" DataField="marca">
                </asp:BoundField>
                 <asp:BoundField HeaderText="Costo" DataField="costos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio Total" DataField="total" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Cantidad Vendida" DataField="cantidades" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Minimo" DataField="minimo" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Maximo" DataField="maximo" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ShowHeader="True">
                <ItemStyle CssClass="control" />
                </asp:TemplateField>
            </Columns>          
            </asp:GridView>
            </asp:Panel>
            </asp:Panel>



            <asp:Panel ID="panel_inspeccion" runat="server"  Visible="False"> 
            <div class="cabecera_informacion"  style="margin-bottom:0px;"><p class="texto_cabecera"><asp:Label ID="texto_inspeccion" runat="server"></asp:Label></p></div>
            <asp:Panel ID="inspeccion" class="datos_ventas" runat="server" style="margin-top:0px;" >
            <asp:GridView ID="tabla_inspeccion"  runat="server" AutoGenerateColumns="False" DataSource='<%# datos_inspeccion() %>'  CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_productos_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="ID Inspeccion" DataField="id_inspeccion">
                </asp:BoundField>
                <asp:BoundField HeaderText="Codigo Proveedor" DataField="codigo_proveedor" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Cantidad Inventario" DataField="cantidad_inventario">
                </asp:BoundField>
                 <asp:BoundField HeaderText="Cantidad Fisica" DataField="cantidad_fisica" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Fecha" DataField="fechas" DataFormatString="{0:dd/MM/yyyy}" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Comentarios" DataField="comentarios" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Sucursal" DataField="id_sucursal_sistema" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Vendedor" DataField="id_vendedor" >
                </asp:BoundField>
                
            </Columns>          
            </asp:GridView>
            </asp:Panel>
            </asp:Panel>



            <asp:Panel ID="Ultima_venta"  runat="server"  Visible="False"> 
            <div class="cabecera_informacion" style="margin-bottom:0px;"><p class="texto_cabecera"> <asp:Label ID="mensage2" runat="server"></asp:Label></p></div>
            <asp:Panel ID="panel_grafica_ultimas_fechas" class="datos_ventas" runat="server" style="margin:0px;" > 
            <asp:GridView ID="tabla_ultima_fecha"  runat="server"  AutoGenerateColumns="False" DataSource='<%# ultimas_ventas() %>'   CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_productos_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor">
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripcion" DataField="productos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Marca" DataField="marca">
                </asp:BoundField>
                <asp:BoundField HeaderText="Ultima Venta" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ShowHeader="True">
                <ItemStyle CssClass="control" />
                </asp:TemplateField>
            </Columns>          
            </asp:GridView>
            </asp:Panel>
            </asp:Panel>




            <asp:Panel ID="panel_maximos_minimos" runat="server"  Visible="False">   
            <div class="cabecera_informacion" id="panel4"  runat="server"  ><p class="texto_cabecera" >Modificar Maximos Y Minimos</p></div>
            <asp:Panel ID="panel_modificar" class="datos_ventas" runat="server"  >   
            <asp:GridView ID="tabla_modificar" runat="server" AutoGenerateColumns="False" DataSource='<%# datos_productos() %>'  CssClass="tabla"  DataKeyNames="codigo_proveedor" onrowcommand="tabla_modificar_RowCommand"  ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor">
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripcion" DataField="productos" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Marca" DataField="marca">
                </asp:BoundField>
                 <asp:BoundField HeaderText="Costo" DataField="costos" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Cantidad" DataField="cantidades" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Minimo" DataField="minimo" >
                </asp:BoundField>
                 <asp:BoundField HeaderText="Maximo" DataField="maximo" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="Modificar" ShowHeader="True">
                     <ItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ToolTip="Modificar Minimo" CommandName="Modificar_minimos" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"><i class="fa fa-arrow-down control-tabla-verde"></i></asp:LinkButton> 
                       <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" ToolTip="Modificar Maximo" CommandName="Modificar_maximos" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"><i class="fa fa-arrow-up control-tabla-verde"></i></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="control" />
                </asp:TemplateField>
            </Columns>          
            </asp:GridView>
            </asp:Panel>
            </asp:Panel>
            </div>
            </div>
            </div>
            <!-- Panel Sobrepuesto Modificar Maximos-->
            <!-- Panel Sobrepuesto Modificar Maximos-->
            <!-- Panel Sobrepuesto Modificar Maximos-->
            <asp:Panel ID="panel_modificar_maximos" runat="server" CssClass="modulo-esp-foto"
                Visible="False">
                <div class="controles_atender_pedido">
                    <div class="cerrar-modulo">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="boton_cerrar_atender_pedido"
                            OnClick="boton_cerrar_panel_maximo_Click"><i class="fa fa-times"></i>  &nbsp;&nbsp;Cerrar modulo</asp:LinkButton></div>
                    <ul class="vertical">
                        <li>
                            <p>
                                <asp:Label ID="producto_caja" runat="server" Text="Producto"></asp:Label></p>
                            <asp:TextBox ID="caja_nombre_producto_maximos" runat="server" Enabled="False" CssClass="caja_codigo_pedido"></asp:TextBox></li>
                    </ul>
                    <ul class="horizontal inline-list inputs-cotizacion">
                        <li>
                            <p>
                                <asp:Label ID="Label2" runat="server" Text="No Existencia"></asp:Label></p>
                            <asp:TextBox ID="caja_numero_productos_maximos" runat="server" CssClass="cajas_chicas"
                                Enabled="False"></asp:TextBox></li>
                        <li>
                            <p>
                                <asp:Label ID="Label7" runat="server" Text="Maximo Actual"></asp:Label></p>
                            <asp:TextBox ID="cantidad_actual_maximos" runat="server" CssClass="cajas_chicas"
                                Enabled="False"></asp:TextBox></li>
                        <li>
                            <p>
                                <asp:Label ID="Label3" runat="server" Text="Nuevo Maximo"></asp:Label></p>
                            <asp:TextBox ID="caja_cantidad_surtir_maximos" runat="server" CssClass="cajas_chicas"
                                Enabled="True" MaxLength="4"></asp:TextBox>
                            <asp:LinkButton ID="boton_modificar_maximo" runat="server" OnClick="boton_modificar_maximos_Click"
                                ToolTip="Modificar Maximos" Style="text-decoration: none" CssClass="boton_regresar button small">Modificar</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </asp:Panel>

             <!-- Panel Sobrepuesto Modificar Minimos-->
             <!-- Panel Sobrepuesto Modificar Minimos-->
             <!-- Panel Sobrepuesto Modificar Minimos-->
            <asp:Panel ID="panel_modificar_minimo" runat="server" CssClass="modulo-esp-foto"
                Visible="False">
                <div class="controles_atender_pedido">
                    <div class="cerrar-modulo">
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="boton_cerrar_atender_pedido"
                            OnClick="boton_cerrar_panel_minimo_Click"><i class="fa fa-times"></i>  &nbsp;&nbsp;Cerrar modulo</asp:LinkButton></div>
                    <ul class="vertical">
                        <li>
                            <p>
                                <asp:Label ID="Label1" runat="server" Text="Producto"></asp:Label></p>
                            <asp:TextBox ID="caja_nombre_producto_minimos" runat="server" Enabled="False" CssClass="caja_codigo_pedido"></asp:TextBox></li>
                    </ul>
                    <ul class="horizontal inline-list inputs-cotizacion">
                        <li>
                            <p>
                                <asp:Label ID="Label4" runat="server" Text="No Existencia"></asp:Label></p>
                            <asp:TextBox ID="caja_numero_productos_minimos" runat="server" CssClass="cajas_chicas"
                                Enabled="False"></asp:TextBox></li>
                        <li>
                            <p>
                                <asp:Label ID="Label6" runat="server" Text="Minimo Actual"></asp:Label></p>
                            <asp:TextBox ID="cantidad_actual_minimo" runat="server" CssClass="cajas_chicas" Enabled="False"></asp:TextBox></li>
                        <li>
                            <p>
                                <asp:Label ID="Label5" runat="server" Text="Nuevo Minimo"></asp:Label></p>
                            <asp:TextBox ID="caja_cantidad_surtir_minimos" runat="server" CssClass="cajas_chicas"
                                Enabled="True" MaxLength="4"></asp:TextBox>
                            <asp:LinkButton ID="boton_modificar_minimo" runat="server" OnClick="boton_modificar_minimos_Click"
                                ToolTip="Modificar Minimos" Style="text-decoration: none" CssClass="boton_regresar button small">Modificar</asp:LinkButton>
                        </li>
                    </ul>
              </div>
            </asp:Panel>

            <asp:Panel ID="panel_detalles_compra" runat="server" CssClass="panel_surtir_inventario modulo-esp tabla_larga" Visible="False">
        <asp:LinkButton ID="boton_cerrar_detalles_venta" runat="server" CssClass="boton_cerrar_precios boton-cerrar"  onclick="boton_cerrar_detalles_venta_Click">
        <div class="cerrar-modulo">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-times"></i>&nbsp;&nbsp;Cerrar Modulo</div>
        </asp:LinkButton>
        <div class="controles_surtir_inventario tabla_sobrepuesta">
        <ul class="vertical">
        <li>       
             <asp:GridView ID="tabla_detalles_producto" runat="server" AutoGenerateColumns="False" CssClass="tabla_cantidades" ShowHeaderWhenEmpty="True" DataSource='<%# detalles_producto() %>'>
             <HeaderStyle CssClass="GridHeader" />
		     <PagerSettings Visible="False" />
		     <RowStyle CssClass="GridRow" HorizontalAlign="Left" />
             <Columns>
                <asp:BoundField HeaderText="Precio Credito" DataField="precio_credito" DataFormatString="{0:C}" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio Contado" DataField="precio_contado" DataFormatString="{0:C}" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio A Gobierno" DataField="precio_gobierno" DataFormatString="{0:C}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio A Mayoreo" DataField="precio_mayoreo" DataFormatString="{0:C}" >
                </asp:BoundField>
             </Columns>
             </asp:GridView>
         </li>
         </ul>
         </div>
         </asp:Panel>

            <!-- PANEL NO RESULTADOS-->
            <asp:Panel ID="panel_no_resultados_busqueda" runat="server" CssClass="no_resultados"
                Visible="False">
                <br />
                <br />
                <i class="fa fa-frown-o fa-2x"></i>&nbsp;&nbsp;<asp:Label ID="leyenda" runat="server"
                    CssClass="texto" Text="No se encontraron resultados relacionados con esta busqueda"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="panel_cargando" runat="server" Visible="True" Style="text-decoration: none;
                display: none">
                <asp:Label ID="loading_rojo" runat="server"><i class="fa fa-circle-o-notch fa-spin fa-3x"></i></asp:Label>
            </asp:Panel>
        </div>
        

        <!-- botones ocultos-->
        <asp:Button ID="boton_oculto_modificar_maximo" runat="server" OnClick="boton_oculto_modificar_maximo_Click" Text="boton_oculto_modificar_maximo"
                            Visible="True" Style="visibility: hidden" />
        <asp:Button ID="boton_oculto_modificar_minimo" runat="server" OnClick="boton_oculto_modificar_minimo_Click" Text="boton_oculto_modificar_minimo"
                            Visible="True" Style="visibility: hidden" />

        </form>
    </body>
 </html>