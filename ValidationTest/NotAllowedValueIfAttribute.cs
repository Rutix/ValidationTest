using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ValidationTest
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class NotAllowedValueIfAttribute : ValidationAttribute
	{
		public object NotAllowedValue { get; }
		public string DependentPropertyName { get; }
		public object[] DependentValues { get; }

		public NotAllowedValueIfAttribute(object notAllowedValue, string dependentPropertyName, params object[] dependentValues)
		{
			NotAllowedValue = notAllowedValue ?? throw new ArgumentNullException(nameof(notAllowedValue));
			DependentPropertyName = dependentPropertyName ?? throw new ArgumentNullException(nameof(dependentPropertyName));
			DependentValues = dependentValues ?? throw new ArgumentNullException(nameof(dependentValues));
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var dependentProperty = validationContext.ObjectType.GetProperty(DependentPropertyName);

			if (dependentProperty == null)
			{
				throw new ArgumentException(
					$"Het veld {DependentPropertyName} bestaat niet in het type {validationContext.ObjectType.Name}");
			}

			var dependentPropertyValue = dependentProperty.GetValue(validationContext.ObjectInstance);

			if (dependentPropertyValue != null && value != null && IsInTargetList(dependentPropertyValue) && value.Equals(NotAllowedValue))
			{
				return new ValidationResult(ErrorMessage =
					$"De combinatie van {NotAllowedValue} en {string.Join(" of ", DependentValues.ToArray())} is niet toegestaan.");
			}

			return ValidationResult.Success;
		}

		private bool IsInTargetList(object dependentValue)
		{
			return DependentValues.Contains(dependentValue);
		}
	}
}