$(document).ready(function () {
    const token = $("#token").data("token");
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
                data: "status",
                render: function (data, type, row) {
                    if (row.status === 0) {
                        return "waiting_manager_approval_reimburse"
                    } else if (row.status === 1) {
                        return "waiting_finances_approval_reimburse"
                    } else if (row.status === 2) {
                        return "reimburse_approved"
                    } else if (row.status === 3) {
                        return "reimburse_rejected_by_manager"
                    } else if (row.status === 4) {
                        return "reimburse_rejected_by_finances"
                    }
                }
            },
            {
                data: "createdDate",
                render: DataTable.render.datetime('D/M/YYYY')
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-link text-primary text-sm mb-0 px-0 ms-0" data-bs-toggle="modal" data-bs-target="#invoiceModal">
                                <i class="fas fa-file-pdf text-lg me-1" aria-hidden="true"></i>
                            </button>`;
                }
            },
        ]

    });
});


