window.onload = function() {
    fetchSets();
};

function fetchSets() {
    fetch('/api/Sets')
        .then(response => response.json())
        .then(data => {
            let table = document.getElementById('setsTable');
            table.innerHTML = '';
            data.forEach(set => {
                let row = table.insertRow();
                let nameCell = row.insertCell();
                nameCell.textContent = set.name;
                let codeCell = row.insertCell();
                codeCell.textContent = set.code;
                let releaseDateCell = row.insertCell();
                releaseDateCell.textContent = set.releaseDate;
            });
        });
}

document.getElementById('refreshButton').addEventListener('click', fetchSets);