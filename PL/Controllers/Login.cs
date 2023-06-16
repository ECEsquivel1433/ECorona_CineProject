using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;

        }

        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.UserName != null) // validar con un campo de la vista si se va a insertar o loguear 
            {
                //registrar
                usuario.Password = passwordHash; //asignar contraseña encriptada y agregar usuario
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                ML.Email email1 = new ML.Email();
                email1.EmailDirection = usuario.Email;//pasar el correo de un tipo de objeto a otro ya que mi metodo funciona con ML.Email
                //login
                ML.Result result = BL.Usuario.GetByEmail(email1);
                usuario = (ML.Usuario)result.Object;//obtener resultado del GetByEmail y pasarlo a tipo ML.usuario

                if (result.Correct == true)
                {
                    if (usuario.Password.SequenceEqual(passwordHash)) //comparar contraseña del GetByEmail y mandar a la vista si es correcto
                    {
                        return RedirectToAction("Popular", "Pelicula");
                    }

                }
                else
                {
                    ViewBag.Message = "Correo o contraseña incorrectos";
                }
            }
            return View("Modal");
        }


        [HttpGet]
        public ActionResult EmailContraseña()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmailContraseña(string email, string token)
        {
            ML.Email email1 = new ML.Email();
            email1.EmailDirection = email;
            email1.EmailOrigen = configuration["emailOrigen"];
            email1.EmailRoute = configuration["emailRoute"];
            email1.EmailPassword = configuration["emailPassword"];
            email1.UrlDomain = configuration["urlDomain"];

            ML.Result result = BL.Usuario.GetByEmail(email1);

            if (result.Correct == true)
            {
                ML.Result resultEmail = BL.Email.SendEmail(email1);

                ViewBag.Modal = "show";
                ViewBag.Message = "Se ha enviado un correo de confirmación a tu correo electronico";
                return View("Modal");
            }
            else
            {
                ViewBag.Modal = "show";
                ViewBag.Message = "El correo ingresado no esta registrado";
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult CambiarContrasena(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Email = email;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult CambiarContrasena(string email,string password)
        {
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            ML.Usuario usuario = new ML.Usuario();
            usuario.Password = passwordHash; //asignar contraseña encriptada y agregar usuario
            usuario.Email = email;
            ML.Result result = BL.Usuario.UpdatePassword(usuario);
            ViewBag.Message = "Se actualizo la contraseña";
            return View("Modal");
        }
    }
}
