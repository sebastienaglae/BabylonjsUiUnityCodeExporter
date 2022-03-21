namespace PROJECT
{
    public class UiProperties
    {
        private readonly string _varname;
        private readonly string _declarationValue;
        private string _contentProperties;
        private string _contentExternalProperties;
        private readonly string _type;

        public UiProperties((string declarationValue, string type) value, string varname)
        {
            _varname = varname;
            _declarationValue = value.declarationValue;
            _type = value.type;
        }

        public void AddProperties(string code)
        {
            _contentProperties = code;
        }
        
        public void AddExternalProperties(string code)
        {
            _contentExternalProperties = code;
        }

        public string GetVarname()
        {
            return _varname;
        }

        public string GetDeclarationValue()
        {
            return _declarationValue;
        }

        public string GetContentProperties()
        {
            return _contentProperties;
        }
        
        public string GetContentExternalProperties()
        {
            return _contentExternalProperties;
        }

        public string GetType()
        {
            return _type;
        }
    }
}