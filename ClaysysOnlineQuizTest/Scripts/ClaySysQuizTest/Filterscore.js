function sortResults() {
    const container = document.getElementById('resultsContainer');
    const rows = Array.from(container.getElementsByClassName('table-row'));
    const sortOrder = document.getElementById('sortOrder').value;


    rows.sort((a, b) => {
        const scoreA = parseFloat(a.getAttribute('data-score'));
        const scoreB = parseFloat(b.getAttribute('data-score'));

        return sortOrder === 'asc' ? scoreA - scoreB : scoreB - scoreA;
    });


    container.innerHTML = container.querySelector('.table-header').outerHTML;
    rows.forEach(row => {
        container.appendChild(row);
    });
}