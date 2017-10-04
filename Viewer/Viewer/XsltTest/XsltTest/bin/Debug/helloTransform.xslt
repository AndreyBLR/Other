<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:c="http://s.opencalais.com/1/pred/">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">
    <xsl:for-each select="node()/*">
    <xsl:call-template name="type-is-exist">
      <xsl:with-param name="node" select="."/>
    </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="type-is-exist">
    <xsl:param name="node"/>
    <xsl:if test="$node/type != ''">
      ололо
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>