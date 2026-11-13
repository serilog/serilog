# Serilog.Expressions&nbsp;[![Build status](https://github.com/serilog/serilog-expressions/actions/workflows/ci.yml/badge.svg?branch=dev)](https://github.com/serilog/serilog-expressions/actions)&nbsp;[![NuGet Package](https://img.shields.io/nuget/vpre/serilog.expressions)](https://nuget.org/packages/serilog.expressions)

An embeddable mini-language for filtering, enriching, and formatting Serilog
events, ideal for use with JSON or XML configuration.

## Getting started

Install the package from NuGet:

```shell
dotnet add package Serilog.Expressions
```

The package adds extension methods to Serilog's `Filter`, `WriteTo`, and
`Enrich` configuration objects, along with an `ExpressionTemplate`
type that's compatible with Serilog sinks accepting an
`ITextFormatter`.

### Filtering example

_Serilog.Expressions_ adds `ByExcluding()` and `ByIncludingOnly()`
overloads to the `Filter` configuration object that accept filter
expressions:

```csharp
Log.Logger = new LoggerConfiguration()
    .Filter.ByExcluding("RequestPath like '/health%'")
    .CreateLogger();
```

Events with a `RequestPath` property that matches the expression
will be excluded by the filter.

> Note that if the expression syntax is invalid, an `ArgumentException` will
be thrown from the `ByExcluding()` method, and by similar methods elsewhere
in the package. To check expression syntax without throwing, see the
`Try*()` methods in the `SerilogExpression` class.

#### An `appSettings.json` JSON configuration example

In [`appSettings.json` configuration](https://github.com/serilog/serilog-settings-configuration)
this is written as:

```json
{
  "Serilog": {
    "Using": ["Serilog.Expressions"],
    "Filter": [
      {
        "Name": "ByExcluding",
        "Args": {
          "expression": "RequestPath like '/health%'"
        }
      }
    ]
  }
}
```

#### An `<appSettings>` XML configuration example

In [XML configuration files](https://github.com/serilog/serilog-settings-appsettings),
this is written as:

```xml
  <appSettings>
    <add key="serilog:using:Expressions" value="Serilog.Expressions" />
    <add key="serilog:filter:ByExcluding.expression" value="RequestPath like '/health%'" />
  </appSettings>
```

## Supported configuration APIs

_Serilog.Expressions_ adds a number of expression-based overloads and helper methods to the Serilog configuration syntax:

 * `Filter.ByExcluding()`, `Filter.ByIncludingOnly()` - use an expression to filter events passing through the Serilog pipeline
 * `WriteTo.Conditional()` - use an expression to select the events passed to a particular sink
 * `Enrich.When()` - conditionally enable an enricher when events match an expression
 * `Enrich.WithComputed()` - add or modify event properties using an expression

## Formatting with `ExpressionTemplate`

_Serilog.Expressions_ includes the `ExpressionTemplate` class for text formatting. `ExpressionTemplate` implements `ITextFormatter`, so
it works with any text-based Serilog sink, including `Console`, `File`, `Debug`, and `Email`:

```csharp
// using Serilog.Templates;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new ExpressionTemplate(
        "[{@t:HH:mm:ss} {@l:u3} ({SourceContext})] {@m} (first item is {Cart[0]})\n{@x}"))
    .CreateLogger();

// Produces log events like:
// [21:21:40 INF (Sample.Program)] Cart contains ["Tea","Coffee"] (first item is Tea)
```

Templates are based on .NET format strings, and support standard padding, alignment, and format specifiers.

Along with standard properties for the event timestamp (`@t`), level (`@l`) and so on, "holes" in expression templates can include complex
expressions over the first-class properties of the event, like `{SourceContext}` and `{Cart[0]}` in the example..

Templates support customizable color themes when used with the `Console` sink:

```csharp
    .WriteTo.Console(new ExpressionTemplate(
        "[{@t:HH:mm:ss} {@l:u3}] {@m}\n{@x}", theme: TemplateTheme.Code))
```

![Screenshot showing colored terminal output](https://raw.githubusercontent.com/serilog/serilog-expressions/dev/assets/screenshot.png)

Newline-delimited JSON (for example, replicating the [CLEF format](https://github.com/serilog/serilog-formatting-compact)) can be generated
using object literals:

```csharp
    .WriteTo.Console(new ExpressionTemplate(
        "{ {@t, @mt, @r, @l: if @l = 'Information' then undefined() else @l, @x, ..@p} }\n"))
```

## Language reference

### Properties

The following properties are available in expressions:

 * **All first-class properties of the event** - no special syntax: `SourceContext` and `Cart` are used in the formatting examples above
 * `@t` - the event's timestamp, as a `DateTimeOffset`
 * `@m` - the rendered message (Note: do not add format specifiers like `:lj` or you'll lose theme color rendering. These format specifiers are not supported as they've become the default and only option - [see the discussion here](https://github.com/serilog/serilog-expressions/issues/56#issuecomment-1146472988)
 * `@mt` - the raw message template
 * `@l` - the event's level, as a `LogEventLevel`
 * `@x` - the exception associated with the event, if any, as an `Exception`
 * `@p` - a dictionary containing all first-class properties; this supports properties with non-identifier names, for example `@p['snake-case-name']`
 * `@i` - event id; a 32-bit numeric hash of the event's message template
 * `@r` - renderings; if any tokens in the message template include .NET-specific formatting, an array of rendered values for each such token
 * `@tr` - trace id; The id of the trace that was active when the event was created, if any
 * `@sp` - span id; The id of the span that was active when the event was created, if any

The built-in properties mirror those available in the CLEF format.

The exception property `@x` is treated as a scalar and will appear as a string when formatted into text. The properties of
the underlying `Exception` object can be accessed using `Inspect()`, for example `Inspect(@x).Message`, and the type of the
exception retrieved using `TypeOf(@x)`.

### Literals

| Data type | Description | Examples |
| :--- | :--- | :--- |
| Null | Corresponds to .NET's `null` value | `null` |
| Number | A number in decimal or hexadecimal notation, represented by .NET `decimal` | `0`, `100`, `-12.34`, `0xC0FFEE` |
| String | A single-quoted Unicode string literal; to escape `'`, double it | `'pie'`, `'isn''t'`, `'😋'` |
| Boolean | A Boolean value | `true`, `false` |
| Array | An array of values, in square brackets | `[1, 'two', null]` |
| Object | A mapping of string keys to values; keys that are valid identifiers do not need to be quoted | `{a: 1, 'b c': 2, d}` |

Array and object literals support the spread operator: `[1, 2, ..others]`, `{a: 1, ..others}`. Specifying an undefined
property in an object literal will remove it from the result: `{..User, Email: Undefined()}`

### Operators and conditionals

A typical set of operators is supported:

 * Equality `=` and inequality `<>`, including for arrays and objects
 * Boolean `and`, `or`, `not`
 * Arithmetic `+`, `-`, `*`, `/`, `^`, `%`
 * Numeric comparison `<`, `<=`, `>`, `>=`
 * Existence `is null` and `is not null`
 * SQL-style `like` and `not like`, with `%` and `_` wildcards (double wildcards to escape them)
 * Array membership with `in` and `not in`
 * Accessors `a.b`
 * Indexers `a['b']` and `a[0]`
 * Wildcard indexing - `a[?]` any, and `a[*]` all
 * Conditional `if a then b else c` (all branches required; see also the section below on _conditional blocks_)

Comparision operators that act on text all accept an optional postfix `ci` modifier to select case-insensitive comparisons:

```
User.Name like 'n%' ci
```

### Functions

Functions are called using typical `Identifier(args)` syntax.

Except for the `IsDefined()` function, the result of
calling a function will be undefined if:

 * any argument is undefined, or
 * any argument is of an incompatible type.

| Function                        | Description                                                                                                                                                                                                                   |
|:--------------------------------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Coalesce(a0, a1, [..aN])`      | Returns the first defined, non-null argument.                                                                                                                                                                                 |
| `Concat(s0, s1, [..sN])`        | Concatenate two or more strings.                                                                                                                                                                                              |
| `Contains(s, p)`                | Tests whether the string `s` contains the substring `p`.                                                                                                                                                                      |
| `ElementAt(x, i)`               | Retrieves a property of `x` by name `i`, or array element of `x` by numeric index `i`.                                                                                                                                        |
| `EndsWith(s, p)`                | Tests whether the string `s` ends with substring `p`.                                                                                                                                                                         |
| `IndexOf(s, p)`                 | Returns the first index of substring `p` in string `s`, or -1 if the substring does not appear.                                                                                                                               |
| `IndexOfMatch(s, p)`            | Returns the index of the first match of regular expression `p` in string `s`, or -1 if the regular expression does not match.                                                                                                 |
| `Inspect(o, [deep])`            | Read properties from an object captured as the scalar value `o`.                                                                                                                                                              |
| `IsMatch(s, p)`                 | Tests whether the regular expression `p` matches within the string `s`.                                                                                                                                                       |
| `IsDefined(x)`                  | Returns `true` if the expression `x` has a value, including `null`, or `false` if `x` is undefined.                                                                                                                           |
| `LastIndexOf(s, p)`             | Returns the last index of substring `p` in string `s`, or -1 if the substring does not appear.                                                                                                                                |
| `Length(x)`                     | Returns the length of a string or array.                                                                                                                                                                                      |
| `Nest(o)`                       | Converts dotted (flattened) property names of object `o` into nested sub-objects.                                                                                                                                             |
| `Now()`                         | Returns `DateTimeOffset.Now`.                                                                                                                                                                                                 |
| `Replace(s, p, r)`              | Replace occurrences of substring `p` in string `s` with replacement `r`.                                                                                                                                                      |
| `Rest([deep])`                  | In an `ExpressionTemplate`, returns an object containing the first-class event properties not otherwise referenced in the template. If `deep` is `true`, also excludes properties referenced in the event's message template. |
| `Round(n, m)`                   | Round the number `n` to `m` decimal places.                                                                                                                                                                                   |
| `StartsWith(s, p)`              | Tests whether the string `s` starts with substring `p`.                                                                                                                                                                       |
| `Substring(s, start, [length])` | Return the substring of string `s` from `start` to the end of the string, or of `length` characters, if this argument is supplied.                                                                                            |
| `TagOf(o)`                      | Returns the `TypeTag` field of a captured object (i.e. where `TypeOf(x)` is `'object'`).                                                                                                                                      |
| `ToString(x, [format])`         | Convert `x` to a string, applying the format string `format` if `x` is `IFormattable`.                                                                                                                                        |
| `TypeOf(x)`                     | Returns a string describing the type of expression `x`: a .NET type name if `x` is scalar and non-null, or, `'array'`, `'object'`, `'dictionary'`, `'null'`, or `'undefined'`.                                                |
| `Undefined()`                   | Explicitly mark an undefined value.                                                                                                                                                                                           |
| `UtcDateTime(x)`                | Convert a `DateTime` or `DateTimeOffset` into a UTC `DateTime`.                                                                                                                                                               |

Functions that compare text accept an optional postfix `ci` modifier to select case-insensitive comparisons:

```
StartsWith(User.Name, 'n') ci
```

### Template directives

#### Conditional blocks

Within an `ExpressionTemplate`, a portion of the template can be conditionally evaluated using `#if`.

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new ExpressionTemplate(
        "[{@t:HH:mm:ss} {@l:u3}{#if SourceContext is not null} ({SourceContext}){#end}] {@m}\n{@x}"))
    .CreateLogger();

// Produces log events like:
// [21:21:45 INF] Starting up
// [21:21:46 INF (Sample.Program)] Firing engines
```

The block between the `{#if <expr>}` and `{#end}` directives will only appear in the output if `<expr>` is `true` - in the example, events with a `SourceContext` include this in parentheses, while those without, don't.

It's important to notice that the directive requires a Boolean `true` before the conditional block will be evaluated. It wouldn't be sufficient in this case to write `{#if SourceContext}`, since no values other than `true` are considered "truthy".

The syntax supports `{#if <expr>}`, chained `{#else if <expr>}`, `{#else}`, and `{#end}`, with arbitrary nesting.

#### Repetition

If a log event includes structured data in arrays or objects, a template block can be repeated for each element or member using `#each`/`in` (newlines, double quotes and construction of the `ExpressionTemplate` omitted for clarity):

```
{@l:w4}: {SourceContext}
      {#each s in Scope}=> {s}{#delimit} {#end}
      {@m}
{@x}
```

This example uses the optional `#delimit` to add a space between each element, producing output like:

```
info: Sample.Program
      => Main => TextFormattingExample
      Hello, world!
```

When using `{#each <name> in <expr>}` over an object, such as the built-in `@p` (properties) object, `<name>` will be bound to the _names_ of the properties of the object.

To get to the _values_ of the properties, use a second binding:

```
{#each k, v in @p}{k} = {v}{#delimit},{#end}
```

This example, if an event has three properties, will produce output like:

```
Account = "nblumhardt", Cart = ["Tea", "Coffee"], Powerup = 42
```

The syntax supports `{#each <name>[, <name>] in <expr>}`, an optional `{#delimit}` block, and finally an optional `{#else}` block, which will be evaluated if the array or object is empty.

## Recipes

**Trim down `SourceContext` to a type name only:**

```
Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)
```

This expression takes advantage of `LastIndexOf()` returning -1 when no `.` character appears in `SourceContext`, to yield a `startIndex` of 0 in that case.

**Write not-referenced context properties (only if there are any):**

```
{#if rest(true) <> {}} <Context: {rest(true)}>{#end}
```

**Access a property with a non-identifier name:**

```
@p['some name']
```

Any structured value, including the built-in `@p`, can be indexed by string key. This means that `User.Name` and `User['Name']` are equivalent, for example.

**Access a property with inconsistent casing:**

```
ElementAt(@p, 'someName') ci
```

`ElementAt()` is a function-call version of the `[]` indexer notation, which means it can accept the `ci` case-insensitivity modifier.

**Format events as newline-delimited JSON (template, embedded in C# or JSON):**

```
{ {Timestamp: @t, Username: User.Name} }\n
```

This output template shows the use of a space between the opening `{` of a hole, and the enclosed object literal with `Timestamp` and
`Username` fields. The object will be formatted as JSON. The trailing `\n` is a C# or JSON newline literal (don't escape this any further, as
it's not part of the output template syntax).

## Working with the raw API

The package provides the class `SerilogExpression` in the `Serilog.Expressions` namespace
for working with expressions.

```csharp
if (SerilogExpression.TryCompile("RequestPath like '/health%'", out var compiled, out var error)
{
    // `compiled` is a function that can be executed against `LogEvent`s:
    var result = compiled(someEvent);

    // `result` will contain a `LogEventPropertyValue`, or `null` if the result of evaluating the
    // expression is undefined (for example if the event has no `RequestPath` property).
    if (result is ScalarValue value &&
        value.Value is bool matches &&
        matches)
    {
        Console.WriteLine("The event matched.");
    }
}
else
{
    // `error` describes a syntax error.
    Console.WriteLine($"Couldn't compile the expression; {error}.");
}
```

Compiled expression delegates return `LogEventPropertyValue` because this is the most
convenient type to work with in many Serilog scenarios (enrichers, sinks, ...). To
convert the result to plain-old-.NET-types like `string`, `bool`, `Dictionary<K,V>` and
`Array`, use the functions in the `Serilog.Expressions.ExpressionResult` class:

```csharp
    var result = compiled(someEvent);

    // `true` only if `result` is a scalar Boolean `true`; `false` otherwise:
    if (ExpressionResult.IsTrue(result))
    {
        Console.WriteLine("The event matched.");
    }
```

## Implementing user-defined functions

User-defined functions can be plugged in by implementing static methods that:

 * Return `LogEventPropertyValue?`,
 * Have arguments of type `LogEventPropertyValue?` or `LogEvent`,
 * If the `ci` modifier is supported, accept a `StringComparison`, and
 * If culture-specific formatting or comparisons are used, accepts an `IFormatProvider`.

For example:

```csharp
public static class MyFunctions
{
    public static LogEventPropertyValue? IsHello(
        StringComparison comparison,
        LogEventPropertyValue? maybeHello)
    {
        if (maybeHello is ScalarValue sv && sv.Value is string s)
            return new ScalarValue(s.Equals("Hello", comparison));

        // Undefined - argument was not a string.
        return null;
    }
}
```

In the example, `IsHello('Hello')` will evaluate to `true`, `IsHello('HELLO')` will be `false`, `IsHello('HELLO') ci`
will be `true`, and `IsHello(42)` will be undefined.

User-defined functions are supplied through an instance of `NameResolver`:

```csharp
var myFunctions = new StaticMemberNameResolver(typeof(MyFunctions));
var expr = SerilogExpression.Compile("IsHello(User.Name)", nameResolver: myFunctions);
// Filter events based on whether `User.Name` is `'Hello'` :-)
```

## Acknowledgements

Includes the parser combinator implementation from [Superpower](https://github.com/datalust/superpower), copyright Datalust,
Superpower Contributors, and Sprache Contributors; licensed under the Apache License, 2.0.
