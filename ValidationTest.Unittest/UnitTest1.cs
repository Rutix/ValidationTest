using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ValidationTest.Unittest
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var sut = new Foobar()
			{
				Bar = "Bar",
				Foo = "foo"
			};

			var errors = Validate(sut);
			Assert.NotEmpty(errors);
		}

		public List<ValidationResult> Validate(object sut)
		{
			var ctx = GetValidationContext(sut);
			var errors = new List<ValidationResult>();
			Validator.TryValidateObject(sut, ctx, errors, true);
			return errors;
		}

		public ValidationContext GetValidationContext(object input)
		{
			IServiceCollection serviceCollection = new ServiceCollection();
			var serviceProvider = serviceCollection.BuildServiceProvider();
			var context = new ValidationContext(input, serviceProvider, null);
			return context;
		}
	}
}
