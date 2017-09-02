namespace IGDBLib
{
    public class IGDBFilter
    {
        public IGDBFilter(object field, IGDBFilterCondition condition, string value)
        {
            FilterCondition = condition;
            Value = value;
            if (field.GetType() == typeof(IGDBFields))
                Field = ((IGDBFields)field).ToString().ToLower();
            else
                Field = field.ToString();
        }

        public string Field { get; private set; }

        public IGDBFilterCondition FilterCondition { get; private set; }

        public string Value { get; private set; }
    }
}
