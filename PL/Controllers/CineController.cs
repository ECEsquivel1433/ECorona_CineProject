using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Cines = result.Objects;

            return View(cine);

        }
        public ActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el Cine";

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el Cine";
            }
            return PartialView("Modal");

        }

        [HttpGet]
        public ActionResult FormMapa(int? IdCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            ML.Result resultZonas = BL.Zona.GetAll();
            cine.Zona.Zonas = resultZonas.Objects;
            if (IdCine == null)
            {
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(IdCine.Value);

                cine = (ML.Cine)result.Object;
                cine.Zona.Zonas = resultZonas.Objects;
                return View(cine);

            }

        }
        [HttpPost]
        public ActionResult FormMapa(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if (cine.IdCine == 0)
            {
                result = BL.Cine.Add(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "Se inserto correctamente el cine";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el cine";
                }
            }
            else
            {
                result = BL.Cine.Update(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el cine";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el cine";
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Estadistica()
        {
            ML.Cine cine = new ML.Cine();
            cine.Ventas = new ML.Venta();
            //ML.Result resultCines = BL.Cine.GetAll();
            ML.Result resultVentas = BL.Cine.GetAllVentas();
            

            //cine.Cines = resultCines.Objects;
            cine = (ML.Cine)resultVentas.Object;


            return View(cine);

        }





    }
}
