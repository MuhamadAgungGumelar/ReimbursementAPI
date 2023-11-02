$(document).ready(function () {
    const token = $("#token").data("token");
    $("#table_ManagerEmployee").DataTable({
        ajax: {
            url: "https://localhost:7257/api/Employee",
            dataSrc: "data",
            dataType: "JSON",
            type: 'GET',
            headers: { "Authorization": 'Bearer ' + token }
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return row.firstName + ' ' + (row.lastName ? row.lastName : '');
                }
            },
            {
                data: "birthDate"
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    return row.gender === 1 ? "Laki-laki" : "Perempuan";
                }
            },
            {
                data: "hiringDate"
            },
            {
                data: "email"
            },
            {
                data: "phoneNumber"
            }
        ]
    });
});