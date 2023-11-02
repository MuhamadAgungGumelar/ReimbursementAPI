$(document).ready(function () {
    const token = $("#token").data("token");
    const table = $("#table_ManagerReimbursement").DataTable({
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
                data: "employeeName"
            },
            {
                data: "name"
            },
            {
                data: "createdDate",
            },
            {
                data: "status",
                render: function (data, type, row, meta) {
                    // Daftar opsi status
                    const statusOptions = {
                        0: "waiting_manager_approval_reimburse",
                        1: "waiting_finance_approval_reimburse",
                        3: "reimburse_rejected_by_manager"
                    };

                    // Membuat select option
                    return `<select class="form-select w-45" id="status_id_manager${meta.row}">

                              <option value="0"${data === 0 ? " selected" : ""}>waiting_manager_approval_reimburse</option>
                              <option value="1"${data === 1 ? " selected" : ""}>waiting_finance_approval_reimburse</option>
                              <option value="3"${data === 3 ? " selected" : ""}>reimburse_rejected_by_manager</option>
                            </select>`;
                }
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-link text-primary text-sm mb-0 px-0 ms-0" data-bs-toggle="modal" data-bs-target="#invoiceModal">
                                <i class="fas fa-file-pdf text-lg me-1" aria-hidden="true"></i>
                            </button>`;
                }
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `<button class="btn btn-link text-info text-sm mb-0 px-0 ms-0 approve-manager" onclick="updateManager('${data.guid}','${meta.row}')" data-id="${row.id}"><i class="ni ni-send"></i></button>`;
                }
            },
        ]
    });
});


function updateManager(guid, row) {
    const token = $("#token").data("token");
    let data
    $.ajax({
        url: "https://localhost:7257/api/Reimbursement/" + guid,
        method: "GET",
        contentType: 'application/json',
        dataType: "json",
        beforeSend: (req) => {
            req.setRequestHeader("Authorization", "Bearer " + token)
        },
        async: false,
        //success: (data) => {
        //    return data.data
        //}
    }).done((resp) => {
        data = resp.data
    }).fail((error) => {
        console.log(error);
    })


    data.status = parseInt($("#status_id_manager"+row).val());

    console.log(data)

    $.ajax({
        url: "https://localhost:7257/api/Reimbursement/",
        method: "PUT",
        contentType: 'application/json',
        dataType: "json",
        beforeSend: (req) => {
            req.setRequestHeader("Authorization", "Bearer " + token)
        },
        async: false,
        data: JSON.stringify(data)
        //success: (data) => {
        //    return data.data
        //}
    }).done((resp) => {
        Swal.fire({
            title: 'Success!',
            text: resp.message,
            icon: 'success',
            confirmButtonText: 'OK!'
        })
        $('#employeeTable').DataTable().ajax.reload();
    }).fail((result) => {
        $("#failMessage").removeClass("alert-danger, alert-warning, alert-success").addClass("alert-danger").text(result.responseJSON.message[1] /*+ ", " + "All Field must be set"*/).show();
    })
}

