function changeBtnsText(condition) {
    if (condition.matches) { // If media query matches
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus">';
        document.getElementById("AddNewFileBtn").style.fontSize = "1.3em";
        document.getElementById("AddNewFileBtn").style.marginBottom = "15px";
    } else {
        document.getElementById("AddNewFileBtn").style.fontSize = "1em";
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus"> &nbsp; Create New Note';
    }

}
let condition = window.matchMedia("(max-width: 1050px)");
changeBtnsText(condition);
condition.addListener(changeBtnsText);