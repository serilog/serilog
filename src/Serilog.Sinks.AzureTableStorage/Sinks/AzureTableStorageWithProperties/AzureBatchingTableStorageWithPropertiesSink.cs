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

using Microsoft.WindowsAzure.Storage;
using Serilog.Core;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureTableStorage
{
	class AzureBatchingTableStorageWithPropertiesSink : PeriodicBatchingSink
	{

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        /// <param name="storageAccount">The Cloud Storage Account to use to insert the log entries to.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="batchSizeLimit"></param>
        /// <param name="period"></param>
        /// <param name="storageTableName">Table name that log entries will be written to. Note: Optional, setting this may impact performance</param>
		/// <param name="additionalRowKeyPostfix">Additional postfix string that will be appended to row keys</param>
		public AzureBatchingTableStorageWithPropertiesSink(CloudStorageAccount storageAccount, IFormatProvider formatProvider, int batchSizeLimit, TimeSpan period, string storageTableName = null, string additionalRowKeyPostfix = null)
            :base(batchSizeLimit, period)
        {
			throw new NotImplementedException("AzureBatchingTableStorageWithPropertiesSink constructor");
        }
	}
}
