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
                            deleteBookFromQueue(bookId, stockLevel)
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
                if (sum >= 6) {
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

function deleteBookFromQueue(bookId, stockLevel) {
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