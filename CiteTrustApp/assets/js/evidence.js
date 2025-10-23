document.addEventListener('click', function (e) {
    var btn = e.target.closest('.copy-citation');
    if (!btn) return;

    var card = btn.closest('.evidence-card');
    if (!card) return;

    // Clone to avoid changing DOM; remove buttons/controls from clone
    var clone = card.cloneNode(true);
    var controls = clone.querySelectorAll('.copy-citation, .analysis-button, button, a');
    controls.forEach(function (el) { el.remove(); });

    // Get visible text
    var text = clone.innerText.trim();
    if (!text) return;

    // Use Clipboard API with fallback
    if (navigator.clipboard && navigator.clipboard.writeText) {
        navigator.clipboard.writeText(text).then(function () {
            flashButton(btn, 'Đã chép');
        }, function () {
            fallbackCopy(text, btn);
        });
    } else {
        fallbackCopy(text, btn);
    }
});

function fallbackCopy(text, btn) {
    var ta = document.createElement('textarea');
    ta.value = text;
    ta.style.position = 'fixed';
    ta.style.left = '-9999px';
    document.body.appendChild(ta);
    ta.select();
    try {
        document.execCommand('copy');
        flashButton(btn, 'Đã chép');
    } finally {
        document.body.removeChild(ta);
    }
}

function flashButton(btn, msg) {
    var orig = btn.textContent;
    btn.textContent = msg;
    setTimeout(function () { btn.textContent = orig; }, 1500);
}