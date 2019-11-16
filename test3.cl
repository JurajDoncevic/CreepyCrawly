ON ROOT 'https://www.fer3.net' DO {
    CLICK 'a[href="/login/"]';
    WAIT 500;
    INPUT 'input[autocomplete="username"]' ('Felix');
    INPUT 'input[name="password"]' ('blatiotoesea');
    WAIT 500;
    CLICK 'button.button--primary.button.button--icon.button--icon--login';
    WAIT 500;
    FOREACH 'div.structItem-title' DO {
        WAIT_LOAD 'body' 3000;
        WAIT 5000;
        EXTRACT 'a.username';
    }
}