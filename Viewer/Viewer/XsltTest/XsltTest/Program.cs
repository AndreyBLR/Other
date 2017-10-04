using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;

namespace XsltTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("XsltFile.xslt");

            // Execute the transform and output the results to a file.
            xslt.Transform("input.xml", "output.html");
        }
    }
}
