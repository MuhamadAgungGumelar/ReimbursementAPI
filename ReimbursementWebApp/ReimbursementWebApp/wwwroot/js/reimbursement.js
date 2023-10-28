$(document).ready(function () {
    // Single Page untuk Reimbursement Form
    const reqReimburse = $("div.container-fluid div.row div.col-lg-12 a#Req_Reimburse");

    // Memilih elemen konten dinamis
    const dynamicContent2 = $("#dynamic_content");
    const pageLink1 = $("main.main-content nav.navbar div.container-fluid nav.nav-pages ol.breadcrumb li.active");
    const pageLink2 = $("main.main-content nav.navbar div.container-fluid nav.nav-pages h6");

    reqReimburse.on("click", function (e) {
        e.preventDefault();

        const pageName2 = $(this).text();

        const url2 = $(this).attr("href");
        console.log(pageName2)

        $.get(url2, function (data) {
            // Ekstrak konten yang sesuai dari respons AJAX
            console.log(data)
            const newContent2 = $(data).find("#container-fluid");

            // Memuat konten ke dalam elemen konten dinamis
            dynamicContent2.html(newContent2);

            // Perbarui breadcrumb atau judul halaman seperti yang telah Anda lakukan
            pageLink1.text(pageName2);
            pageLink2.text(pageName2);

            history.pushState(null, null, url2);

        });
    });
});

function previewImage(event) {
    const selectedImage = document.getElementById("selectedImage");
    const fileInput = event.target;

    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    } else {
        // Jika tidak ada file yang dipilih, kembalikan gambar placeholder
        selectedImage.src = "https://mdbootstrap.com/img/Photos/Others/placeholder.jpg";
    }
}