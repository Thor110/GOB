using System.Text;

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
        /// <usage>stringBuilder.AddNewLines(numLines);</usage>
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
namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Adds new lines to a string.
        /// </summary>
        /// <param name="content">The string to add new lines to</param>
        /// <param name="newLines">The number of new lines to add</param>
        /// <returns>The modified string with new lines</returns>
        /// <remarks>Note that strings are immutable, so this method returns a new string with the added new lines.</remarks>
        /// <usage>stringName = stringName.AddNewLines(numLines);</usage>
        public static string AddNewLines(this string content, int newLines)
        {
            var builder = new StringBuilder(content);
            for (int i = 0; i < newLines; i++)
            {
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }
    }
}