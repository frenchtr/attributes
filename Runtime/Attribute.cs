using System;
using System.Collections.Generic;
using UnityEngine;

namespace TravisRFrench.Attributes.Runtime
{
    [Serializable]
    public class Attribute : IReadOnlyAttribute
    {
        [SerializeField]
        private float baseValue;
        [SerializeField]
        private float modifiedValue;
        [SerializeField]
        private List<AttributeModifier> modifiers;
        private bool isDirty = true;

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

        public event Action<Attribute> Modified;
        
        public Attribute()
        {
            this.modifiers = new List<AttributeModifier>();
        }

        public void AddModifier(AttributeModifier modifier)
        {
            if (this.modifiers.Contains(modifier))
            {
                return;
            }
            
            this.modifiers.Add(modifier);
            this.isDirty = true;
            this.NotifyModified();
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            if (!this.modifiers.Contains(modifier))
            {
                return;
            }

            this.modifiers.Remove(modifier);
            this.isDirty = true;
            this.NotifyModified();
        }

        public void ClearModifiers()
        {
            this.modifiers.Clear();
            this.isDirty = true;
            this.NotifyModified();
        }

        public void ForceRecalculateModifiedValue()
        {
            this.modifiedValue = this.CalculateModifiedValue();
            this.isDirty = false;
        }

        private void NotifyModified()
        {
            this.Modified?.Invoke(this);
        }
        
        private float CalculateModifiedValue()
        {
            var result = this.BaseValue;
            var sumOfPercentAdditiveModifiers = 0f;

            for (var i = 0; i < this.modifiers.Count; i++)
            {
                var modifier = this.modifiers[i];
                
                switch (modifier.Operand)
                {
                    case ModifierOperand.FlatAdditive:
                        result += modifier.Value;
                        break;
                    case ModifierOperand.PercentAdditive:
                        sumOfPercentAdditiveModifiers += modifier.Value;
                        
                        if (i + 1 >= this.modifiers.Count || this.modifiers[i + 1].Operand != ModifierOperand.PercentAdditive)
                        {
                            result *= 1 + sumOfPercentAdditiveModifiers;
                            sumOfPercentAdditiveModifiers = 0f;
                        }

                        break;
                    case ModifierOperand.PercentMultiplicative:
                        result *= 1 + modifier.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            result = (float) Math.Round(result, 4);
            
            return result;
        }
    }
}
