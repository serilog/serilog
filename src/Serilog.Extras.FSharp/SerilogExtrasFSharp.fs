// Copyright 2015 Serilog Contributors
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

namespace Serilog.Extras.FSharp

open Microsoft.FSharp.Reflection
open Serilog.Core
open Serilog.Events

// Based on the sample from @vlaci in https://github.com/serilog/serilog/issues/352

type public UnionDestructuringPolicy() =
    interface Serilog.Core.IDestructuringPolicy with
        member this.TryDestructure(value,
                                   propertyValueFactory : ILogEventPropertyValueFactory,
                                   result: byref<LogEventPropertyValue>) =
            if FSharpType.IsUnion(value.GetType()) then
                let case, fields = FSharpValue.GetUnionFields(value, value.GetType())

                let properties = Seq.zip (case.GetFields()) fields |>
                                 Seq.map(fun (n, v) -> LogEventProperty(
                                                           n.Name,
                                                           propertyValueFactory.CreatePropertyValue(v, true)))

                result <- StructureValue(properties, case.Name)
                true
            else
                false

namespace Serilog

open Serilog.Configuration
open Serilog.Extras.FSharp

[<AutoOpen>]
module public LoggerDestructuringConfigurationExtensions =
    type public LoggerDestructuringConfiguration with
        member public this.FSharpTypes() =
            this.With<UnionDestructuringPolicy>()

