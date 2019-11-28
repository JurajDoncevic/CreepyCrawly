ON ROOT '' DO{
        FOREACH_CLICK 'img.thumb' DO {
            EXTRACT_ALL_IMAGES 'div#gallery img.thumb';
        }
}