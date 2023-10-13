using Common;
using Common.Utilities;
using Data.DTO;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.SMS
{
    public class SmsService : ISmsService, IScopedDependency
    {
 

        public SmsService()
        {
         
        }

       public  int SendSMS(string phonenumber , SmsDTO data)
        {
            data.mobile = phonenumber.Fa2En();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.sms.ir/v1/send/verify");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers.Add("X-API-KEY", "Sj007oC8vbWnNgNtLaxef2Ii01CuQCFgUYtyhTIgJNmcigUcFBGEercAcWpsJSJw"); //Add a valid API Key
            
            string postData = JsonConvert.SerializeObject(data);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }
            
            HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();

            Stream webStream = httpWebResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<ResultSms>(response);

            return result.status;
        }

    }
}
