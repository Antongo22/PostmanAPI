// Fix Bearer token prefix in Swagger UI
window.addEventListener('load', function() {
    setTimeout(function() {
        const ui = window.ui;
        if (ui) {
            const originalExecute = ui.fn.execute;
            ui.fn.execute = function(req) {
                if (req.headers && req.headers.Authorization && !req.headers.Authorization.startsWith('Bearer ')) {
                    req.headers.Authorization = 'Bearer ' + req.headers.Authorization;
                }
                return originalExecute.call(this, req);
            };
        }
    }, 1000);
});
