using System;
using UnityEngine;

namespace TravisRFrench.Attributes.Runtime
{
    [Serializable]
    public class AttributeModifier
    {
        [field: SerializeField]
        public float Value { get; set; }
        [field: SerializeField]
        public ModifierOperand Operand { get; set; }
    }
}
