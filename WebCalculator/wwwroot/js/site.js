const calcBtn = document.getElementById('calcBtn');
const clearBtn = document.getElementById('clearBtn');
const backspaceBtn = document.getElementById('backspaceBtn');
const output = document.getElementById('output');
const historyList = document.getElementById('history');
const leftInput = document.getElementById('left');
const rightInput = document.getElementById('right');
const operatorInput = document.getElementById('op');

const HISTORY_LIMIT = 10;
const history = [];

function addHistory(entry) {
    history.unshift(entry);
    if (history.length > HISTORY_LIMIT) {
        history.pop();
    }

    historyList.innerHTML = '';
    for (const item of history) {
        const li = document.createElement('li');
        li.textContent = item;
        historyList.appendChild(li);
    }
}

function activeNumericInput() {
    const active = document.activeElement;
    if (active === leftInput || active === rightInput) {
        return active;
    }
    return rightInput;
}

function clearErrorState() {
    leftInput.classList.remove('field-error');
    rightInput.classList.remove('field-error');
}

function validateInputs(leftRaw, rightRaw) {
    clearErrorState();

    if (leftRaw.trim() === '' || Number.isNaN(Number(leftRaw))) {
        leftInput.classList.add('field-error');
        output.textContent = 'Errore: inserisci un primo numero valido.';
        return false;
    }

    if (rightRaw.trim() === '' || Number.isNaN(Number(rightRaw))) {
        rightInput.classList.add('field-error');
        output.textContent = 'Errore: inserisci un secondo numero valido.';
        return false;
    }

    return true;
}

async function calculate() {
    const leftRaw = leftInput.value;
    const rightRaw = rightInput.value;

    if (!validateInputs(leftRaw, rightRaw)) {
        return;
    }

    const left = Number(leftRaw);
    const right = Number(rightRaw);
    const operator = operatorInput.value;

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
    addHistory(`${left} ${operator} ${right} = ${payload.result}`);
}

function clearCalculator() {
    leftInput.value = '0';
    rightInput.value = '0';
    operatorInput.value = '+';
    output.textContent = '';
    clearErrorState();
    rightInput.focus();
}

function backspaceCurrentInput() {
    const input = activeNumericInput();
    input.value = input.value.slice(0, -1);
}

calcBtn.addEventListener('click', calculate);
clearBtn.addEventListener('click', clearCalculator);
backspaceBtn.addEventListener('click', backspaceCurrentInput);

window.addEventListener('keydown', (event) => {
    if (event.key === 'Enter') {
        event.preventDefault();
        calculate();
        return;
    }

    if (event.key === 'Escape') {
        event.preventDefault();
        clearCalculator();
        return;
    }

    if (event.key === 'Backspace' && document.activeElement !== leftInput && document.activeElement !== rightInput) {
        event.preventDefault();
        backspaceCurrentInput();
    }

    if (['+', '-', '*', '/'].includes(event.key)) {
        operatorInput.value = event.key;
    }
});
