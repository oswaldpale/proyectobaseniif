// Formato de mascara de precio $

var peso = function (el, newValue, oldValue) {

    var dato = el.getValue();

    if (dato != "") {
        var g = dato.replace(/[\.]/g, '');
        el.setValue(Ext.util.Format.number(g, '0,0'));
    }
};

