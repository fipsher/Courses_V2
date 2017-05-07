using MongoDB.Bson;
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
        private string[] _ignore = new[] { "Course" };

        private string[] _objectId = new[] { "ProviderCathedraId" };

        private string[] _objectIdArrays = new[] { "StudentIds", "StudentIds", "SubscriberCathedraIds" };

        public List<Dictionary<string, object>> Build(IList list)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            foreach (var entity in list)
            {
                Dictionary<string, object> localResult = new Dictionary<string, object>();
                foreach (var prop in entity.GetType().GetProperties())
                {
                    var value = prop.GetValue(entity, null);

                    if (value != null && !_ignore.Contains(prop.Name))
                    {
                        if (prop.Name == "Id")
                        {
                            if (ObjectId.TryParse(value.ToString(), out ObjectId parseResult))
                            {
                                localResult.Add("_id", parseResult);
                            }
                        }
                        else
                        {
                            if (_objectId.Contains(prop.Name))
                            {
                                if (ObjectId.TryParse(value.ToString(), out ObjectId parseResult))
                                {
                                    localResult.Add(prop.Name, parseResult);
                                }
                            }
                            if (_objectIdArrays.Contains(prop.Name))
                            {
                                var tempList = new List<ObjectId>();
                                foreach (var el in value as IList)
                                {
                                    if (ObjectId.TryParse(el.ToString(), out ObjectId parseResult))
                                    {
                                        tempList.Add(parseResult);
                                    }
                                }
                                localResult.Add(prop.Name, tempList);
                            }
                            localResult.Add(prop.Name, value);
                        }
                    }

                }
                result.Add(localResult);
            }

            return result;
        }
    }
}
