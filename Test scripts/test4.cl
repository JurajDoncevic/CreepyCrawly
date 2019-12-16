ON ROOT '' DO {
    WAIT_FOR 'body' 3000;
    FOREACH_CLICK 'img.thumb' DO {
        WAIT_FOR 'body' 3000;
        EXTRACT_TEXT 'title';
        FOREACH_CLICK 'div#gallery img.thumb' DO {
            WAIT_FOR 'body' 3000;
            EXTRACT_IMAGE 'img';
        }
    }
}