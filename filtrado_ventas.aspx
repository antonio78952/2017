<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codigo/filtrado_ventas.aspx.cs" Inherits="filtrado_ventas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
     
</script>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Registros De Ventas</title>
</head>
<link rel="stylesheet" href="css/registro_ventas.css" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet">
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<script type="text/javascript">

      function sacalo(divname) {       
          var div = document.getElementById(divname);
          var img = document.getElementById('img' + divname);
          if (div.style.display == "none") {
              div.style.display = "inline";
              $(div).dialog({ width: 'auto' });
              img.src = "png/minus.png";
          } else {
              div.style.display = "none";
              img.src = "png/plus.png";
          }
          $(div).on('dialogclose', function (event) {
              div.style.display = "none";
              img.src = "png/plus.png"; 
          });
      }
      
</script>

  <script type="text/javascript">
      $(function () {
          $('.esconder').click(function () {
              var id_boton = $(this).attr("id");
              $('td:nth-child(' + id_boton + '),th:nth-child(' + id_boton + ')').toggle();
              $(this).toggleClass("mostrar");
          });

          $('.desactivado').click(function () {
              $(this).toggleClass("mostrar");
          });
      });
    </script>
    
<script type="text/javascript">      
$(function () {
    for (var i = 10; i <= 21; i++) {
        $('td:nth-child(' + i + '),th:nth-child(' + i + ')').toggle();
        $('#'+i+'').toggleClass("mostrar");
    }
});

$(function () {
    $("#buscarfecha").datepicker({ dateFormat: 'dd/mm/yy', maxDate: "0D" }).val();
});

$(function () {
    var suma = 0;
    $("tbody tr").click(function () {
        if ($('#boton_calcular').hasClass("mostrar")) {
            $(this).toggleClass("selected");
            var currentRow = $(this).closest("tr");
            var productos = $(this).closest("tr").find("div >.tabla_venta_lista > tbody > .orales > td");
           // var productos2 = find("[0].children[16].children["divVEN-10lv"].children["0"].children.tabla_principal_ctl02_tabla_ventas_lista.children["0"].children[1]")
            var cantidad = currentRow.find("td:eq(3)").text();
            $.each($(productos).eq(1),
         function (i , val) {
             var muestra = $(val).text();

          
             //document.getElementById('panel_mamalon').innerHTML += '<br/><input type="text" id="caja' + i + '" value="' + val.text(); +'" "  /><br/>';
         });
            if ($(this).hasClass("selected")) {
                if (!isNaN(cantidad) && cantidad.length !== 0) {
                    suma += parseInt(cantidad, 10);
                    //suma += parseFloat(cantidad);
                    //alert('la suma de las columnas va en ' + suma + '');
                }
            }
            else if (!isNaN(cantidad) && cantidad.length !== 0) {
                suma -= parseInt(cantidad, 10);
                //suma -= parseFloat(cantidad);
                //alert('le restamos ala cuenta ' + cantidad + ' y nos quedan' + suma + ' ');  
            } $("#resultados").text('' + suma + '');
            $("#panel_mamalon").dialog();
        }
    });
});

$(function () {
    var $rows = $('#tabla_principal ');
    $('#buscarfecha').change(function () {
        var that = this;
        $.each($('tr'),
        function (i, val) {
            if (val.className != "orales") {
             
                if ($(val).text().indexOf($(that).val()) == -1) {
                    $('tr').eq(i).hide();
                } else {
                    $('tr').eq(i).show();
                }
                if (val.className == "cabecera_tabla") {
                    $('tr').eq(i).show(); 
                 }
            }
        });
    });
});


</script>
<script>
    function buscador() {
        // Declare variables 
        var input, filter, table, tr, td, i;
        input = document.getElementById("#buscarfecha");
        filter = input.value.toUpperCase();
        table = document.getElementById("#tabla_principal");
        tr = table.getElementsByTagName("tr");
        // Loop through all table rows, and hide those who don't match the search query
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
</script>

<script type="text/javascript">
    function estadisticas() {
        // Variables
        var entrada, entrada_fecha, tabla_fecha, filtro, tabla, tr, td, i, numero_ventas, productos_consume, promedio_consumo_mes, total, fecha_fila, fecha_busca;
        var venta = 0;
        entrada = document.getElementById("calcular");
        entrada_fecha = document.getElementById("buscarfecha");
        filtro = entrada.value.toUpperCase();
        fecha = entrada_fecha.value.toUpperCase();
        tabla = document.getElementById('tabla_principal');
        tr = tabla.getElementsByTagName('tr');
        // ciclo mamalon [1].children[4].cellIndex [1].cells[4] [1].cells[4]
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            //tabla_fecha = (tr[i].getElementsByTagName("td"));

            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filtro) > -1) {
                    tr[i].style.display = "";
                    //total = tr[i].getElementsByTagName("td")[20];
                    //venta += parseInt(total.textContent, 10);
                } else if (tr[i].className != "orales") { //devido alos gridview anidados es nesesario excluirlos
                    tr[i].style.display = "none";
                }
            }
        }
        //document.getElementById('panel_mamalon').innerHTML += '<br/><input type="text" id="caja' + i + '" value="' + venta + '" "  /><br/>';
        //$('#id = "caja' + i + '"').dialog({ width: 'auto' });
    }
</script>  
<script type="text/javascript" language="javascript">
    $(function () {
        var tableOffset = $("#tabla_principal").offset().top;
        var $header = $("#tabla_principal > tbody > tr >th").clone();
        var $fixedHeader = $("#header-fixed").append($header);
        $(window).bind("scroll", function () {
            var offset = $(this).scrollTop();
            if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
                $fixedHeader.show();
            }
            else if (offset < tableOffset) {
                $fixedHeader.hide();
            }
        });
    });


    function productos() {
        // Variables
        var entrada, filtro, i,productos;
        var venta = 0;
        entrada = document.getElementById("calcular");
        filtro = entrada.value.toUpperCase();
        var cells = $(".tabla_venta_lista > tbody > .orales > td");
        $.each($(cells),
        function (i, val) {
            if ($(val).text().indexOf(filtro) > -1) {
                alert($(val).text());
            } else {

            }
        });
    }
</script>    


<body>

    <form id="registro_ventas" runat="server">
   <!----- esto es un comentario ----->
    <div id="menu">
        <div id="menu_mobiles">
            <asp:LinkButton ID="boton_menu" runat="server"  ToolTip="Menu de opciones"><i class="fa fa-bars fa-2x"></i></asp:LinkButton>
            <asp:LinkButton ID="boton_menu_cerrar" runat="server"  ToolTip="Cerrar menu de opciones"><i class="fa fa-bars fa-2x"></i></asp:LinkButton>
        </div>
   
        <asp:Label ID="vendedor" runat="server" Text="Ventas" CssClass="nombre_vendedor"/>
    <!----- esto es un comentario ----->    
        <div class="botones_menu">
            <asp:LinkButton ID="boton_exportar" runat="server" CssClass="boton" ToolTip="Exportar" onclick="boton_exportar_Click" ><img id="d" class="icono" src="png/z.png"></asp:LinkButton>               
            <asp:LinkButton ID="boton_imprimir" runat="server" CssClass="boton" ToolTip="Imprimir" onclick="boton_imprimir_Click" ><img id="e" class="icono" src="png/e.png"></asp:LinkButton>               
            <asp:LinkButton ID="salir" runat="server" CssClass="boton" ToolTip="Salir" onclick="boton_salir_Click"><img id="f" class="icono" src="png/f.png"></asp:LinkButton>
        </div> 
    </div>
   <!----- esto es un comentario ----->  
    <div id="submenu">
        <div id="elementos">                                  
            <asp:LinkButton CssClass="elemento_menu" ID="m_exportar" runat="server"  ToolTip="Exportar" onclick="boton_exportar_Click" Enabled="False"><p class="texto_m">Exportar</p></asp:LinkButton>                
            <asp:LinkButton CssClass="elemento_menu" ID="m_salir" runat="server"  ToolTip="Salir" onclick="boton_salir_Click"><p class="texto_m">Salir</p></asp:LinkButton>
        </div>
    </div> 
   <!----- Main -----> 
    <div id="main">
   <!----- Main Izquierdo ----->
        <div id="main_izquierdo">
          <div class="paneles_graficas">         
            <div class="cabecera_informacion"><asp:Label runat="server" CssClass="texto_cabecera">Buscar</asp:Label></div>
            <div class="controles_busqueda">
                <asp:TextBox ID="calcular" runat="server" CssClass="caja"  Enabled="true" placeholder="Buscar" ></asp:TextBox>
                <a href="JavaScript:estadisticas();"><input id="buscar" type="button" value="Buscar" class="esconder"/></a>
                <a href="JavaScript:productos();"><input id="hola" type="button" value="Buscar Productos" /></a>
                <asp:TextBox ID="buscarfecha" runat="server" CssClass="caja" placeholder="Buscar Fecha" ></asp:TextBox>
                <div class="cabecera_informacion"><asp:Label ID="Label1" runat="server" CssClass="texto_cabecera">Controles</asp:Label></div>
                <input id="boton_calcular" type="button" value="Calcular" class="desactivado"/>
                <asp:Panel ID="panel_mamalon" title="Suma De Ventas Totales"  runat="server" style="display: none">
                    <asp:Label  ID ="resultados" Text="" runat="server" class="texto_cabecera resultado" />
                </asp:Panel>
            </div>
          </div>
        </div>
   <!----- Main Derecho ----->
        <div id="main_derecho">
        <div class="paneles_graficas"> 
        <div class="cabecera_informacion"><asp:Label ID="Label2" runat="server" CssClass="texto_cabecera">Columnas</asp:Label></div>        
        <div class="controles_busqueda">
            <div class="paneles_graficas">
                  <input id="5" type="button" value="Fecha Venta" class="esconder"/>
                  <input id="1" type="button" value="Cliente" class="esconder"/>
                  <input id="8" type="button" value="Clave Cotizacion" class="esconder"/>
                  <input id="9" type="button" value="Clave Venta" class="esconder"/>
                  <input id="17" type="button" value="Detalles Venta" class="esconder"/>
                  <input id="6" type="button" value="Vendedor" class="esconder"/>
                  <input id="7" type="button" value="Sucursal" class="esconder"/>
                  <input id="10" type="button" value="Clave Pedido" class="esconder"/>
                  <input id="16" type="button" value="Detalle Pedido" class="esconder"/>
                  <input id="2" type="button" value="IVA" class="esconder"/>
                  <input id="3" type="button" value="Subtotal" class="esconder"/>
                  <input id="4" type="button" value="Total" class="esconder"/>
                  <input id="11" type="button" value="Comision Negocio" class="esconder"/>
                  <input id="12" type="button" value="Comision Vendedor" class="esconder"/>
                  <input id="13" type="button" value="Estado Venta" class="esconder"/>
                  <input id="14" type="button" value="Fecha Cotizacion" class="esconder"/>
                  <input id="15" type="button" value="Fecha Pedido" class="esconder"/>
                  <input id="18" type="button" value="Requisicion" class="esconder"/>
                  <input id="19" type="button" value="Trasferencias" class="esconder"/>
                  <input id="20" type="button" value="Orden Compra" class="esconder"/>
                  <input id="21" type="button" value="Factura" class="esconder"/>                 
            </div>
            </div>
           </div>  
        </div>
        
   <!----- Main Centro ----->
        <div id="main_centro">
            <div class="paneles_graficas display">
                <table id="header-fixed"></table>
                <asp:GridView ID="tabla_principal" CssClass="enchufe" AutoGenerateColumns="false" runat="server" DataKeyNames="clave_cotizacion,clave_venta,clave_pedido"  OnRowDataBound="llenado_tabla_principal" DataSource='<%# ventas() %>' >
                <HeaderStyle CssClass="cabecera_tabla" />
                <Columns>
                    
                    <asp:BoundField HeaderText="Cliente" DataField="cliente" >
                    </asp:BoundField>
                    <asp:BoundField HeaderText="IVA" DataField="iva">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Subtotal" DataField="subtotal">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Total" DataField="total">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Venta" DataField="fecha_venta" DataFormatString="{0:dd/MM/yyyy}">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Vendedor" DataField="vendedor">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Sucursal" DataField="sucursal">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Clave Cotizacion" DataField="clave_cotizacion"  >
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Clave Venta" DataField="clave_venta">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Clave Pedido" DataField="clave_pedido" >
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Comision Negocio" DataField="comision_negocio">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Comision Vendedor" DataField="comision_vendedor">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Estado Venta" DataField="estado_venta">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Cotizacion" DataField="fecha_cotizacion" DataFormatString="{0:dd/MM/yyyy}">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Pedido" DataField="fecha_pedido" DataFormatString="{0:dd/MM/yyyy}">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Detalles Pedido" >
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate >
                            <a href="JavaScript:sacalo('div<%# Eval("clave_pedido") %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_pedido") %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_pedido") %>" style="display: none"  title="Pedido <%# Eval("clave_pedido")%>">
                                <asp:GridView ID="tabla_pedido" class="display" runat="server" AutoGenerateColumns="false" >
                                    <RowStyle CssClass ="orales" />
                                    <Columns>                                  
                                        <asp:BoundField HeaderText="Codigo Proveedor" DataField="codigo_lista_pedido" >
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cantidad" DataField="cantidad_lista_pedido" >
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cantidad Surtida" DataField="cantidad_surtida_lista_pedido" >
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cantidad Backorder" DataField="cantidad_backorder_lista_pedido" >
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detalles Venta">
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate>
                            <a href="JavaScript:sacalo('div<%# Eval("clave_venta")+ "lv" %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_venta") + "lv" %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_venta") + "lv" %>" style="display: none" title="Productos <%# Eval("clave_venta")%>">
                                <asp:GridView ID="tabla_ventas_lista" class="tabla_venta_lista" runat="server" AutoGenerateColumns="false">
                                    <RowStyle CssClass ="orales" />
                                    <Columns>
                                    <asp:BoundField HeaderText="Clave Venta" DataField="clave_venta" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Codigo Proveedor" DataField="codigo_proveedor_lista_venta" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Descripcion" DataField="proveedor_lista_venta" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cantidad" DataField="cantidad_backorder_lista_venta" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cantidad Backorder" DataField="cantidad_lista_venta" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Total" DataField="precio_lista_venta" >
                                    </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Requisicion">
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate>
                            <asp:Panel id="panel_requisiciones" runat="server" Visible="false">                           
                            <a href="JavaScript:sacalo('div<%# Eval("clave_cotizacion")+ "re" %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_cotizacion")+ "re" %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_cotizacion")+ "re" %>" style="display:none" class="panel" title="Requisicion <%# Eval("clave_cotizacion")%>">
                                <asp:GridView ID="tabla_requisiciones" class="display" runat="server" AutoGenerateColumns="false" OnRowDataBound="llenado_tabla_requisiciones" DataKeyNames="clave_requisicion">
                                    <RowStyle CssClass ="orales" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Detalles">
                                        <ItemStyle  CssClass="centro" />
                                        <ItemTemplate>
                                                <a href="JavaScript:sacalo('div<%# Eval("clave_requisicion") %>');">
                                                <img alt="Detalles" id="imgdiv<%# Eval("clave_requisicion") %>" src="png/plus.png" /></a>
                                                <div id="div<%# Eval("clave_requisicion") %>" style="display: none" class="panel" title="Productos <%# Eval("clave_requisicion")%>">
                                                <asp:GridView ID="tabla_lista_productos_requisiciones" class="display" runat="server" AutoGenerateColumns="false">
                                                    <RowStyle CssClass ="orales" />
                                                    <Columns>
                                                    <asp:BoundField HeaderText="Codigo" DataField="codigo_proveedor_requisicion" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Producto" DataField="producto_requisicion" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Aprobacion" DataField="aprobacion_requisicion" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Cantidad" DataField="cantidad_requisicion" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Precio" DataField="precio_requisicion" >
                                                    </asp:BoundField>   
                                                    <asp:BoundField HeaderText="Total" DataField="precio_total_requisicon" >
                                                    </asp:BoundField>   
                                                    <asp:BoundField HeaderText="Proveedor" DataField="proveedor_requisicion" >
                                                    </asp:BoundField>                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Clave Requisiscion" DataField="clave_requisicion" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Subtotal" DataField="subtotal_requisicion" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="IVA" DataField="iva_requisicion" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Total" DataField="total_requisicion" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Estado" DataField="estado_requisicion" >
                                    </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                           </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trasferencias">
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate>
                            <asp:Panel ID="panel_trasferencias" runat="server" Visible="false" >                           
                            <a href="JavaScript:sacalo('div<%# Eval("clave_cotizacion") + "tr" %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_cotizacion")+ "tr" %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_cotizacion")+ "tr" %>" style="display: none" class="panel" title="Trasferencias <%# Eval("clave_venta")%>">
                                <asp:GridView ID="tabla_trasferencias" class="display" runat="server" AutoGenerateColumns="false">
                                    <RowStyle CssClass ="orales" />
                                    <Columns>
                                    <asp:BoundField HeaderText="ID Sucursal Transfiere" DataField="id_sucursal_transfer" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ID Sucursal Requiere" DataField="id_sucursal_trasfer_requiere" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Codigo" DataField="codigo_trasfer" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cantidad" DataField="cantidad_trasfer" >
                                    </asp:BoundField>                                    
                                    </Columns>
                                </asp:GridView>
                            </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Orden Compra">
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate>
                            <asp:Panel ID="panel_ordenes_compra" runat="server" Visible="false">                        
                            <a href="JavaScript:sacalo('div<%# Eval("clave_cotizacion")+ "or" %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_cotizacion")+ "or" %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_cotizacion")+ "or" %>" style="display: none" class="panel" title="Ordene De Compra <%# Eval("clave_cotizacion")%>">
                            <asp:GridView ID="tabla_ordenes_compra" class="display" runat="server" AutoGenerateColumns="false" DataKeyNames="clave_orden_compra" OnRowDataBound="llenado_tabla_ordenes_compra">
                                <RowStyle CssClass ="orales" />
                                <Columns>
                                <asp:TemplateField HeaderText="Detalles">
                                    <ItemStyle  CssClass="centro" />
                                    <ItemTemplate>
                                        <a href="JavaScript:sacalo('div<%# Eval("clave_orden_compra") %>');">
                                        <img alt="Detalles" id="imgdiv<%# Eval("clave_orden_compra") %>" src="png/plus.png" /></a>
                                        <div id="div<%# Eval("clave_orden_compra") %>" style="display: none" class="panel" title="Productos <%# Eval("clave_orden_compra")%>">
                                            <asp:GridView ID="tabla_lista_productos_ordenes_compra" class="display" runat="server" AutoGenerateColumns="false">
                                                <RowStyle CssClass ="orales" />
                                                <Columns>
                                                <asp:BoundField HeaderText="Codigo" DataField="codigo" >
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Cantidad" DataField="cantidad" >
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Producto" DataField="producto" >
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Aprobacion" DataField="aprobacion" >
                                                </asp:BoundField>                                    
                                                </Columns>
                                            </asp:GridView>
                                        <div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Clave Orden Compra" DataField="clave_orden_compra" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Estado" DataField="estado_orden_compra" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fecha" DataField="fecha_orden_compra" DataFormatString="{0:dd/MM/yyyy}" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Listo" DataField="listo" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Almacen" DataField="almacen" >
                                </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Factura">
                        <ItemStyle  CssClass="centro" />
                        <ItemTemplate>
                        <asp:Panel ID="panel_factura" runat="server" Visible="false">                  
                            <a href="JavaScript:sacalo('div<%# Eval("clave_cotizacion" )+ "fa" %>');">
                            <img alt="Detalles" id="imgdiv<%# Eval("clave_cotizacion")+ "fa" %>" src="png/plus.png" /></a>
                            <div id="div<%# Eval("clave_cotizacion")+ "fa" %>" style="display: none" class="panel" title="Factura <%# Eval("clave_cotizacion")%>">
                                <asp:GridView ID="tabla_facturas" class="display" runat="server" AutoGenerateColumns="false" >
                                    <RowStyle CssClass ="orales" />
                                    <Columns>
                                    <asp:BoundField HeaderText="Factura" DataField="factura" >
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Fecha" DataField="fecha_factura" DataFormatString="{0:dd/MM/yyyy}" >
                                    </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>      
                </Columns>  
                </asp:GridView>    
            </div>
        </div>
    </div> 
   </form>
</body>
</html>
