using System.Collections;
using NUnit.Framework;
using TravisRFrench.Attributes.Runtime;
using UnityEngine;
using UnityEngine.TestTools;

namespace TravisRFrench.Attributes.Tests.PlayMode
{
    public class AttributeSystemPlayModeTests
    {
        private GameObject testCharacter;
        private AttributeComponent attributeComponent;

        [SetUp]
        public void SetUp()
        {
            // Initialize GameObject and AttributeComponent for each test
            this.testCharacter = new GameObject("TestCharacter");
            this.attributeComponent = this.testCharacter.AddComponent<AttributeComponent>(); // Custom component managing attributes
            this.attributeComponent.Initialize(100); // Example initialization with a base value
        }

        [UnityTest]
        public IEnumerator AddingFlatAdditiveModifier_UpdatesAttributeCorrectly()
        {
            // Arrange
            var modifier = new AttributeModifier
            {
                Type = ModifierType.FlatAdditive, 
                Value = 20f
            };

            // Act
            this.attributeComponent.ApplyModifier(modifier);
            
            // Wait a frame for any updates
            yield return null; 

            // Assert
            Assert.AreEqual(120f, this.attributeComponent.CharacterAttribute.ModifiedValue);
        }

        [UnityTest]
        public IEnumerator RemovingModifier_RevertsAttributeValue()
        {
            // Arrange
            var modifier = new AttributeModifier { Type = ModifierType.FlatAdditive, Value = 20f };
            this.attributeComponent.ApplyModifier(modifier);

            // Act
            this.attributeComponent.RemoveModifier(modifier);
            
            // Wait a frame for updates
            yield return null; 

            // Assert
            Assert.AreEqual(100f, this.attributeComponent.CharacterAttribute.ModifiedValue);
        }

        [UnityTest]
        public IEnumerator UpdatingModifierValue_ReflectsInModifiedValue()
        {
            // Arrange
            var modifier = new AttributeModifier { Type = ModifierType.FlatAdditive, Value = 15f };
            this.attributeComponent.ApplyModifier(modifier);

            // Act
            modifier.Value = 25f;
            this.attributeComponent.ForceRecalculateModifiedValue();
            yield return null;

            // Assert
            Assert.AreEqual(125f, this.attributeComponent.CharacterAttribute.ModifiedValue);
        }

        [UnityTest]
        public IEnumerator AttributeIntegration_WithGameplayEvents_ReflectsCorrectValue()
        {
            // Simulate a gameplay event that affects the attribute, such as receiving a health buff
            // Arrange
            var healthBuff = new AttributeModifier
            {
                Type = ModifierType.PercentAdditive, 
                Value = 0.1f
            }; // 10% buff
            this.attributeComponent.ApplyModifier(healthBuff);

            // Act
            
            // Wait for the buff to apply
            yield return null; 

            // Assert
            Assert.AreEqual(110f, this.attributeComponent.CharacterAttribute.ModifiedValue);
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup
            Object.DestroyImmediate(this.testCharacter);
        }
    }
}
