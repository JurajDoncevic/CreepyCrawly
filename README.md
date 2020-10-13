# CreepyCrawly
An extensible and scriptable web crawler/scraper

![CreepyCrawly Logo](https://github.com/JurajDoncevic/CreepyCrawly/blob/master/Wiki/CreppyCrawlyTranspImg.png)


## What is CreepyCrawly?
CreepyCrawly is a scriptable tool for web crawling and scraping. It can be used for a variety of situations when a website needs to be scraped. Just write a script, begin crawling thorugh pages and scraping text, images, links etc.

CreepyCrawly is built in C# .NET Core 3.1, using ANTLR 4 and Selenuim (with chromedriver) as the default execution engine.

It comes in form of a command line tool and a WPF desktop app. The desktop app is Windows only, sorry :disappointed:.

For more information check the [Wiki](https://github.com/JurajDoncevic/CreepyCrawly/wiki)!

## Scripting? In what language?
CreepyCrawly uses its own scripting language - **CrawlLang**.

Learn more about CrawlLang [HERE](https://github.com/JurajDoncevic/CreepyCrawly/wiki/CrawlLang)

## Extensible?
CreepyCrawly can use other tools as a basis for an execution engine, not just Selenium.

All implemented engines can be interchangeable, the user can just select which one to use before running a script.

Learn more about the design [HERE](https://github.com/JurajDoncevic/CreepyCrawly/wiki/Design-and-architecture)
