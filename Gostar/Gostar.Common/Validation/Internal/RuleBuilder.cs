#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/jeremyskinner/FluentValidation
#endregion

namespace Gostar.Common.Validation.Internal {
	using System;
	using System.Linq.Expressions;
	using Validators;

	/// <summary>
	/// Builds a validation rule and constructs a validator.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	public class RuleBuilder<T, TProperty> : IRuleBuilderOptions<T, TProperty>, IRuleBuilderInitial<T, TProperty>, IRuleBuilderInitialCollection<T,TProperty>, IExposesParentValidator<T> {
		/// <summary>
		/// The rule being created by this RuleBuilder.
		/// </summary>
		public PropertyRule Rule { get; }

		/// <summary>
		/// Parent validator
		/// </summary>
		public IValidator<T> ParentValidator { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="RuleBuilder{T,TProperty}">RuleBuilder</see> class.
		/// </summary>
		public RuleBuilder(PropertyRule rule, IValidator<T> parent) {
			Rule = rule;
			ParentValidator = parent;
		}

		/// <summary>
		/// Sets the validator associated with the rule.
		/// </summary>
		/// <param name="validator">The validator to set</param>
		/// <returns></returns>
		public IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator) {
			validator.Guard("Cannot pass a null validator to SetValidator.", nameof(validator));
			Rule.AddValidator(validator);
			return this;
		}

		/// <summary>
		/// Sets the validator associated with the rule. Use with complex properties where an IValidator instance is already declared for the property type.
		/// </summary>
		/// <param name="validator">The validator to set</param>
		/// <param name="ruleSets"></param>
		public IRuleBuilderOptions<T, TProperty> SetValidator(IValidator<TProperty> validator, params string[] ruleSets) {
			validator.Guard("Cannot pass a null validator to SetValidator", nameof(validator));
			var adaptor = new ChildValidatorAdaptor(validator, validator.GetType()) {
				RuleSets = ruleSets
			};
			SetValidator(adaptor);
			return this;
		}

		/// <summary>
		/// Sets the validator associated with the rule. Use with complex properties where an IValidator instance is already declared for the property type.
		/// </summary>
		/// <param name="validatorProvider">The validator provider to set</param>
		/// <param name="ruleSet"></param>
		public IRuleBuilderOptions<T, TProperty> SetValidator<TValidator>(Func<T, TValidator> validatorProvider, params string[] ruleSets)
			where TValidator : IValidator<TProperty> {
			validatorProvider.Guard("Cannot pass a null validatorProvider to SetValidator", nameof(validatorProvider));
			SetValidator(new ChildValidatorAdaptor(context => validatorProvider((T) context.InstanceToValidate), typeof (TValidator)) {
				RuleSets = ruleSets
			});
			return this;
		}

		/// <summary>
		/// Sets the validator associated with the rule. Use with complex properties where an IValidator instance is already declared for the property type.
		/// </summary>
		/// <param name="validatorProvider">The validator provider to set</param>
		public IRuleBuilderOptions<T,TProperty> SetValidator<TValidator>(Func<IValidationContext, TValidator> validatorProvider) where TValidator : IValidator<TProperty> {
			validatorProvider.Guard("Cannot pass a null validatorProvider to SetValidator", nameof(validatorProvider));
			SetValidator(new ChildValidatorAdaptor(context => validatorProvider(context), typeof (TValidator)));
			return this;
		}

		IRuleBuilderOptions<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderOptions<T, TProperty>>.Configure(Action<PropertyRule> configurator) {
			configurator(Rule);
			return this;
		}

		IRuleBuilderInitial<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderInitial<T, TProperty>>.Configure(Action<PropertyRule> configurator) {
			configurator(Rule);
			return this;
		}

		IRuleBuilderInitialCollection<T, TProperty> IConfigurable<CollectionPropertyRule<TProperty>, IRuleBuilderInitialCollection<T, TProperty>>.Configure(Action<CollectionPropertyRule<TProperty>> configurator) {
			configurator((CollectionPropertyRule<TProperty>) Rule);
			return this;
		}
	}

	internal interface IExposesParentValidator<T> {
		IValidator<T> ParentValidator { get; }
	}
}