using Microsoft.AspNetCore.Mvc;

namespace KH.Pepper.Web.Config
{
    public class ProblemDetailsMapping
    {

        public ProblemDetailsMapping()
        {
            Fields = new Dictionary<string,object>();
        }

        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }

        public Dictionary<string, object> Fields { get; set; }

        public void AddField(string name, object value)
        {
            Fields.Add(name, value);
        }

        public ProblemDetails ToProblemDetails()
        {
            var details = new ProblemDetails
            {
                Type = Type,
                Title = Title,
                Status = Status,
                Detail = Detail,
                Instance = Instance
            };

            if(Fields.Any())
            {
                //details.Extensions.Add("extensions", Fields
                //    .ToDictionary(k => k.Key.ToCamelCase(), v => v.Value));

                details.Extensions.Add("extensions", Fields
                    .ToDictionary(k => k.Key, v => v.Value));


            }
            return details;
        }

        //public string ToCamelCase(this string s)
        //{
        //    var words = s.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
        //        .Select(word => word.Substring(0, 1).ToLower() +
        //                        word.Substring(1));

        //    var result = String.Concat(words);
        //    return result;
        //}

    }
   
}
