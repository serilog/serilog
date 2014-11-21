﻿// Copyright 2014 Serilog Contributors
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
using System.Linq;
using System.Web;
using Serilog.Events;

namespace Serilog.Extras.Web
{
    /// <summary>
    /// HTTP module that logs application request and error events.
    /// </summary>
    public class ApplicationLifecycleModule : IHttpModule
    {
        static volatile bool _logPostedFormData;
        static volatile bool _isEnabled = true;
        public static EventHandler OnBeginRequest { get; set; }
        public static EventHandler OnEndRequest { get; set; }
        public static EventHandler OnError { get; set; }

        /// <summary>
        /// Register the module with the application (called automatically;
        /// do not call this explicitly from your code).
        /// </summary>
        public static void Register()
        {
            HttpApplication.RegisterModule(typeof(ApplicationLifecycleModule));
        }

        /// <summary>
        /// When set to true, form data will be written via a debug-level event.
        /// The default is false. Requires that <see cref="IsEnabled"/> is also
        /// true (which it is, by default).
        /// </summary>
        public static bool DebugLogPostedFormData
        {
            get { return _logPostedFormData; }
            set { _logPostedFormData = value; }
        }

        /// <summary>
        /// When set to true, request details and errors will be logged. The default
        /// is true.
        /// </summary>
        public static bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.Error += Error;
            context.EndRequest += EndRequest;
        }

        static void EndRequest(object sender, EventArgs e)
        {
            if (OnEndRequest != null)
                OnEndRequest(sender, e);
        }

        static void Error(object sender, EventArgs e)
        {
            if (!_isEnabled) return;
            if (OnError != null)
                OnError(sender, e);
            else
            {
                var ex = ((HttpApplication)sender).Server.GetLastError();
                Log.Error(ex, "Error caught in global handler");
            }
        }

        static void BeginRequest(object sender, EventArgs e)
        {
            if (!_isEnabled) return;

            if (OnBeginRequest != null)
                OnBeginRequest(sender, e);
            else
            {
                var request = HttpContext.Current.Request;
                Log.Information("Beginning HTTP {Method} for {RawUrl}", request.HttpMethod, request.RawUrl);
                if (_logPostedFormData && Log.IsEnabled(LogEventLevel.Debug))
                {
                    var form = request.Form;
                    if (form.HasKeys())
                    {
                        var formData = form.AllKeys.SelectMany(k => (form.GetValues(k) ?? new string[0]).Select(v => new { Name = k, Value = v }));
                        Log.Debug("Client provided {@FormData}", formData);
                    }
                }
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
