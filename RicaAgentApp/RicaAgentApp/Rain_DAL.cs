using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RicaAgent.Classes;
using RicaAgent.Requests;
using RicaAgent.Responses;
using RicaAgentApp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RicaAgent
{
    public class Rain_DAL
    {


        public static HttpMessageHandler DefaultHttpMessageHandler = new HttpClientHandler();

        public static async System.Threading.Tasks.Task<LoginResponse> LoginAsync(String id_number, String password)
        {
            LoginResponse resp = new LoginResponse();
            resp.ResponseCode = 99;
            resp.ResponseMessage = "Unable to authenticate ID number";

            dynamic order = new JObject();

            order.id_number = id_number;
            order.password = password;


            try
            {
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(order);

                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    client.DefaultRequestHeaders.Add("key", Globals.KEY);
                    client.DefaultRequestHeaders.Add("apiKey", Globals.API_KEY);
                    client.Timeout = new TimeSpan(0, 1, 0);


                    using (HttpResponseMessage response = await client.PostAsync(Globals.API_URL + "rica/agent/login",
                        cont))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                resp.ResponseCode = 0;
                                resp.ResponseMessage = "Success";
                                resp.token = mycontent;
                            }
                            else
                            {
                                if (mycontent.Contains("Already logged in"))
                                {
                                    resp.ResponseCode = 0;
                                    resp.ResponseMessage = "Success";
                                    resp.token = mycontent;

                                }
                                else
                                {
                                    resp.ResponseCode = 99;
                                    resp.ResponseMessage = mycontent;
                                }
                            }

                        }
                    }
                }
            }
            catch 
            {

            }

            return resp;
        }



        public static async System.Threading.Tasks.Task<BaseResponse> UploadDocumentAsync(String token, String id_number, String file, String base64, String document_type)
        {
            BaseResponse resp = new BaseResponse();
            resp.ResponseCode = 99;
            resp.ResponseMessage = "Unable to authenticate ID number";


            dynamic order = new JObject();

            order.id_number = id_number;
            order.file_name = file;
            order.document_type = document_type;
            order.content = base64;

            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    client.DefaultRequestHeaders.Add("key", Globals.KEY);
                    client.DefaultRequestHeaders.Add("apiKey", Globals.API_KEY);
                    client.DefaultRequestHeaders.Add("rica-session", token);
                    client.Timeout = new TimeSpan(0, 1, 0);

                    string json = JsonConvert.SerializeObject(order);

                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(Globals.API_URL + "rica/add/document", cont))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                resp.ResponseCode = 0;
                                resp.ResponseMessage = "Success";
//                                resp.token = mycontent;
                            }
                            else
                            {
                                resp.ResponseCode = 99;
                                resp.ResponseMessage = mycontent;
                            }

                        }
                    }
                }
            }
            catch
            {

            }

            return resp;
        }
    }
}
