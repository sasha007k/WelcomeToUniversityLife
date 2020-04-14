function doSomething(obj) {

    var btnValue = obj.options[obj.selectedIndex].value;
    var x = obj.name;

    if (x === "MarksModel.SecondZnoModel.Name") {
        remove("MarksModel.FourZnoModel.Name", btnValue);
        remove("MarksModel.ThirdZnoModel.Name", btnValue);

    } else if (x === "MarksModel.ThirdZnoModel.Name") {
        remove("MarksModel.SecondZnoModel.Name", btnValue);
        remove("MarksModel.FourZnoModel.Name", btnValue);

    } else if (x === "MarksModel.FourZnoModel.Name") {
        remove("MarksModel.SecondZnoModel.Name", btnValue);
        remove("MarksModel.ThirdZnoModel.Name", btnValue);

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

    fileinput.onchange = function(e) {
        document.getElementById("photoform").submit();
    };
}

function inputchanged() {
    let inputValue = document.getElementById("searchspec").value;
    let url = `https://localhost:44336/UniversityAdmin/SearchSpeciality?filter=${inputValue}`;
    $.get(url, function (res) {
        removeAllChildren("searchdiv");

        let maindiv = document.getElementById("searchdiv");

        for (let i = 0; i < res.length; ++i) {
            let atag = createATag();

            atag.onclick = getSpecialityInfo;

            let divtag = createDivTag();
            let contentdiv = createContentDivTag();
            let htag = createHTag();
            htag.id = `${res[i].id}`;

            htag.innerHTML = `${res[i].universityName}:${res[i].facultyName}:${res[i].specialityName}`;
            contentdiv.appendChild(htag);
            divtag.appendChild(contentdiv);
            atag.appendChild(divtag);

            maindiv.append(atag);
        }
    });
}


function removeAllChildren(id) {
    let element = document.getElementById(id);
    while (element.firstChild) {
        element.removeChild(element.lastChild);
    }
}

function createATag() {
    let atag = document.createElement("a");
    atag.className = "dropdown-item dropitem m-0 p-0 w-100";

    return atag;
}

function createDivTag() {
    let divtag = document.createElement("div");
    divtag.className = "row m-0 p-1 mt-1";

    return divtag;
}

function createContentDivTag() {
    let divtag = document.createElement("div");

    divtag.className = "col-sm mt-1 ml-3 overflow-auto";
    divtag.id = "searchname";
    return divtag;
}

function createHTag() {
    let htag = document.createElement("h5");
    htag.className = "dropdowntext m-0 p-0";

    return htag;
}

function getSpecialityInfo(event) {
    removeAllChildren("searchdiv");
    console.log(event.target.id);
}