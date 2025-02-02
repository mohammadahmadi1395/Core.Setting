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

namespace Gostar.Common.Validation.Validators {
	using Resources;

	public class NotNullValidator : PropertyValidator, INotNullValidator {
		public NotNullValidator() : base(new LanguageStringSource(nameof(NotNullValidator))) {
			
		}
		protected override bool IsValid(PropertyValidatorContext context) {
			if (context.PropertyValue == null) {
				return false;
			}
			return true;
		}
	}

	public interface INotNullValidator : IPropertyValidator {
	}
}