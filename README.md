# AnvilX

A framework library for building modern Unity projects, encouraging best practices and simple application design.

_**AnvilX is still a work-in-progress; breaking changes are possible.**_

### Service Locator Pattern
AnvilX provides a simple but powerful scene-centric service locator pattern that fits well with the typical Unity application lifecycle.

Automatically generate boilerplate properties using the DependsOn attribute, powered by Roslyn source generators.

```c#
public class MyService : Service<MyService> 
{
    public void DoSomething()
    {
        Debug.Log("Hello World!");
    }
}

[DependsOn(typeof(MyService), CustomPropertyName = "MyCustomService"), AccessModifierLevel = AccessModifierLevel.Protected)]
public partial class MyApplicationFeature : Feature
{
    private void Start()
    {
        MyCustomService.DoSomething();
    }
}
```
### Global Object Pattern
AnvilX provides the concept of global prefabs, automatically instantiated before the first scene loads,
allowing you to separate your global app behaviour, improving iteration times and allowing you to take 
advantage of multi-scene workflows.

Simply create a GlobalAssetManifest ScriptableObject, assign it as a preloaded asset and away you go!
All prefabs referenced in the manifest will be automatically instantiated (in the DontDestroyOnLoad scene)
before your scene loads; in editor and the built game.

