using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using VFKN.Parser;

namespace VFKN.Tests
{
    [TestClass]
    public class ReadingTests
    {
        [TestMethod]
        [DeploymentItem("Files\\SingleLineWithCrLf.txt")]
        public void EndLinesInText()
        {
            var scanner = Scanner.Create("Files\\SingleLineWithCrLf.txt");
            var tok = (Tokens)scanner.yylex();
            while (tok != Tokens.EOF)
            {
                if (tok == Tokens.STRING)
                {
                    Assert.AreEqual("\"ahoj,¤\r\n\"\"jak se mate?\"\"¤\r\nJa se mam¤\r\nDobre\"", scanner.yytext);
                    Assert.AreEqual("ahoj,\r\n\"jak se mate?\"\r\nJa se mam\r\nDobre", scanner.Text);
                }

                tok = (Tokens)scanner.yylex();
            }
        }

        [TestMethod]
        [DeploymentItem("Files\\SingleLineWithDiacritics.txt")]
        public void DiacriticsInText()
        {
            var scanner = Scanner.Create("Files\\SingleLineWithDiacritics.txt");
            var tok = (Tokens)scanner.yylex();
            while (tok != Tokens.EOF)
            {
                if (tok == Tokens.STRING)
                {
                    Assert.AreEqual("Příliš žluťoučký kůň úpěl ďábelské kódy.", scanner.Text);
                }

                tok = (Tokens)scanner.yylex();
            }
        }

        [TestMethod]
        [DeploymentItem("Files\\SingleLineWithDiacriticsWin1250.txt")]
        public void DiacriticsInText1250()
        {
            var scanner = Scanner.Create("Files\\SingleLineWithDiacriticsWin1250.txt");
            var tok = (Tokens)scanner.yylex();
            while (tok != Tokens.EOF)
            {
                if (tok == Tokens.STRING && scanner.Text[0] == 'P')
                {
                    Assert.AreEqual("Příliš žluťoučký kůň úpěl ďábelské kódy.", scanner.Text);
                }

                tok = (Tokens)scanner.yylex();
            }
        }

        [TestMethod]
        [DeploymentItem("Files\\HeaderLine.txt")]
        public void HeaderLine()
        {
            var scanner = Scanner.Create("Files\\HeaderLine.txt");
            var tok = (Tokens)scanner.yylex();
            var nextToken = Tokens.BLOCK;
            while (tok != Tokens.EOF)
            {
                Assert.AreEqual(nextToken, tok);
                switch (tok)
                {
                    case Tokens.BLOCK:
                        nextToken = Tokens.ENDCOL;
                        break;
                    case Tokens.ENDCOL:
                        nextToken = Tokens.COL_HEADER;
                        break;
                    case Tokens.COL_HEADER:
                        nextToken = Tokens.ENDCOL;
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
                tok = (Tokens)scanner.yylex();
            }
        }

        [TestMethod]
        [DeploymentItem("Files\\Export_vse.vfk")]
        public void CompleteFileScan()
        {
            var scanner = Scanner.Create("Files\\Export_vse.vfk");
            var tok = (Tokens)scanner.yylex();
            while (tok != Tokens.EOF)
            {
                if (tok == Tokens.error)
                    Assert.Fail();

                tok = (Tokens)scanner.yylex();
            }
        }

        [TestMethod]
        [DeploymentItem("Files\\Invalid.txt")]
        public void InvalidFileScan()
        {
            var scanner = Scanner.Create("Files\\Invalid.txt");
            var tok = (Tokens)scanner.yylex();
            Assert.AreEqual(Tokens.error, tok);
        }

        [TestMethod]
        [DeploymentItem("Files\\PM.txt")]
        public void PMString()
        {
            var scanner = Scanner.Create("Files\\PM.txt");
            var tok = (Tokens)scanner.yylex();
            Assert.AreEqual(Tokens.STRING, tok);
        }
    }
}
