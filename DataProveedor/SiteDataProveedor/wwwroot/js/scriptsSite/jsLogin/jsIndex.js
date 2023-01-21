$(document).ready(function () {
    console.log("ready!");
    //$.toaster({ priority: 'success', title: 'Title', message: 'Your message here' });
    SetHrefUserRegister();
});

function SetHrefUserRegister() {
    $('#registrateAqui').attr('href', urlUserRegister);
}

function Login() {

    var userMail = $('#userMail').val();
    var password = $('#password').val();
    var confirmar = document.getElementById('confirmar').checked;

    if (userMail === '' || password === '' || !confirmar) {
        toastr.warning("Todos los campos son requeridos");
        return false;
    }


    $.ajax({
        type: "GET",
        url: urlLogin,
        data: { userMail: userMail, password: password },
        datatype: "json",
        success: function (data) {
            if (data.estado) {
                toastr.success(data.mensaje);
                setTimeout(IrHome, 2000);
            }
            else
                toastr.warning(data.mensaje);
        }
    });
    return false;
}

function IrHome() {
    window.location.href = urlHome;
}