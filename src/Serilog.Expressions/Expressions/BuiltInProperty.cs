// Copyright © Serilog Contributors
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

namespace Serilog.Expressions;

// See https://github.com/serilog/serilog-formatting-compact#reified-properties
static class BuiltInProperty
{
    public const string Exception = "x";
    public const string Level = "l";
    public const string Timestamp = "t";
    public const string Message = "m";
    public const string MessageTemplate = "mt";
    public const string Properties = "p";
    public const string Renderings = "r";
    public const string EventId = "i";
    public const string TraceId = "tr";
    public const string SpanId = "sp";
}