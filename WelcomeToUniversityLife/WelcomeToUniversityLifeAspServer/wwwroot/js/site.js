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

    if (inputValue != "") {

        let url = `/UniversityAdmin/SearchSpeciality?filter=${inputValue}`;
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

function createTrTag() {
    let tag = document.createElement("tr");
    tag.style.textAlign = "center";

    return tag;
}

function createPtag(content) {
    let tag = document.createElement("p");
    tag.innerHTML = content;
    tag.className = "m-0 p-0 ratingcontent";

    return tag;
}

function createTdTag() {
    return document.createElement("td");
}

function getSpecialityInfo(event) {
    let searchInput = document.getElementById("searchspec");
    searchInput.value = "";
    removeAllChildren("searchdiv");
    removeAllChildren("ratingbody");

    let url = `/UniversityAdmin/GetSpecialityRatingInfo?id=${event.target.id}`;

    $.get(url, function (res) {

        let ratingspeciality = document.getElementById("ratingspeciality");
        ratingspeciality.innerHTML = `${res.speciality.name}: Free spaces (${res.speciality.freeSpaces}) Paid spaces (${res.speciality.paidSpaces})`;


        let tablebody = document.getElementById("ratingbody");

        let spaces = res.speciality.freeSpaces + res.speciality.paidSpaces;

        for (var index = 0; index < res.requests.length; ++index) {
            let tr = createTrTag();
            if (index + 1 <= res.speciality.freeSpaces) {
                tr.className = "#66bb6a green lighten-1 m-0 p-0";
            }
            else if (index + 1 <= spaces ) {
                tr.className = "#e6ee9c lime lighten-3 m-0 p-0";

            }
            else {
                tr.className = "#f1f8e9 light-green lighten-5 m-0 p-0";
            }

            let tdplace = createTdTag();
            let place = createPtag(index+1);
            tdplace.appendChild(place);

            let tdemail = createTdTag();
            let pemail = createPtag(res.requests[index].userEmail);
            tdemail.appendChild(pemail);

            let tdmark = createTdTag();
            let pmark = createPtag(res.requests[index].averageMark);
            tdmark.appendChild(pmark);

            tr.appendChild(tdplace);
            tr.appendChild(tdemail);
            tr.appendChild(tdmark);

            tablebody.append(tr);
        }

        while (index + 1 <= spaces) {
            let tr = createTrTag();
            tr.className = "#f1f8e9 light-green lighten-5 m-0 p-0";
            let tdplace = createTdTag();
            let place = createPtag(index + 1);
            tdplace.appendChild(place);

            let tdemail = createTdTag();
            let pemail = createPtag("");
            tdemail.appendChild(pemail);

            let tdmark = createTdTag();
            let pmark = createPtag("");
            tdmark.appendChild(pmark);

            tr.appendChild(tdplace);
            tr.appendChild(tdemail);
            tr.appendChild(tdmark);

            tablebody.append(tr);

            index += 1;
        }
    });
}

function invokepassportinput() {
    document.getElementById("passportinput").click();
}

function invokeschoolinput() {
    document.getElementById("schoolcertificateinput").click();
}

function invokeznoinput() {
    document.getElementById("znoinput").click();
}

function docchanged(id,doc) {
    let elem = document.getElementById(id);
    elem.innerText = `${doc} Loaded`;
    elem.style.fontWeight = "500";
}