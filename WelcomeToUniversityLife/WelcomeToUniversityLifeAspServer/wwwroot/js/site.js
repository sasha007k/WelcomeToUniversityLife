function uploadphotoclicked() {
    let fileinput = document.getElementById("fileinputid");

    fileinput.onchange = function (e) {
        document.getElementById("photoform").submit();
    }

    fileinput.click();
}
