using System.Collections.Generic;
using System.Linq;

namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Responsible for providing the calculation logic when applying AttributeModifiers to an Attribute.
    /// </summary>
    public class CalculationStrategy : ICalculationStrategy
    {
        /// <summary>
        /// Calculates the resulting value after all modifiers are applied.
        /// </summary>
        /// <param name="baseValue">The base value that should be used.</param>
        /// <param name="modifiers">A collection of modifiers to be applied to the base value.</param>
        /// <returns></returns>
        public virtual float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers)
        {
            var result = baseValue;
            var attributeModifiers = modifiers.ToList();
            
            // Sum of all flat additive modifiers. These are applied directly to the base value.
            
            var sumOfFlatAdditiveMultipliers = attributeModifiers
                .Where(modifier => modifier.Type == ModifierType.FlatAdditive)
                .Select(modifier => modifier.Value)
                .Sum();
            
            // Total impact of percent additive modifiers on the base value.
            // This calculates how much to add to the base value based on a percentage.
            var percentAdditiveTotalImpact = baseValue * attributeModifiers
                .Where(modifier => modifier.Type == ModifierType.PercentAdditive)
                .Sum(modifier => modifier.Value);

            // Multiplicative factor from all flat multiplicative modifiers.
            // This factor is calculated by multiplying all such modifiers together,
            // then applied to the current result.
            var multiplicativeFactor = attributeModifiers
                .Where(modifier => modifier.Type == ModifierType.FlatMultiplicative)
                .Aggregate(1.0f, (current, modifier) => current * modifier.Value);
            
            // Multiplicative percent factor from all percent multiplicative modifiers.
            // Similar to flat multiplicative, but these are based on a percentage increase or decrease,
            // calculated by aggregating each modifier as a factor (1 + modifier.Value),
            // then applied to the current result.
            var multiplicativePercentFactor = attributeModifiers
                .Where(modifier => modifier.Type == ModifierType.PercentMultiplicative)
                .Aggregate(1.0f, (current, modifier) => current * (1 + modifier.Value));
            
            // Check for the presence of an override modifier.
            // If one exists, it sets the result to a specific value, ignoring all previous calculations.
            var overrideModifier = attributeModifiers
                .LastOrDefault(modifier => modifier.Type == ModifierType.Override);

            // Apply the sum of flat additive multipliers and the percent additive total impact to the result.
            result += sumOfFlatAdditiveMultipliers;
            result += percentAdditiveTotalImpact;
            
            // Apply the multiplicative factors to the result.
            result *= multiplicativeFactor;
            result *= multiplicativePercentFactor;
            
            // Apply the override modifier's value if it exists.
            if (overrideModifier != null)
            {
                result = overrideModifier.Value;
            }
            
            return result;
        }
    }
}
