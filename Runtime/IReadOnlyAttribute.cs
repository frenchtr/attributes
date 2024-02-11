using System;

namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Defines a read-only interface for an attribute, providing access to its base and modified values,
    /// and an event that is triggered when the attribute is modified.
    /// </summary>
    public interface IReadOnlyAttribute
    {
        /// <summary>
        /// Gets the base (unmodified) value of the attribute.
        /// </summary>
        float BaseValue { get; }

        /// <summary>
        /// Gets the value of the attribute after applying all modifiers.
        /// </summary>
        float ModifiedValue { get; }
        
        /// <summary>
        /// Occurs when the attribute's modified value changes.
        /// This event can be used to notify observers of changes to the attribute's state.
        /// </summary>
        event Action<Attribute> Modified;
    }
}
