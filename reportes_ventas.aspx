<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codigo/reportes_ventas.aspx.cs" Inherits="reportes_ventas" %>

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
    <script type="text/javascript" language="javascript" >
        <%  var serializer =  new  System . Web . Script . Serialization . JavaScriptSerializer ();  %>   
 
       var compras_anuales =  <%= serializer . Serialize ( top_cantidades )  %>;
       var datos =  <%= serializer . Serialize ( top_productos )  %>;
       var datos_anuales = {
        labels: datos,
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
                				    data: compras_anuales
                				}
			]
    }
  </script>
  <script type="text/javascript" language="javascript">
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
  <script type="text/javascript" language="javascript">
      var headertext = [];
      var headers = document.querySelectorAll("th");
      var tablebody = document.querySelectorAll("tbody");

      for (var i = 0; i < headers.length; i++) {
          headertext[i] = [];
          for (var j = 0, headrow; headrow = headers[i].rows[0].cells[j]; j++) {
              var current = headrow;
              headertext[i].push(current.textContent);
          }
      }

      for (var h = 0, tbody; tbody = tablebody[h]; h++) {
          for (var i = 0, row; row = tbody.rows[i]; i++) {
              for (var j = 0, col; col = row.cells[j]; j++) {
                  col.setAttribute("data-th", headertext[h][j]);
              }
          }
      }
  </script>
  <script type="text/javascript">
      $(function () {
          var $rows = $('#tabla_ventas tr[class!="cabecera_tabla"]');
          $('#buscale').click(function () {
              var val = $.trim($("#buscar").val()).replace(/ +/g, ' ').toLowerCase();

              $rows.show().filter(function () {
                  var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                  return ! ~text.indexOf(val);
              }).hide();
          });
      });

      $(function () {
          var $rows = $('#tabla_ventas tr[class!="cabecera_tabla"]');
          $('#buscarfecha').change(function () {
              var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

              $rows.show().filter(function () {
                  var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                  return ! ~text.indexOf(val);
              }).hide();
          });
      }); 
       
  </script>
   <script type="text/javascript">
       $(function () {
           $("#buscarfecha").datepicker({ dateFormat: 'dd/mm/yy', maxDate: "0D" }).val();
       });
  </script>
  

  

 
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
    <link rel="stylesheet" href="css/dashboard.css" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Reportes de ventas - Inklaser</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/custom.css" />
   
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
           <i class="fa fa-dashboard fa-2x blanco"></i><asp:Label ID="vendedor" runat="server" CssClass="nombre_vendedor">Ventas</asp:Label>
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
         <div class="paneles_graficas">               
         <div id="main_derecho">  
         
         <ul>
         <li class="cabecera_informacion"><p class="texto_cabecera">Filtrar</p></li>
         <li>
         <asp:DropDownList ID="tipo_busqueda" runat="server" CssClass="lista_categoria_medina " AutoPostBack="True" Width="150px" onselectedindexchanged="tipo_busqueda_SelectedIndexChanged">
                <asp:ListItem>Seleccionar</asp:ListItem>
                <asp:ListItem>Cliente</asp:ListItem>
                <asp:ListItem>Vendedor</asp:ListItem>
                <asp:ListItem>Producto</asp:ListItem>
                <asp:ListItem>Top 10 Ventas X Mes</asp:ListItem>
                <asp:ListItem>Mes</asp:ListItem>
                <asp:ListItem>Año</asp:ListItem>
                <asp:ListItem>Todas</asp:ListItem>

         </asp:DropDownList></li>
         <li class="cabecera_informacion"><p class="texto_cabecera">Opciones</p></li>
         <li>
         <asp:DropDownList ID="lista_fechas" runat="server" CssClass="lista_categoria_medina"  AutoPostBack="True" Width="200px" onselectedindexchanged="lista_fechas_SelectedIndexChanged">
         </asp:DropDownList>
         </li>    
         </ul> <!--termina listado-->
        
         </div>
         <div id="main_izquierdo">
    

        
     
             <asp:Panel ID="grafica_puntos" runat="server">
             <div class="cabecera_informacion"><p class="texto_cabecera">
             <asp:Label id="texto_cabezera" runat="server" /></p></div>
             <div class="grafica puntos">
             <canvas id="grafica_compras_anuales" class="graficas"></canvas>
             </div>
             </asp:Panel>
              <div class="espacio"></div>  
             <asp:Panel id="panel_buscar" runat="server">
             <asp:Label ID="Label2" runat="server" Text="Buscar" class="margen_izquierdo"></asp:Label>
             <asp:TextBox type="text" id="buscar" runat="server"  class="cajas_chicas margen_izquierdo" Height="16px" Width="1071px"></asp:TextBox>
             <asp:Label ID="buscale" runat="server"  ToolTip="Buscar"  Style="text-decoration: none" CssClass="boton_regresar button small margen_izquierdo">Buscar</asp:Label>
             <br />
             <asp:Label ID="buscarfechalabel" runat="server" Text="Buscar Por Fecha" class="margen_izquierdo" ></asp:Label>
             <asp:TextBox type="text" id="buscarfecha" runat="server" Width="120px" class="cajas_chicas margen_izquierdo"></asp:TextBox>
             </asp:Panel>
                         
        

        </div>
        <div id="main_centro">
        <div class="cabecera_informacion"><p class="texto_cabecera"><asp:Label ID="mensage" runat="server" /></p></div>
         <asp:Panel ID="panel_detalles_venta" runat="server" CssClass="panel_surtir_inventario modulo-esp tabla_larga" Visible="False">
         <asp:LinkButton ID="boton_cerrar_detalles_venta" runat="server" CssClass="boton_cerrar_precios boton-cerrar" onclick="boton_cerrar_detalles_venta_Click">
         <div class="cerrar-modulo">
            
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <i class="fa fa-times"></i>&nbsp;&nbsp;Cerrar Modulo</div>

         </asp:LinkButton>
                <div class="controles_surtir_inventario tabla_sobrepuesta">
                <ul class="vertical">
                <li>
                <asp:GridView ID="tabla_detalles_venta" runat="server" AutoGenerateColumns="False" CssClass="tabla_cantidades" ShowHeaderWhenEmpty="True" DataSource='<%# detalles_venta() %>'>
            <HeaderStyle CssClass="GridHeader" />
		    <PagerSettings Visible="False" />
		    <RowStyle CssClass="GridRow" HorizontalAlign="Left" />
            <Columns>
                <asp:BoundField HeaderText="C. Proveedor" DataField="codigo_proveedor" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Modelo" DataField="producto" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Cantidad" DataField="cantidad">
                </asp:BoundField>
                <asp:BoundField HeaderText="Precio" DataField="precio" DataFormatString="{0:C}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:C}">
                </asp:BoundField>
            </Columns>
            </asp:GridView>
            </li>
            </ul>
            </div>
            </asp:Panel>


          
        
            <asp:Panel ID="panel_datos" CssClass="datos_ventas" runat="server"  Visible="False">
            
            <asp:GridView ID="tabla_ventas"  runat="server" AutoGenerateColumns="False" DataSource='<%# datos_ventas() %>'  CssClass="tabla "  DataKeyNames="venta" onrowcommand="tabla_ventas_RowCommand" ShowHeaderWhenEmpty="True" PageSize="50">
            <HeaderStyle CssClass="cabecera_tabla"/>
		    <PagerSettings Visible="False" />
		    <AlternatingRowStyle CssClass="renglon_tabla" />
            <Columns>
                <asp:BoundField HeaderText="Venta" DataField="venta">
                </asp:BoundField>
                <asp:BoundField HeaderText="Cliente" DataField="cliente">
                </asp:BoundField>
                <asp:BoundField HeaderText="Subtotal" DataField="subtotal" DataFormatString="{0:C}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Iva" DataField="iva" DataFormatString="{0:C}">
                </asp:BoundField>
                 <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:C}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Comision" DataField="comision" DataFormatString="{0:C}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}">
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ShowHeader="True">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ToolTip="Ver detalles de la venta" 
                            CommandName="Ver" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"><i class="fa fa-eye control-tabla-verde"></i></asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle CssClass="control" />
                </asp:TemplateField>
             </Columns>          
             </asp:GridView>
             </asp:Panel>
          
            
          

        <asp:Panel ID="panel_no_resultados_busqueda" runat="server" CssClass="no_resultados" Visible="False">
        <br />
        <br />
        <i class="fa fa-frown-o fa-2x"></i>&nbsp;&nbsp;<asp:Label ID="leyenda" runat="server" CssClass="texto" Text="No se encontraron resultados relacionados con esta busqueda"></asp:Label>
        </asp:Panel>

        </div>       
        </div>
        </div>

      </form>
    </body>
 </html>