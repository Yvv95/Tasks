using System;

namespace WordLoaderSpace
{
    public class MyDataRow
    {
        public string id { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public string type { get; set; }

        public MyDataRow(string id, string value, string text, string type)
        {
            this.id = id;
            this.value = value;
            this.text = text;
            this.type = type;
        }
        public bool IsValid()
        {
            return (value != null);
        }
        public bool isDisplaable()
        {
            return (type != "curdate");
        }

        public string getValue()
        {
            return (type == "curdate" ? DateTime.Now.ToString("dd.MM.yyyy") : value);
        }
    }
}
