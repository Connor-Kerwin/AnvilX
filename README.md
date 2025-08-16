# AnvilX

A framework library for building modern Unity projects, encouraging best practices and simple application design.

### Service Locator Pattern
AnvilX provides a simple but powerful scene-centric service locator pattern that fits well with the typical Unity application lifecycle.

Register and resolve services with ease.

```c#
public class MyService : Service<MyService> { }

public class MyApplicationFeature : Feature
{
    private void Start()
    {
        var myService = ResolveRequiredDependency<MyService>();         
    }
}
```
