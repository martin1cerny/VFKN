using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Parser
{
    public enum Tokens
    {
        EOF = 0,
        maxParseToken = int.MaxValue,
        error = 63,
        HEADER = 64,
        BLOCK = 65,
        DATA = 66,
        EOL = 67,
        STRING = 68,
        INTEGER = 69,
        FLOAT = 70,
        ENDCOL = 71,
        COL_HEADER = 72,
        END_VFKN = 73
    }
}
