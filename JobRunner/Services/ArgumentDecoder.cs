using System.Text.RegularExpressions;
using JobRunner.ObjectModel;

namespace JobRunner.Services
{
    public class ArgumentDecoder
    {
        private readonly IVariableList _variables;

        public ArgumentDecoder(IVariableList variables)
        {
            _variables = variables;
        }

        public string GetDecodedText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text ?? "";
            
            var matches = Regex.Matches(text, @"\[(.*?)\]");
            
            if (matches.Count <= 0)
                return text;

            foreach (Match match in matches)
            {
                var variableName = match.Value;
                var noBrackets = variableName.Substring(1, variableName.Length - 2);
                var variable = _variables.GetVariable(noBrackets);
                
                if (variable == null)
                    continue;
                
                text = text.Replace(variableName, variable.Value ?? "");
            }
            
            return text;
        }
    }
}