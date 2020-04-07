function doSomething(obj) {


    var btnValue = obj.options[obj.selectedIndex].value;
    var x = obj.name
    alert(x)

    if (x === 'MarksModel.SecondZno.Name') {
        remove('MarksModel.FourZno.Name', btnValue);
        remove('MarksModel.ThreedZno.Mark', btnValue)

    } else if (x === 'MarksModel.ThreedZno.Mark') {

        remove('MarksModel.SecondZno.Name', btnValue);
        remove('MarksModel.FourZno.Name', btnValue)
    } else if (x === 'MarksModel.FourZno.Name') {
        remove('MarksModel.SecondZno.Name', btnValue);
        remove('MarksModel.ThreedZno.Mark', btnValue)

    }


    function remove(id, value) {
        var selectobject = document.getElementById(id);
        for (var i = 0; i < selectobject.length; i++) {
            if (selectobject.options[i].value == value)
                selectobject.remove(i);
        }

    }
}
