ON ROOT 'https://www.crwflags.com/fotw/flags/country.html' DO {
    GOTO_CLICK 'ul > li > a[href="af.html"]' DO {
        WAIT_MS 3000;
        EXTRACT_TITLE;
    }
}