<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="html"></xsl:output>

	<xsl:template match="ArrayOfStudent">
		<html>
			<body>
				<table border ="1">
					<TR>
						<th>FullName</th>
						<th>Faculty</th>
						<th>Group</th>
						<th>Speciality</th>
						<th>Marks</th>
					</TR>
					
					<xsl:for-each select = "Student">
						<tr>
							<td>
								<xsl:value-of select ="FullName"/>
							</td><td>
								<xsl:value-of select ="Faculty"/>
							</td><td>
								<xsl:value-of select ="Group"/>
							</td><td>
								<xsl:value-of select ="Speciality"/>
							</td><td>
								<xsl:value-of select ="Marks"/>
							</td>
						</tr>
					</xsl:for-each>
						
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
