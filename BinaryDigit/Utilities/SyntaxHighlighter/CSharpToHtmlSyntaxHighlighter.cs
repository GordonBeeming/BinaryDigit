namespace BinaryDigit.Utilities.SyntaxHighlighter
{
    using System.Text;
    using System.Web;

    using Roslyn.Compilers;
    using Roslyn.Compilers.CSharp;

    public static class CSharpToHtmlSyntaxHighlighter
    {
        #region Static Fields

        private static readonly MetadataReference mscorlib = MetadataReference.CreateAssemblyReference(typeof(object).Assembly.Location);

        #endregion

        #region Public Methods and Operators

        public static string GetHtml(string snippetOfCode)
        {
            SyntaxTree syntaxTree = SyntaxTree.ParseText(snippetOfCode);
            SemanticModel semanticModel = GetSemanticModelForSyntaxTree(syntaxTree);
            var htmlColorizerSyntaxWalker = new HtmlColorizerSyntaxWalker();

            var htmlBuilder = new StringBuilder();
            htmlColorizerSyntaxWalker.DoVisit(syntaxTree.GetRoot(), semanticModel, (tk, text) =>
            {
                switch (tk)
                {
                    case TokenKind.None:
                        htmlBuilder.Append(text);
                        break;
                    case TokenKind.Keyword:
                    case TokenKind.Identifier:
                    case TokenKind.StringLiteral:
                    case TokenKind.CharacterLiteral:
                    case TokenKind.Comment:
                    case TokenKind.DisabledText:
                    case TokenKind.Region:
                        htmlBuilder.Append("<span class=\"" + tk.ToString() + "\">" + HttpUtility.HtmlEncode(text) + "</span>");
                        break;
                    default:
                        break;
                }
            });
            return htmlBuilder.ToString();
        }

        #endregion

        #region Methods

        private static SemanticModel GetSemanticModelForSyntaxTree(SyntaxTree syntaxTree)
        {
            Compilation compilation = Compilation.Create("CSharpToHtmlSyntaxHighlighterCompilation",
                syntaxTrees: new[] { syntaxTree },
                references: new[] { mscorlib });

            return compilation.GetSemanticModel(syntaxTree);
        }

        #endregion
    }
}