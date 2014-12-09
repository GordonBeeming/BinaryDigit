namespace BinaryDigit.Utilities.SyntaxHighlighter
{
    public static class SyntaxCSS
    {
        #region Public Methods and Operators

        public static string GetDefault(string keywordColour = "#0000ff", string stringLiteralColour = "#a31515", string characterLiteralColour = "#d202fe", string identifierColour = "#2b91af", string commentColour = "#008000", string regionColour = "#e0e0e0")
        {
            return @".Keyword { color: " + keywordColour + @"; }
  .StringLiteral { color: " + stringLiteralColour + @"; }
  .CharacterLiteral { color: " + characterLiteralColour + @"; }
  .Identifier { color: " + identifierColour + @"; }
  .Comment { color: " + commentColour + @"; }
  .Region { color: " + regionColour + @"; }";
        }

        #endregion
    }
}