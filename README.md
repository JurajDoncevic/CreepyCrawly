# CreepyCrawly
An extensible and scriptable web crawler/scraper

![CreepyCrawly Logo](https://github.com/JurajDoncevic/CreepyCrawly/blob/master/Wiki/CreppyCrawlyTranspImg.png)


## What is CreepyCrawly?
CreepyCrawly is a scriptable tool for web crawling and scraping. It can be used for a variety of situations when a website needs to be scraped. Just write a script, begin crawling thorugh pages and scraping text, images, links etc.

CreepyCrawly is built in C# .NET Core 3.1, using Antlr 4 and Selenuim (with chromedriver) as the default execution engine.

It comes in form of a command line tool and a WPF desktop app. The desktop app is Windows only, sorry :disappointed:.

## Scripting? In what language?
CreepyCrawly uses its own scripting language - **CrawlLang**. CrawlLang was built incrementaly as CreepyCrawly was being used and tested over multiple websites (even some really shady ones). If an opportunity or need arises, CrawlLang might get new features as it is easy to maintain and extend.

Learn more about CrawlLang [HERE]()

## Extensible?
CreepyCrawly was first brainstormed and written in Python with just Selenium as an *execution engine* in mind. A decoupled functional design we used on a team project (to avoid stepping on eachothers toes) allowed it to be used with another execution engine variant. So, the idea to build a tool that could be used with **any** such execution engine emerged. If a more adequate engine than Selenium appears, one can just implement the commands using that new engine.

All implemented engines can be interchangeable, the user can just select which one to use before running a script.

Learn more about the design [HERE]()
