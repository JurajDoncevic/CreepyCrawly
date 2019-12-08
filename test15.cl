ON ROOT 'http://books.toscrape.com/' DO {
    DO {
        FOREACH_CLICK 'article.product_pod > h3 > a' DO {
            EXTRACT_TEXT '#content_inner > article > div.row > div.col-sm-6.product_main > h1';
            EXTRACT_TEXT '#content_inner > article > div.row > div.col-sm-6.product_main > p.price_color';
            EXTRACT_IMAGE '#product_gallery > div > div > div > img';
        }
    } WHILE_CLICK 'li.next > a';
}