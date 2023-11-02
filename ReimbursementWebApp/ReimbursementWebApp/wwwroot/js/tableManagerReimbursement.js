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
                    return `<select class="form-select w-45" id="status_id">
                              <option value="0"${data === 0 ? " selected" : ""}>waiting_manager_approval_reimburse</option>
                              <option value="1"${data === 1 ? " selected" : ""}>waiting_finance_approval_reimburse</option>
                              <option value="2"${data === 3 ? " selected" : ""}>reimburse_rejected_by_manager</option>
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
                    return `<button class="btn btn-link text-info text-sm mb-0 px-0 ms-0 approve-manager" onclick="update('${data.guid}')" data-id="${row.id}"><i class="ni ni-send"></i></button>`;
                }
            },
        ]
    });



    // Event handler for the "Acc" button
    //$("#table_ManagerReimbursement").on("click", ".approve-manager", function (e) {
    //    const token = $("#token").data("token");

    //    const indexRow = $(e.target).closest(".approve-manager").attr("data-id");
    //    const currentRow = $("#table_ManagerReimbursement").DataTable().row(indexRow).data();

    //    const newStatus = parseInt($(this).closest("tr").find("select").val());

    //    const managerApprove = new Object();

    //    managerApprove.guid = currentRow.guid,
    //        managerApprove.employeeGuid = currentRow.employeeGuid,
    //        managerApprove.name = currentRow.name,
    //        managerApprove.description = currentRow.description,
    //        managerApprove.value = currentRow.value,
    //        managerApprove.imageType = currentRow.imageType,
    //        managerApprove.image = currentRow.image,
    //        managerApprove.status = newStatus,
    //        managerApprove.createdDate = currentRow.createdDate

    //    $.ajax({
    //        url: `https://localhost:7257/api/Reimbursement`,
    //        type: "PUT",
    //        data: JSON.stringify(managerApprove),
    //        dataType: "json",
    //        contentType: "application/json;charset=utf-8",
    //        headers: { "Authorization": 'Bearer ' + token }
    //    }).done(function (result) {
    //        // Handle successful response, you can reload the table here
    //        console.log(result)
    //        table.ajax.reload();
    //        Swal.fire("Edited!", "Your file has been edited.", "success");
    //    }).fail(function (error) {
    //        if (error.responseJSON) {
    //            console.log(error.responseJSON.error);
    //            alert(error.responseJSON.error);
    //        } else {
    //            alert("Terjadi kesalahan dalam permintaan.");
    //        }
    //    });
    //});
});

function update(guid) {
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

    data.status = parseInt($("#status_id").val());

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


