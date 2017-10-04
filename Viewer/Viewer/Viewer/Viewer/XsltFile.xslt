<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:c="http://s.opencalais.com/1/pred/">
  <xsl:output method="html" indent="yes" />
  <xsl:key name="desc" match="rdf:Description/rdf:type" use="@rdf:resource"/>
  <xsl:template match="/">
    <table>
      <tr>
        <td id="termsContainer">
          <div>
            <ul id="termsBrowser" class="TermsSidebar">
              <xsl:apply-templates select="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/e/')]"/>
            </ul>
          </div>
          <div>
            <ul id="eventsAndFactsBrowser" class="TermsSidebar">
              <xsl:apply-templates select="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/r/')]"/>
            </ul>
          </div>
        </td>
        <td id="markupTextContainer">
            <xsl:apply-templates select="rdf:RDF/rdf:Description/c:document"/>
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="rdf:RDF/rdf:Description/c:document">
    <xsl:value-of select="."/>
  </xsl:template>
  
  <xsl:template match="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/e/')]">
    <xsl:if test="not(@rdf:resource=preceding-sibling::node()/rdf:type/@rdf:resource)">
        <li><span><xsl:value-of select="substring-after(@rdf:resource, 'type/em/e/')"/></span>
        <ul>
          <xsl:apply-templates mode="second" select="key('desc',@rdf:resource)" />
        </ul>
        </li>
      </xsl:if>
  </xsl:template>

  <xsl:template match="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/r/')]">
    <xsl:if test="rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/r/')]">
      <xsl:if test="not(rdf:type/@rdf:resource=preceding-sibling::node()/rdf:type/@rdf:resource)">
        <li>
          <span>
            <xsl:value-of select="substring-after(rdf:type/@rdf:resource, 'type/em/r/')"/>
          </span>
          <ul>
            <xsl:apply-templates mode="second" select="key('desc',rdf:type/@rdf:resource)" />
          </ul>
        </li>
      </xsl:if>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="rdf:Description/rdf:type" mode="second">
    <li>
      <input type="checkbox" />
      <span><xsl:value-of select="." /></span>
      ололо
    </li>
  </xsl:template>
</xsl:stylesheet>
  
 
  
     
  
  
