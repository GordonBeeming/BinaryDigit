namespace BinaryDigit.Utilities.SyntaxHighlighter
{
    using System;

    using Roslyn.Compilers.CSharp;

    // UPDATES : http://social.msdn.microsoft.com/Forums/nb-NO/roslyn/thread/5a56122b-d6e5-40e0-8912-60eba3fc9a01
    internal class HtmlColorizerSyntaxWalker : SyntaxWalker
    {
        #region Fields

        private SemanticModel semanticModel;

        private Action<TokenKind, string> writeDelegate;

        #endregion

        // Handle SyntaxTokens

        #region Public Methods and Operators

        public override void VisitToken(SyntaxToken token)
        {
            base.VisitLeadingTrivia(token);

            bool isProcessed = false;
            if (token.IsKeyword())
            {
                this.writeDelegate(TokenKind.Keyword, token.ToString());
                isProcessed = true;
            }
            else
            {
                switch (token.Kind)
                {
                    case SyntaxKind.StringLiteralToken:
                        this.writeDelegate(TokenKind.StringLiteral, token.ToString());
                        isProcessed = true;
                        break;
                    case SyntaxKind.CharacterLiteralToken:
                        this.writeDelegate(TokenKind.CharacterLiteral, token.ToString());
                        isProcessed = true;
                        break;
                    case SyntaxKind.IdentifierToken:
                        if (token.Parent is SimpleNameSyntax)
                        {
                            // SimpleName is the base type of IdentifierNameSyntax, GenericNameSyntax etc.
                            // This handles type names that appear in variable declarations etc.
                            // e.g. "TypeName x = a + b;"
                            var name = (SimpleNameSyntax)token.Parent;
                            SymbolInfo semanticInfo = this.semanticModel.GetSymbolInfo(name);
                            if (semanticInfo.Symbol != null && semanticInfo.Symbol.Kind != SymbolKind.ErrorType)
                            {
                                switch (semanticInfo.Symbol.Kind)
                                {
                                    case SymbolKind.NamedType:
                                        this.writeDelegate(TokenKind.Identifier, token.ToString());
                                        isProcessed = true;
                                        break;
                                    case SymbolKind.Namespace:
                                    case SymbolKind.Parameter:
                                    case SymbolKind.Local:
                                    case SymbolKind.Field:
                                    case SymbolKind.Property:
                                        this.writeDelegate(TokenKind.None, token.ToString());
                                        isProcessed = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else if (token.Parent is TypeDeclarationSyntax)
                        {
                            // TypeDeclarationSyntax is the base type of ClassDeclarationSyntax etc.
                            // This handles type names that appear in type declarations
                            // e.g. "class TypeName { }"
                            var name = (TypeDeclarationSyntax)token.Parent;
                            NamedTypeSymbol symbol = this.semanticModel.GetDeclaredSymbol(name);
                            if (symbol != null && symbol.Kind != SymbolKind.ErrorType)
                            {
                                switch (symbol.Kind)
                                {
                                    case SymbolKind.NamedType:
                                        this.writeDelegate(TokenKind.Identifier, token.ToString());
                                        isProcessed = true;
                                        break;
                                }
                            }
                        }
                        break;
                }
            }

            if (!isProcessed)
            {
                this.HandleSpecialCaseIdentifiers(token);
            }

            base.VisitTrailingTrivia(token);
        }

        // Handle SyntaxTrivia
        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            switch (trivia.Kind)
            {
                case SyntaxKind.MultiLineCommentTrivia:
                case SyntaxKind.SingleLineCommentTrivia:
                    this.writeDelegate(TokenKind.Comment, trivia.ToString());
                    break;
                case SyntaxKind.DisabledTextTrivia:
                    this.writeDelegate(TokenKind.DisabledText, trivia.ToString());
                    break;
                case SyntaxKind.DocumentationCommentTrivia:
                    this.writeDelegate(TokenKind.Comment, trivia.ToString());
                    break;
                case SyntaxKind.RegionDirectiveTrivia:
                case SyntaxKind.EndRegionDirectiveTrivia:
                    this.writeDelegate(TokenKind.Region, trivia.ToString());
                    break;
                default:
                    this.writeDelegate(TokenKind.None, trivia.ToString());
                    break;
            }
            base.VisitTrivia(trivia);
        }

        #endregion

        #region Methods

        internal void DoVisit(SyntaxNode token, SemanticModel semanticModel, Action<TokenKind, string> writeDelegate)
        {
            this.semanticModel = semanticModel;
            this.writeDelegate = writeDelegate;
            this.Visit(token);
        }

        private void HandleSpecialCaseIdentifiers(SyntaxToken token)
        {
            switch (token.Kind)
            {
                    // Special cases that are not handled because there is no semantic context/model that can truely identify identifiers.
                case SyntaxKind.IdentifierToken:
                    if ((token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.Parameter)
                        || (token.Parent.Kind == SyntaxKind.EnumDeclaration)
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.Attribute)
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.CatchDeclaration)
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.ObjectCreationExpression)
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.ForEachStatement && !(token.GetNextToken().Kind == SyntaxKind.CloseParenToken))
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Parent.Kind == SyntaxKind.CaseSwitchLabel && !(token.GetPreviousToken().Kind == SyntaxKind.DotToken))
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.MethodDeclaration)
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.CastExpression)
                        //e.g. "private static readonly HashSet patternHashSet = new HashSet();" the first HashSet in this case
                        || (token.Parent.Kind == SyntaxKind.GenericName && token.Parent.Parent.Kind == SyntaxKind.VariableDeclaration)
                        //e.g. "private static readonly HashSet patternHashSet = new HashSet();" the second HashSet in this case
                        || (token.Parent.Kind == SyntaxKind.GenericName && token.Parent.Parent.Kind == SyntaxKind.ObjectCreationExpression)
                        //e.g. "public sealed class BuilderRouteHandler : IRouteHandler" IRouteHandler in this case
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.BaseList)
                        //e.g. "Type baseBuilderType = typeof(BaseBuilder);" BaseBuilder in this case
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Parent.Parent.Kind == SyntaxKind.TypeOfExpression)
                        // e.g. "private DbProviderFactory dbProviderFactory;" OR "DbConnection connection = dbProviderFactory.CreateConnection();"
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.VariableDeclaration)
                        // e.g. "DbTypes = new Dictionary();" DbType in this case
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.TypeArgumentList)
                        // e.g. "DbTypes.Add("int", DbType.Int32);" DbType in this case
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.MemberAccessExpression && token.Parent.Parent.Parent.Kind == SyntaxKind.Argument && !(token.GetPreviousToken().Kind == SyntaxKind.DotToken || Char.IsLower(token.ToString()[0])))
                        // e.g. "schemaCommand.CommandType = CommandType.Text;" CommandType in this case
                        || (token.Parent.Kind == SyntaxKind.IdentifierName && token.Parent.Parent.Kind == SyntaxKind.MemberAccessExpression && !(token.GetPreviousToken().Kind == SyntaxKind.DotToken || Char.IsLower(token.ToString()[0])))
                        )
                    {
                        this.writeDelegate(TokenKind.Identifier, token.ToString());
                    }
                    else
                    {
                        if (token.ToString() == "HashSet")
                        {
                        }
                        this.writeDelegate(TokenKind.None, token.ToString());
                    }
                    break;
                default:
                    this.writeDelegate(TokenKind.None, token.ToString());
                    break;
            }
        }

        #endregion
    }
}