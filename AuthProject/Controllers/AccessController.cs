using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;
using System.Security.Principal;

using MySqlConnector;
using System.Net;

using  PacketCaptureServer;
namespace AuthProject.Controllers
{
    public class AccessController : Controller
    {

  

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }


        MySqlConnection mySqlConnection = new MySqlConnection();
        MySqlCommand mySqlCommand = new MySqlCommand();

        MySqlDataReader mySqlDataReader = null;


        string server = "thisIsNotPassword:)";
        string username = "thisIsNotPassword:)";
        string password = "thisIsNotPassword:)";

        void connectionString(MySqlConnection mySqlConnection)
        {
            mySqlConnection.ConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            username + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
        }

        public static long GetUnixTimestamp(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset.HasValue)
            {
                return dateTimeOffset.Value.ToUnixTimeSeconds();
            }
            else
            {
                return 0; // Return 0 if the DateTimeOffset is null
            }
        }

        public static DateTimeOffset? GetCurrentTime()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync("https://google.com",
                          HttpCompletionOption.ResponseHeadersRead).Result;
                    return result.Headers.Date;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static int  calculateDaysLeft(int subscriptionDate, int currentTime , int days )
        {


            int difference = currentTime - subscriptionDate;


            difference = difference / 60 / 60 / 24;


            return (days - difference);


        }

        void updateLoginTime(int loginDate, string username)
        {
            string query = "UPDATE userstemp SET logindate = " + "'" + (loginDate ) + "'" + "  WHERE  username = " + "'" + username + "'";


            MySqlConnection mySqlConnection = new MySqlConnection();
            connectionString(mySqlConnection);
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand();
            MySqlDataReader mySqlDataReader;

            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandText = query;

            mySqlDataReader = mySqlCommand.ExecuteReader();
            mySqlConnection.Close();

        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {

           

       


              int daysLeft = 999999;
















    
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, "f*ck jarl software"),
                    new Claim(ClaimTypes.UserData,"99999999")
                
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme );

                AuthenticationProperties properties = new AuthenticationProperties() { 
                
                    AllowRefresh = false,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                


            return RedirectToAction("Index", "Home");
        }
            

    }
}
