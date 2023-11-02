$(document).ready(function () {
    const token = $("#token").data("token");
    console.log(token)
    $("#table_Reimbursement").DataTable({
        ajax: {
            url: "https://localhost:7257/api/Reimbursement",
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
                data: "name"
            },
            {
                data: "description"
            },
            {
                data: "status"
            },
            {
                data: "date",
            },
            {
                data: "null",
                render: function (data, type, row, meta) {
                    return ``;
                }
            }
        ]

    });
});


