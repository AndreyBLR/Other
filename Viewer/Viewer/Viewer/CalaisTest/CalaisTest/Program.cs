using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using System.Threading;

namespace ConsoleApplicationCalaisCodeSapmle
{
   
    class CalaisManualPostExample
    {
        
        static string m_Licence = "g48ngkd58tv33aj26qbvrhgq";

        static string m_CParams = null;
        static string m_ParamsRDF =
@"&lt;c:params xmlns:c=""http://s.opencalais.com/1/pred/"" xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""&gt;&lt;c:processingDirectives c:allowDistribution=""true"" c:allowSearch=""true"" c:contentType=""text/txt"" c:outputFormat=""xml/rdf""&gt;&lt;/c:processingDirectives&gt;&lt;c:externalMetadata&gt;&lt;/c:externalMetadata&gt;&lt;/c:params&gt;";

        static string m_ParamsMicro =
@"&lt;c:params xmlns:c=""http://s.opencalais.com/1/pred/"" xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""&gt;&lt;c:processingDirectives c:allowDistribution=""true"" c:allowSearch=""true"" c:contentType=""text/txt"" c:outputFormat=""Text/Microformats""&gt;&lt;/c:processingDirectives&gt;&lt;c:externalMetadata&gt;&lt;/c:externalMetadata&gt;&lt;/c:params&gt;";

        static string m_ParamsSimple =
@"&lt;c:params xmlns:c=""http://s.opencalais.com/1/pred/"" xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""&gt;&lt;c:processingDirectives c:allowDistribution=""true"" c:allowSearch=""true"" c:contentType=""text/txt"" c:outputFormat=""Text/Simple""&gt;&lt;/c:processingDirectives&gt;&lt;c:externalMetadata&gt;&lt;/c:externalMetadata&gt;&lt;/c:params&gt;";

        static string m_ParamsEmpty = @"<c:params xmlns:c=""http://s.opencalais.com/1/pred/"" xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""  > </c:params>";

        static string m_ParamsNoContentNoOutputType = @"&lt;c:params xmlns:c=""http://s.opencalais.com/1/pred/"" xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""&gt;&lt;c:processingDirectives c:allowDistribution=""true"" c:contentType=""text/txt"" c:allowSearch=""true"" &gt;&lt;/c:processingDirectives&gt;&lt;c:externalMetadata&gt;&lt;/c:externalMetadata&gt;&lt;/c:params&gt;";

        static string m_Content = @"Thomas M. Laughlin to Join Zila Inc. as Chief Operating Officer
PHOENIX, March 28 /PRNewswire/ -- Zila, Inc. (Nasdaq: ZILA) Chairman and President Joseph Hines announced that Thomas M. Laughlin will join the Company as Chief Operating Officer on April 11, 2000. 
Hines said, 'Continuing internal growth and an aggressive acquisition program have created a clear need for additional management breadth. Tom Laughlin will bring to the Company a wealth of knowledge, resulting from his extensive 
experience in several of the world's largest and most successful healthcare products companies. We believe Zila's corporate management team and all of our product lines will benefit substantially from Tom's keen business instincts and 
growth-oriented leadership talents.'";


        /// <summary>
        /// Build XML for SOAP1.1
        /// </summary>
        /// <returns>xml as string</returns>
        static private string BuildPostXmlSoap11()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xml.Append(@"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">");
            xml.Append(@"<soap:Body>");
            xml.Append(@"<Enlighten xmlns=""http://clearforest.com/"">");
            xml.Append(@"<licenseID>");
            xml.Append(m_Licence);
            xml.Append(@"</licenseID>");
            xml.Append(@"<content>");
            xml.Append(m_Content);
            xml.Append(@"</content>");
            xml.Append(@"<paramsXML>");
            //the encode decode issue
            xml.Append(HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(m_CParams)));
            xml.Append(@"</paramsXML>");
            xml.Append(@"</Enlighten>");
            xml.Append(@"</soap:Body>");
            xml.Append(@"</soap:Envelope>");



            return xml.ToString();
        }
        /// <summary>
        /// Build XML for SOAP1.1
        /// </summary>
        /// <returns>xml as string</returns>
        static private string BuildPostXmlSoap12()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xml.Append(@"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">");
            xml.Append(@"<soap12:Body>");
            xml.Append(@"<Enlighten xmlns=""http://clearforest.com/"">");
            xml.Append(@"<licenseID>");
            xml.Append(m_Licence);
            xml.Append(@"</licenseID>");
            xml.Append(@"<content>");
            xml.Append(m_Content);
            xml.Append(@"</content>");
            xml.Append(@"<paramsXML>");
            //the encode decode issue
            xml.Append(HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(m_CParams)));
            xml.Append(@"</paramsXML>");
            xml.Append(@"</Enlighten>");
            xml.Append(@"</soap12:Body>");
            xml.Append(@"</soap12:Envelope>");

            return xml.ToString();
        }

        /// <summary>
        /// Create POST params
        /// </summary>
        /// <returns>Post params as string</returns>
        private static string CreatePostParams()
        {
            string sendParams = null;
            //see the url and html endcoding...
            sendParams = string.Format("licenseID={0}&content={1}&paramsXML={2}", m_Licence, HttpUtility.UrlEncode(m_Content), HttpUtility.HtmlDecode(m_CParams));

            return sendParams;
        }


        static void Main(string[] args)
        {
            string response = null;

            m_CParams = m_ParamsRDF;//or your custom params
            m_Content = m_Content;//or  your custom content
            m_Licence = m_Licence;// your key
            WebProxy proxy = null;//add proxy if needed
            if (proxy != null)
            {
                proxy.Credentials = new NetworkCredential("userName", "password", "domain");
            }

            //POST
            response = ProccessText("http://api.opencalais.com/enlighten/calais.asmx/Enlighten", "application/x-www-form-urlencoded", null, CreatePostParams(), proxy);

            //SOAP POST
            ///response= ProccessText("http://api.opencalais.com/enlighten/calais.asmx", "text/xml; charset=utf-8", "http://clearforest.com/Enlighten", BuildPostXmlSoap11()); 
            Console.WriteLine(response);
            Console.Read();

        }


        /// <summary>
        /// This function send an manual request to Calais API
        /// </summary>
        /// <param name="i_Uri">API location</param>
        /// <param name="i_ContentType">content type (see at http://www.opencalais.com/APIcalls) </param>
        /// <param name="i_SOAPAction">if SOAP is used then required otherwise, null</param>
        /// <param name="i_SendParams">the data to be send</param>
        /// <param name="i_Proxy">if required, otherwise null</param>
        /// <returns>response or error</returns>
        private static string ProccessText(string i_Uri, string i_ContentType, string i_SOAPAction, string i_SendParams, WebProxy i_Proxy)
        {
            string response = null;

            try
            {

                WebClient client = new WebClient();

                client.Headers.Add("Content-Type", i_ContentType);

                string sendParams = i_SendParams;

                if (i_SOAPAction != null)
                {
                    client.Headers.Add("SOAPAction", i_SOAPAction);
                }

                if (i_Proxy != null)
                {
                    client.Proxy = i_Proxy;//some might need it ...
                }

                byte[] byteArray = PostToByteArray(sendParams);
                //send the data to uri using post methode
                byte[] responseArray = client.UploadData(i_Uri, "POST", byteArray);
                var str = client.ResponseHeaders;
                response = ByteArrayToString(responseArray);
            }
            catch (Exception ex)
            {

                response = "---> Error : " + ex.ToString();
            }

            return response;

        }


        private static string ByteArrayToString(byte[] responseArray)
        {
            string response = Encoding.ASCII.GetString(responseArray);
            return response;
        }
        /// <summary>
        /// convert string to byte array
        /// </summary>
        /// <param name="sendParams">string to be converted</param>
        /// <returns>byte array</returns>
        private static byte[] PostToByteArray(string sendParams)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendParams);
            return byteArray;
        }


    }
}
