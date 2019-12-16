ON ROOT 'https://eldorado.ua/led/c1038962/' DO {
    DO {
        WAIT_FOR 'body' 10000;
        FOREACH_HREF 'div.goods-item-content > div.good-description > div.title > a' DO {
            WAIT_FOR 'body' 1000;
            EXTRACT_TO_CSV  '#content > div > div.page-container.top-level.product-page > div > div > div > section > div.product-head-container > div.product-head-text > div > h1',
                            '#content > div > div.page-container.top-level.product-page > div > div > div > section > div.product-head-container > div.product-general-information > div.product-right-part.product-information-part > div.product-buy-container > div.left-part.price-information > div.content-information > div > div.price-information > div.price-value',
                            '#content > div > div.page-container.top-level.product-page > div > div > div > section > div.product-head-container > div.product-general-information > div.product-right-part.product-information-part > div.product-short-attributes-container > div.product-status.in-stock > span.text';
            CLICK 'img.image-magnify-img';
            WAIT_MS 300;
            EXTRACT_IMAGE 'div.media-container > img';
            CLICK 'span.close-button';
        }
    } WHILE_CLICK 'ul.text-n-o-c.pages > li.page-i:last-child';
}