function doSomething(obj) {


    var btnValue = obj.options[obj.selectedIndex].value;
    var x = obj.name

    if (x === 'secondsub') {
        remove('foursub', btnValue);
        remove('thirdsub', btnValue)

    } else if (x === 'thirdsub') {

        remove('secondsub', btnValue);
        remove('foursub', btnValue)
    } else if (x === 'foursub') {
        remove('secondsub', btnValue);
        remove('thirdsub', btnValue)

    }

}


function remove(id, value) {
    var selectobject = document.getElementById(id);
    for (var i = 0; i < selectobject.length; i++) {
        if (selectobject.options[i].value == value)
            selectobject.remove(i);
    }

}
