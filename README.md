# Attribute System Package for Unity

## Introduction
This package provides a flexible and extensible system for managing attributes in Unity games. Attributes represent character or item properties, such as damage, strength, or speed, which can be modified by various in-game factors. The system supports flat and percentage-based modifiers, including additive and multiplicative types, as well as override capabilities for setting attributes to specific values.

## Features
- **Attribute Management**: Define and manage base values for any game attribute.
- **Modifiers**: Apply modifiers to attributes, supporting flat additions, multiplicative factors, and percentage-based adjustments.
- **Custom Calculation Strategies**: Implement custom strategies for calculating modified attribute values using the Strategy pattern.
- **Serialization Support**: Attributes and modifiers are `[Serializable]`, allowing them to be easily integrated with Unity's serialization system.

## Installation
To install the Attribute System Package, follow these steps:

1. Download the latest package release.
2. In your Unity project, navigate to `Assets -> Import Package -> Custom Package`.
3. Select the downloaded package file and click `Open`.
4. Ensure all files are selected and click `Import`.

## Usage

### Defining an Attribute
```csharp
using TravisRFrench.Attributes.Runtime;

// Create a new attribute with a base value
var healthAttribute = new Attribute { BaseValue = 100f };
```

### Applying Modifiers
```csharp
// Create a flat additive modifier
var bonusHealth = new AttributeModifier { Type = ModifierType.FlatAdditive, Value = 25f };
healthAttribute.AddModifier(bonusHealth);

// Create a percentage additive modifier
var buff = new AttributeModifier { Type = ModifierType.PercentAdditive, Value = 0.1f }; // 10% buff
healthAttribute.AddModifier(buff);
```

### Custom Calculation Strategy
Implement ICalculationStrategy to create custom logic for calculating the modified value of an attribute.
```csharp
public class CustomCalculationStrategy : ICalculationStrategy
{
    public float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers)
    {
        // Custom calculation logic
    }
}

// Apply the custom strategy
healthAttribute.UseCalculationStrategy(new CustomCalculationStrategy());
```
### Accessing Modified Value
```csharp
var modifiedHealth = healthAttribute.ModifiedValue;
```
### Running Tests
This package includes unit tests to ensure the reliability of its core functionalities. To run the tests:

Open the Unity Test Runner via `Window` -> `General` -> `Test Runner`.
Navigate to the `EditMode` tab.
Click Run All to execute all tests.

## Contributing
Contributions to the Attribute System Package are welcome. Please follow the existing code style and add unit tests for any new or changed functionality. Submit pull requests for review.

## License
This package is available under the MIT License. See the `LICENSE.md` file for more details.
