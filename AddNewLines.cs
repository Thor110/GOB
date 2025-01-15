using System.Text;

namespace VideoLOB
{
    internal class AddNewLines
    {
        /// <summary>
        /// Adds new lines to a string
        /// </summary>
        /// <param name="newLines">The number of new lins to add</param>
        /// <param name="inputString">The string to add new lines to</param>
        public void AddNewLine(int newLines, StringBuilder inputString)
        {
            for (int i = 0; i < newLines; i++)
            {
                inputString.Append(Environment.NewLine);
            }
        }
    }
}
