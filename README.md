# Attribute System Package for Unity

## Introduction
This package provides a flexible and extensible system for managing attributes in Unity games. Attributes represent character or item properties, such as damage, strength, or speed, which can be modified by various in-game factors. The system supports flat and percentage-based modifiers, including additive and multiplicative types, as well as override capabilities for setting attributes to specific values.

## Features
- **Attribute Management**: Define and manage base values for any game attribute.
- **Modifiers**: Apply modifiers to attributes, supporting flat additions, multiplicative factors, and percentage-based adjustments.
- **Custom Calculation Strategies**: Implement custom strategies for calculating modified attribute values using the Strategy pattern.
- **Serialization Support**: Attributes and modifiers are `[Serializable]`, allowing them to be easily integrated with Unity's serialization system and adjusted in the inspector.

## Installation
To install the Attribute System Package via Unity's Package Manager with a .git URL, follow these steps:

- Open your Unity project.
- Navigate to `Window` `->` `Package Manager` to open the Package Manager window.
- Click the `+` button at the top left of the Package Manager window.
- Select `Add package from git URL...`.
- Enter the following .git URL: `https://github.com/frenchtr/attributes.git`
- Click Add. Unity will now resolve the package and download it into your project.

## Usage

### Defining an attribute
```csharp
// Create a new attribute with a base value
var healthAttribute = new Attribute { BaseValue = 100f };
```

### Applying modifiers
```csharp
// Create a flat additive modifier
var bonusHealth = new AttributeModifier { Type = ModifierType.FlatAdditive, Value = 25f };
healthAttribute.AddModifier(bonusHealth);

// Create a percentage additive modifier
var buff = new AttributeModifier { Type = ModifierType.PercentAdditive, Value = 0.1f }; // 10% buff
healthAttribute.AddModifier(buff);
```

### Custom calculation strategy
Implement `ICalculationStrategy` to create custom logic for calculating the modified value of an attribute.

#### Defining a custom calculation strategy
```csharp
public class CustomCalculationStrategy : ICalculationStrategy
{
    public float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers)
    {
        // Custom calculation logic
    }
}
```

#### Using the strategy
```csharp
// Apply the custom strategy
healthAttribute.UseCalculationStrategy(new CustomCalculationStrategy());
```
### Accessing modified value
```csharp
var modifiedHealth = healthAttribute.ModifiedValue;
```
### Running tests
This package includes unit tests to ensure the reliability of its core functionalities. To run the tests:

Open the Unity Test Runner via `Window` `->` `General` `->` `Test Runner`.
Navigate to the `EditMode` tab.
Click Run All to execute all tests.

## Contributing
Contributions to the Attribute System Package are welcome. Please follow the existing code style and add unit tests for any new or changed functionality. Submit pull requests for review.

## License
This package is available under the MIT License. See the `LICENSE.md` file for more details.
