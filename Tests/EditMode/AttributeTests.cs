using NUnit.Framework;
using TravisRFrench.Attributes.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace TravisRFrench.Attributes.Tests
{
    /// <summary>
    /// Contains unit tests for the Attribute class to verify the correct application of modifiers and calculation strategies.
    /// </summary>
    public class AttributeTests
    {
        /// <summary>
        /// Tests that an attribute with no modifiers has its base value unchanged.
        /// </summary>
        [Test]
        public void Attribute_WithNoModifiers_HasBaseValue()
        {
            var attribute = new Attribute
            {
                BaseValue = 10f
            };
            Assert.AreEqual(10f, attribute.BaseValue);
        }

        /// <summary>
        /// Tests that applying a flat additive modifier to an attribute correctly updates its modified value.
        /// </summary>
        [Test]
        public void Attribute_WithFlatAdditiveModifier_UpdatesModifiedValue()
        {
            var attribute = new Attribute
            {
                BaseValue = 10f
            };
            var modifier = new AttributeModifier
            {
                Type = ModifierType.FlatAdditive, 
                Value = 5f
            };
            
            attribute.AddModifier(modifier);
            attribute.ForceRecalculateModifiedValue();
            
            Assert.AreEqual(15f, attribute.ModifiedValue);
        }

        /// <summary>
        /// Tests that applying a percent additive modifier to an attribute correctly updates its modified value.
        /// </summary>
        [Test]
        public void Attribute_WithPercentAdditiveModifier_UpdatesModifiedValue()
        {
            var attribute = new Attribute
            {
                BaseValue = 100f
            };
            var modifier = new AttributeModifier
            {
                Type = ModifierType.PercentAdditive,
                Value = 0.1f // Represents a 10% increase.
            };
            
            attribute.AddModifier(modifier);
            attribute.UseCalculationStrategy(new PercentAdditiveStrategy());
            attribute.ForceRecalculateModifiedValue();
            
            Assert.AreEqual(110f, attribute.ModifiedValue);
        }

        /// <summary>
        /// Tests that an override modifier directly sets the attribute's modified value, ignoring other modifiers.
        /// </summary>
        [Test]
        public void Attribute_WithOverrideModifier_SetsModifiedValueDirectly()
        {
            var attribute = new Attribute
            {
                BaseValue = 50f
            };
            var overrideModifier = new AttributeModifier
            {
                Type = ModifierType.Override, 
                Value = 100f
            };
            
            attribute.AddModifier(overrideModifier);
            attribute.UseCalculationStrategy(new OverrideStrategy());
            attribute.ForceRecalculateModifiedValue();
            
            Assert.AreEqual(100f, attribute.ModifiedValue);
        }
    }

    /// <summary>
    /// A calculation strategy that applies percent additive modifiers to the base value of an attribute.
    /// </summary>
    public class PercentAdditiveStrategy : ICalculationStrategy
    {
        /// <summary>
        /// Calculates the attribute's modified value by applying percent additive modifiers to its base value.
        /// </summary>
        /// <param name="baseValue">The base value of the attribute.</param>
        /// <param name="modifiers">A collection of modifiers to apply.</param>
        /// <returns>The modified value after applying percent additive modifiers.</returns>
        public float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers)
        {
            return baseValue + modifiers
                .Where(modifier => modifier.Type == ModifierType.PercentAdditive)
                .Sum(modifier => baseValue * modifier.Value);
        }
    }

    /// <summary>
    /// A calculation strategy that applies an override modifier, setting the attribute's value directly.
    /// </summary>
    public class OverrideStrategy : ICalculationStrategy
    {
        /// <summary>
        /// Calculates the attribute's value using an override modifier, if present, ignoring other modifiers.
        /// </summary>
        /// <param name="baseValue">The base value of the attribute.</param>
        /// <param name="modifiers">A collection of modifiers to consider.</param>
        /// <returns>The attribute's value as determined by the override modifier, or the base value if no override is present.</returns>
        public float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers)
        {
            var overrideMod = modifiers.FirstOrDefault(m => m.Type == ModifierType.Override);
            return overrideMod?.Value ?? baseValue;
        }
    }
}
