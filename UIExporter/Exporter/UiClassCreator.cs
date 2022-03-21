using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PROJECT
{
    public class UiClassCreator
    {
        private string _classContent;
        private readonly string _type;
        private readonly string _declarationValue;
        private readonly string _getterTemplate;
        private readonly string _uiName;
        private readonly string _classname;
        private readonly string _properties;
        private const string Pattern = @"<[({value})]>";
        private readonly List<UiProperties> _uiPropertiesList;

        public UiClassCreator((string type, string declarationValue) value, string uiName, string properties)
        {
            _uiName = uiName;
            _classname = ToConvention(_uiName) + "Ui";
            _properties = properties;
            _type = value.type;
            _declarationValue = value.declarationValue;
            _classContent = File.ReadAllText("Assets/UIExporter/Exporter/class_ui.txt");
            _getterTemplate = File.ReadAllText("Assets/UIExporter/Exporter/getter_ui.txt");
            _uiPropertiesList = new List<UiProperties>();
        }

        public void Add(UiProperties uiProperties)
        {
            _uiPropertiesList.Add(uiProperties);
        }

        private static string ToConvention(string value)
        {
            var first = value[0].ToString().ToUpper();
            return value.Substring(1).Insert(0, first);
        }

        private static string AddTab(string value, int tab)
        {
            var tabs = "";
            for (var i = 0; i < tab; i++)
                tabs += "\t";

            return AddBefore(value, tabs);
        }

        private static string AddBefore(string value, string before)
        {
            var split = value.Split('\n');

            return split.Where(s => !string.IsNullOrWhiteSpace(s))
                .Aggregate("", (current, s) => current + (before + s + "\n"));
        }

        private void Generate()
        {
            var tmpDeclaration = AddTab($"private {_uiName} : {_type};\n", 2);
            var tmpDeclarationValue = AddTab(AddBefore($"{_uiName} = {_declarationValue}", "this."), 3);
            var tmpGetter = AddGetter(_uiName, _type) + "\n\n";
            var tmpValue = AddTab(AddBefore(_properties, "this."), 3) + "\n";
            var tmpValueExt = "";
            foreach (var properties in _uiPropertiesList)
            {
                tmpDeclaration += AddTab($"private {properties.GetVarname()} : {properties.GetType()};", 2);
                tmpDeclarationValue +=
                    AddTab(AddBefore($"{properties.GetVarname()} = {properties.GetDeclarationValue()}", "this."), 3);
                tmpGetter += AddGetter(properties.GetVarname(), properties.GetType()) + "\n\n";
                tmpValue += AddTab(AddBefore(properties.GetContentProperties(), "this."), 3) + "\n";
                if (properties.GetContentExternalProperties() != null)
                    tmpValueExt += AddTab(properties.GetContentExternalProperties(), 3) + "\n";
            }

            tmpValue += tmpValueExt;

            _classContent = _classContent.Replace(Pattern.Replace("value", "classname"), ToConvention(_uiName) + "Ui");
            _classContent = _classContent.Replace(Pattern.Replace("value", "vardecl"), tmpDeclaration);
            _classContent = _classContent.Replace(Pattern.Replace("value", "vardeclvalue"), tmpDeclarationValue);
            _classContent = _classContent.Replace(Pattern.Replace("value", "getter"), tmpGetter);
            _classContent = _classContent.Replace(Pattern.Replace("value", "value"), tmpValue);
        }

        private string AddGetter(string varname, string type)
        {
            var clone = (string) _getterTemplate.Clone();
            clone = clone.Replace(Pattern.Replace("value", "varnameconv"), ToConvention(varname));
            clone = clone.Replace(Pattern.Replace("value", "varname"), varname);
            clone = clone.Replace(Pattern.Replace("value", "vartype"), type);
            return clone;
        }

        public string GetClassName()
        {
            return _classname;
        }

        public override string ToString()
        {
            Generate();
            return _classContent;
        }
    }
}