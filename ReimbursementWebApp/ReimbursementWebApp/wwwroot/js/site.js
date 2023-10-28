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
        });
    });
});
