# TPost

[![Build & test](https://github.com/nazarovsa/TPost/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/nazarovsa/TPost/actions/workflows/dotnet-core.yml)

TPost is a starter project for an app that can crawl websites and make posts in different sources, for example, It can use a telegram bot to publish posts into the telegram channel.

## How to start?
In progress...

## How it works?

In progress...

### IPostCrawler

Crawler extracts required content from a web page and returns posts.  
Interface for crawlers, it has single method `GetPosts`, which returns `IReadOnlyCollection<IPost>`.  
For example, you can implement crawler using [HtmlAgilityPack](https://html-agility-pack.net/).

### IPostCrawlerManager

Crawler manager takes all crawlers from DI container and refill store with posts.
Default implementations is: [`DefaultPostCrawlerManager`](https://github.com/nazarovsa/TPost/blob/main/src/TPost.Core/Crawlers/DefaultPostCrawlerManager.cs).
`DefaultPostCrawlerManager` takes `count / n` posts from each registered crawler. Where `count` is argument of method `RenewPostStore` (10 by default) and `n` is amount of registered in DI `IPostCrawler`'s.

### IPostStore

Post store stores crawled posts.  
Default implemetation is: [`ConcurrentQueuePostStore`](https://github.com/nazarovsa/TPost/blob/main/src/TPost.Core/Stores/ConcurrentQueuePostStore.cs).  
`GetAndRemoveOne` method takes one post from a store and removes it. If you want to use the persistent store, and want to save published posts.  You should implement a mechanism, that will not return already published posts. (Extra column in a table like `IsPublished`, etc.)

### IPostPublisher

Interface for post publisher.  
Default implementation is [`CompositePostPuiblisher`](https://github.com/nazarovsa/TPost/blob/main/src/TPost.Core/Publishers/CompositePostPublisher.cs), which takes all `IPostPublisherTransport` from DI container and calls `Publish` method on them.

### IPostPublisherTransport

Publisher transport. Inherits from `IPostPublisher`. Added to make `CompositePostPuiblisher` work.
By default behavior, you need to implement just that interface. It will allow you to have multiple publish destinations.

### PostJob

Post job takes post from the store and publishes it to registered `IPostPublisher` by cron schedule.  

## Publishers

In progress...

## Sample
You can find working sample in *_Samples/TPost.Host.Sample* [folder](https://github.com/nazarovsa/TPost/tree/main/_Samples/TPost.WebHost.Sample).
This sample crawling jokes from two sites and publishing them into a console. As you can see, it just implements two crawlers and registers them in a host.  
I have a real project, which works like that, and it posts jokes into [@cringedot](https://t.me/cringedot) Telegram channel.

## List of Packages
| Package Name                                                                             | Version                                                                                                                                 | Downloads                                                                                                                                       | Description                           |
|------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------|
| *[TPost.Core](https://www.nuget.org/packages/TPost.Core/)*                               | [![nuget version](https://img.shields.io/nuget/v/TPost.Core)](https://www.nuget.org/packages/TPost.Core/)                               | [![Nuget](https://img.shields.io/nuget/dt/TPost.Core?color=%2300000)](https://www.nuget.org/packages/TPost.Core/)                               | Core abstractions                     |
| *[TPost.Hosting](https://www.nuget.org/packages/TPost.Hosting/)*                         | [![nuget version](https://img.shields.io/nuget/v/TPost.Hosting)](https://www.nuget.org/packages/TPost.Hosting/)                         | [![Nuget](https://img.shields.io/nuget/dt/TPost.Hosting?color=%2300000)](https://www.nuget.org/packages/TPost.Hosting/)                         | Hosting abstractions                  |
| *[TPost.Publishers.Telegram](https://www.nuget.org/packages/TPost.Publishers.Telegram/)* | [![nuget version](https://img.shields.io/nuget/v/TPost.Publishers.Telegram)](https://www.nuget.org/packages/TPost.Publishers.Telegram/) | [![Nuget](https://img.shields.io/nuget/dt/TPost.Publishers.Telegram?color=%2300000)](https://www.nuget.org/packages/TPost.Publishers.Telegram/) | Telegram bot publisher implementation |
