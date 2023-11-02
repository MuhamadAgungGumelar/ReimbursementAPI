$(document).ready(function () {
    // Memilih semua tautan navigasi dalam navbar
    const navLinks = $("aside.sidenav div.collapse ul.navbar-nav li.nav-item a.nav-link");

    // Memilih elemen konten dinamis
    const dynamicContent = $("#dynamic_content");
    const pageLink1 = $("main.main-content nav.navbar div.container-fluid nav.nav-pages ol.breadcrumb li.active");
    const pageLink2 = $("main.main-content nav.navbar div.container-fluid nav.nav-pages h6");

    // Menambahkan event click pada tautan di dalam navbar
    navLinks.click(function (e) {
        e.preventDefault();

        // Menghapus kelas "active" dari semua tautan navigasi
        navLinks.removeClass("active");

        // Menambahkan kelas "active" pada tautan yang diklik
        $(this).addClass("active");

        const pageName = $(this).text();

        // Mengambil URL yang sesuai dari atribut href
        const url = $(this).attr("href");

        // Mengirim permintaan AJAX
        $.get(url, function (data) {
            // Ekstrak konten yang sesuai dari respons AJAX
            const newContent = $(data).find("#container-fluid");

            // Memuat konten ke dalam elemen konten dinamis
            dynamicContent.html(newContent);

            // Perbarui breadcrumb atau judul halaman seperti yang telah Anda lakukan
            pageLink1.text(pageName);
            pageLink2.text(pageName);

            history.pushState(null, null, url);

            //Inisialisasi Tabel-tabel
            tableReimbursement();
            tableAdmin();
            tableReimbursementManager();
            tableReimbursementFinance();
        });
    });
});

function tableReimbursement() {
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
                data: "status"
            },
            {
                data: "createdDate",
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
};

function tableAdmin() {
    const token = $("#token").data("token");
    $("#table_Admin").DataTable({
        ajax: {
            url: "https://localhost:7257/api/Employee/employeeDetails",
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
            },
            {
                data: "isActivated"

            }
        ]
    });
};

function tableReimbursementManager() {
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
                    return `<button class="btn btn-link text-info text-sm mb-0 px-0 ms-0 approve-manager" onclick="update('${data.guid}')" data-id="${meta.row}"><i class="ni ni-send"></i></button>`;
                }
            },
        ]
    });
}

function tableReimbursementFinance() {
    const token = $("#token").data("token");
    const table = $("#table_FinanceReimbursement").DataTable({
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
                        1: "waiting_finance_approval_reimburse",
                        2: "reimburse_approved",
                        3: "reimburse_rejected_by_manager",
                        4: "reimburse_rejected_by_finances"
                    };

                    // Membuat select option
                    return `<select class="form-select w-45" id="status_id">
                              <option value="1"${data === 1 ? " selected" : ""}>waiting_finance_approval_reimburse</option>
                              <option value="2"${data === 2 ? " selected" : ""}>reimburse_approved</option>
                              <option value="3"${data === 3 ? " selected" : ""}>reimburse_rejected_by_manager</option>
                              <option value="4"${data === 4 ? " selected" : ""}>reimburse_rejected_by_finances</option>
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
                    return `<button class="btn btn-link text-info text-sm mb-0 px-0 ms-0 approve-manager" onclick="update('${data.guid}')" data-id="${meta.row}"><i class="ni ni-send"></i></button>`;
                }
            },
        ]
    });
}
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
