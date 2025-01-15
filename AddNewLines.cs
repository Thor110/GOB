namespace System.Text
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Adds new lines to a StringBuilder object.
        /// </summary>
        /// <param name="builder">The StringBuilder object to add new lines to</param>
        /// <param name="newLines">The number of new lines to add</param>
        /// <returns>The modified StringBuilder object</returns>
        public static StringBuilder AddNewLines(this StringBuilder builder, int newLines)
        {
            for (int i = 0; i < newLines; i++)
            {
                builder.Append(Environment.NewLine);
            }
            return builder;
        }
    }
}