# TPost

[![Build & test](https://github.com/nazarovsa/TPost/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/nazarovsa/TPost/actions/workflows/dotnet-core.yml)

TPost is a starter project for app which can crawl web sites and make post in different sources, for example: It can use telegram bot to publish post into telegram channel.

## How to start?
In progress...

## How it works?

In progress...

### IPostCrawler

In progress...

### IPostCrawlerManager

In progress...

### IPostStore

In progress...

### IPostPublisher

In progress...

### IPostPublisherTransport

In progress...

## Publishers

In progress...

## Sample
You can find working sample in *_Samples/TPost.Host.Sample* [folder](https://github.com/nazarovsa/TPost/tree/main/_Samples/TPost.WebHost.Sample).
This sample crawling jokes from two sites and publishing them into console. As you can see it just implements two crawlers and registering them in host.  
I have real project, which works like that and it posts jokes into [@cringedot](https://t.me/cringedot) telegram channel.

## List of Packages
| Package Name                                                                             | Version                                                                                                                                 | Downloads                                                                                                                                       |
|------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| *[TPost.Core](https://www.nuget.org/packages/TPost.Core/)*                               | [![nuget version](https://img.shields.io/nuget/v/TPost.Core)](https://www.nuget.org/packages/TPost.Core/)                               | [![Nuget](https://img.shields.io/nuget/dt/TPost.Core?color=%2300000)](https://www.nuget.org/packages/TPost.Core/)                               |
| *[TPost.Hosting](https://www.nuget.org/packages/TPost.Hosting/)*                         | [![nuget version](https://img.shields.io/nuget/v/TPost.Hosting)](https://www.nuget.org/packages/TPost.Hosting/)                         | [![Nuget](https://img.shields.io/nuget/dt/TPost.Hosting?color=%2300000)](https://www.nuget.org/packages/TPost.Hosting/)                         |
| *[TPost.Publishers.Telegram](https://www.nuget.org/packages/TPost.Publishers.Telegram/)* | [![nuget version](https://img.shields.io/nuget/v/TPost.Publishers.Telegram)](https://www.nuget.org/packages/TPost.Publishers.Telegram/) | [![Nuget](https://img.shields.io/nuget/dt/TPost.Publishers.Telegram?color=%2300000)](https://www.nuget.org/packages/TPost.Publishers.Telegram/) |
