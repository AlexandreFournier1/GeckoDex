<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Documentation GeckoDex</title>
				<style>
					body { font-family: Arial, sans-serif; margin: 2em; }
					h1 { color: #2c3e50; }
					.member { margin-bottom: 1.5em; }
					.name { font-weight: bold; color: #2980b9; }
					.summary { margin-top: 0.5em; }
				</style>
			</head>
			<body>
				<h1>Documentation générée depuis XML</h1>
				<xsl:for-each select="doc/members/member">
					<div class="member">
						<div class="name">
							<xsl:value-of select="@name"/>
						</div>
						<div class="summary">
							<xsl:apply-templates select="summary"/>
						</div>
					</div>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="summary">
		<xsl:value-of select="."/>
	</xsl:template>

</xsl:stylesheet>