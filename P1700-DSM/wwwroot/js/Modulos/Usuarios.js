$(document).ajaxStart(function () {
    $("#loader-overlay").show();
}).ajaxStop(function () {
    $("#loader-overlay").hide();
});

function IniciarSesion() {

    $.ajax({
        type: "POST",
        url: "/Usuario/IniciarSesion/",
        data: $("#frmLogin").serialize(),
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "html",
        success: function (response) {
            window.location.href = 'Home';
        },
        failure: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        },
        error: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        }
    });
}

function NuevoUsuario() {
    $.ajax({
        type: "POST",
        url: "/Usuario/RegistroPartial/",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#partialModal").find(".modal-body").html(response);
            $("#partialModal").modal('show');
        },
        failure: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        },
        error: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        }
    });
}

function RegistrarUsuario() {
    $.ajax({
        type: "POST",
        url: "/Usuario/Registrar/",
        data: $("#frmUsuario").serialize(),
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "html",
        success: function (response) {
            var modalElement = document.getElementById('partialModal');
            var modalInstance = bootstrap.Modal.getInstance(modalElement);
            if (modalInstance) {
                modalInstance.hide();
            }

            Swal.fire({
                title: response,
                text: "",
                icon: "success"
            });
        },
        failure: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        },
        error: function (response) {
            Swal.fire({
                title: response.responseText,
                text: "",
                icon: "error"
            });
        }
    });

}