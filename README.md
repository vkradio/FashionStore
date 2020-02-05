# Fashion Store

## Purpose and history

This program is used in small network of fashion stores of one of my
clients. It manages items in stock using visual dimension matrix. It
supports a few warehouses, movement of goods between them, sells,
returns, reports and other such a typical features.

There were already existed many store automation suites, but main
motivation force for developing the new one was to allow to be used by
people without any PC experience, “non-users”. The UI should be as
simple and intuitively understood as possible. And that consideration
has lead to many design constraints, so this software is essentially
tailored to suit that particular client, and maybe not suitable at all
for any “generic” fashion store.

After many years I’m publishing it as an open source (complying to
copyrights, of course) as part of *my professional portfolio*.

## Modern development plan

Because this project was initially written so many years ago, *it does
not represent my _current_ skills*. It has many flaws in design, coding
practices, and even in business logic. So, I plan to update this repo
to comply with my state of the art skill set. Nearest fixes presumably
will include the following:

* Add English language to UI (I18N → L11N);
* Initial reformatting and refactoring C# code to comply more with my
  current coding practices;
* Add unit tests;
* Migrate back-end DLL project from .NET Framework → .NET Standard;
* Migrate front-end WinForms project from .NET Framework → .NET Core;
* Switch custom-written DAL to EF;
* Compliance with SOLID, design patterns and other best practices.