ON ROOT 'https://www.fer3.net' DO {
    CLICK 'a[href="/login/"]';
    WAIT 200;
    INPUT 'input[autocomplete="username"]' ('Felix');
    INPUT 'input[name="password"]' ('blatiotoesea');
    WAIT 200;
    CLICK 'button.button--primary.button.button--icon.button--icon--login';
    WAIT 200;
    FOREACH 'a[data-xf-init="preview-tooltip"]' DO {
        WAIT_LOAD 'body' 500;
        EXTRACT 'a.username';
    }
}