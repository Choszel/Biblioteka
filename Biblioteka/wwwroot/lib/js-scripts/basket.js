const out = document.getElementById("out");
const table = document.createElement("TABLE");
const btn = document.getElementById("but1");

const form3 = document.getElementById("form3");
const dlg3 = document.getElementById("dialog3");
const form5 = document.getElementById("form5");
const dlg5 = document.getElementById("dialog5");

btn.onclick = () => {
    var storedBooks = localStorage.getItem("books");

    if (localStorage.length == 1 || storedBooks == null || storedBooks.length == 2) {
        form3.reset();
        dlg3.showModal();
    }
    else window.location.href = "/Rentals/Place";
}

form3.onsubmit = () => {
    dlg3.close();
}

let index = localStorage.length - 1;

let x = 0;

table.appendChild(addCaption());
table.appendChild(addHeader());
table.appendChild(addBody());
table.appendChild(addFooter());

out.appendChild(table);

const lowermessage = document.getElementById("low");

const btn2 = document.getElementById("but2");
const form2 = document.getElementById("form2");
const dlg2 = document.getElementById("dialog2");
const form4 = document.getElementById("form4");
const dlg4 = document.getElementById("dialog4");

btn2.onclick = () => {
    form2.reset();
    dlg2.showModal();
}

form2.onsubmit = () => {
    const modif = JSON.parse(localStorage.getItem(-1));
    localStorage.clear();
    localStorage.setItem((-1).toString(),
        JSON.stringify(
            {
                modif: modif
            }));
    location.reload();
}

form2.onreset = () => {
    dlg2.close();
}

form4.onsubmit = () => {
    removeProduct();
}

form4.onreset = () => {
    dlg4.close();
}

function removeProduct() {
    var storedBooks = localStorage.getItem("books");
    var books = {};

    if (storedBooks) books = JSON.parse(storedBooks);

    delete books[x];

    localStorage.setItem("books", JSON.stringify(books));
    location.reload();
}

function addCaption() {
    const caption = document.createElement("CAPTION");
    let today = new Date();
    caption.innerHTML = "KOSZYK " + today.getDate() + "-" + (today.getMonth() + 1) + "-" + (today.getFullYear() % 100);
    return caption;
}

function addHeader() {
    const thead = document.createElement("THEAD");
    const headerRow = thead.insertRow();
    const headers = ["ZDJĘCIE", "NAZWA", "ILOŚĆ"];

    for (let i = 0; i < headers.length; ++i) {
        let th = document.createElement("TH");
        if (i == 1) {
            th.style.textAlign = "left";
        }
        if (i == 2) {
            th.style.textAlign = "right";
        }
        th.style.width = "100px";
        th.innerHTML = headers[i];
        headerRow.appendChild(th);
    }

    return thead;
}

function addBody() {
    const tbody = document.createElement("TBODY");

    var storedBooks = localStorage.getItem("books");

    if (storedBooks) {
        var books = JSON.parse(storedBooks);

        for (var bookId in books) {
            if (books.hasOwnProperty(bookId)) {
                let obj = new Object();
                for (const element of bookList) {
                    if (element.bookId == bookId) {
                        obj = element;
                        break;
                    }
                    else;
                }

                var quantity = books[bookId];

                const headerRow = tbody.insertRow();
                let td = document.createElement("TD");
                let img = document.createElement("img");
                img.src = obj.bookPhoto;
                img.style = "width: 85px; height: 120px;";

                td.appendChild(img);
                headerRow.appendChild(td);

                td = document.createElement("TD");
                td.style.textAlign = "left";
                td.innerHTML = obj.title;
                headerRow.appendChild(td);

                td = document.createElement("TD"); //ilość
                td.style.textAlign = "right";

                const div = document.createElement("div");
                const but4 = document.createElement("BUTTON");
                but4.innerHTML = "+";
                but4.id = "ba" + bookId; //ButtonAdd
                but4.classList.add("customButton");
                but4.onclick = function () {
                    x = obj.bookId;
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
                        $.ajax({
                            url: '/api/Queue',
                            type: 'GET',
                            success: function (data) {
                                console.log('Dane pobrane:', data);

                                var stockLevel = obj.stockLevel;
                                for (var bookQueue in data) {
                                    if (bookQueue != obj.bookId) continue;
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

                                            // Usuwanie rekordu, jeśli różnica dni jest większa niż 2 dni
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
                                stockLevel = stockLevel - parseInt(books[obj.bookId]);
                                console.log('stockLevel', stockLevel);

                                if (stockLevel > 0) {
                                    books[obj.bookId] = books[obj.bookId] + 1;
                                    localStorage.setItem("books", JSON.stringify(books));

                                    location.reload();
                                }
                                else {
                                    const form6 = document.getElementById("form6");
                                    const dlg6 = document.getElementById("dialog6");

                                    form6.reset();
                                    dlg6.showModal();

                                    form6.onsubmit = () => {
                                        dlg6.close();
                                    }
                                }
                            }
                        });
                    }
                };

                const but3 = document.createElement("BUTTON");
                but3.innerHTML = "-";
                but3.id = "bd" + bookId;
                but3.classList.add("customButton");
                but3.onclick = function () {
                    x = obj.bookId;
                    var storedBooks = localStorage.getItem("books");
                    var books = {};

                    if (storedBooks) books = JSON.parse(storedBooks);
                    if (books[obj.bookId] == 1) {
                        form4.reset();
                        dlg4.showModal();
                    }
                    else {
                        books[obj.bookId] = books[obj.bookId] - 1;
                        localStorage.setItem("books", JSON.stringify(books));

                        location.reload();
                    }
                };
                but3.style = but4.style = "margin-top: 15px; margin-left: 5px; margin-right: 5px;";

                div.innerHTML += quantity;
                div.appendChild(but3);
                div.appendChild(but4);

                td.appendChild(div);
                headerRow.appendChild(td);
            }
        }
    }
    return tbody;
}

function addFooter() {
    const tfoot = document.createElement("TFOOT");
    const footerRow = tfoot.insertRow();
    let remainingCell = document.createElement("TH");
    remainingCell.colSpan = 3;
    footerRow.appendChild(remainingCell);

    for (let i = 0; i < 2; ++i) {
        let th = document.createElement("TH");
        if (i == 0) th.innerHTML = "CZAS WYPOŻYCZENIA";
        else {
            //console.log(limits);
            if (limits[0].limitTimeTaken <= 4) th.innerHTML = limits[0].limitTimeTaken + " tygodnie";
            else if (limits[0].limitTimeTaken == 1) th.innerHTML = limits[0].limitTimeTaken + " tygodnień";
            else th.innerHTML = limits[0].limitTimeTaken + " tygodni";
        }
        footerRow.appendChild(th);
    }

    return tfoot;
}