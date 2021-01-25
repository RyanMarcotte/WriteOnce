# WriteOnce

## WriteOnce<T>

`WriteOnce<T>` is a thread-safe value container whose value can be set exactly once.  Install the `WriteOnceContainer` NuGet package.

``` csharp
using WriteOnceContainer;


var writeOnce = new WriteOnce<int>();
var value = writeOnce.Value; // throws ValueNotSetException because value has not been set yet
writeOnce.Value = 1337; // OK because value is being set for the first time
writeOnce.Value = 9001; // throws ValueAlreadySetException because value has already been set

// override the default error messages with your own
var writeOnceWithCustomErrorMessages = new WriteOnce<int>(
    () => new ValueNotSetException("Value not set!"),
    () => new ValueAlreadySetException("Value already set!"));
```

## Fluent Assertions

Install the `WriteOnceContainer.FluentAssertions` NuGet package.  This NuGet package extends the [`FluentAssertions` library](https://github.com/fluentassertions/fluentassertions) so that assertions can be performed against `WriteOnce<T>` instances.

``` csharp
using FluentAssertions;
using WriteOnceContainer;
using WriteOnceContainer.FluentAssertions;

// assert that a WriteOnce<T> instance holds a value
const int VALUE = 1337;
var writeOnceWithValue = new WriteOnce<int>();
writeOnceWithValue = VALUE;
writeOnceWithValue.Should().HaveValue().AndValue.Should().Be(VALUE);

// assert that a WriteOnce<T> instance does not hold a value
var writeOnceWithoutValue = new WriteOnce<string>();
writeOnceWithoutValue.Should().NotHaveValue();
```

## Blazor Components

Install the `WriteOnceContainer.Blazor` NuGet package.  This NuGet package defines components used for rendering `WriteOnce<T>` in Blazor applications.

``` html
@using WriteOnceContainer
@using WriteOnceContainer.Blazor

<WriteOnceMatch WriteOnce="_container">
    <ValueSet Context="number">
        <!-- displayed if WriteOnce<T> container holds a value -->
        <p>@number</p>
    </ValueSet>
    <ValueNotSet>
        <!-- displayed if WriteOnce<T> container does not hold a value -->
        <p>No value set!</p>
    </ValueNotSet>
</WriteOnceMatch>

@code {
    private readonly WriteOnce<int> _container;
}
```
