<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <style>
          body {
              font-family: Arial, sans-serif;
              background-size: cover;
              height: 100vh;
          }
          h1 {
              font-family: Tahoma, Arial, sans-serif;
              margin: 80px 0;
          }
          .box {
              margin: 0 auto;
              background-clip: padding-box;
              text-align: center;
          }
          .button {
              font-size: 1em;
              cursor: pointer;
          }
          .overlay {
              position: fixed;
              top: 0;
              bottom: 0;
              left: 0;
              right: 0;
              background: rgba(0, 0, 0, 0.7);
              transition: opacity 500ms;
              visibility: hidden;
              opacity: 0;
          }
          .overlay:target {
              visibility: visible;
              opacity: 1;
          }
          .popup {
              margin: 70px auto;
              padding: 20px;
              background: #fff;
              border-radius: 5px;
              width: 30%;
              position: relative;
              transition: all 5s ease-in-out;
          }
          .popup h2 {
              margin-top: 0;
              color: #333;
              font-family: Tahoma, Arial, sans-serif;
          }
          .popup .close {
              position: absolute;
              top: 20px;
              right: 30px;
              transition: all 200ms;
              font-size: 30px;
              font-weight: bold;
              text-decoration: none;
              color: #333;
          }
          .popup .close:hover {
              color: #06D85F;
          }
          .popup .content {
              max-height: 30%;
              overflow: auto;
              color : black;
          }
          @media screen and (max-width: 700px){
            .box{
              width: 70%;
            }
            .popup{
              width: 70%;
            }
          }

          .table_dark {
              font-family: "Lucida Sans Unicode", "Lucida Grande", Sans-Serif;
              font-size: 14px;
              border: 1px;
              text-align: left;
              border-collapse: collapse;
              background: #252F48;
              margin: 10px;
              width: 100%; 
          }
          .table_dark th {
              color: #EDB749;
              border-bottom: 1px solid #37B5A5;
              padding: 12px 17px;
          }
          .table_dark td {
              color: #CAD4D6;
              border-bottom: 1px solid #37B5A5;
              border-right:1px solid #37B5A5;
              padding: 7px 17px;
          }
          .table_dark tr:last-child td {
              border-bottom: none;
          }
          .table_dark td:last-child {
              border-right: none;
          }
          .table_dark tr:hover td {
              text-decoration: underline;
          }

          .close {
              position: absolute;
              right: 32px;
              top: 32px;
              width: 32px;
              height: 32px;
              opacity: 0.3;
          }
          .close:hover {
              opacity: 1;
          }
          .close:before, .close:after {
              position: absolute;
              left: 15px;
              content: ' ';
              height: 20px;
              width: 2px;
              background-color: #333;
          }
          .close:before {
              transform: rotate(45deg);
          }
          .close:after {
              transform: rotate(-45deg);
          }

          black  {
              color: #000000;
          }

        </style>
        <div>
          <h1>RESULTS</h1>
          <h3>
            Summary -> Test Case Count :
            <xsl:value-of select="test-run/@testcasecount"/>
            Passed : <xsl:value-of select="test-run/@passed"/>
            Failed : <xsl:value-of select="test-run/@failed"/>
            Skipped : <xsl:value-of select="test-run/@skipped"/>
          </h3>
        </div>
        <xsl:for-each select="test-run/case-folder">
          <h2>
            <xsl:value-of select="@name"/> Folder
          </h2>
          <table class="table_dark">
            <tr>
              <th>
                Name | Passed : <xsl:value-of select="@passes"/>
                Failed : <xsl:value-of select="@failures"/>
                Skipped : <xsl:value-of select="@skips"/>
              </th>
              <th>Result</th>
              <th>Time</th>
            </tr>
            <xsl:for-each select="test-case">
              <xsl:variable name="GeneratedId" select="position()"/>
              <tr>
                <td>
                  <xsl:value-of select="@name"/>
                </td>
                <xsl:variable name="TestResult" select="@result" />
                <xsl:choose>
                  <xsl:when test="$TestResult = 'FAILED'">
                    <td bgcolor="#FF0000">
                      <div class="box">
                        <a class="button">
                          <xsl:attribute name="href">
                            <xsl:value-of select="concat('#popup', $GeneratedId)"/>
                          </xsl:attribute>
                          <black> FAILED </black>
                        </a>
                      </div>
                      <div class="overlay">
                        <xsl:attribute name="id">
                          <xsl:value-of select="concat('popup', $GeneratedId)"/>
                        </xsl:attribute>
                        <div class="popup">
                          <h2>Test Failed!</h2>
                          <a class="close" href="#close"></a>
                          <div class="content">
                              <xsl:value-of select="output"/>
                          </div>
                        </div>
                      </div>
                    </td>
                  </xsl:when>
                  <xsl:when test="$TestResult = 'SKIPPED'">
                    <td bgcolor="#808080">
                      <black> SKIPPED </black>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td bgcolor="#9acd32">
                      <black> PASSED </black>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
                <td>
                  <xsl:value-of select="@time"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
<!--TO ENABLE CSS in Jenkins Go To : Manage Jenkins-> Manage Nodes-> Click settings(gear icon)-> click Script console on left and type in the following command:
      System.setProperty("hudson.model.DirectoryBrowserSupport.CSP", "default-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline';")
-->
