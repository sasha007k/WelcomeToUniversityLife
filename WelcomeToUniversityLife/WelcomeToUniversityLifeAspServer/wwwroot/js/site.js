function doSomething(obj) {

    var btnValue = obj.options[obj.selectedIndex].value;
    var x = obj.name

    if (x === 'MarksModel.SecondZnoModel.Name') {
        remove('MarksModel.FourZnoModel.Name', btnValue);
        remove('MarksModel.ThirdZnoModel.Name', btnValue);

    } else if (x === 'MarksModel.ThirdZnoModel.Name') {
        remove('MarksModel.SecondZnoModel.Name', btnValue);
        remove('MarksModel.FourZnoModel.Name', btnValue);

    } else if (x === 'MarksModel.FourZnoModel.Name') {
        remove('MarksModel.SecondZnoModel.Name', btnValue);
        remove('MarksModel.ThirdZnoModel.Name', btnValue);

    }
}

function remove(id, value) {
        var selectobject = document.getElementById(id);
        for (var i = 0; i < selectobject.length; i++) {
            if (selectobject.options[i].value == value)
                selectobject.remove(i);
        }
}


function uploadphotoclicked() {
    let fileinput = document.getElementById("fileinputid");

    fileinput.onchange = function (e) {
        document.getElementById("photoform").submit();
    }
}