using System.Collections.Generic;

namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Defines a strategy for calculating the modified value of an attribute,
    /// given its base value and a collection of modifiers.
    /// </summary>
    public interface ICalculationStrategy
    {
        /// <summary>
        /// Calculates the modified value of an attribute based on its base value and a set of modifiers.
        /// </summary>
        /// <param name="baseValue">The base value of the attribute before any modifications.</param>
        /// <param name="modifiers">A collection of <see cref="AttributeModifier"/> objects that will modify the base value.</param>
        /// <returns>The calculated value after all modifiers have been applied.</returns>
        float Calculate(float baseValue, IEnumerable<AttributeModifier> modifiers);
    }
}
