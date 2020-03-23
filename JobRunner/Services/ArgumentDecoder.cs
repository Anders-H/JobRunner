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
            
        }
    }
}