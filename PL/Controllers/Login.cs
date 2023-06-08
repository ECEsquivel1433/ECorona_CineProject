using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string password)
        {
            ML.Usuario usuario = new ML.Usuario();
            // ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(configuration["WebApi"]);

                var postTask = client.PostAsJsonAsync<string>("Usuario/GetByUserName", UserName);
                postTask.Wait();

                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Usuario>();
                    readTask.Wait();
                    usuario = readTask.Result;
                    if (password == usuario.Password)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Contraseña invalida";
                        return PartialView("ModalLogin");
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Usuario invalido";
                }
            }


            return PartialView("ModalLogin");
        }
    }
}
