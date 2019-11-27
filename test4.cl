ON ROOT '' DO {
    WAIT_LOAD 'body' 3000;
    FOREACH 'img.thumb' DO {
        WAIT_MS 500;
        FOREACH 'div.thumbs img.thumb' DO {
            WAIT_MS 500;
            EXTRACT_IMAGE 'img';
        }
    }
}