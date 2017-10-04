<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:c="http://s.opencalais.com/1/pred/">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="no" encoding="utf-8" />
  <xsl:key name="desc" match="rdf:Description/rdf:type" use="@rdf:resource"/>
  <xsl:key name="desc2" match="rdf:Description" use="@rdf:about"/>
  <xsl:template match="/">
    <data>
      <document>
        <xsl:value-of select="rdf:RDF/rdf:Description/c:document"/>
      </document>
      <metadata>
        <em-e>
          <xsl:apply-templates select="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/e/')]"/>
        </em-e>
        <em-r>
          <xsl:apply-templates select="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/r/')]"/>
        </em-r>
      </metadata>
    </data>
  </xsl:template>
  
  <xsl:template match="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/e/')]">
    <xsl:if test="not(@rdf:resource=parent::node()/preceding-sibling::node()/rdf:type/@rdf:resource)">
      <entity>
        <xsl:attribute name="type">
          <xsl:value-of select="substring-after(@rdf:resource, 'type/em/e/')"/>
        </xsl:attribute>
        <terms>
          <xsl:apply-templates mode="second" select="key('desc', @rdf:resource)" />
        </terms>
      </entity>
    </xsl:if>
  </xsl:template>

  <xsl:template match="rdf:RDF/rdf:Description/rdf:type[starts-with(@rdf:resource, 'http://s.opencalais.com/1/type/em/r/')]">
      <xsl:if test="not(@rdf:resource=parent::node()/preceding-sibling::node()/rdf:type/@rdf:resource)">
        <entity>
          <xsl:attribute name="type">
            <xsl:value-of select="substring-after(@rdf:resource, 'type/em/r/')"/>
          </xsl:attribute>
          <facts>
            <xsl:apply-templates mode="second" select="key('desc', @rdf:resource)" />
          </facts>
        </entity>
      </xsl:if>
  </xsl:template>
  
  <xsl:template match="rdf:Description/rdf:type" mode="second">
    
      <xsl:if test="contains(@rdf:resource, 'type/em/e/')">
        <term>
          <xsl:value-of select="parent::node()/c:name" />
        </term>
      </xsl:if>
      <xsl:if test="contains(@rdf:resource, 'type/em/r/')">
        <fact>
          <xsl:for-each select="parent::node()/*">
            <xsl:choose>
              <xsl:when test="not(position()=1) and @rdf:resource">
                <xsl:value-of select="key('desc2', @rdf:resource)/c:name" />
                <xsl:text> </xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="." />
                <xsl:text> </xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:for-each>
        </fact>
      </xsl:if>
  </xsl:template>
</xsl:stylesheet>
  
  
