
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.goldparser;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                  =  0, // (EOF)
        SYMBOL_ERROR                =  1, // (Error)
        SYMBOL_COMMENT              =  2, // Comment
        SYMBOL_WHITESPACE           =  3, // Whitespace
        SYMBOL_MINUS                =  4, // '-'
        SYMBOL_PERCENT              =  5, // '%'
        SYMBOL_LPAREN               =  6, // '('
        SYMBOL_RPAREN               =  7, // ')'
        SYMBOL_TIMES                =  8, // '*'
        SYMBOL_COMMA                =  9, // ','
        SYMBOL_DOTDOT               = 10, // '..'
        SYMBOL_DIV                  = 11, // '/'
        SYMBOL_LBRACKET             = 12, // '['
        SYMBOL_RBRACKET             = 13, // ']'
        SYMBOL_PLUS                 = 14, // '+'
        SYMBOL_LT                   = 15, // '<'
        SYMBOL_LTEQ                 = 16, // '<='
        SYMBOL_LTGT                 = 17, // '<>'
        SYMBOL_EQ                   = 18, // '='
        SYMBOL_EQEQ                 = 19, // '=='
        SYMBOL_GT                   = 20, // '>'
        SYMBOL_GTEQ                 = 21, // '>='
        SYMBOL_AND                  = 22, // and
        SYMBOL_BOOLEANLITERAL       = 23, // BooleanLiteral
        SYMBOL_ELSE                 = 24, // else
        SYMBOL_END                  = 25, // end
        SYMBOL_FOR                  = 26, // for
        SYMBOL_IDENTIFIER           = 27, // Identifier
        SYMBOL_IF                   = 28, // if
        SYMBOL_IN                   = 29, // in
        SYMBOL_INPUT                = 30, // Input
        SYMBOL_NEWLINE              = 31, // NewLine
        SYMBOL_NOT                  = 32, // not
        SYMBOL_NUMBER               = 33, // Number
        SYMBOL_OR                   = 34, // or
        SYMBOL_PRINT                = 35, // print
        SYMBOL_REALCOMMENT          = 36, // RealComment
        SYMBOL_STRINGLITERAL        = 37, // StringLiteral
        SYMBOL_WHILE                = 38, // while
        SYMBOL_ADDEXP               = 39, // <AddExp>
        SYMBOL_ANDEXPRESSION        = 40, // <AndExpression>
        SYMBOL_ASSIGNMENT           = 41, // <Assignment>
        SYMBOL_BLOCKSTATEMENT       = 42, // <BlockStatement>
        SYMBOL_COMPARISON           = 43, // <Comparison>
        SYMBOL_EXPRESSION           = 44, // <Expression>
        SYMBOL_FOR2                 = 45, // <For>
        SYMBOL_IF2                  = 46, // <If>
        SYMBOL_INDEXING             = 47, // <Indexing>
        SYMBOL_LIST                 = 48, // <List>
        SYMBOL_LISTELEMS            = 49, // <ListElems>
        SYMBOL_MULEXP               = 50, // <MulExp>
        SYMBOL_NEGEXP               = 51, // <NegExp>
        SYMBOL_NOTEXPRESSION        = 52, // <NotExpression>
        SYMBOL_OPTIONALNEWLINE      = 53, // <OptionalNewLine>
        SYMBOL_OPTIONALNEWLINEOREOF = 54, // <OptionalNewLineOrEOF>
        SYMBOL_OREXPRESSION         = 55, // <OrExpression>
        SYMBOL_PRINT2               = 56, // <Print>
        SYMBOL_PROGRAM              = 57, // <Program>
        SYMBOL_RANGE                = 58, // <Range>
        SYMBOL_SIMPLESTATEMENT      = 59, // <SimpleStatement>
        SYMBOL_STATEMENT            = 60, // <Statement>
        SYMBOL_STATEMENTS           = 61, // <Statements>
        SYMBOL_TERMINATOR           = 62, // <Terminator>
        SYMBOL_VALUE                = 63, // <Value>
        SYMBOL_WHILE2               = 64  // <While>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                               =  0, // <Program> ::= <OptionalNewLine> <Statements> <OptionalNewLineOrEOF>
        RULE_OPTIONALNEWLINE_NEWLINE               =  1, // <OptionalNewLine> ::= NewLine
        RULE_OPTIONALNEWLINE                       =  2, // <OptionalNewLine> ::= 
        RULE_OPTIONALNEWLINEOREOF_NEWLINE          =  3, // <OptionalNewLineOrEOF> ::= NewLine
        RULE_OPTIONALNEWLINEOREOF                  =  4, // <OptionalNewLineOrEOF> ::= 
        RULE_STATEMENTS                            =  5, // <Statements> ::= <Statement> <Terminator> <Statements>
        RULE_STATEMENTS2                           =  6, // <Statements> ::= <Statement> <Terminator>
        RULE_TERMINATOR_NEWLINE                    =  7, // <Terminator> ::= NewLine
        RULE_STATEMENT                             =  8, // <Statement> ::= <SimpleStatement>
        RULE_STATEMENT2                            =  9, // <Statement> ::= <BlockStatement>
        RULE_STATEMENT_REALCOMMENT                 = 10, // <Statement> ::= RealComment
        RULE_SIMPLESTATEMENT                       = 11, // <SimpleStatement> ::= <Assignment>
        RULE_SIMPLESTATEMENT2                      = 12, // <SimpleStatement> ::= <Print>
        RULE_BLOCKSTATEMENT                        = 13, // <BlockStatement> ::= <If>
        RULE_BLOCKSTATEMENT2                       = 14, // <BlockStatement> ::= <While>
        RULE_BLOCKSTATEMENT3                       = 15, // <BlockStatement> ::= <For>
        RULE_ASSIGNMENT_IDENTIFIER_EQ              = 16, // <Assignment> ::= Identifier '=' <Expression>
        RULE_PRINT_PRINT                           = 17, // <Print> ::= print <Expression>
        RULE_IF_IF_NEWLINE_END                     = 18, // <If> ::= if <Expression> NewLine <Statements> end
        RULE_IF_IF_NEWLINE_ELSE_NEWLINE_END        = 19, // <If> ::= if <Expression> NewLine <Statements> else NewLine <Statements> end
        RULE_WHILE_WHILE_NEWLINE_END               = 20, // <While> ::= while <Expression> NewLine <Statements> end
        RULE_FOR_FOR_IDENTIFIER_IN_NEWLINE_END     = 21, // <For> ::= for Identifier in <Expression> NewLine <Statements> end
        RULE_EXPRESSION                            = 22, // <Expression> ::= <OrExpression>
        RULE_OREXPRESSION_OR                       = 23, // <OrExpression> ::= <AndExpression> or <OrExpression>
        RULE_OREXPRESSION                          = 24, // <OrExpression> ::= <AndExpression>
        RULE_ANDEXPRESSION_AND                     = 25, // <AndExpression> ::= <NotExpression> and <AndExpression>
        RULE_ANDEXPRESSION                         = 26, // <AndExpression> ::= <NotExpression>
        RULE_NOTEXPRESSION_NOT                     = 27, // <NotExpression> ::= not <NotExpression>
        RULE_NOTEXPRESSION                         = 28, // <NotExpression> ::= <Comparison>
        RULE_COMPARISON_GT                         = 29, // <Comparison> ::= <Comparison> '>' <AddExp>
        RULE_COMPARISON_LT                         = 30, // <Comparison> ::= <Comparison> '<' <AddExp>
        RULE_COMPARISON_GTEQ                       = 31, // <Comparison> ::= <Comparison> '>=' <AddExp>
        RULE_COMPARISON_LTEQ                       = 32, // <Comparison> ::= <Comparison> '<=' <AddExp>
        RULE_COMPARISON_EQEQ                       = 33, // <Comparison> ::= <Comparison> '==' <AddExp>
        RULE_COMPARISON_LTGT                       = 34, // <Comparison> ::= <Comparison> '<>' <AddExp>
        RULE_COMPARISON                            = 35, // <Comparison> ::= <AddExp>
        RULE_ADDEXP_PLUS                           = 36, // <AddExp> ::= <AddExp> '+' <MulExp>
        RULE_ADDEXP_MINUS                          = 37, // <AddExp> ::= <AddExp> '-' <MulExp>
        RULE_ADDEXP                                = 38, // <AddExp> ::= <MulExp>
        RULE_MULEXP_TIMES                          = 39, // <MulExp> ::= <MulExp> '*' <NegExp>
        RULE_MULEXP_DIV                            = 40, // <MulExp> ::= <MulExp> '/' <NegExp>
        RULE_MULEXP_PERCENT                        = 41, // <MulExp> ::= <MulExp> '%' <NegExp>
        RULE_MULEXP                                = 42, // <MulExp> ::= <NegExp>
        RULE_NEGEXP_MINUS                          = 43, // <NegExp> ::= '-' <Value>
        RULE_NEGEXP                                = 44, // <NegExp> ::= <Value>
        RULE_VALUE_IDENTIFIER                      = 45, // <Value> ::= Identifier
        RULE_VALUE_NUMBER                          = 46, // <Value> ::= Number
        RULE_VALUE_STRINGLITERAL                   = 47, // <Value> ::= StringLiteral
        RULE_VALUE_BOOLEANLITERAL                  = 48, // <Value> ::= BooleanLiteral
        RULE_VALUE_INPUT                           = 49, // <Value> ::= Input
        RULE_VALUE                                 = 50, // <Value> ::= <List>
        RULE_VALUE2                                = 51, // <Value> ::= <Indexing>
        RULE_VALUE_LPAREN_RPAREN                   = 52, // <Value> ::= '(' <Expression> ')'
        RULE_LIST_LBRACKET_RBRACKET                = 53, // <List> ::= '[' <ListElems> ']'
        RULE_LISTELEMS                             = 54, // <ListElems> ::= <Range>
        RULE_LISTELEMS_COMMA                       = 55, // <ListElems> ::= <Expression> ',' <ListElems>
        RULE_LISTELEMS2                            = 56, // <ListElems> ::= <Expression>
        RULE_LISTELEMS3                            = 57, // <ListElems> ::= 
        RULE_RANGE_DOTDOT                          = 58, // <Range> ::= <Expression> '..' <Expression>
        RULE_INDEXING_IDENTIFIER_LBRACKET_RBRACKET = 59  // <Indexing> ::= Identifier '[' <Expression> ']'
    };

    public class MyParser
    {
        private LALRParser parser;
        private ListBox lstBox;
        private ListBox lstBox2;
        public MyParser(string filename, ListBox lstBox,ListBox lstBox2)
        {
            this.lstBox = lstBox;
            this.lstBox2 = lstBox2;
            FileStream stream = new FileStream(filename,
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
            Init(stream);
            // stream.Close();
        }


        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent); 

        }

        public void Parse(string sourceCode)
        {
            NonterminalToken token = parser.Parse(sourceCode);
            if (token != null)
            {
                object obj = CreateObject(token);  
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOTDOT :
                //'..'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTGT :
                //'<>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AND :
                //and
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOLEANLITERAL :
                //BooleanLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INPUT :
                //Input
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NOT :
                //not
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OR :
                //or
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINT :
                //print
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REALCOMMENT :
                //RealComment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<AddExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ANDEXPRESSION :
                //<AndExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCKSTATEMENT :
                //<BlockStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPARISON :
                //<Comparison>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR2 :
                //<For>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF2 :
                //<If>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INDEXING :
                //<Indexing>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LIST :
                //<List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LISTELEMS :
                //<ListElems>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULEXP :
                //<MulExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEGEXP :
                //<NegExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NOTEXPRESSION :
                //<NotExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPTIONALNEWLINE :
                //<OptionalNewLine>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPTIONALNEWLINEOREOF :
                //<OptionalNewLineOrEOF>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OREXPRESSION :
                //<OrExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINT2 :
                //<Print>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGE :
                //<Range>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SIMPLESTATEMENT :
                //<SimpleStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTS :
                //<Statements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERMINATOR :
                //<Terminator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE2 :
                //<While>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <OptionalNewLine> <Statements> <OptionalNewLineOrEOF>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPTIONALNEWLINE_NEWLINE :
                //<OptionalNewLine> ::= NewLine
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPTIONALNEWLINE :
                //<OptionalNewLine> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPTIONALNEWLINEOREOF_NEWLINE :
                //<OptionalNewLineOrEOF> ::= NewLine
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPTIONALNEWLINEOREOF :
                //<OptionalNewLineOrEOF> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS :
                //<Statements> ::= <Statement> <Terminator> <Statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS2 :
                //<Statements> ::= <Statement> <Terminator>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERMINATOR_NEWLINE :
                //<Terminator> ::= NewLine
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <SimpleStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <BlockStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_REALCOMMENT :
                //<Statement> ::= RealComment
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SIMPLESTATEMENT :
                //<SimpleStatement> ::= <Assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SIMPLESTATEMENT2 :
                //<SimpleStatement> ::= <Print>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENT :
                //<BlockStatement> ::= <If>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENT2 :
                //<BlockStatement> ::= <While>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENT3 :
                //<BlockStatement> ::= <For>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<Assignment> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRINT_PRINT :
                //<Print> ::= print <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_NEWLINE_END :
                //<If> ::= if <Expression> NewLine <Statements> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_NEWLINE_ELSE_NEWLINE_END :
                //<If> ::= if <Expression> NewLine <Statements> else NewLine <Statements> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_WHILE_NEWLINE_END :
                //<While> ::= while <Expression> NewLine <Statements> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_FOR_IDENTIFIER_IN_NEWLINE_END :
                //<For> ::= for Identifier in <Expression> NewLine <Statements> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <OrExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXPRESSION_OR :
                //<OrExpression> ::= <AndExpression> or <OrExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXPRESSION :
                //<OrExpression> ::= <AndExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXPRESSION_AND :
                //<AndExpression> ::= <NotExpression> and <AndExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXPRESSION :
                //<AndExpression> ::= <NotExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NOTEXPRESSION_NOT :
                //<NotExpression> ::= not <NotExpression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NOTEXPRESSION :
                //<NotExpression> ::= <Comparison>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_GT :
                //<Comparison> ::= <Comparison> '>' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_LT :
                //<Comparison> ::= <Comparison> '<' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_GTEQ :
                //<Comparison> ::= <Comparison> '>=' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_LTEQ :
                //<Comparison> ::= <Comparison> '<=' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_EQEQ :
                //<Comparison> ::= <Comparison> '==' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON_LTGT :
                //<Comparison> ::= <Comparison> '<>' <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPARISON :
                //<Comparison> ::= <AddExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<AddExp> ::= <AddExp> '+' <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<AddExp> ::= <AddExp> '-' <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<AddExp> ::= <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_TIMES :
                //<MulExp> ::= <MulExp> '*' <NegExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_DIV :
                //<MulExp> ::= <MulExp> '/' <NegExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_PERCENT :
                //<MulExp> ::= <MulExp> '%' <NegExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP :
                //<MulExp> ::= <NegExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGEXP_MINUS :
                //<NegExp> ::= '-' <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGEXP :
                //<NegExp> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_IDENTIFIER :
                //<Value> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_NUMBER :
                //<Value> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_STRINGLITERAL :
                //<Value> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_BOOLEANLITERAL :
                //<Value> ::= BooleanLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_INPUT :
                //<Value> ::= Input
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE :
                //<Value> ::= <List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE2 :
                //<Value> ::= <Indexing>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LIST_LBRACKET_RBRACKET :
                //<List> ::= '[' <ListElems> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTELEMS :
                //<ListElems> ::= <Range>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTELEMS_COMMA :
                //<ListElems> ::= <Expression> ',' <ListElems>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTELEMS2 :
                //<ListElems> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTELEMS3 :
                //<ListElems> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANGE_DOTDOT :
                //<Range> ::= <Expression> '..' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INDEXING_IDENTIFIER_LBRACKET_RBRACKET :
                //<Indexing> ::= Identifier '[' <Expression> ']'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            lstBox.Items.Add($"Token error at line {args.Token.Location.LineNr}, text: '{args.Token.Text}'");
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "In line: " + args.UnexpectedToken.Location.LineNr;
            lstBox.Items.Add(message);
            string m2 = "Expected token: " + args.ExpectedTokens.ToString();
            lstBox.Items.Add(m2);
        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            string info = args.Token.Text + "   \t \t" + (SymbolConstants)args.Token.Symbol.Id;
            lstBox2.Items.Add(info);
        }

    }
}
