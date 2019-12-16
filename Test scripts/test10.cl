ON ROOT 'http://books.toscrape.com/' DO {
    DO {
        EXTRACT_TITLE;
        EXTRACT_ALL_IMAGES 'img.thumbnail';
    } WHILE_CLICK  'li.next > a'; 
}