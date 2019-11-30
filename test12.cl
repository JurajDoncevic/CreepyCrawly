ON ROOT 'http://books.toscrape.com/' DO {
    DO {
        FOREACH_CLICK 'article.product_pod > h3 > a' DO {
            EXTRACT_TO_CSV  '#content_inner > article > div.row > div.col-sm-6.product_main > h1',
                            '#content_inner > article > div.row > div.col-sm-6.product_main > p.price_color',
                            '#content_inner > article > div.row > div.col-sm-6.product_main > p.instock.availability',
                            '#content_inner > article > table > tbody > tr:nth-child(7) > td';
            EXTRACT_IMAGE '#product_gallery > div > div > div > img';
        }
    } WHILE_CLICK 'li.next > a';
}