namespace JobRunner.ObjectModel
{
    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Variable(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string GetXml() =>
            $@"   <variable>
      <name>{System.Net.WebUtility.HtmlEncode(Name)}</name>
      <value>{System.Net.WebUtility.HtmlEncode(Value)}</value>
   </variable>";
    }
}