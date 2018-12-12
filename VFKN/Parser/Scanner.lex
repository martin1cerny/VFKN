%namespace VFKN.Parser
%scannertype Scanner
%tokentype InternalTokens
%option verbose, summary, noCompressNext, noPersistBuffer, noParser, codePage:ISO--8859-2, unicode

%{
    LexLocation yylloc;
%}

%%

%{
		
%}

"\t"																{ }
" "																	{ }
[\0]+																{ } 
[\r\n]																{ return (int)Tokens.EOL; } 

&H[a-zA-Z0-9]*														{ return (int)Tokens.HEADER; }
&B[a-zA-Z0-9]*														{ return (int)Tokens.BLOCK; }
&D[a-zA-Z0-9]*														{ return (int)Tokens.DATA; }
&K																	{ return (int)Tokens.END_VFKN; }

[\-\+0-9][0-9]*													    { return (int)Tokens.INTEGER; } 
[\-\+\.0-9][\.0-9]+												    { return (int)Tokens.FLOAT; } 
[\"](¤[\r][\n]|[\"][\"]|[^\"])*[\"]									{ return (int)Tokens.STRING; } 
[a-zA-Z0-9_]+[ ][A-Z]+[0-9\.]*											{ return (int)Tokens.COL_HEADER; } 
[;]																	{ return (int)Tokens.ENDCOL; }
.																	{ return (int)Tokens.error; }

%{
	yylloc = new LexLocation(tokLin,tokCol,tokELin,tokECol);
%}

%%