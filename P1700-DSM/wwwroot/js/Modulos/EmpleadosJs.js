﻿$(document).ajaxStart(function () {
    $("#loader-overlay").show();
}).ajaxStop(function () {
    $("#loader-overlay").hide();
});

function NuevoEmpleado() {
    $.ajax({
        type: "POST",
        url: "/Empleados/CrearPartial/",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#partialModal").find(".modal-body").html(response);
            $("#partialModal").modal('show');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function RegistrarEmpleado() {

    $.ajax({
        type: "POST",
        url: "/Empleados/Crear/",
        data: $("form").serialize(),
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "html",
        success: function (response) {

            var modalElement = document.getElementById('partialModal');
            var modalInstance = bootstrap.Modal.getInstance(modalElement);

            if (modalInstance) {
                modalInstance.hide();
            }

            Swal.fire({
                title: "Registrado correctamente",
                showDenyButton: false,
                showCancelButton: false,
                confirmButtonText: "Ok",
                denyButtonText: `Don't save`,
                icon: "success"
            }).then((result) => {
                if (result.isConfirmed) {
                    $("#loader-overlay").show();
                    window.location.href = 'Empleados';
                } 
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

function EditarEmpleado() {
    var selectedEmployee = document.querySelector('input[name="selectedEmployee"]:checked');

    if (selectedEmployee) {
        var idEmpleado = selectedEmployee.value;

        $.ajax({
            type: "GET",
            url: "/Empleados/ActualizarPartial?idEmpleado=" + idEmpleado,
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

    } else {
        Swal.fire("Seleccione un empleado.");
    }
}

function ActializarEmpleado() {

    $.ajax({
        type: "POST",
        url: "/Empleados/Actualizar/",
        data: $("form").serialize(),
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "html",
        success: function (response) {

            var modalElement = document.getElementById('partialModal');
            var modalInstance = bootstrap.Modal.getInstance(modalElement);
            if (modalInstance) {
                modalInstance.hide();
            }

            Swal.fire({
                title: "Actualizado correctamente",
                showDenyButton: false,
                showCancelButton: false,
                confirmButtonText: "Ok",
                denyButtonText: `Don't save`,
                icon: "success"
            }).then((result) => {
                if (result.isConfirmed) {
                    $("#loader-overlay").show();
                    window.location.href = 'Empleados';
                }
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

function Procesar() {

    var modo = $("#ModoEdicion").val();
    if (modo == "Crear") {
        RegistrarEmpleado();
    } else if (modo == "Editar") {
        ActializarEmpleado();
    }
}

function EliminarEmpleado() {
    var selectedEmployee = document.querySelector('input[name="selectedEmployee"]:checked');

    if (selectedEmployee) {
        var idEmpleado = selectedEmployee.value;

        Swal.fire({
            title: "¿Desea eliminar el empleado seleccionado?",
            showDenyButton: false,
            showCancelButton: true,
            confirmButtonText: "Eliminar",
            denyButtonText: `Cancelar`
        }).then((result) => {

            if (result.isConfirmed) {

                $.ajax({
                    type: "GET",
                    url: "/Empleados/Eliminar?idEmpleado=" + idEmpleado,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (response) {
                        Swal.fire({
                            title: "Eliminado correctamente",
                            showDenyButton: false,
                            showCancelButton: false,
                            confirmButtonText: "Ok",
                            denyButtonText: `Don't save`,
                            icon: "success"
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $("#loader-overlay").show();
                                window.location.href = 'Empleados';
                            }
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
        });
    } else {
        Swal.fire("Seleccione un empleado.");
    }
}

$("#tblEmpleados").DataTable({
    "language": {
        "emptyTable": "No hay registros",
        "search": "Buscar",
        "searchPlaceholder": "Búsqueda rápida",
        "decimal": "",
        "emptyTable": "No hay datos disponibles",
        "info": "Mostrando _START_ a _END_ de _TOTAL_ items",
        "infoEmpty": "Mostrando 0 items",
        "infoFiltered": "(filtrado de _MAX_ total items)",
        "infoPostFix": "",
        "thousands": ",",
        "lengthMenu": "Mostrar _MENU_ items",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "paginate": {
            "first": "Primera",
            "last": "Última",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    },
    select: true
});


