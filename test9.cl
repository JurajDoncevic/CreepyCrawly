ON ROOT 'http://books.toscrape.com/catalogue/page-48.html' DO {
    WHILE_CLICK  'li.next > a' DO {
        EXTRACT_TITLE;
    }
    WAIT_MS 5000;
}