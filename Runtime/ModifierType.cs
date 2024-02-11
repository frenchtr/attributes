namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Defines the types of modifiers that can be applied to an attribute.
    /// </summary>
    public enum ModifierType
    {
        /// <summary>
        /// Represents a flat additive modifier, which directly adds or subtracts from the attribute's value.
        /// </summary>
        FlatAdditive,

        /// <summary>
        /// Represents a flat multiplicative modifier, which multiplies the attribute's value by a specified factor.
        /// </summary>
        FlatMultiplicative,

        /// <summary>
        /// Represents a percent additive modifier, which modifies the attribute's value by a specified percentage,
        /// adding to the base value.
        /// </summary>
        PercentAdditive,

        /// <summary>
        /// Represents a percent multiplicative modifier, which modifies the attribute's value by applying a percentage
        /// as a multiplicative factor, affecting the total value.
        /// </summary>
        PercentMultiplicative,

        /// <summary>
        /// Represents an override modifier, which sets the attribute's value directly, ignoring other modifiers.
        /// </summary>
        Override,
    }
}
