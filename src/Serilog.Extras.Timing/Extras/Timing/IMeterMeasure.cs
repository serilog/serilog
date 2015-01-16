// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Extras.Timing
{

	/// <summary>
	/// Measures a metered operation.
	/// </summary>
	public interface IMeterMeasure : IMeasure
	{
		/// <summary>
		/// Marks the occurrence of an operation.
		/// </summary>
		void Mark(long n =1);

		/// <summary>
		///  Returns the total number of events which have been marked.
		/// </summary>
		/// <value>The total number.</value>
		long Count{ get; }
	}
}