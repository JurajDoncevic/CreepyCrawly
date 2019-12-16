ON ROOT 'https://www.google.hr' DO {
    FOREACH 'li' DO {
        CLICK '#column-wrap > div:nth-child(1) > div > div.overview-list > div:nth-child(1) > a > div';
        WAIT_MS 3;
        CLICK 'div.rounded-link.dark-blue';
        WAIT_MS 3;
        CLICK '#normal > div.blue-middle-box > form > div > p > input.button_88px_darkblue_bg_blue.div-right';
        WAIT_MS 3;
        EXTRACT '#normal > p > a';
    }
}

