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

namespace Gostar.Common.Validation.Resources {
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using Internal;
	using Validators;

	/// <summary>
	/// Represents a localized string.
	/// </summary>
	public class LocalizedStringSource : IStringSource {
		readonly Func<string> _accessor;

		/// <summary>
		/// Creates a new instance of the LocalizedErrorMessageSource class using the specified resource name and resource type.
		/// </summary>
		/// <param name="resourceType">The resource type</param>
		/// <param name="resourceName">The resource name</param>
		public LocalizedStringSource(Type resourceType, string resourceName) {
			var resourceAccessor = BuildResourceAccessor(resourceType, resourceName);

			ResourceType = resourceAccessor.ResourceType;
			ResourceName = resourceAccessor.ResourceName;
			_accessor = resourceAccessor.Accessor;
		}

		/// <summary>
		/// Construct the error message template
		/// </summary>
		/// <returns>Error message template</returns>
		public string GetString(IValidationContext context) {
			return _accessor();
		}

		/// <summary>
		/// The name of the resource if localized.
		/// </summary>
		public string ResourceName { get; }

		/// <summary>
		/// The type of the resource provider if localized.
		/// </summary>
		public Type ResourceType { get; }

		protected virtual ResourceAccessor BuildResourceAccessor(Type resourceType, string resourceName) {
			var property = GetResourceProperty(ref resourceType, ref resourceName);

			if (property == null) {
				throw new InvalidOperationException($"Could not find a property named '{resourceName}' on type '{resourceType}'.");
			}

			if (property.PropertyType != typeof(string)) {
				throw new InvalidOperationException($"Property '{resourceName}' on type '{resourceType}' does not return a string");
			}

			var accessor = (Func<string>)property.GetMethod.CreateDelegate(typeof(Func<string>));

			return new ResourceAccessor {
				Accessor = accessor,
				ResourceName = resourceName,
				ResourceType = resourceType
			};
		}

		/// <summary>
		/// Gets the PropertyInfo for a resource.
		/// ResourceType and ResourceName are ref parameters to allow derived types
		/// to replace the type/name of the resource before the delegate is constructed.
		/// </summary>
		protected virtual PropertyInfo GetResourceProperty(ref Type resourceType, ref string resourceName) {
			return resourceType.GetRuntimeProperty(resourceName);
		}
	}
}