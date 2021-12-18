using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebAPI
{
    [TestFixture()]
    public class Test
    {
        static readonly string token = "2YGUuZ49GUMAAAAAAAAAAR-v0K0Z8IOwhWl2JGQGH70IPNnnS00S7pglc0RHOSws";

        public string GetIdFile(string file)
        {
            RestClient client = new RestClient("https://api.dropboxapi.com/2/files/get_metadata");
            RestRequest request = new RestRequest(Method.POST);
            

            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            string data = JsonConvert.SerializeObject(new
            {
                path = "/ArtemFolder/" + file
            });
            string result = client.Execute(request.AddJsonBody(data)).Content.ToString();
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            return response["id"];
        }

        [Test()]
        public void Test1Upload()
        {
            string dropboxPath = "/ArtemFolder/Photo.jpeg";
            string path = Directory.GetCurrentDirectory() + "/../../Photo.jpeg";

            RestClient client = new RestClient("https://content.dropboxapi.com/2/files/upload");
            RestRequest request = new RestRequest(Method.POST);

            string jsonHeader = JsonConvert.SerializeObject(new
            {
                path = dropboxPath,
                mode = "add",
                autorename = true,
                mute = false,
                strict_conflict = false

            });
            request.AddHeader("Dropbox-API-Arg", jsonHeader);
            request.AddHeader("Content-Type", "application/octet-stream");
            request.AddHeader("Authorization", "Bearer " + token);

            byte[] data = File.ReadAllBytes(path);
            var body = new Parameter("file", data, ParameterType.RequestBody);
            request.Parameters.Add(body);

            string result = client.Execute(request).Content.ToString();
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual(dropboxPath, response["path_display"]);
        }

        [Test()]
        public void Test2GetFileMetadata()
        {
            string file = "Photo.jpeg";
            var idFile = GetIdFile(file);
            Console.WriteLine(idFile);
            string dropboxPath = "/ArtemFolder/" + file;
            RestClient client = new RestClient("https://api.dropboxapi.com/2/sharing/get_file_metadata");
            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            string data = JsonConvert.SerializeObject(new
            {
                file = idFile,
                actions = new string[] {}
            });

            var result = client.Execute(request.AddJsonBody(data)).Content.ToString();
            Console.WriteLine(result);
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            Console.WriteLine(response["path_display"]);
            Assert.AreEqual(dropboxPath, response["path_display"]);

        }

        [Test()]
        public void Test3Delete()
        {
            string file = "Photo.jpeg";
            string dropboxPath = "/ArtemFolder/" + file;

            RestClient client = new RestClient("https://api.dropboxapi.com/2/files/delete_v2");
            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            string data = JsonConvert.SerializeObject(new
            {
                path = dropboxPath
            });

            var result = client.Execute(request.AddJsonBody(data)).Content.ToString();
            Console.WriteLine(result);
            var response = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(result);
            Console.WriteLine(response);
            Assert.AreEqual(dropboxPath, response["metadata"]["path_display"]);
        }

    }

}
