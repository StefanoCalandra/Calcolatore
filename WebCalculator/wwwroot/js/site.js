const calcBtn = document.getElementById('calcBtn');
const output = document.getElementById('output');

calcBtn.addEventListener('click', async () => {
    const left = Number(document.getElementById('left').value);
    const right = Number(document.getElementById('right').value);
    const operator = document.getElementById('op').value;

    const response = await fetch('/api/calculator/calculate', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ left, right, operator })
    });

    const payload = await response.json();

    if (!response.ok) {
        output.textContent = `Errore: ${payload.error ?? 'Operazione non riuscita.'}`;
        return;
    }

    output.textContent = `Risultato: ${payload.result}`;
});
