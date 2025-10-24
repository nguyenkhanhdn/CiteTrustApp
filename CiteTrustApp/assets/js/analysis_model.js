(function () {
    function hideModalFallback($modal) {
        // remove visible classes and attributes
        $modal.removeClass('show').hide().attr('aria-hidden', 'true').attr('aria-modal', 'false');
        // remove backdrop(s)
        $('.modal-backdrop').remove();
        // restore body scroll
        $('body').removeClass('modal-open').css('padding-right', '');
    }

    // delegated handler covers dynamically injected content
    $(document).on('click', '#analysisModal .close, #analysisModal [data-dismiss="modal"], #analysisModal [data-bs-dismiss="modal"]', function (e) {
        e.preventDefault();

        var $modal = $('#analysisModal');
        if (!$modal.length) return;

        // Bootstrap 5: use bootstrap.Modal instance if available
        if (window.bootstrap && typeof window.bootstrap.Modal === 'function') {
            try {
                var inst = window.bootstrap.Modal.getInstance(document.getElementById('analysisModal'));
                if (inst) {
                    inst.hide();
                    return;
                }
                // if no instance, create one and hide
                inst = new window.bootstrap.Modal(document.getElementById('analysisModal'));
                inst.hide();
                return;
            } catch (err) {
                // fallback below
            }
        }

        // Bootstrap 4 (jQuery plugin)
        if (typeof $modal.modal === 'function') {
            try {
                $modal.modal('hide');
                return;
            } catch (err) {
                // fallback below
            }
        }

        // Final fallback
        hideModalFallback($modal);
    });
})();