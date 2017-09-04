# Contributing to the Serilog project

It's awesome that you are considering contributing to the Serilog project.  As can be witnessed by the list of [sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks) and [community projects](https://github.com/serilog/serilog/wiki/Community-Projects), it is people like yourself that make the Serilog ecosystem great to use.

The following are a set of guidelines for contributing to [Serilog](https://serilog.net/) and its related packages, which are hosted in the [Serilog Organization](https://github.com/serilog) on GitHub. We are an ever evolving community, so feel free to propose changes to this document via a pull request.


## Where to start?

The [Serilog repository][serilog] is the location for enhancements and fixes to the core library.  Generally we try to keep the core library lean and performant which attempting to deliver the features of a first class logging library.  

If you are interested in contributing to a [sink][sinks] or one of the other [community projects][community_projects] then please create a PR in the respective repository.

Serilog caters a vast set of technologies and stacks that encounter a wide range of problems. As such we need your help! This help can be in many forms such as PRs, documentation updates or testing new releases.

We keep a list of issues that are approachable for newcomers under the [up-for-grabs](https://github.com/serilog/serilog/issues?labels=up-for-grabs&state=open) label. Before starting work on a pull request, we suggest commenting on an existing issue or raising an issue to ensure we all work together towards a better Serilog.

## Reporting an issue

Bugs are tracked via [GitHub][issue_list] issues.  Below are some notes to help create an issue.  The issue template will help you on the way

* Create an issue via the [issues list][create_issue].
* List the version of Serilog that is affected
* List the target framework and operating system
* If possible, provide a sample that reproduces the issue.

## Requesting a feature/enhancement

Feature as also tracked via [GitHub][issue_list] issues.  Below are some notes to help create an issue.  The issue template will help you on the way

* Create an issue via the [issues list][create_issue].
* List the version of Serilog that is affected
* List the target framework and operating system
* If possible, provide a sample that reproduces the issue.

## Making a PR

* If an issue does not already exist please create one via the issues list.
* Fork the repository and create a branch with a descriptive name.
* Attempt to make commits of logical units.
* When committing, please reference the issue the commit relates to.
* Run the build and tests.
    * Windows platforms can use the `build.ps1` script
    * nix/OSX platforms can use the `build.sh` script
* Create the PR, the PR template will help provide a stub of what information is required including:
    * The issue this PR addresses
    * Unit Tests for the changes have been added.

## Questions?

Serilog has an active and helpful community who are happy to help point you in the right direction or work through any issues you might encounter. You can get in touch via:

 * [Stack Overflow](http://stackoverflow.com/questions/tagged/serilog) - this is the best place to start if you have a question
 * Our [issue tracker](https://github.com/serilog/serilog/issues) here on GitHub
 * [Gitter chat](https://gitter.im/serilog/serilog)
 * The [#serilog tag on Twitter](https://twitter.com/search?q=%23serilog)


Finally when contributing please keep in mind our [Code of Conduct](CODE_OF_CONDUCT.md).


[serilog]: https://github.com/serilog/serilog
[sinks]: https://github.com/serilog/serilog/wiki/Provided-Sinks
[community_projects]: https://github.com/serilog/serilog/wiki/Community-Projects
[create_issue]: https://github.com/serilog/serilog/issues/new
[issue_list]: https://github.com/serilog/serilog/issues/
