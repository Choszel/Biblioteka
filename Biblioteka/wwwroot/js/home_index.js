
if (JSON.parse(localStorage.getItem(-1)) == null) {
    let modif = 0;
    localStorage.setItem((-1).toString(),
        JSON.stringify(
            {
                modif: modif
            }));
}

var $select;

const form = document.getElementById("form1");
const dlg = document.getElementById("dialog1");
const form2 = document.getElementById("form2");
const dlg2 = document.getElementById("dialog2");
const form3 = document.getElementById("form3");
const dlg3 = document.getElementById("dialog3");
const form4 = document.getElementById("form4");
const dlg4 = document.getElementById("dialog4");
const form5 = document.getElementById("form5");
const dlg5 = document.getElementById("dialog5");


$(document).ready(function () {
    $select = $('#select-to').selectize({
        persist: false,
        maxItems: null,
        optgroupField: 'title',
        valueField: 'bookId',
        labelField: 'title',
        searchField: ['title', 'authors', 'isbn', 'releaseDate'],
        sortField: 'title',
        onInitialize: function () {
            loadBooksAJAX();
        },
        onChange: function (value) {
            loadBooksAJAX();
        },
        render: {

            item: function (item, escape) {
                return (
                    "<div>" +
                    (item.title ? '<span class="name">' + escape(item.title) + "</span>" : "") +
                    "</div>"
                );
            },
            option: function (item, escape) {

                var photoHtml = item.bookPhoto ? '<img src="' + escape(item.bookPhoto) + '" alt="' + escape(item.title) + '" class="searchBookImage" />' : '';
                var authorsHtml = item.authors ? '<span class="authors">' + escape(item.authors.join(', ')) + '</span>' : '';


                return '<div class="selectDiv">' +
                    '<span class="mainName">' + photoHtml + '</span>' +
                    '<span class="subLabel">' + escape(item.title) + '</span>' +
                    '<span class="subAuthors">' + authorsHtml + '</span>' +
                    '</div>';
            }
        }
    });

    function sortBooks(sortField) {
        var container = $(".okienko").parent();
        var booksArray = $(".okienko").toArray();

        booksArray.sort(function (a, b) {
            var aValue;
            var bValue;

            if (sortField === "0") {
                const indexnegative = JSON.parse(localStorage.getItem(-1));
                let modif = 0;
                localStorage.setItem((-1).toString(),
                    JSON.stringify(
                        {
                            modif: modif
                        }));
                aValue = $(a).find(".booktitle").text();
                bValue = $(b).find(".booktitle").text();
                // Sortowanie rosnące
                return aValue.localeCompare(bValue);
            } else if (sortField === "1") {
                const indexnegative = JSON.parse(localStorage.getItem(-1));
                let modif = 1;
                localStorage.setItem((-1).toString(),
                    JSON.stringify(
                        {
                            modif: modif
                        }));
                aValue = $(a).find(".booktitle").text();
                bValue = $(b).find(".booktitle").text();
                // Sortowanie malejące
                return bValue.localeCompare(aValue);
            } else if (sortField === "2") {
                const indexnegative = JSON.parse(localStorage.getItem(-1));
                let modif = 2;
                localStorage.setItem((-1).toString(),
                    JSON.stringify(
                        {
                            modif: modif
                        }));
                aValue = $(a).find(".releaseDatehidden").text();
                bValue = $(b).find(".releaseDatehidden").text();
                return bValue.localeCompare(aValue);
            } else if (sortField === "3") {
                const indexnegative = JSON.parse(localStorage.getItem(-1));
                let modif = 3;
                localStorage.setItem((-1).toString(),
                    JSON.stringify(
                        {
                            modif: modif
                        }));
                aValue = $(a).find(".releaseDatehidden").text();
                bValue = $(b).find(".releaseDatehidden").text();
                return aValue.localeCompare(bValue);
            }


            return 0;
        });

        container.empty();
        container.append(booksArray);
    }

    // Event handler for sorting when the select changes
    $("#sortSelect").change(function () {
        var selectedSortField = $(this).val();
        sortBooks(selectedSortField);
    });

    const indexnegative = JSON.parse(localStorage.getItem(-1));
    sortBooks(indexnegative.modif.toString());
});

function submitSearchForm() {
    var selectedValues = $select[0].selectize.getValue();
    console.log(selectedValues);

    // Przypisz wartość do ukrytego pola formularza
    $('#selectedBooksInput').val(selectedValues);

    console.log(selectedBooksInput.selectedValues);

    // Złóż formularz
    $('#searchForm').submit();
}

function loadBooksAJAX() {
    $.ajax({
        url: '/Home/GetBooks',
        type: 'POST',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $select[0].selectize.addOption(data[i]);
                $select[0].selectize.refreshOptions();
            }
        },
        error: function (response) {
            alert('Błąd: ' + response)
        }
    });
}

function addProduct(bookId, stockLevel) {
    console.log("\nuserId " + userId + "\n" + stockLevel + "\n");
    $.ajax({
        url: '/api/Queue',
        type: 'GET',
        success: function (data) {
            console.log('Dane pobrane:', data);

            for (var bookQueue in data) {
                if (bookQueue != bookId) continue;
                else {
                    var booksArray = data[bookQueue];
                    console.log('booksArray ', booksArray);

                    var currentDate = new Date();
                    booksArray.forEach(book => {
                        console.log('quantity ', book.Quantity + "\ndate " + book.Date);

                        var dateParts = book.Date.split(".");
                        var bookDate = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);

                        // Obliczanie różnicy w dniach między datami
                        var timeDiff = currentDate - bookDate;
                        var daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));

                        // Usuwanie rekordu, jeśli różnica dni jest większa niż 2
                        if (daysDiff > 3) {
                            console.log('Usuwanie rekordu: ', book);

                            var queueData = {
                                BookId: bookQueue,
                                UserId: book.UserId,
                                Quantity: 0
                            };

                            $.ajax({
                                url: `/api/Queue/${queueData.BookId}`,
                                type: 'DELETE',
                                contentType: 'application/json',
                                data: JSON.stringify(queueData),
                                success: function (data) {
                                    if (data.success) {
                                        console.log(data.message);
                                    } else {
                                        console.error(data.message);
                                    }
                                },
                                error: function (error) {
                                    console.error(`Error: ${error.statusText}`);
                                }
                            });
                        }
                        else {
                            stockLevel = stockLevel - parseInt(book.Quantity);
                        }
                    });
                }
                //bookQueue to id książki w queue
            }
            console.log('stockLevel', stockLevel);

            if (parseInt(stockLevel) < 1 && userId != null) {
                form3.reset();
                dlg3.showModal();

                form3.onsubmit = () => {
                    var data = {
                        bookId: parseInt(bookId),
                        userId: parseInt(userId),
                        quantity: 1 // Załóżmy, że dodajemy jedną pozycję do kolejki
                    };
                    console.log(data);
                    fetch('/api/Queue', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    })
                        .then(response => {
                            if (!response.ok) {
                                throw new Error(`HTTP error! Status: ${response.status}`);
                            }
                            return response.json();
                        })
                        .then(data => {
                            console.log(data);
                            if (data.success) {
                                dlg3.close();
                                form4.reset();
                                dlg4.showModal();
                            } else {
                                console.error(data.message);
                            }
                        })
                        .catch(error => {
                            console.error('Fetch Error:', error);
                        });
                }

                form3.onreset = () => {
                    dlg3.close();
                }
            }
            else if (parseInt(stockLevel) < 1 && userId == null) {
                form2.reset();
                dlg2.showModal();
            }

            else {
                var storedBooks = localStorage.getItem("books");
                var books = {};
                if (storedBooks) books = JSON.parse(storedBooks);
                var sum = 0;

                if (storedBooks) {
                    Object.keys(books).forEach(function (key) {
                        sum += books[key];
                    });

                    console.log("Sum of values:", sum);
                }

                if (sum >= limits[0].limitWaiting) {
                    form5.reset();
                    dlg5.showModal();
                }
                else {
                    if (books[bookId] != null) books[bookId] = books[bookId] + 1;
                    else books[bookId] = 1;

                    localStorage.setItem("books", JSON.stringify(books));

                    let modif = (localStorage.getItem(-1).modif) + 1;
                    localStorage.setItem((-1).toString(),
                        JSON.stringify(
                            {
                                modif: modif
                            }));

                    form.reset();
                    dlg.showModal();
                }
            }
        },
        error: function (error) {
            console.error('Błąd pobierania danych:', error);
        }
    });


}

function filterElements() {
    var container = $(".okienko").parent();
    var booksArray = $(".okienko").toArray();
    booksArray.forEach(book => { $(book).show(); });

    var selectedValues = $(".checkboxFilter:checked").map(function () {
        return $(this).val();
    }).get();

    console.log(selectedValues);

    booksArray.forEach(book => {
        var yearValue = $(book).find(".releaseDatehidden").text();
        var categoryValue = $(book).find(".category").text().trim();
        // console.log(yearValue);
        // console.log(categoryValue);
        // console.log(selectedValues.length);

        if ((selectedValues.length > 0) && !(
            (selectedValues.includes("2021") && yearValue.includes(2021)) ||
            (selectedValues.includes("2022") && yearValue.includes(2022)) ||
            (selectedValues.includes("2023") && yearValue.includes(2023)) ||
            (selectedValues.includes("2024") && yearValue.includes(2024)) ||
            (selectedValues.includes(categoryValue)))
        ) {
            $(book).hide();
            // console.log("2021 " + !(selectedValues.includes("2021")) && yearValue.includes(2021));
            // console.log("2022 " + !(selectedValues.includes("2022")) && yearValue.includes(2022));
            // console.log("2023 " + !(selectedValues.includes("2023")) && yearValue.includes(2023));
            // console.log("2024 " + !(selectedValues.includes("2024")) && yearValue.includes(2024));
            // console.log("cat " + !(selectedValues.includes(categoryValue)));
            // console.log("długość " + selectedValues.length > 0);
        }
    });

    container.empty();
    container.append(booksArray);
}