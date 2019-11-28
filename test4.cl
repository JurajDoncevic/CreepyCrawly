ON ROOT '' DO {
    WAIT_LOAD 'body' 3000;
    FOREACH 'img.thumb' DO {
        WAIT_LOAD 'body' 3000;
        EXTRACT_TEXT 'title';
        FOREACH 'div#gallery img.thumb' DO {
            WAIT_LOAD 'body' 3000;
            EXTRACT_IMAGE 'img';
        }
    }
}