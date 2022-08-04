//Makes buttons responsive

function changeBtnsText(condition) {
    if (condition.matches) { // If media query matches

        //Add Note and folder buttons
        document.getElementById("AddNewFolderBtn").innerHTML = '<i class="bi bi-folder-plus"></i>';
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus">';
        document.getElementById("AddNewFileBtn").style.fontSize = "1.3em";
        document.getElementById("AddNewFolderBtn").style.fontSize = "1.3em";
        document.getElementById("AddNewFolderBtn").style.marginBottom = "15px";
        document.getElementById("AddNewFileBtn").style.marginBottom = "15px";
        document.getElementById("AddNewFolderBtn").style.marginRight = "20px";

        //Edit, Delete, View note/folder buttons
        let viewButtons = document.getElementsByClassName("view");
        for (let i = 0; i < viewButtons.length; i++) {
            viewButtons[i].innerHTML = `<i class="bi bi-stickies"></i>`;
        }

        let deleteButtons = document.getElementsByClassName("delete");
        for (let i = 0; i < deleteButtons.length; i++) {
            deleteButtons[i].innerHTML = `<i class="bi bi-trash-fill"></i>`;
        }

        let editButtons = document.getElementsByClassName("edit");
        for (let i = 0; i < editButtons.length; i++) {
            editButtons[i].innerHTML = `<i class="bi bi-pencil-square"></i>`;
        }
    } else {
        //Add Note and folder buttons
        document.getElementById("AddNewFileBtn").style.fontSize = "1em";
        document.getElementById("AddNewFolderBtn").style.fontSize = "1em";
        document.getElementById("AddNewFolderBtn").innerHTML = '<i class="bi bi-folder-plus"></i> &nbsp; Create New Folder';
        document.getElementById("AddNewFileBtn").innerHTML = '<i class="bi bi-file-earmark-plus"> &nbsp; Create New Note';

        //Edit, Delete, View note/folder buttons
        let viewButtons = document.getElementsByClassName("view");
        for (let i = 0; i < viewButtons.length; i++) {
            viewButtons[i].innerHTML = `<i class="bi bi-stickies"></i>View`;
        }

        let deleteButtons = document.getElementsByClassName("delete");
        for (let i = 0; i < deleteButtons.length; i++) {
            deleteButtons[i].innerHTML = `<i class="bi bi-trash-fill"></i>Delete`;
        }

        let editButtons = document.getElementsByClassName("edit");
        for (let i = 0; i < editButtons.length; i++) {
            editButtons[i].innerHTML = `<i class="bi bi-pencil-square"></i>Edit`;
        }
    }

}
let condition = window.matchMedia("(max-width: 1050px)");
changeBtnsText(condition);
condition.addListener(changeBtnsText);