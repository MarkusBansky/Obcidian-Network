# How to contribute

Third-party patches are essential for keeping Obcidian Network great. I simply can't
access the huge number of platforms and myriad configurations for running
Obcidian Network. I want to keep it as easy as possible to contribute changes that
get things working in your environment. There are a few guidelines that
contributors need to follow so that I can have a chance of keeping on
top of things.

## The core of Obcidian Network

New functionality is appreciated but not always approved. I want to keep Obcidian network simple to use and not all methods and extentions that contributors create are good and simple. But if you have any changes to productivity, simple functions, useful methods and so on, your contributions are good enough.

If you are unsure of whether your contribution to the core is useful and would be accepted, you may ask on the [ON mailing list](https://groups.google.com/forum/#!forum/puppet-dev) for advice.

## Getting Started

* Make sure you have a [GitHub account](https://github.com/signup/free)
* Submit your issue, assuming one does not already exist.
  * Clearly describe the issue including steps to reproduce when it is a bug.
  * Make sure you fill in the earliest version that you know has the issue.
  * If it is possible describe, where you think the issue or bug is.
* Fork the repository on GitHub

## Making Changes

* Create a topic branch from where you want to base your work.
  * This is usually the master or nightly branch.
  * Only target release branches if you are certain your fix must be on that
    branch.
  * To quickly create a topic branch based on nightly; `git checkout -b
    fix/nightly/my_contribution nightly`.
    Please avoid working directly on the `nightly` branch.
* Make commits of logical units.
* Check for unnecessary whitespace with `git diff --check` before committing.
* Make sure your commit messages are in the proper format.
* Make sure you have added the necessary tests for your changes.
* Run _all_ the tests to assure nothing else was accidentally broken.

## Submitting Changes

* Push your changes to a topic branch in your fork of the repository.
* Submit a pull request to the repository of NeuralNetworks.
* Update your Github ticket to mark that you have submitted code and are ready for it to be reviewed (Status: Ready for Merge).
  * Include a link to the pull request in the ticket.
* After feedback has been given I expect responses within two weeks. After two
  weeks I may close the pull request if it isn't showing any activity.

## Revert Policy
By running tests in advance and by engaging with peer review for prospective
changes, your contributions have a high probability of becoming long lived
parts of the project. After being merged, the code will run through a
series of testing pipelines on a large number of operating system
environments. These pipelines can reveal incompatibilities that are difficult
to detect in advance.

If the code change results in a test failure, I will make my best effort to
correct the error. If a fix cannot be determined and committed within 24 hours
of its discovery, the commit(s) responsible _may_ be reverted, at the
discretion of the committer and Obcidian Network maintainers. This action would be taken
to help maintain passing states in our testing pipelines.

The original contributor will be notified of the revert in the Github ticket
associated with the change. A reference to the test(s) and operating system(s)
that failed as a result of the code change will also be added to the Github
ticket. This test(s) should be used to check future submissions of the code to
ensure the issue has been resolved.

### Summary
* Changes resulting in test pipeline failures will be reverted if they cannot
  be resolved within one business day.
