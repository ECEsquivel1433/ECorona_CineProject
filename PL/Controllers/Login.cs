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
		public ActionResult Login( byte[] pass, string UserName, string Email, int tab)
		{
			ML.Result result = BL.Usuario.GetByUserName(UserName);
			if (result.Correct)
			{
				ML.Usuario usuario = (ML.Usuario)result.Object;
				if (pass == usuario.Password)
				{

					return RedirectToAction("Pupular", "Pelicula");
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
			return PartialView("ModalLogin");
		}
	}
}
