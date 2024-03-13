const manaCostInput = document.getElementById("mana-cost-input");
const updateButton = document.getElementById("update-button");

async function updateCard(id, updatedCard) {
    const response = await fetch(`/api/cards/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedCard)
    });

    if (response.status === 401) {
        console.error('Unauthorized. Please log in to update the card.');
        return;
    }

    if (response.ok) {
        const updatedCard = await response.json();
        console.log(updatedCard);
    } else {
        console.error(`Failed to update card with ID ${id}`);
    }
}

updateButton.addEventListener("click", () => {
    const id = updateButton.dataset.id;
    const updatedCard = {
        manaCost: manaCostInput.value
    };
    updateCard(id, updatedCard);
});