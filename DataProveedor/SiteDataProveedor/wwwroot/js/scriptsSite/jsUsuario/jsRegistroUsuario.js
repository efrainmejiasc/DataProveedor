$(document).ready(function () {
    console.log("ready!");
    $('#nombre').val('');
    $('#apellido').val('');
    $('#userName').val('');
    $('#mail').val('');
    $('#password').val('');
    $('#password2').val('');
});

var usuario = {

    Id: 0,
    Nombre: '',
    Apellido: '',
    UserName: '',
    Email: '',
    Password: '',
    RolId: 0,
};

function PostUsuario() {

    var nombre = $('#nombre').val();
    var apellido = $('#apellido').val();
    var userName = $('#userName').val();
    var mail = $('#mail').val();
    var password = $('#password').val();
    var password2 = $('#password2').val();
    var rolId = 0;

    if (nombre === '') {
        toastr.warning('Debe ingresar el nombre');
        return false;
    } else if (apellido === '') {
        toastr.warning('Debe ingresar el apellido');
        return false;
    }
    else if (userName === '') {
        toastr.warning('Debe ingresar un nombre de usuario');
        return false;
    }
    else if (mail === '') {
        toastr.warning('Debe ingresar una direccion de correo');
        return false;
    }
    else if (!EmailValido(mail)) {
        toastr.warning(mail + ' no es una direccion de correo valida');
        return false;
    }
    else if (password === '') {
        toastr.warning('Debe ingresar la contraseña');
        return false;
    }
    else if (password != password2) {
        toastr.warning('Las contraseñas deben ser identicas');
        return false;
    }
    else if (password.length < 8) {
        toastr.warning('La contraseña debe estar formada por al menos ocho caracteres');
        return false;
    }

    var mayuscula = false;
    var minuscula = false;
    var numero = false;
 

    for (var i = 0; i < password.length; i++)
    {
        if (password.charCodeAt(i) >= 65 && password.charCodeAt(i) <= 90)
            mayuscula = true;
        else if (password.charCodeAt(i) >= 97 && password.charCodeAt(i) <= 122)
            minuscula = true
        else if (password.charCodeAt(i) >= 48 && password.charCodeAt(i) <= 57)
            numero = true
    }

    if (mayuscula === false || minuscula === false || numero === false) {
        toastr.warning('La contraseña debe ser una combinacion de letras mayusculas, minusculas y numeros');
        return false;
    }

    usuario = {

        Id: 0,
        Nombre: nombre,
        Apellido: apellido,
        UserName: userName,
        Email: mail,
        Password: password,
        RolId: 0,
    };

    $.ajax(
        {
            type: "POST",
            data: JSON.stringify(usuario),
            url: urlPostUsuario,
            cache: false,
            contentType: 'application/json',
            success: function (modelo) {
                console.log(modelo);
                if (modelo.estado) {
                    toastr.success(modelo.mensaje);
                  //  setTimeout(NavToLogin, 2000);
                }
                else {
                    toastr.error(modelo.mensaje);
                }
            }, error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR + ' ' + textStatus + ' ' + errorThrown)
            }
        }
    );

    return false;
}

function EmailValido(mail) {
    const regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(mail);
}

function NavToLogin() {
    window.location.href = urlUserLogin;
}
