<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:c="http://s.opencalais.com/1/pred/">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">
    <xsl:text>[</xsl:text>
    <xsl:apply-templates select="//rdf:RDF" />
    <xsl:text>] </xsl:text>
  </xsl:template>
  <xsl:template match="rdf:Description/rdf:type">
    <xsl:if test="contains(rdf:type/@rdf:resource, 'type/em/e/')" >
      <xsl:text>{</xsl:text>
      <xsl:value-of select="substring-after(rdf:type/@rdf:resource, 'type/em/e/')"/>
      <xsl:text>: </xsl:text>
      <xsl:value-of select="c:name"/>
      <xsl:text>},</xsl:text>
    </xsl:if>
    <xsl:if test="contains(rdf:type/@rdf:resource, 'type/em/r/')" >
      <xsl:text>{</xsl:text>
      <xsl:value-of select="substring-after(rdf:type/@rdf:resource, 'type/em/r/')"/>
      <xsl:text>: </xsl:text>
      <xsl:value-of select="c:name"/>
      <xsl:text>},</xsl:text>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
