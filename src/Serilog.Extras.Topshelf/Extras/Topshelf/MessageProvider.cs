// Copyright 2013 Serilog Contributors
// Based on Topshelf.Log4Net, copyright 2007-2012 Chris Patterson,
// Dru Sellers, Travis Smith, et. al.
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

using Topshelf.Logging;

namespace Serilog.Extras.Topshelf
{
    class MessageProvider
    {
        readonly LogWriterOutputProvider _messageProvider;
        object _message;

        public MessageProvider(LogWriterOutputProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public override string ToString()
        {
            return _messageProvider != null ?
                (_message = _message ?? _messageProvider()).ToString() :
                "";
        }
    }
}