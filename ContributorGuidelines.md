# Becoming a Member #

If you are not a member of the google code project, but would like to contribute, here is what you can do:
  1. Write your contribution as a patch, and submit it to one of the project admins ( 	 [ian](http://tpforums.org/forum/private.php?do=newpm&u=902)/ian320, [ame](http://tpforums.org/forum/private.php?do=newpm&u=116)/hugo.persson) via email or a PM at tpforums.
  1. If we like your work, and find the code useful and well-written, you will be a candidate for membership.
  1. The existing members will discuss any new members and decide as a group whether or not to include someone new.
  1. New members also must have a plan of what they will contribute (new features, keeping up with releases, etc)

# Writing code #

  * Remember to use the [issue list](http://code.google.com/p/tibiaapi/issues/list)! When you want to work on something, or want others to work on it, make an issue. When you complete a task, close the corresponding issue.
  * When committing, add comments explaining which issue you're fixing. Examples:
    1. "In the last revision, [r100](https://code.google.com/p/tibiaapi/source/detail?r=100) something broke. Fix for that." ([r100](https://code.google.com/p/tibiaapi/source/detail?r=100) will be replaced with link to the specific revision automatically)
    1. "Fixed [issue 99](https://code.google.com/p/tibiaapi/issues/detail?id=99) by changing..." ([issue 99](https://code.google.com/p/tibiaapi/issues/detail?id=99) will be replaced with a link to the issue)
  * The same as above goes for creating issues: if you need to, reference the specific revision something broke or another related issue that must be accomplished first.
  * Don't forget to update programs in the apps directory as well; we don't want the examples to be broken. It is a good idea to include them in your solution so that you get compile errors when you change things in TibiaAPI.
  * Must use proper coding conventions (table based on http://www.irritatedvowel.com/Programming/Standards.aspx):

## Coding Conventions (C# and C++) ##
  * Braces always go on the next line
```
void Foo(string bar)
{
    if (bar == string.Empty)
    {
        // do something
    }
    else if (bar == "foo")
    {
        // do something else
    }
}
```
  * If you are ever in doubt, look at other code; yours should match

### Naming Convention ###
| **Type** | **Convention** | **Example** |
|:---------|:---------------|:------------|
| Classes and Structs | Pascal Case | Client, ContextMenu, VipList|
| Collection, Delegate, Exception, or Attribute classes | Follow class naming conventions, but add the appropriate nown to the end | WidgetCollection, WidgetCallbackDelegate, InvalidTransactionException, WebServiceAttribute |
| Interfaces | Follow class naming conventions, but start the name with "I" and capitalize the letter following the "I" | IWidget |
| Enumerations | Follow class naming conventions. Do not add "Enum" to the end. If the enumeration represents a set of bitwise flags, end the name with a plural. | LoginStatus, OutfitAddon, SearchOptions (bitwise flags) |
| Functions | Pascal Case, no underscores except in event handlers. Avoid abbreviations; it is better to be verbose than confusing. | StartRawSocket, TransformItem |
| Properties and Public Variables | Pascal Case, no underscores. | PlayerLocation, HasExited, IsTopMost |
| Parameters | Camel Case | value, checkSoulPoints |
| Procedure-level (local) variables | Camel Case | player, allClear, itemMovedToAmmo |
| Class-level private or protected variables | Camel Case | processHandle, pipeIsReady, screen |
| Controls on Forms | "ux" prefix | uxLog, uxStart, uxMap |
| Constants | Pascal Case | DefaultLoginServers, MaxConnections |


# Reviewing code #

  * Please review the code that others have written often.
  * Feel free to comment on their code at the revision page (at the bottom), and discuss potential changes or problems.