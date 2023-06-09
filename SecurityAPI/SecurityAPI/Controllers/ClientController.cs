using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecurityAPI.Constants;
using SecurityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class ClientController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            UserLogin _login = new UserLogin();
            _login.UserName = "duocnv123";
            _login.Password = "CodeMega2023@";
            string baseUrl = "http://localhost:63832/";  
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync("http://localhost:63832/api/Authentication", _login);


                var checkRespon = await res.Content.ReadAsStringAsync();
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await res.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ResponeModel>(responseContent);
                    //AppJWToken.token = "responseObject.token";
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseObject.token);
                    HttpResponseMessage res1 = await httpClient.GetAsync("http://localhost:63832/api/Permission/UserRole");
                    if(res1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var checl = res1.Content.ReadAsStringAsync();
                    }    
                    return Ok("Đăng nhập thành công");
                }
                return BadRequest();

            }
           
        }
        
    }
}
