using System;

namespace TravisRFrench.Attributes.Runtime
{
    public interface IReadOnlyAttribute
    {
        float BaseValue { get; }
        float ModifiedValue { get; }
        
        event Action<Attribute> Modified;
    }
}
