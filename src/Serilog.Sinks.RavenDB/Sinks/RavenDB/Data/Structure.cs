// Copyright 2013 Serilog Contributors
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

using System;
using System.Collections.Generic;

namespace Serilog.Sinks.RavenDB.Data
{
    /// <summary>
    /// The representation of an object in RavenDB.
    /// </summary>
    public class Structure
    {
        readonly string _typeTag;
        readonly Dictionary<string, object> _properties;
        
        /// <summary>
        /// Create a <see cref="Structure"/>.
        /// </summary>
        /// <param name="typeTag">A notion of the structure's type.</param>
        /// <param name="properties">The properties of the object.</param>
        public Structure(string typeTag, Dictionary<string, object> properties)
        {
            if (typeTag == null) throw new ArgumentNullException("typeTag");
            if (properties == null) throw new ArgumentNullException("properties");
            _typeTag = typeTag;
            _properties = properties;
        }

        /// <summary>
        /// A notion of the structure's type.
        /// </summary>
        public string TypeTag
        {
            get { return _typeTag; }
        }

        /// <summary>
        /// The object's properties.
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
        }
    }
}