using System;
using UnityEngine;

namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Represents a modifier that can be applied to an attribute, altering its value according to a specific type and value.
    /// </summary>
    [Serializable]
    public class AttributeModifier
    {
        /// <summary>
        /// Gets or sets the value of the modifier. This could represent an additive or multiplicative change,
        /// or a new value entirely, depending on the modifier type.
        /// </summary>
        [field: SerializeField]
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the modifier, which determines how the Value is applied to an attribute's base value.
        /// See <see cref="ModifierType"/> for descriptions of each type.
        /// </summary>
        [field: SerializeField]
        public ModifierType Type { get; set; }
    }
}
