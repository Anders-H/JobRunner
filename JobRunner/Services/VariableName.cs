using System;
using System.Linq;
using System.Text;

namespace JobRunner.Services
{
    public class VariableName
    {
        private readonly string _source;

        public VariableName(string source)
        {
            _source = source ?? "";
        }

        public string GetCleanName()
        {
            var result = new StringBuilder();
            const string accepted = "_abcdefghijklmnopqrstuvwxyz0123456789";
            foreach (var c in _source.Where(c => accepted.IndexOf(c.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0))
                result.Append(c);
            return result.ToString();
        }

        public string GetCleanValue()
        {
            var result = new StringBuilder();
            const string accepted = @"_abcdefghijklmnopqrstuvwxyz0123456789 .,!:;<>|()@#-?=$/\";
            foreach (var c in _source.Where(c => accepted.IndexOf(c.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0))
                result.Append(c);
            return result.ToString();
        }
    }
}