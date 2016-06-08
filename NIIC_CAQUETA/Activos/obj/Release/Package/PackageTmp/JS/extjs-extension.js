/*
* Ext.grid.Panel
*/
Ext.grid.Panel.prototype.applyFilter = function () {
    var store = this.getStore(),
        cols = this.columns,
        fieldsFilter = ['textfield', 'numberfield', 'datefield', 'textareafield', 'combobox', 'checkboxfield', 'radiofield'],
        fields = [];
    // FilterTypes
    filterString = function (value, dataIndex, record) {
        var val = record.get(dataIndex);

        if (typeof val != 'string')
            val = String(val);
        if (typeof value != 'string')
            value = String(value);
        //return value.length == 0;
        return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
    };

    var filterDate = function (value, dataIndex, record) {
        var temp = record.get(dataIndex);

        if (temp != null && typeof value != 'string') {
            var val = Ext.Date.clearTime(record.get(dataIndex), true).getTime();

            if (!Ext.isEmpty(value, false) && val != Ext.Date.clearTime(value, true).getTime()) {                
                return false;
            }
            return true;
        }
        
    };

    // Return fields components filter
    getHeaderField = function (container) {
        for (var i = 0, item; item = container[i]; i++) {
            var _t = item.getXType();
            if (fieldsFilter.indexOf(_t) != -1) {
                return item;
            }
            else {
                if (_t == 'container') {
                    return getHeaderField(item.items.items);
                }
                else {
                    return undefined;
                }
            }
        }
    };
    // Return records filters
    getRecordFilter = function (fields) {
        return function (record) {
            for (var i = 0, field; field = fields[i]; i++) {
                var _row = false;
                if (field.column.xtype == 'datefield')
                    _row = filterDate(field.column.getValue(), field.data, record) || filterDate(field.column.getRawValue(), field.data, record);
                else
                    if (field.column.xtype == 'combobox')
                        _row = filterString(field.column.getValue(), field.data, record) || filterString(field.column.getRawValue(), field.data, record);
                    else
                        _row = filterString(field.column.getValue(), field.data, record);
                if (!_row) return false;

            }
            return true;
        };
    };
    Ext.Array.each(cols, function (column, index, array) {
        var _items = column.items.items;

        if (!Ext.isEmpty(_items)) {
            var cmp = getHeaderField(_items);

            if (!Ext.isEmpty(cmp)) {
                fields.push({ column: cmp, data: column.dataIndex });
            }
        }
    });
    store.filterBy(getRecordFilter(fields));
    store.applyPaging();
}
/*
* Ext.form.field.File
*/
Ext.form.field.File.prototype.getImageDataURL = function () {
    var _reader,
        target = Ext.getElementById(this.id).getElementsByTagName('input').namedItem(this.id);
    if (target.files.length > 0) {
        var f = target.files[0];
        if (!f.type.match('image.*')) {
            throw Error("El archivo no posee un formato de imagen.");
        }
        else {
            _reader = new FileReader();
            _reader.readAsDataURL(f);
        }
    }
    return _reader;
}

Ext.form.field.File.prototype.getImageData = function (store) {
    var dataReader = 0,
        data = [],
        target = Ext.getElementById(this.id).getElementsByTagName('input').namedItem(this.id);

    readData = function (r, f) {
        var t = setInterval(function () {
            if (r.readyState == r.DONE) {
                f.src = r.result;
                store.add(f);
                clearInterval(t);
            }
        }, 50);
    }

    if (target.files.length > 0) {
        for (var i = 0; i < target.files.length; i++) {
            var file = target.files[i];
            if (!file.type.match('image.*')) {
                throw Error("Uno de los archivos no posee un formato de imagen.");
            }
            else {
                var f = { key: null, caption: file.name, src: '', status: false, action: 0 },
                    _reader = new FileReader();
                _reader.readAsDataURL(file);
                readData(_reader, f);
                dataReader++;
            }
        }
    }
}