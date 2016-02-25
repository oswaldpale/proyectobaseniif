<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DarBajaActivo.aspx.cs" Inherits="Activos.Modulo.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Baja Activos</title>
    <script src="JS/FormatoPeso.js"></script>
    <style>
        .x-column-header-inner {
            padding: 7px 10px 7px 10px;
            text-overflow: ellipsis;
            font-size: smaller;
        }

        .TTOTALES {
            background-color: white;
            border-color: green;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        var showSummary = true;

        function CALCULARTOTALES() {
            var grilla = App.GPACTIVOIN.getRowsValues();
            var i = 0,
                TIMPORTELIBROS = 0,
                TBASEDEPRECIABLE = 0,
                record;

            for (; i < grilla.length; ++i) {
                TIMPORTELIBROS += grilla[i]['IMPORTELIBROS'];
                TBASEDEPRECIABLE += grilla[i]['BASEDEPRECIABLE'];
            }
            App.TBASEDEPRECIABLE.setValue(TBASEDEPRECIABLE);
            App.TIMPORTELIBROS.setValue(TIMPORTELIBROS);

        };

    </script>
</head>
<body>
    <ext:ResourceManager runat="server" Theme="Neptune" />
    <ext:Hidden ID="HID_USER" runat="server" />
    <ext:Hidden ID="HTIPO" runat="server" />
    <form id="FACTIVO" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>

                    <ext:FormPanel ID="FBAJA" runat="server" Region="Center" Border="true">
                        <Items>
                            <ext:FieldSet runat="server" Padding="5" Width="760" Title="Descripción " Height="110" Layout="ColumnLayout" Margins="5 0 0 0">
                                <Items>
                                    <ext:Container runat="server">
                                        <Items>

                                            <ext:ComboBox ID="CTIPOBAJA" runat="server" AllowBlank="false" DisplayField="Nombre" ForceSelection="true" FieldStyle="text-align: center;"
                                                ValueField="Codigo" Mode="Local" ItemSelector="tr.list-item" Width="270" MarginSpec="3" FieldLabel="Tipo de Baja ">
                                                <Items>
                                                    <ext:ListItem Text="Obsolescencia" Value="OBSOLENCIA" />
                                                    <ext:ListItem Text="Deterioro" Value="DETERIORO" />
                                                </Items>
                                            </ext:ComboBox>
                                            <ext:DateField ID="DFECHA" runat="server" Editable="false" FieldStyle="text-align: center; " FieldLabel="Fecha " MarginSpec="3" Width="270" Format="yyyy-MM-dd" AllowBlank="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container runat="server" Layout="AbsoluteLayout">
                                        <Items>
                                            <ext:TextArea ID="TOBSERVACION" FieldLabel="Observación" runat="server" MarginSpec="3" Width="460" Height="60" EmptyText="Observaciones de la baja" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet runat="server" Flex="1" Border="true" Frame="false" BodyStyle="background:transparent;" Layout="AutoLayout" Padding="5" Title="Detalle" Plain="true">
                                <Items>
                                    <ext:Panel ID="PACTIVO" runat="server" Layout="Column" Border="true">
                                        <Items>
                                            <ext:GridPanel ID="GPACTIVOUT" runat="server" AutoExpandColumn="CVEHI_OBSERVACION" ColumnWidth="0.5" Title="Activos Disponibles" Icon="Basket" EnableDragDrop="true" DDGroup="secondGridDDGroup" Border="true" Height="400" MaxHeight="500" Padding="2">
                                                <Store>
                                                    <ext:Store ID="SACTIVOUT" runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="IDACTIVO" />
                                                                    <ext:ModelField Name="PLACA" />
                                                                    <ext:ModelField Name="NOMBREACTIVO" />
                                                                    <ext:ModelField Name="COSTOINICIAL" />
                                                                    <ext:ModelField Name="BASEDEPRECIABLE" />
                                                                    <ext:ModelField Name="IMPORTELIBROS" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn runat="server" />
                                                        <ext:Column ColumnID="PLACA" DataIndex="PLACA" runat="server" Header="Placa" Flex="2" />
                                                        <ext:Column ColumnID="NOMBREACTIVO" DataIndex="NOMBREACTIVO" runat="server" Header="Nombre" Flex="3" />

                                                        <ext:Column ColumnID="BASEDEPRECIABLE" DataIndex="BASEDEPRECIABLE" runat="server" Header="Base Depreciable" Flex="2">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ColumnID="IMPORTELIBROS" DataIndex="IMPORTELIBROS" runat="server" Header="Importe Libros" Flex="2">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <BottomBar>
                                                    <ext:PagingToolbar runat="server" HideRefresh="true" />
                                                </BottomBar>
                                                <View>
                                                    <ext:GridView runat="server">
                                                        <Plugins>
                                                            <ext:GridDragDrop runat="server" DragGroup="firstGridDDGroup" DropGroup="secondGridDDGroup" />
                                                        </Plugins>
                                                        <Listeners>
                                                            <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                                            <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('NOMBREACTIVO') : ' on empty view'; 
                                                                        Ext.net.Notification.show({title:'Notificación', html:'Quitado '  + data.records[0].get('NOMBREACTIVO') + '(' + data.records[0].get('PLACA') +')'});" />
                                                        </Listeners>
                                                    </ext:GridView>
                                                </View>
                                            </ext:GridPanel>
                                            <ext:GridPanel ID="GPACTIVOIN" runat="server" AutoExpandColumn="CVEHI_OBSERVACION" ColumnWidth="0.5" Title="Activos Para Dar Baja" Icon="BasketAdd" EnableDragDrop="true" DDGroup="firstGridDDGroup" Border="true" Padding="2" Height="400" MaxHeight="500">
                                                <Store>
                                                    <ext:Store ID="SACTIVOIN" runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="IDACTIVO" />
                                                                    <ext:ModelField Name="PLACA" />
                                                                    <ext:ModelField Name="NOMBREACTIVO" />
                                                                    <ext:ModelField Name="COSTOINICIAL" />
                                                                    <ext:ModelField Name="BASEDEPRECIABLE" />
                                                                    <ext:ModelField Name="IMPORTELIBROS" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:RowNumbererColumn runat="server" />
                                                        <ext:Column ColumnID="PLACA" DataIndex="PLACA" runat="server" Header="Placa" Flex="2" />
                                                        <ext:Column ColumnID="NOMBREACTIVO" DataIndex="NOMBREACTIVO" runat="server" Header="Nombre" Flex="3" />
                                                        <ext:Column ColumnID="BASEDEPRECIABLE" DataIndex="BASEDEPRECIABLE" runat="server" Header="Base Depreciable" Flex="2">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ColumnID="IMPORTELIBROS" DataIndex="IMPORTELIBROS" runat="server" Header="Importe Libros" Flex="2">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>

                                                <View>
                                                    <ext:GridView runat="server">
                                                        <Plugins>
                                                            <ext:GridDragDrop runat="server" DragGroup="secondGridDDGroup" DropGroup="firstGridDDGroup" />
                                                        </Plugins>
                                                        <Listeners>
                                                            <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" Delay="1" />
                                                            <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('NOMBREACTIVO') : ' on empty view'; 
                                                                           Ext.net.Notification.show({
                                                                                hideFx     : {
                                                                                    fxName : 'ghost', 
                                                                                    args   : [ 'tr', {} ]
                                                                                },
                                                                                showFx     : {
                                                                                    fxName : 'slideIn', 
                                                                                    args   : [ 
                                                                                    'tr', { 
                                                                                        easing   : 'bounceOut',
                                                                                        duration : 1000
                                                                                        }
                                                                                    ]},                                    
                                                                                alignToCfg : {
                                                                                    offset   : [ -20, 20 ],
                                                                                    position : 'tr-tr'
                                                                                },
                                                                                html:'Agregado '  + data.records[0].get('NOMBREACTIVO') + '(' + data.records[0].get('PLACA') +')',
                                                                                title : 'Notificación'
                                                                            }); 
                                                                            CALCULARTOTALES();
                                                             " />
                                                        </Listeners>
                                                    </ext:GridView>

                                                </View>
                                                <BottomBar>
                                                    <ext:Toolbar ID="Toolbar2" runat="server">
                                                        <Items>
                                                            <ext:ToolbarFill runat="server" />
                                                            <ext:Label runat="server" Flex="3" />
                                                            <ext:TextField runat="server" ID="TBASEDEPRECIABLE" Flex="2" FieldCls="TTOTALES"
                                                                Selectable="false">
                                                                <Listeners>
                                                                    <AfterRender Fn="peso" Delay="250" />
                                                                    <Change Fn="peso" Delay="250" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                            <ext:TextField runat="server" ID="TIMPORTELIBROS" Flex="2" FieldCls="TTOTALES"
                                                                Selectable="false">
                                                                <Listeners>
                                                                    <AfterRender Fn="peso" Delay="250" />
                                                                    <Change Fn="peso" Delay="250" />
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                        <FooterBar>
                            <ext:Toolbar runat="server" ID="asdas">
                                <Items>
                                    <ext:Button runat="server" ID="btn_reporte" Text="REPORTE..." Icon="Printer" Scale="Small" FormBind="true">
                                        <Listeners>
                                            <Click Handler="if (#{FBAJA}.getForm().isValid()) {App.direct.GenerarReporteBaja(#{GPACTIVOIN}.getStore().getRecordsValues());}else{'true'}" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarFill />
                                    <ext:Button runat="server" ID="btn_guardar" Text="GUARDAR" Icon="Disk" Scale="Small">
                                        <Listeners>
                                            <Click Handler="if (#{FBAJA}.getForm().isValid()){ App.direct.GuardarBaja(#{GPACTIVOIN}.getStore().getRecordsValues()); App.GPACTIVOIN.disable(true); }else{'true'} " />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </FooterBar>
                    </ext:FormPanel>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
