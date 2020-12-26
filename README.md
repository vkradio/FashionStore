# Fashion Store

## Purpose and history

This program is used in small network of fashion stores of one of my
clients. It manages items in stock using visualized size matrices. It
supports multiple warehouses, movement of goods between them, sells,
returns, reports and other typical features.

Prior to this project, there were already existed many store automation suites, but main
motivation for developing this another one was to be able to be
used by employees without any PC experience, by “non-users”. The UI
should've be as simple and intuitively understandable as possible. And
that consideration has lead to many design constraints, so this software
is essentially tailored to suit that particular client, and maybe not
suitable to any “generic” fashion store.

After many years I’m publishing it as an open source (with respective
copyright permissions) as part of **my professional portfolio**.

## Demonstration of my current skills

### 1. Project architecture

The central part of the solution is an [ApplicationCore](https://github.com/vkradio/FashionStore/src/ApplicationCore) subproject. It contains domain entities (according to DDD), service interfaces (to support DI) and other basic stuff. Entities, according to Single Responsibility principle, are almost 100% decoupled from any persistence storage infrastructure such as EF, JSON etc, they only contain Primary and Foreign Key properties when needed.

User Interface (in it's modern WPF Store Selection control) employs MVVM pattern. ViewModels are located in the dedicated project [ViewModels](https://github.com/vkradio/FashionStore/src/ViewModels), they are totally ignorant of UI infrastructure (they are not coupled to WinForms or WPF or any other such dependencies). ViewModels have dedicated [unit tests](https://github.com/vkradio/FashionStore/tests/tests/ViewModelsTests).

I do not use any third-party MVVM libraries here, so there is a simplest [MvvmInfrastructure](https://github.com/vkradio/FashionStore/src/MvvmInfrastructure) project which contains reusable functionality such as subscription to property change events. Also I have a couple of miscellaneous utilities projects. And the remaining parts are legacy WinForms and custom DAL code which were just renamed to comply with modern practices.

Accordance to Single Responsibility principle in solution structure, where projects are loosely coupled with each other, allow us to relatively easy refactor and and extend functionality. For example, near [FashionStoreWinForms](https://github.com/vkradio/FashionStore/src/FashionStoreWinForms) I can add FashionStoreWinFormsCore project with migration to .NET Core 3 WinForms, or FashionStoreWpf, if I would need to port it to WPF, or even FashionStoreAngular, if there will be a need to create an Angular SPA. I can add Web API subproject which will be a bridge to storage and other core functionality, and I can even restructure project to a set of different microservices and serverless function groups. For example, one microservice could contain functionality for sales person and another one - for business administrator.

### 2. Unit tests

Here I employ xUnit with Moq.

### 3. Other implementation details

#### 3.1. Async

I like **async** feature and TAP and use it whenever it's suitable to do - on external resources access (file, database, network) and on computationally intensive tasks.

#### 3.2. Nullable reference types

Nullable reference types feature of C# 8 is enabled throughout almost all subprojects, except for legacy code.

#### 3.3. Hybrid WPF/WinForms

The whole legacy application is written in WinForms, but modern code is a WPF/MVVM code, which it injected to old app thanks to Hybrid capability and some DI.

#### 3.4. Localization

You can see here Russian localization resources, which are automatically used on PCs with Russian locale.

#### 3.5. Code analyzers and guards

Here I employ **Microsoft.CodeAnalysis.FxCopAnalyzers** to view and fix any warnings and hints to ensure my code-base is comply with modern style best practices. Also I use defensive programming technique, particularly, with help of **Ardalis.GuardClauses** library.

## Modern development plan

Because this project was initially written so many years ago, **it does
not represent my _current_ skills**. It has many flaws in design, coding
practices, and even in business logic. So, I plan to update this repo
to comply with my state of the art skill set. Nearest fixes presumably
will include the following:

* ~~Add English to UI instead of Russian (ideally I18N → L11N);~~
* Initial reformatting and refactoring of C# code to comply more with my
  current coding practices;
* Add unit tests;
* ~~Migrate back-end DLL projects from .NET Framework → .NET Standard;~~
* Migrate front-end WinForms project from .NET Framework → .NET Core;
* Switch custom-written DAL to EF Core;
* Compliance with SOLID, design patterns and other best practices.