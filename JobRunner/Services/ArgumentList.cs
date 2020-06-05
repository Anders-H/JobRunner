using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JobRunner.ObjectModel;

namespace JobRunner.Services
{
    public class ArgumentList : List<string>
    {
        public ArgumentList()
        {
        }

        public ArgumentList(string source)
        {
            var parts = Regex.Matches(source, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            foreach (var part in parts)
            {
                var p = part.Trim();
                if (p.StartsWith("\"") && p.EndsWith("\""))
                    p = p.Substring(1, p.Length - 2).Trim();
                if (!string.IsNullOrWhiteSpace(p))
                    Add(p);
            }
        }

        public string GetAfter(string name)
        {
            if (Count <= 0)
                return "";

            for (var i = 0; i < Count; i++)
            {
                if (string.Compare(this[i], name, StringComparison.InvariantCulture) != 0)
                    continue;
                
                if (i < Count - 1)
                    return this[i + 1];
                
                break;
            }

            return "";
        }

        public ArgumentList Decode(IVariableList variableList)
        {
            var result = new ArgumentList();
            var argumentDecoder = new ArgumentDecoder(variableList);
            result.AddRange(
                this.Select(a => a.StartsWith("-") ? a : argumentDecoder.GetDecodedText(a))
            );
            return result;
        }
    }
}