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


