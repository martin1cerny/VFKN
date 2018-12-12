using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace VFKN.Parser
{
    public partial class Scanner
    {
        public static Scanner Create(string path)
        {
            var stream = File.OpenRead(path);
            return Create(stream);
        }

        public static Scanner Create(Stream stream)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var codePage = GetCodePage(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return new Scanner(stream, codePage);
        }

        private static string GetCodePage(Stream stream)
        {
            var log = LogProvider.GetLogger<Scanner>();
            var e = Encoding.GetEncoding("ISO-8859-2");
            using (var r = new StreamReader(stream, e, false, 1024, true))
            {
                var count = 0;
                var line = r.ReadLine();
                while (line != null || count > 50)
                {
                    count++;
                    if (line.StartsWith("&HCODEPAGE;"))
                    {
                        var parts = line.Split(';');
                        if (parts.Length < 2)
                        {
                            line = r.ReadLine();
                            continue;
                        }
                        var codePage = parts[1].Trim('"');
                        if (codePage == "EE8MSWIN1250")
                            return "windows-1250";
                        else if (codePage == "WE8ISO8859P2")
                            return "ISO-8859-2";
                    }

                    line = r.ReadLine();
                }
            }
            // default encoding
            log.LogWarning("Codepage not defined in the file. Using default ISO-8859-2");
            return "ISO-8859-2";
        }

        public string Text
        {
            get
            {
                return yytext
                    .Replace("¤\r\n", "\r\n")
                    .Replace("\"\"", "\"")
                    .Trim('"');
            }
        }
    }
}
