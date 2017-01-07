<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codigo/reportes_sucursales.aspx.cs" Inherits="reportes_sucursales" %>

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
    <script type="text/javascript" language="javascript">
        <%  var serializer =  new  System . Web . Script . Serialization . JavaScriptSerializer ();  %>   
 
       var datos_matriz =  <%= serializer . Serialize ( compras_anuales_matriz )  %>;
       var datos_cortez =  <%= serializer . Serialize ( compras_anuales_cortez )  %>;
       var datos_cuarta =  <%= serializer . Serialize ( compras_anuales_cuarta )  %>;
       var datos_cbtis =  <%= serializer . Serialize ( compras_anuales_cbtis )  %>;
       var datos_anuales = {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio","Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        datasets: [
                				{
                                    responsive: true,
                				    label: "My Second dataset",
                				    fillColor: "rgba(87,11,175,0.5)",
                                    strokeColor: "rgba(22,6,40,1)",
                                    pointColor: "rgba(79,193,233,0.5)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "#fff",
                                    pointHighlightStroke: "rgba(220,220,220,1)",
                				    data: datos_matriz
                				},
                                 {
                				    label: "My Second dataset",
                				    fillColor: "rgba(64, 108, 44, 0.9)",
                                    strokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointColor: "rgba(26, 82, 118  ,0.8)",
                                    pointStrokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightFill: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightStroke: "rgba(26, 82, 118  ,0.8)",
                				    data: datos_cortez
                				},
                                 {
                				    label: "My Second dataset",
                				    fillColor: "rgba(255, 61, 155, 0.8)",
                                    strokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointColor: "rgba(26, 82, 118  ,0.8)",
                                    pointStrokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightFill: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightStroke: "rgba(26, 82, 118  ,0.8)",
                				    data: datos_cuarta
                				},
                                 {
                				    label: "My Second dataset",
                				    fillColor: "rgba(195, 189, 0, 0.8)",
                                    strokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointColor: "rgba(26, 82, 118  ,0.8)",
                                    pointStrokeColor: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightFill: "rgba(26, 82, 118  ,0.8)",
                                    pointHighlightStroke: "rgba(26, 82, 118  ,0.8)",
                				    data: datos_cbtis
                				}
			]
    }
  </script>
  <script type="text/javascript" language="javascript" >
      $(function () {
          $("#buscarfecha").datepicker({ dateFormat: 'mm/dd/yy', maxDate: "-1D" }).val();
          $("#buscarfecha2").datepicker({ dateFormat: 'mm/dd/yy', maxDate: "-1D" }).val();
          $("#buscarfecha_java").datepicker({ dateFormat: 'dd/mm/yy', maxDate: "0D" }).val();
         
      });
  </script>

  <script type="text/javascript" language="javascript" >
      $(function () {
          var $rows = $('#tabla_ventas tr[class!="cabecera_tabla"]');
          var $rows2 = $('#tabla_detalles_venta tr[class!="cabecera_tabla"]');
         
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
          });
      });
  </script>
  <script type="text/javascript" language="javascript">
      $(function () {
          $("#mensages").dialog();
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
  <script type="text/javascript">
      

      $(function () {
          var $rows = $('#tabla_detalles_venta tr[class!="cabecera_tabla"]');
          $('#buscarfecha_java').change(function () {
              var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

              $rows.show().filter(function () {
                  var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                  return ! ~text.indexOf(val);
              }).hide();
          });
      });
  </script>

 
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Reportes de Sucursales - Inklaser</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/custom.css" />
    <link rel="stylesheet" href="css/dashboard.css" />
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
           <i class="fa fa-dashboard fa-2x blanco"></i><asp:Label ID="vendedor" runat="server" CssClass="nombre_vendedor">Sucursales</asp:Label>
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
         <ul>
         <li class="cabecera_informacion"><p class="texto_cabecera">Filtrar</p></li>
        <li>
         <asp:DropDownList ID="tipo_busqueda" runat="server" CssClass="lista_categoria_medina " AutoPostBack="True" Width="150px" onselectedindexchanged="tipo_busqueda_SelectedIndexChanged">
                <asp:ListItem>Seleccionar</asp:ListItem>
                <asp:ListItem>Sucursales</asp:ListItem>
                <asp:ListItem>Ventas Dia</asp:ListItem>
                <asp:ListItem>Ventas Mes</asp:ListItem>
                <asp:ListItem>Ventas Año</asp:ListItem>
                <asp:ListItem>Ventas Turno</asp:ListItem>
                <asp:ListItem>Pronostico</asp:ListItem>
                
                

            </asp:DropDownList></li>
            <li class="cabecera_informacion"><p class="texto_cabecera">Opciones</p></li>
            <li>
            <asp:DropDownList ID="lista_fechas" runat="server" CssClass="lista_categoria_medina"  AutoPostBack="True" Width="200px" onselectedindexchanged="lista_fechas_SelectedIndexChanged">
            </asp:DropDownList>
            </li>    
            </ul> <!--termina listado-->
               <asp:Panel ID="panel_busqueda" runat="server">
               <div class="cabecera_informacion"><p class="texto_cabecera">Buscar</p></div>
              
               <asp:TextBox ID="buscar" runat="server" CssClass="cajas_chicas margen_izquierdo" Enabled="true" ></asp:TextBox>
               <asp:Label ID="buscale" runat="server"  ToolTip="Buscar"
                            Style="text-decoration: none" CssClass="boton_regresar button small margen_izquierdo">Buscar</asp:Label>
               <asp:Label ID="mensages" runat="server" Visible="false"></asp:Label>
               <asp:Label ID="Label3"  Text="Buscar Fecha" runat="server" Visible="false"></asp:Label>
               <asp:TextBox ID="buscarfecha_java" runat="server" CssClass="caja_medina margen_izquierdo" Enabled="true" ></asp:TextBox>
               </asp:Panel>   
        </div> 
        </div>

         <div id="main_izquierdo">
         <div class="paneles_graficas">
         <asp:Panel ID="panelpredictivo" runat="server" Visible = "false" >
           <div class="cabecera_informacion"><p class="texto_cabecera">Calcular Ventas</p></div>
           <asp:TextBox ID="buscarfecha" runat="server" class="caja_medina" Enabled="true" placeholder="De" ></asp:TextBox>
           <asp:TextBox ID="buscarfecha2" runat="server" class="caja_medina" Enabled="True" placeholder="A"  ></asp:TextBox>
           <asp:LinkButton ID="calcular" runat="server"  OnClick="prediccion_Click" ToolTip="Buscar" Style="text-decoration: none" CssClass="boton_regresar button small margen_izquierdo">Hecho</asp:LinkButton>
           <div id="pronosticoDiv" class="cabecera_informacion" runat="server" visible="false"><p class="texto_cabecera">Pronostico</p></div>
           <asp:TextBox ID="cantidad_calculo" runat="server" CssClass="caja_medina margen_izquierdo" Enabled="False" Visible="false" ></asp:TextBox>              
         </asp:Panel>

          

            <asp:Panel id="panelgrafica" runat="server">            
                
                <div class="cabecera_informacion"><p class="texto_cabecera">Ventas Sucursales</p></div>
                <div class="grafica puntos">
                   <canvas id="grafica_compras_anuales" class="graficas"></canvas>
                </div>
                <div class="espacio"></div>
                
            </asp:Panel>

            <div class="cabecera_informacion"><p class="texto_cabecera"><asp:Label ID="titulo" runat="server" /></p></div>
            <asp:Panel ID="panel_detalles_venta" runat="server"  Visible="False" class="datos_ventas">
                <asp:GridView ID="tabla_detalles_venta" runat="server" AutoGenerateColumns="False" CssClass="tabla" ShowHeaderWhenEmpty="True" DataSource='<%# detalles_venta() %>'>
                    <HeaderStyle CssClass="cabecera_tabla" />
		            <PagerSettings Visible="False" />
		            <RowStyle  HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField HeaderText="Clave Venta" DataField="codigo_proveedor" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha" DataField="fechas" DataFormatString="{0:dd/MM/yyyy}" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:C}">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="ID Vendedor" DataField="id_clientes" >
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                
               
                
             </asp:Panel>

         
            </div>
            </div>

            <div id="main_centro">


            <asp:Panel ID="panel_datos" CssClass="datos_ventas" runat="server"  Visible="False">
            <div class="cabecera_informacion"><p class="texto_cabecera"><asp:Label ID="cabezera" runat="server" /></p></div>
            <asp:GridView ID="tabla_ventas"  runat="server" AutoGenerateColumns="False" DataSource='<%# datos_ventas() %>'  CssClass="tabla "  DataKeyNames="id_sucursal,compras,nombre" onrowcommand="tabla_ventas_RowCommand" ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="ID Sucursal" DataField="id_sucursal">
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombre" DataField="nombre" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Numero De Compras" DataField="compras" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:C}">
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

        <asp:Panel ID="panel_no_resultados_busqueda" runat="server" CssClass="no_resultados paneles_graficas" Visible="False">
        <br />
        <br />
        <i class="fa fa-frown-o fa-2x"></i>&nbsp;&nbsp;<asp:Label ID="leyenda" runat="server" CssClass="texto" Text="No se encontraron resultados relacionados con esta busqueda"></asp:Label>
        </asp:Panel>

            </div>
            </div>

             


        </form>
    </body>
 </html>
