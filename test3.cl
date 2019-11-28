ON ROOT 'https://www.fer3.net' DO {
    CLICK 'a[href="/login/"]';
    WAIT_MS 200;
    INPUT 'input[name="login"]' ('Felix');
    WAIT_MS 200;
    INPUT 'input[name="password"]' ('blatiotoesea');
    WAIT_MS 200;
    CLICK 'button.button--primary.button.button--icon.button--icon--login';
    WAIT_MS 200;
    EXTRACT_TEXT 'a.username';
    FOREACH_CLICK 'a[data-xf-init="preview-tooltip"]' DO {
        WAIT_FOR 'body' 500;
        EXTRACT_TITLE;
        EXTRACT_TEXT 'div#some_id';
        CLICK 'div#id';
        EXTRACT_TEXT 'a.username';
    }
}