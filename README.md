<!-- [![.NET Core](https://github.com/ardalis/CleanArchitecture/workflows/.NET%20Core/badge.svg)](https://github.com/ardalis/CleanArchitecture/actions) -->
<br />

![TestRunner](https://github.com/studentutu/Unity3DCleanArchitecture/workflows/TestRunner/badge.svg?branch=main)
![CodeCoverage](/app-ca/CodeCoverage/Report/badge_combined.svg)
[![CodeFactor](https://www.codefactor.io/repository/github/studentutu/unity3dcleanarchitecture/badge)](https://www.codefactor.io/repository/github/studentutu/unity3dcleanarchitecture)
[![GitHub issues](https://img.shields.io/github/issues/studentutu/Unity3DCleanArchitecture)](https://github.com/studentutu/Unity3DCleanArchitecture/issues)
<a href="https://mergify.io">
   <img src="https://img.shields.io/endpoint.svg?url=https://gh.mergify.io/badges/openupm/openupm&style=flat" />
</a>
![GitHub last commit](https://img.shields.io/github/last-commit/studentutu/Unity3DCleanArchitecture)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/a1416484f71747d0a26326cb37c91676)](https://www.codacy.com/gh/studentutu/Unity3DCleanArchitecture/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=studentutu/Unity3DCleanArchitecture&amp;utm_campaign=Badge_Grade)
<br />

# Clean Architecture

It is an attemp to make a CLean Architecture project in Unity based on the repo [Clean Architecture](https://github.com/ardalis/CleanArchitecture).
It is just the latest in a series of names for the same loosely-coupled, dependency-inverted architecture. You will also find it named [hexagonal](http://alistair.cockburn.us/Hexagonal+architecture), [ports-and-adapters](http://www.dossier-andreas.net/software_architecture/ports_and_adapters.html), or [onion architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/).

## Give a Star! :star:
If you like or are using this project to learn or start your solution, please give it a star. Thanks!
Make sure to open a ticket or even make a contribution to the project.

## Versions

The master branch is now using Unity 2019.4 LTS. 

## Learn More

- [Live Stream Recordings Working on Clean Architecture](https://www.youtube.com/c/Ardalis/search?query=clean%20architecture)
- [DotNetRocks Podcast Discussion with Steve "ardalis" Smith](https://player.fm/series/net-rocks/clean-architecture-with-steve-smith)
- [Fritz and Friends Streaming Discussion with Steve "ardalis" Smith](https://www.youtube.com/watch?v=k8cZUW4MS3I)

# Getting Started

To use this template, there are a few options:

- CLone repo via the template
- Alternative - download this Repository

These are all covered below.

## Using the GitHub Repository Template
The GitHub repository will always have the latest bug fixes and enhancements, so the template is always the latest.
You can also fork a repo that will pull out the forked repositories daily. See [Daily Pull from github](https://wei.github.io/pull/)

After choosing this template, provide a project name and finish the project creation wizard. You should be all set.
![Clean Architecture Project Template step 2](https://user-images.githubusercontent.com/782127/80412455-e5818300-889b-11ea-8219-379581583a92.png)

## Using the GitHub Repository

To get started based on this repository, you need to get a copy locally. You have three options: fork, clone, or download. Most of the time, you probably just want to download.

You should **download the repository**, unblock the zip file, and extract it to a new folder if you just want to play with the project or you wish to use it as the starting point for an application.

You should **fork this repository** only if you plan on submitting a pull request. Or if you'd like to keep a copy of a snapshot of the repository in your own GitHub account.

You should **clone this repository** if you're one of the contributors and you have commit access to it. Otherwise you probably want one of the other options.

# Goals

The goal of this repository is to provide a basic solution structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET 4.5 and Unity3D engine. Learn more about these topics here:

- [SOLID Principles for C# Developers](https://www.pluralsight.com/courses/csharp-solid-principles)
- [SOLID Principles of Object Oriented Design](https://www.pluralsight.com/courses/principles-oo-design) (the original, longer course)
- [Domain-Driven Design Fundamentals](https://www.pluralsight.com/courses/domain-driven-design-fundamentals)

If you're used to building applications as single-project or as a set of projects that follow the traditional UI -> Manager's logic -> Data Access Layer "N-Tier" (Retrieval) architecture, I recommend you check out these two courses (ideally before DDD Fundamentals):

- [Creating N-Tier Applications in C#, Part 1](https://www.pluralsight.com/courses/n-tier-apps-part1)
- [Creating N-Tier Applications in C#, Part 2](https://www.pluralsight.com/courses/n-tier-csharp-part2)

Also see Microsoft's reference application, eShopOnWeb, and its associated free eBook. Check them out here:

- [eShopOnWeb on GitHub](https://github.com/dotnet-architecture/eShopOnWeb)
- [Architecting Modern Web Applications with ASP.NET Core and Microsoft Azure](https://aka.ms/webappebook) (eBook)

## History and Shameless Plug Section

This is by no means an ideal solution, but hopefully over time it will become one, be more robust and feature-proofed. 


# Design Decisions and Dependencies

The goal of this sample is to provide a fairly bare-bones starter kit for new projects. It does not include every possible framework, tool, or feature that a particular enterprise application might benefit from. Its choices of technology for things like data access are rooted in what is the most common, accessible technology for most business software developers using common found technology stack. It doesn't (currently) include extensive support for things like logging, monitoring, or analytics, though these can all be added easily. Below is a list of the technology dependencies it includes, and why they were chosen. Most of these can easily be swapped out for your technology of choice, since the nature of this architecture is to support modularity and encapsulation.

## The Core Project

The Core project is the center of the Clean Architecture design, and all other project dependencies should point toward it. As such, it has very few external dependencies. The one exception in this case is the `System.Reflection.TypeExtensions` package, which is used by `ValueObject` to help implement its `IEquatable<>` interface. The Core project should include things like:

- Entities
- Aggregates
- Domain Events
- DTOs
- Interfaces
- Event Handlers
- Domain Services
- Specifications

## The SharedKernel Project

Many solutions will also reference a separate **Shared Kernel** project/package. I recommend creating a separate SharedKernel project and solution if you will require sharing code between multiple projects. I further recommend this be published as a package (more likely privately within your organization) and referenced as a dependency by those projects that require it. For this sample, in the interest of simplicity, I've added a SharedKernel project to the solution. It contains types that would likely be shared between multiple projects, in my experience.

## The Infrastructure Project

Most of your application's dependencies on external resources should be implemented in classes defined in the Infrastructure project. These classes should implement interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have multiple Infrastructure projects (e.g. Infrastructure.Data), but for most projects one Infrastructure project with folders works fine. The sample includes data access and domain event implementations, but you would also add things like email providers, file access, web api clients, etc. to this project so they're not adding coupling to your Core or UI projects.

The Infrastructure project depends on `Extenject`.

## The View/UI Project

It currently uses the default MVC organization (Controllers and Views folders) as well as most of the default ASP.NET Core project template code. This includes its configuration system, which uses the default `appsettings.json` file plus environment variables, and is configured in `Startup.cs`. The project delegates to the `Infrastructure` project to wire up its services.

## The Test Projects

Test projects could be organized based on the kind of test (unit, functional, integration, performance, etc.) or by the project they are testing (Core, Infrastructure, View/UIWeb), or both. For this simple starter kit, the test projects are organized based on the kind of test, with unit, functional and integration test projects existing in this solution. In terms of dependencies, there are three worth noting:

- [NSubstutute](https://nsubstitute.github.io/) I'm using NSubstutute as a mocking framework for white box behavior-based tests. If I have a method that, under certain circumstances, should perform an action that isn't evident from the object's observable state, mocks provide a way to test that. I could also use my own Fake implementation, but that requires a lot more typing and files. Moq is great once you get the hang of it, and assuming you don't have to mock the world (which we don't in this case because of good, modular design).

- [FluentValidation](https://github.com/FluentValidation/FluentValidation) Using the validator to check the database you can spot the breaking changes early. I've included it only in the test solution as it is not needed under the runtime of the application itself, though the validation of the ValueObjects and DTO's are important. You can swap the make a validation service for them in your `Infrastructure` layer.

# Patterns Used

This solution template has code built in to support a few common patterns, especially Domain-Driven Design patterns. Here is a brief overview of how a few of them work.

## Domain Events

Domain events are a great pattern for decoupling a trigger for an operation from its implementation. This is especially useful from within domain entities since the handlers of the events can have dependencies while the entities themselves typically do not. In the sample, you can see this in action with the `ToDoItem.MarkComplete()` method. The following sequence diagram demonstrates how the event and its handler are used when an item is marked complete.

![Domain Event Sequence Diagram](https://user-images.githubusercontent.com/782127/75702680-216ce300-5c73-11ea-9187-ec656192ad3b.png)

## Related Projects

- [ApiEndpoints](https://github.com/ardalis/apiendpoints)
- [GuardClauses](https://github.com/ardalis/guardclauses)
- [Result](https://github.com/ardalis/result)
- [Specification](https://github.com/ardalis/specification)

