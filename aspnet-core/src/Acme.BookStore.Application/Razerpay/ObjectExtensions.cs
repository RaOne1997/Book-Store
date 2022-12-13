using Newtonsoft.Json;

namespace BOOKSTore.Razerpay
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(dynamic source)
            where T : class, new()
        {

            var stringdata = JsonConvert.SerializeObject(source);
            var Result = JsonConvert.DeserializeObject<T>(stringdata);

            return Result;
        }
        
    }
}
 