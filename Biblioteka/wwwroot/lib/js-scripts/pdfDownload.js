var btnDownload = document.getElementById("btnDownloadPdf");

btnDownload.onclick = function () {
    console.log(pdfBytesBase64);

    // Dekodowanie danych base64 do tablicy bajtów
    var pdfBytes = new Uint8Array(atob(pdfBytesBase64).split('').map(char => char.charCodeAt(0)));
    console.log(pdfBytes);

    const blob = new Blob([pdfBytes], { type: 'application/pdf' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = bookTitleForPDF + ".pdf"; // Nazwa pliku do pobrania

    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);

    URL.revokeObjectURL(url);
}