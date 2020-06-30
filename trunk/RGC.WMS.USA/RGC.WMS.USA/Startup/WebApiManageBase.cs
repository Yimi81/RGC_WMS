using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace RGC.WMS.USA
{
    [Route("rest/[controller]")]
    public class WebApiManageBase : WebApiBase
    {
        public const string SessionValidateCode = "Manage_ValidateCode"; //验证码
        public WebApiManageBase()
        {
           
        }

        protected async Task<T> getPostData<T>(Stream str)
        {
            string data = "";
            if (str != null)
            {
                StreamReader streamReader = new StreamReader(str);
                data = await streamReader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected async Task<string> getPostData(Stream str)
        {
            string data = "";
            if (str != null)
            {
                StreamReader streamReader = new StreamReader(str);
                data = await streamReader.ReadToEndAsync();
            }
            return data;
        }
    }
}
