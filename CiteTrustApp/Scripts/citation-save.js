(function () {
    function getAntiForgeryToken() {
        var el = document.querySelector('input[name="__RequestVerificationToken"]');
        return el ? el.value : null;
    }

    function ajaxPost(url, data, cb) {
        var token = getAntiForgeryToken();
        var xhr = new XMLHttpRequest();
        xhr.open('POST', url, true);
        xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
        if (token) xhr.setRequestHeader('RequestVerificationToken', token);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                var resp = null;
                try { resp = JSON.parse(xhr.responseText); } catch (e) { }
                if (cb) cb(resp);
            }
        };
        xhr.send(JSON.stringify(data));
    }

    function copyToClipboard(text, onSuccess, onError) {
        if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(text).then(onSuccess).catch(onError);
            return;
        }
        // fallback
        var ta = document.createElement('textarea');
        ta.value = text;
        ta.style.position = 'fixed';
        ta.style.left = '-9999px';
        document.body.appendChild(ta);
        ta.select();
        try {
            var ok = document.execCommand('copy');
            document.body.removeChild(ta);
            if (ok) onSuccess();
            else onError();
        } catch (e) {
            document.body.removeChild(ta);
            onError();
        }
    }

    // helper to decode an HTML-encoded string into plain text
    function decodeHtmlEntities(encodedStr) {
        if (!encodedStr) return "";
        var container = document.createElement('div');
        container.innerHTML = encodedStr;
        return container.textContent || container.innerText || "";
    }

    function handleCopyClick(btn, ev) {
        ev && ev.preventDefault && ev.preventDefault();

        // prefer data-citation attribute on the button
        var dataCitation = btn.getAttribute && btn.getAttribute('data-citation');
        var html = "";

        if (dataCitation) {
            // decode possible HTML-encoded value
            html = decodeHtmlEntities(dataCitation);
        } else {
            // otherwise look for nearest citation-html container
            var wrapper = btn.closest('.citation-block') || btn.closest('.list-group-item') || document;
            var citationEl = wrapper.querySelector('.citation-html') || wrapper.querySelector('[data-citation-html]');
            if (citationEl) {
                // prefer visible text (no HTML tags)
                html = citationEl.innerText || citationEl.textContent || citationEl.innerHTML || "";
            }
        }

        if (!html) {
            // nothing to copy
            alert('Không tìm thấy trích dẫn để sao chép.');
            return;
        }

        copyToClipboard(html, function () {
            // copied — save to server
            ajaxPost('/Citations/Save', { citationHtml: html }, function (resp) {
                if (resp && resp.success) {
                    alert("Saved.");
                    // optional: show a toast/snackbar
                    console.log('Saved citation id', resp.id);
                } else {
                    console.warn('Could not save citation', resp);
                }
            });
            // show user feedback
            var original = btn.textContent;
            btn.textContent = 'Đã sao chép';
            setTimeout(function () { btn.textContent = original; }, 1200);
        }, function () {
            alert('Không thể sao chép trích dẫn. Vui lòng thử thủ công.');
        });
    }

    // delegate clicks; use closest to support clicks on inner elements
    document.addEventListener('click', function (ev) {
        var btn = ev.target && ev.target.closest && ev.target.closest('.copy-citation-btn');
        if (btn) {
            handleCopyClick(btn, ev);
        }
    });

})();