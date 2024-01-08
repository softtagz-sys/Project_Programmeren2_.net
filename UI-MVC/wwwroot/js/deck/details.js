const cardTableBody = document.getElementById("card-table-body");
const cardSelect = document.getElementById("card-select");
const quantityInput = document.getElementById("quantity-input");
const addedOnInput = document.getElementById("added-on-input");
const addButton = document.getElementById("add-button");

function getDeckEntryId() {
    const urlSearchParams = new URLSearchParams(window.location.search);
    let deckEntryId = urlSearchParams.get("id");
    if (!deckEntryId) {
        const path = window.location.pathname;
        deckEntryId = path.substring(path.lastIndexOf("/") + 1);
    }
    console.log(deckEntryId)
    return parseInt(deckEntryId);
}

async function loadCardsWithDecks() {
    const response = await fetch(`/api/DeckEntries/${getDeckEntryId()}/deckentries`, {
        headers: {
            Accept: "application/json"
        }
    });

    if (response.status === 200) {
        /**
         * @type {[{card: string, quantity: number, addedOn: Date}]} 
         */
        const deckentries = await response.json();

        cardTableBody.innerHTML = "";
        for (const deckentry of deckentries) {
            cardTableBody.innerHTML += `
                <tr>
                    <td>${deckentry.card}</td>
                    <td>${deckentry.quantity}</td>
                    <td>${new Date(deckentry.addedOn).toLocaleDateString()}</td>
                </tr>
                `;
        }
    } else {
        cardTableBody.innerHTML = `
                <tr>
                    <td colspan="3">No cards were found!</td>
                </tr>
                `
    }
}

async function loadAllCards() {
    const response = await fetch(`/api/cards`, {
        headers: {
            Accept: "application/json"
        }
    });
    if (response.status === 200) {
        const cards = await response.json();

        cardSelect.innerHTML = "";
        for (const card of cards) {
            cardSelect.innerHTML += `
                <option value="${card.id}">${card.name}</option>
                `
        }
    }
}

async function addNewCard() {
    console.log(cardSelect.value, quantityInput.value, addedOnInput.value);
    const response = await fetch('/api/DeckEntries/',
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                deckId: getDeckEntryId(),
                cardId: parseInt(cardSelect.value),
                quantity: parseInt(quantityInput.value),
                addedOn: addedOnInput.value
            })
        }
    );
    if (response.status === 200) {
        await loadCardsWithDecks();
    }
}

loadCardsWithDecks()
loadAllCards()

addButton.addEventListener("click", addNewCard);