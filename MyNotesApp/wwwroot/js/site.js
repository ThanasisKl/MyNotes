/*function changeBtnsText(condition) {
    if (condition.matches) { // If media query matches
        document.getElementById("AddNewFolderBtn").innerHTML = '<i class="bi bi-folder-plus"></i>';
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus">';
        document.getElementById("AddNewFileBtn").style.fontSize = "1.3em";
        document.getElementById("AddNewFolderBtn").style.fontSize = "1.3em";
        document.getElementById("AddNewFolderBtn").style.marginBottom = "15px";
        document.getElementById("AddNewFileBtn").style.marginBottom = "15px";
        document.getElementById("AddNewFolderBtn").style.marginRight = "20px";

        document.getElementById("AddNewFileBtn2").innerHTML = '<i class="bi bi-file-earmark-plus">';
        document.getElementById("AddNewFileBtn2").style.fontSize = "1.3em";
        document.getElementById("AddNewFileBtn").style.marginBottom = "15px";
    } else {
        document.getElementById("AddNewFileBtn").style.fontSize = "1em";
        document.getElementById("AddNewFileBtn2").style.fontSize = "1em";
        document.getElementById("AddNewFolderBtn").style.fontSize = "1em";
        document.getElementById("AddNewFolderBtn").innerHTML = '<i class="bi bi-folder-plus"></i> &nbsp; Create New Folder';
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus"> &nbsp; Create New Note';
        document.getElementById("AddNewFileBtn2").innerHTML = '<i class="bi bi-file-earmark-plus"> &nbsp; Create New Note';
    }

}

window.addEventListener('load', (event) => {
    let condition = window.matchMedia("(max-width: 1050px)");
    changeBtnsText(condition); 
    condition.addListener(changeBtnsText);
});
*/