<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/BeltLevel">
<html>
	<body>
		<table border="1">
			<tr>
				<td colspan="2">
					<table border="1">
						<tr><td colspan="4">Fight Club Grading Sheet - INSERT NAME HERE</td></tr>
						<tr>
							<td>Name:...........</td><td>Age:.......</td><td>Date:................</td><td>Instructor:..........</td>
						</tr>
						<tr><td colspan="2">Current Rank:.............</td><td colspan="2">Belt Attempting:..............</td></tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table border="1">
						<xsl:for-each select="categories/TechniqueCategory">
							<tr>
								<td colspan="3"><b><xsl:value-of select="@name" /></b></td>
							</tr>
							<xsl:for-each select="techniques/Technique">
								<tr>
									<td><xsl:value-of select="@name" /></td><td>Pass</td><td>Fail</td>
								</tr>
							</xsl:for-each>					
						</xsl:for-each>
					</table>
				</td>
				<td>
					<table border="1">
						<tr>
							<td>Comments.</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
						<tr>
							<td>............</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<table border="1">
						<tr>
							<td>Graded By:</td><td>........</td><td>Date:</td><td>............</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</html>
</xsl:template>	
</xsl:stylesheet>


