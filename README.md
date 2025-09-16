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

[DependsOn(
        typeof(MyService),
        CustomPropertyName = "MyCustomService",
        AccessModifierLevel = AccessModifierLevel.Protected)]
public partial class MyApplicationFeature : MonoBehaviour
{
    private void Start()
    {
        MyCustomService.DoSomething();
    }
}
```

#### DependsOn Attribute

The DependsOn attribute is used to guide the source generator to automatically generate a C# property 
and other relevant boilerplate code to lazily resolve a dependency upon usage. The optional parameters are shown below.

| Property              | Description                                                                                                    | Default Value        |
|-----------------------|----------------------------------------------------------------------------------------------------------------|----------------------|
| Custom Property Name  | The name to give to the generated C# property.                                                                 | The name of the type |
| Access Modifier Level | The access level to give to the generated C# property.                                                         | Protected            |
| Optional              | Whether the dependency is optional. When false, an exception will be thrown if the dependency cannot be found. | False                |
| Resolve As            | The type to resolve the dependency as. Use this to specify a unique interface to resolve as.                   | The type             |

Any class using the DependsOn attribute **must** be marked as partial for it to work.

An example of some generated code is shown below.

```c#

[DependsOn(typeof(EntityManager))]
[DependsOn(
    typeof(PlayerManager),
    CustomPropertyName = "ThePlayerManager",
    AccessModifierLevel = AccessModifierLevel.Internal]
public partial class MyFeature : MonoBehaviour
{
    private void Start()
    {
        // As soon as we access EntityManager, dependencies will be resolved.
        var playerEntity = EntityManager.SpawnEntity("player");
        ThePlayerManager.SetPlayerEntity(playerEntity);
    }
}

// An approximation of the generated code (implementation is hidden for simplicity)
// Note that the class must be partial for this to work!
public partial class MyFeature
{
    // An internal type that runs internal behaviour, injection, etc.
    // Direct usage of this is not recommended. This will be null until
    // properties have gone through initialization.
    private Generated_MyFeature __Sample2_Generated;
    
    // These are the generated properties, upon access of any property, an
    // initialization flow will be invoked, triggering resolution of dependencies.
    // It should be noted that if a non-optional dependency fails to resolve, one
    // of these will throw an exception. This should help you catch missing dependencies
    // as early as possible during development.
    protected EntityManager EntityManager { get; } // Default to protected visibility 
    
    // Notice that this property is marked as internal, as defined in the attribute.
    internal PlayerManager ThePlayerManager { get; }
    
    // These are specialized utility properties, intended for edge-case usages.
    // Imagine that your class never touches any of the dependencies. But you're
    // in a cleanup phase, and you may want to unsubscribe from something. You don't
    // want to run initialization, you just want to get the property if it exists.
    // This is what these are for!
    protected EntityManager EntityManagerRaw { get; }
    protected PlayerManager PlayerManagerRaw { get; }
    
    // This is where the internal implementation goes.
    private class Generated_MyFeature() { }
}

```
#### Dependency Framework

The Dependency resolution framework is pretty simple, objects are stored within ObjectRegistry instances, 
one for each scene. This includes the DontDestroyOnLoad scene, which is a special edge-case. When you 
register a service globally using the global object pattern, it will become available to all other scenes.

When querying for an object, the framework first looks in its own scene registry, then falls back to looking
in the global registry. Objects are stored in a dictionary-style collection, indexed by a particular type.
An object can be registered under many different types. You must always specify the exact type when you are looking
up an object.

```c#

public partial class EntityManager : MonoBehaviour, IEntityManager
{
    private ObjectRegistry registry;
    
    // The cleanest place to perform registration is during Awake.
    // Somewhat non-standard for Unity because we usually don't want
    // to talk to other objects until we're running Start. The framework
    // guarantees that a registry is available as soon as you call EnsureRegistry.
    protected virtual void Awake()
    {
        // NOTE: Registries are actually visible as GameObjects in your scene, which
        // lets you interact and figure out what is registered where! Generated as
        // soon as you call EnsureRegistry.
        
        // Find the ObjectRegistry for this GameObject.
        registry = RegistryCore.EnsureRegistry(gameObject);
        
        // Register this object by type.
        registry.Register<EntityManager>(this);
        
        // Another way you could register the object
        registry.Register<IEntityManager>(this);
    }
    
    protected virtual void OnDestroy()
    {
        // Its important to unregister
        registry.Unregister<EntityManager>();
        registry.Unregister<IEntityManager>();
    }
}

```

**You may have wondered, will this all break if an object changes scene? The answer is yes, it probably will.
It is strongly advised to avoid moving objects between scenes. The framework should provide enough tools to
avoid the need to move anything between scenes!**

#### Service class

Currently, the library does not provide a convenience attribute for object registration. To help with reducing
boilerplate code, the library provides a set of Service classes. Use these to reduce the boilerplate code you
need to write. The generic service classes handle the self-registration behaviour for you, similarly to the 
code example above.

```c#

// This code is roughly equivalent to the above code sample, with lots of the boilerplate done for you!
public partial class EntityManager : Service<EntityManager, IEntityManager>, IEntityManager
{
    // The library provides a few variants of the Service class, with a varying number of generic type parameters.
}
```

### Global Object Pattern
AnvilX provides the concept of global prefabs, automatically instantiated before the first scene loads,
allowing you to separate your global app behaviour, improving iteration times and allowing you to take 
advantage of multi-scene workflows.

Simply create a GlobalAssetManifest ScriptableObject, assign it as a preloaded asset and away you go!
All prefabs referenced in the manifest will be automatically instantiated (in the DontDestroyOnLoad scene)
before your scene loads; in editor and the built game.

### Roadmap

* A high-performance generalized data framework for game data persistence.
* A generalized project configuration framework.
* A sample Unity project.

**_Note: this roadmap is exploratory and subject to change as the framework evolves._**

