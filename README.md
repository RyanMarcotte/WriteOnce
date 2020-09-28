# WriteOnce

`WriteOnce<T>` is a thread-safe value container whose value can be set exactly once.

``` csharp
var writeOnce = new WriteOnce<int>();
var value = writeOnce.Value; // throws ValueNotSetException because value has not been set yet
writeOnce.Value = 1337; // OK because value is being set for the first time
writeOnce.Value = 9001; // throws ValueAlreadySetException because value has already been set

// override the default error messages with your own
var writeOnceWithCustomErrorMessages = new WriteOnce<int>(
    () => new ValueNotSetException("Value not set!"),
    () => new ValueAlreadySetException("Value already set!"));
```
