using System;
using System.Collections.Generic;
using TravisRFrench.Attributes.Runtime;
using UnityEngine;
using Attribute = TravisRFrench.Attributes.Runtime.Attribute;

namespace TravisRFrench.Attributes.Tests.PlayMode
{
    public class AttributeComponent : MonoBehaviour
    {
        [SerializeField]
        private Attribute characterAttribute;

        public IReadOnlyAttribute CharacterAttribute => this.characterAttribute;

        // Example initialization method
        public void Initialize(float baseValue)
        {
            // Initialize the attribute if null
            this.characterAttribute ??= new Attribute();
            
            // Assign a base value
            this.characterAttribute.BaseValue = baseValue;
        }

        public void ApplyModifier(AttributeModifier modifier)
        {
            this.characterAttribute.AddModifier(modifier);
            this.ForceRecalculateModifiedValue();
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            this.characterAttribute.RemoveModifier(modifier);
            this.ForceRecalculateModifiedValue();
        }

        public void ForceRecalculateModifiedValue()
        {
            this.characterAttribute.ForceRecalculateModifiedValue();
        }

        private void UseCalculationStrategy(ICalculationStrategy strategy)
        {
            this.characterAttribute.UseCalculationStrategy(strategy);
        }
    }
}
