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


// Copyright (c) 2014 Logentries
   
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
   
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
   
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Serilog.Sinks.Logentries
{
    class LeClient
    {
        // Logentries API server address. 
        protected const String LeApiUrl = "api.logentries.com";

        // Port number for token logging on Logentries API server. 
        protected const int LeApiTokenPort = 10000;

        // Port number for TLS encrypted token logging on Logentries API server 
        protected const int LeApiTokenTlsPort = 20000;

        // Port number for HTTP PUT logging on Logentries API server. 
        protected const int LeApiHttpPort = 80;

        // Port number for SSL HTTP PUT logging on Logentries API server. 
        protected const int LeApiHttpsPort = 443;

        // Logentries API server certificate. 
        protected static readonly X509Certificate2 LeApiServerCertificate =
            new X509Certificate2(Encoding.UTF8.GetBytes(
                @"-----BEGIN CERTIFICATE-----
MIIFSjCCBDKgAwIBAgIDCQpNMA0GCSqGSIb3DQEBBQUAMGExCzAJBgNVBAYTAlVT
MRYwFAYDVQQKEw1HZW9UcnVzdCBJbmMuMR0wGwYDVQQLExREb21haW4gVmFsaWRh
dGVkIFNTTDEbMBkGA1UEAxMSR2VvVHJ1c3QgRFYgU1NMIENBMB4XDTE0MDQxNTEz
NTcxNVoXDTE2MDkxMzA0MTMzMFowgcExKTAnBgNVBAUTIEhpL1RHbXlmUEpJYTFy
b0NQdlJ1U1NNRVdLOFp0NUtmMRMwEQYDVQQLEwpHVDAzOTM4NjcwMTEwLwYDVQQL
EyhTZWUgd3d3Lmdlb3RydXN0LmNvbS9yZXNvdXJjZXMvY3BzIChjKTEyMS8wLQYD
VQQLEyZEb21haW4gQ29udHJvbCBWYWxpZGF0ZWQgLSBRdWlja1NTTChSKTEbMBkG
A1UEAxMSYXBpLmxvZ2VudHJpZXMuY29tMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A
MIIBCgKCAQEAwGsgjVb/pn7Go1jqNQVFsN+VEMRFpu7bJ5i+Lv/gY9zXBDGULr3d
j9/hB/pa49nLUpy9GsaFru2AjNoveoVoe5ng2QhZRlUn77hxkoZsaiD+rrH/D/Yp
LP3b/pNQg+nNTC81uwbhlxjIoeMSaPGjr1SFjZ1StCprZKFRu3IV+2/wZ+STUz/L
aA3r6J86DRptasbzYMkDyWlUzN3nhYUcPUNrd4jSk+soSDEuDpHMahgRdQBo6Dht
EKCSY+vB5ZIgEydI7mra8ygRjXotvc0zeb8Jvo8ZhyLDwvxjgo9F6Li3h/tfAjRR
4ngV7yg9o8MgXN852GMHpUxzqhygLeyqSQIDAQABo4IBqDCCAaQwHwYDVR0jBBgw
FoAUjPTZkwpHvACgSs5LdW6gtrCyfvwwDgYDVR0PAQH/BAQDAgWgMB0GA1UdJQQW
MBQGCCsGAQUFBwMBBggrBgEFBQcDAjAdBgNVHREEFjAUghJhcGkubG9nZW50cmll
cy5jb20wQQYDVR0fBDowODA2oDSgMoYwaHR0cDovL2d0c3NsZHYtY3JsLmdlb3Ry
dXN0LmNvbS9jcmxzL2d0c3NsZHYuY3JsMB0GA1UdDgQWBBRowYR/aaGeiRRQxbaV
1PI8hS4m9jAMBgNVHRMBAf8EAjAAMHUGCCsGAQUFBwEBBGkwZzAsBggrBgEFBQcw
AYYgaHR0cDovL2d0c3NsZHYtb2NzcC5nZW90cnVzdC5jb20wNwYIKwYBBQUHMAKG
K2h0dHA6Ly9ndHNzbGR2LWFpYS5nZW90cnVzdC5jb20vZ3Rzc2xkdi5jcnQwTAYD
VR0gBEUwQzBBBgpghkgBhvhFAQc2MDMwMQYIKwYBBQUHAgEWJWh0dHA6Ly93d3cu
Z2VvdHJ1c3QuY29tL3Jlc291cmNlcy9jcHMwDQYJKoZIhvcNAQEFBQADggEBAAzx
g9JKztRmpItki8XQoGHEbopDIDMmn4Q7s9k7L9nT5gn5XCXdIHnsSe8+/2N7tW4E
iHEEWC5G6Q16FdXBwKjW2LrBKaP7FCRcqXJSI+cfiuk0uywkGBTXpqBVClQRzypd
9vZONyFFlLGUwUC1DFVxe7T77Dv+pOPuJ7qSfcVUnVtzpLMMWJsDG6NHpy0JhsS9
wVYQgpYWRRZ7bJyfRCJxzIdYF3qy/P9NWyZSlDUuv11s1GSFO2pNd34p59GacVAL
BJE6y5eOPTSbtkmBW/ukaVYdI5NLXNer3IaK3fetV3LvYGOaX8hR45FI1pvyKYvf
S5ol3bQmY1mv78XKkOk=
-----END CERTIFICATE-----"));

        public LeClient(bool useHttpPut, bool useSsl)
        {
            m_UseSsl = useSsl;
            if (!m_UseSsl)
                m_TcpPort = useHttpPut ? LeApiHttpPort : LeApiTokenPort;
            else
                m_TcpPort = useHttpPut ? LeApiHttpsPort : LeApiTokenTlsPort;
        }

        private bool m_UseSsl = false;
        private int m_TcpPort;
        private TcpClient m_Client = null;
        private Stream m_Stream = null;
        private SslStream m_SslStream = null;

        private Stream ActiveStream
        {
            get
            {
                return m_UseSsl ? m_SslStream : m_Stream;
            }
        }

        public void Connect()
        {
            m_Client = new TcpClient(LeApiUrl, m_TcpPort);
            m_Client.NoDelay = true;

            m_Stream = m_Client.GetStream();

            if (m_UseSsl)
            {
                m_SslStream = new SslStream(m_Stream, false, (sender, cert, chain, errors) => cert.GetCertHashString() == LeApiServerCertificate.GetCertHashString());
                m_SslStream.AuthenticateAsClient(LeApiUrl);
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            ActiveStream.Write(buffer, offset, count);
        }

        public void Flush()
        {
            ActiveStream.Flush();
        }

        public void Close()
        {
            if (m_Client != null)
            {
                try
                {
                    m_Client.Close();
                }
                catch
                {
                }
            }
        }
    }
}