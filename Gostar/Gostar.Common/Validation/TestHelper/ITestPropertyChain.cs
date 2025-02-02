#region License

// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
//  
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
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
// The latest version of this file can be found at http://github.com/JeremySkinner/FluentValidation

#endregion

#pragma warning disable 1591
namespace Gostar.Common.Validation.TestHelper {
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Results;

	public interface ITestPropertyChain<TValue> {
		ITestPropertyChain<TValue1> Property<TValue1>(Expression<Func<TValue, TValue1>> memberAccessor);
		IEnumerable<ValidationFailure> ShouldHaveValidationError();
		void ShouldNotHaveValidationError();
	}
}