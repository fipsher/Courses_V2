using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    static class Constructor
    {
        private static IBuilder _builder;

        static Constructor()
        {
            _builder = new Builder();
        }

        public static List<Dictionary<string, object>> ConstructOptionList(IList list)
        {
            var result = _builder.Build(list);
            return result;
        }
    }

    interface IBuilder
    {
        List<Dictionary<string, object>> Build(IList list);
    }

    class Builder : IBuilder
    {
        private string[] _exceptions = new[] { "Course" };

        public List<Dictionary<string, object>> Build(IList list)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            foreach (var entity in list)
            {
                Dictionary<string, object> localResult = new Dictionary<string, object>();
                foreach (var prop in entity.GetType().GetProperties())
                {
                    var value = prop.GetValue(entity, null);
                    if (value != null && !_exceptions.Contains(prop.Name))
                    {
                        localResult.Add(prop.Name, value);
                    }
                }
                result.Add(localResult);
            }
            
            return result;
        }
    }
}
