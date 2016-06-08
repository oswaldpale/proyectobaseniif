/*
* Config static variables extjs
*/
Ext.onReady(function () {
    // Currency
    var f = Ext.util.Format;
    f.currencyPrecision = 2;
    f.currencySign = "$";
    f.decimalSeparator = ",";
    f.thousandSeparator = ".";
    f.currencyAtEnd = false;
    // Date
    Ext.Date.defaultFormat = 'Y-m-d';
}, this);

/*
* Config static variables app
*/
var config = {
    requiredField: {
        title: 'Alerta',
        html: 'No se puede realizar la acción por que existen campos no válidos en el formulario',
        ui: 'warning'
    },
    notAvailable: {
        title: 'Alerta',
        html: 'Esta opción no se encuentra disponible',
        ui: 'warning'
    },
    requiredGrid: function (msg) {
        return {
            title: 'Alerta',
            html: msg,
            ui: 'warning'
        }
    },
    imgBase: '/Content/images/1401498333_upload.png'
};

function fileUploadChange(me, image) {
    var src = me.getImageDataURL(),
        t = setInterval(function () {
            if (t.readyState == t.DONE) {
                var img = Ext.getElementById(image.id).getElementsByTagName('img')[0];
                img.src = src.result;
                clearInterval(t);
            }
        }, 100);
}

/*
* Render checked column image 
*/
function RenderColumnChecked(value, chk, uchk, ps) {
    var params = {
        checked: chk || '../Content/images/checkbox.png',
        unchecked: uchk || '../Content/images/checkbox.png',
        pos: ps || '15px ' + (value ? '-15px' : '0')
    },
    _tpl = "<div style='background-image:url({0});background-position:{1};width:15px;height:15px;margin-left:-5px;'/>";
    return Ext.String.format(_tpl, value ? params.checked : params.unchecked, params.pos);
}

/*NEW*/
function ViewImageStore(store, item) {
    var st = new Ext.data.Store({
        id: 'imagesStore',
        fields: ['caption', 'src'],
        pageSize: 1,
        data: store.getRecordsValues(),
        proxy: {
            type: 'pagingmemory'
        }
    });

    st.loadPage(item || 1);

    var imageTpl = new Ext.XTemplate(
                    '<tpl for=".">',
                        '<div class="thumb-wrap">',
                            '<div class="thumb"><img src="{src}" title="{caption}"></div>',
                        '</div>',
                    '</tpl>',
                    '<div class="x-clear"></div>'
    );

    Ext.create('Ext.window.Window', {
        //title: data.nombre,
        modal: true,
        //maximized: true,
        maximizable: true,
        shadow: false,
        height: 550,
        width: 900,
        layout: 'anchor',
        cls: "images-view",
        items: {
            xtype: 'dataview',
            store: st,
            tpl: imageTpl,
            itemSelector: 'div.thumb-wrap',
            overItemCls: 'x-view-over',
            emptyText: 'No images available'
        },
        style: {
            border: 0
        },
        dockedItems: [{
            xtype: 'pagingtoolbar',
            store: st,
            dock: 'bottom',
            displayInfo: true,
            hideRefresh: true
        }]
    }).show();
}
