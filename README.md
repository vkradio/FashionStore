# Fashion Store

## Purpose and history

This program is used in small network of fashion stores of one of my
clients. It manages items in stock using visual size matrix. It
supports a few warehouses, movement of goods between them, sells,
returns, reports and other typical features.

There were already existed many store automation suites, but main
motivation force for developing this another one was to be able to be
used by employees without any PC experience, by “non-users”. The UI
should've be as simple and intuitively understandable as possible. And
that consideration has lead to many design constraints, so this software
is essentially tailored to suit that particular client, and maybe not
suitable at all for any “generic” fashion store.

After many years I’m publishing it as an open source (with respective
copyright permissions) as part of **my professional portfolio**.

## Modern development plan

Because this project was initially written so many years ago, **it does
not represent my _current_ skills**. It has many flaws in design, coding
practices, and even in business logic. So, I plan to update this repo
to comply with my state of the art skill set. Nearest fixes presumably
will include the following:

* Add English language to UI (I18N → L11N);
* Initial reformatting and refactoring of C# code to comply more with my
  current coding practices;
* Add unit tests;
* ~~Migrate back-end DLL projects from .NET Framework → .NET Standard;~~
* Migrate front-end WinForms project from .NET Framework → .NET Core;
* Switch custom-written DAL to EF;
* Compliance with SOLID, design patterns and other best practices.