$(document).ready(function () {
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
                render: DataTable.render.datetime('D/M/YYYY')
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

    // Bagian AddAccount
    function addAccount() {
        var employee = new Object();
        employee.FirstName = $("#firstNameInputAdd").val();
        employee.LastName = $("#lastNameInputAdd").val();
        employee.BirthDate = $("#birthDateInputAdd").val();
        employee.Gender = parseInt($("#genderInputAdd").val());
        employee.HiringDate = $("#hiringInputAdd").val();
        employee.Email = $("#emailInputAdd").val();
        employee.PhoneNumber = $("#phoneInputAdd").val();
        employee.Password = $("#passwordInputAdd").val();
        employee.PasswordConfirm = $("#passwordConfirmInputAdd").val();

        console.log(employee)

        const token = $("#token").data("token");
        console.log(token)

        $.ajax({
            url: "https://localhost:7257/api/Employee",
            type: "POST",
            data: JSON.stringify(employee),
            dataType: "json",
            contentType: 'application/json;charset=utf-8',
            headers: { "Authorization": 'Bearer ' + token }
        }).done((result) => {
            console.log(result.message)
            $("#modalPoke").modal(`hide`);
            Swal.fire(
                'Added!',
                'Your file has been added.',
                'success'
            )
            tableEmployee.ajax.reload();
        }).fail((error) => {
            if (error.responseJSON) {
                console.log(error.responseJSON.message)
                alert(error.responseJSON.message);
            } else {
                alert("Terjadi kesalahan dalam permintaan.");
            }
        });
    };

    $("#addAccountSubmit").click(() => {
        addAccount();
    })
});


