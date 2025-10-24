
// Handles "Chép trích dẫn" and "Phân tích với AI"
(function ($) {
    // Copy full visible card text
    $(document).on('click', '.copy-citation', function (e) {
        var btn = $(this);
        var card = btn.closest('.evidence-card')[0];
        if (!card) return;
        var clone = card.cloneNode(true);
        // remove interactive elements
        $(clone).find('button, a').remove();
        var text = clone.innerText.trim();
        if (!text) return;
        if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(text).then(function () {
                flash(btn, 'Đã chép');
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
            flash(btn, 'Đã chép');
        } finally {
            document.body.removeChild(ta);
        }
    }

    function flash(btn, msg) {
        var orig = btn.text();
        btn.text(msg);
        setTimeout(function () { btn.text(orig); }, 1400);
    }

    // Analysis: POST to /Evidence/Analyze and show partial in modal
    $(document).on('click', '.analysis-button', function (e) {
        e.preventDefault();
        
        var btn = $(this);
        var id = btn.data('evidence-id') || btn.closest('.evidence-card').data('evidence-id');
        if (!id) return;

        btn.prop('disabled', true);
        var origText = btn.text();
        btn.text('Đang phân tích...');

        $.ajax({
            url: '/Evidence/Analyze',
            type: 'POST',
            data: { id: id },
            success: function (html) {
                showAiModal(html);
            },
            error: function (xhr) {
                var content = xhr.responseText || 'Lỗi khi phân tích';
                showAiModal('<div class="p-3 text-danger">' + $('<div/>').text(content).html() + '</div>');
            },
            complete: function () {
                btn.prop('disabled', false);
                btn.text(origText);
            }
        });
    });

    function ensureModal() {
        var modal = $('#aiAnalysisModal');
        if (modal.length) return modal;
        var markup = '<div class="modal fade" id="aiAnalysisModal" tabindex="-1" role="dialog" aria-hidden="true">' +
            '<div class="modal-dialog modal-lg" role="document"><div class="modal-content">' +
            '<div class="modal-header"><h5 class="modal-title">Phân tích</h5>' +
            '<button type="button" id="btnClose" class="close" onclick="doClose()" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '</div><div class="modal-body"></div>' +
            '</div></div></div>';
        $('body').append(markup);
        return $('#aiAnalysisModal');
    }

    function showAiModal(html) {
        var modal = ensureModal();
        modal.find('.modal-body').html(html);
        modal.modal('show');
    }

})(jQuery);