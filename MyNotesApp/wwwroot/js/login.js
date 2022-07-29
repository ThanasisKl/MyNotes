document.getElementById("header").innerHTML = `
                <a style="font-size:1.3em" class="navbar-brand">MyNotes</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item" id="home">
                            <a style="color:white" class="nav-link">Home</a>
                        </li>
                        <li class="nav-item" id="contact">
                            <a style="color:white" class="nav-link">Contact</a>
                        </li>
                         <li class="nav-item" id="notes">
                            <a style="color:white" class="nav-link">Notes</a>
                        </li>
                    </ul>
                </div>`;

function changeInputsWidth(condition) {
    const inputs = document.querySelectorAll('.inputs');
    if (condition.matches) {
        inputs.forEach(input => {
            input.style.width = '100%';
            input.style.margin = "0 auto";
        });
    } else {
        inputs.forEach(input => {
            input.style.width = '60%';
            input.style.margin = "0 auto";
        });
    }

}
let condition = window.matchMedia("(max-width: 1050px)");
changeInputsWidth(condition);
condition.addListener(changeInputsWidth);

document.getElementById("password-icon").addEventListener("click", changePasswordIcon);

function changePasswordIcon() {
    let password = document.getElementById("password");
    if (password.type == "password") {
        password.type = "text";
        document.getElementById("password-icon").innerHTML = `<i class="bi bi-eye-slash-fill" id="togglePassword" style="cursor: pointer"></i>`;
    } else {
        password.type = "password";
        document.getElementById("password-icon").innerHTML = `<i class="bi bi-eye-fill" id="togglePassword" style="cursor: pointer"></i>`;
    }
}