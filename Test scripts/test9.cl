ON ROOT 'http://books.toscrape.com/' DO {
    WHILE_CLICK  'li.next > a' DO {
        EXTRACT_TITLE;
        EXTRACT_ALL_IMAGES 'img.thumbnail';
    }
}