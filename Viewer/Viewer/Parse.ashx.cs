using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.Configuration;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.Resources;
using Viewer.Properties;

namespace Viewer
{
    /// <summary>
    /// Summary description for Bridge
    /// </summary>
    public class Parse : IHttpHandler
    {
        string _uri = "http://api.opencalais.com/enlighten/rest/";
        string _options = @"<c:params xmlns:c='http://s.opencalais.com/1/pred/' xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'> <c:processingDirectives c:contentType='text/txt' c:enableMetadataType='GenericRelations, SocialTags' c:outputFormat='xml/rdf' c:docRDFaccesible='true' > </c:processingDirectives> <c:userDirectives c:allowDistribution='true' c:allowSearch='true' c:externalID='17cabs901' c:submitter='ABC'></c:userDirectives><c:externalMetadata></c:externalMetadata></c:params>";
  
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["content"] != String.Empty)
            {
                var sendParams = string.Format("licenseID={0}&content={1}&paramsXML={2}", ConfigurationManager.AppSettings["keyApi"], HttpUtility.UrlEncode(context.Request["content"]), HttpUtility.HtmlDecode(_options));
                try
                {
                    var client = new WebClient();
                    var transform = new XslCompiledTransform();
                    transform.Load(XmlReader.Create(new StringReader(Resources.XsltFile)));
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var response = client.UploadString(_uri, "POST", sendParams);
                    var xmlReader = XmlReader.Create(new StringReader(response));
                    context.Response.ContentType = "text/xml";
                    transform.Transform(xmlReader, XmlWriter.Create(context.Response.OutputStream));
                }
                catch (Exception ex)
                {
                    context.Response.Output.Write("---> Error : " + ex.ToString());
                }
            }
            else 
            {
                context.Response.Output.Write(String.Empty);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}