using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace TravisRFrench.Attributes.Runtime
{
    /// <summary>
    /// Represents a modifiable attribute that can have modifiers applied to it
    /// and uses a calculation strategy to determine its modified value.
    /// </summary>
    [Serializable]
    public class Attribute : IReadOnlyAttribute
    {
        [SerializeField]
        private float baseValue;
        [SerializeField]
        private float modifiedValue;
        [SerializeField]
        private List<AttributeModifier> modifiers;
        private ICalculationStrategy strategy;
        private bool isDirty = true;

        /// <summary>
        /// Gets or sets the base value of the attribute. Setting this value marks the attribute as dirty
        /// and triggers recalculation of the modified value.
        /// </summary>
        public float BaseValue
        {
            get => this.baseValue;
            set
            {
                this.baseValue = value;
                this.isDirty = true;
                this.NotifyModified();
            }
        }

        /// <summary>
        /// Gets the modified value of the attribute, recalculating it if necessary.
        /// </summary>
        public float ModifiedValue
        {
            get
            {
                if (this.isDirty)
                {
                    this.ForceRecalculateModifiedValue();
                }

                return this.modifiedValue;
            }
        }

        /// <summary>
        /// Event that is triggered whenever the attribute's modified value changes.
        /// </summary>
        public event Action<Attribute> Modified;
        
        /// <summary>
        /// Initializes a new instance of the Attribute class with an empty list of modifiers.
        /// </summary>
        public Attribute()
        {
            this.strategy = new CalculationStrategy();
            this.modifiers = new List<AttributeModifier>();
        }

        /// <summary>
        /// Adds a modifier to the attribute. If the modifier is already present, it will not be added again.
        /// Adding a modifier marks the attribute as dirty and triggers recalculation.
        /// </summary>
        /// <param name="modifier">The modifier to add.</param>
        public void AddModifier([NotNull] AttributeModifier modifier)
        {
            if (this.modifiers.Contains(modifier))
            {
                return;
            }
            
            this.modifiers.Add(modifier);
            this.isDirty = true;
            this.NotifyModified();
        }

        /// <summary>
        /// Removes a modifier from the attribute. If the modifier is not present, no action is taken.
        /// Removing a modifier marks the attribute as dirty and triggers recalculation.
        /// </summary>
        /// <param name="modifier">The modifier to remove.</param>
        public void RemoveModifier([NotNull] AttributeModifier modifier)
        {
            if (!this.modifiers.Contains(modifier))
            {
                return;
            }

            this.modifiers.Remove(modifier);
            this.isDirty = true;
            this.NotifyModified();
        }

        /// <summary>
        /// Clears all modifiers from the attribute, marks it as dirty, and triggers recalculation.
        /// </summary>
        public void ClearModifiers()
        {
            this.modifiers.Clear();
            this.isDirty = true;
            this.NotifyModified();
        }

        /// <summary>
        /// Forces the recalculation of the attribute's modified value immediately.
        /// </summary>
        public void ForceRecalculateModifiedValue()
        {
            this.modifiedValue = this.CalculateModifiedValue();
            this.isDirty = false;
        }

        /// <summary>
        /// Sets the calculation strategy used to determine the attribute's modified value.
        /// </summary>
        /// <param name="strategy">The calculation strategy to use.</param>
        public void UseCalculationStrategy([NotNull] ICalculationStrategy strategy)
        {
            this.strategy = strategy;
        }

        /// <summary>
        /// Notifies subscribers that the attribute has been modified.
        /// </summary>
        private void NotifyModified()
        {
            this.Modified?.Invoke(this);
        }
        
        /// <summary>
        /// Calculates the modified value of the attribute using the assigned calculation strategy.
        /// </summary>
        /// <returns>The calculated modified value.</returns>
        private float CalculateModifiedValue()
        {
            return this.strategy?.Calculate(this.baseValue, this.modifiers) ?? this.baseValue;
        }
    }
}
