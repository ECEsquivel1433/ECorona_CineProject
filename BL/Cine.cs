using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Cine
    {
        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new();

            try
            {
                using DL.EcoronaCineProjectContext context = new();
                int queryEF = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Nombre}', '{cine.Direccion}', {cine.Zona.IdZona}, {cine.Venta}");
                if (queryEF > 0)
                {
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el cine" + ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new();

            try
            {
                using DL.EcoronaCineProjectContext context = new();
                int queryEF = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine},'{cine.Nombre}', '{cine.Direccion}', {cine.Zona.IdZona}, {cine.Venta}");
                if (queryEF > 0)
                {
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al actualizar el cine" + ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new();

            try
            {
                using DL.EcoronaCineProjectContext context = new();
                int queryEF = context.Database.ExecuteSqlRaw($"CineDelete {IdCine}");
                if (queryEF > 0)
                {
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al eliminar el cine" + ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new();

            try
            {
                using DL.EcoronaCineProjectContext context = new();
                var cineList = context.Cines.FromSqlRaw("CineGetAll").ToList();

                result.Objects = new List<object>();
                foreach (var row in cineList)
                {
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.Zona = new ML.Zona();

                        cine.IdCine = row.IdCine;
                        cine.Nombre = row.Nombre;
                        cine.Direccion = row.Direccion;
                        cine.Venta = row.Venta;
                        cine.Zona.IdZona = row.IdZona;
                        cine.Zona.Nombre = row.Zona;


                        result.Objects.Add(cine);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new();
            try
            {
                using DL.EcoronaCineProjectContext context = new();
                var row = context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    ML.Cine cine = new ML.Cine();
                    cine.Zona = new ML.Zona();

                    cine.IdCine = row.IdCine;
                    cine.Nombre = row.Nombre;
                    cine.Direccion = row.Direccion;
                    cine.Venta = row.Venta;
                    cine.Zona.IdZona = row.IdZona;
                    cine.Zona.Nombre = row.Zona;

                    result.Object = cine;
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAllVentas()
        {
            ML.Result result = new();
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            cine.Ventas = new ML.Venta();
            try
            {
                using DL.EcoronaCineProjectContext context = new();
                var cineList = context.Cines.FromSqlRaw("CineGetAll").ToList();

                result.Objects = new List<object>();
                foreach (var row in cineList)
                {
                    {
                        cine.IdCine = row.IdCine;
                        cine.Venta = row.Venta;
                        cine.Zona.IdZona = row.IdZona;

                        if (cine.Zona.IdZona == 1){
                            cine.Ventas.SumaNorte= cine.Ventas.SumaNorte + cine.Venta;
                            cine.Ventas.Sumatotal = cine.Ventas.Sumatotal + cine.Venta;
                        }
                        if (cine.Zona.IdZona == 2)
                        {
                            cine.Ventas.SumaSur = cine.Ventas.SumaSur + cine.Venta;
                            cine.Ventas.Sumatotal = cine.Ventas.Sumatotal + cine.Venta;
                        }
                        if (cine.Zona.IdZona == 3)
                        {
                            cine.Ventas.SumaEste = cine.Ventas.SumaEste + cine.Venta;
                            cine.Ventas.Sumatotal = cine.Ventas.Sumatotal + cine.Venta;
                        }
                        if (cine.Zona.IdZona == 4)
                        {
                            cine.Ventas.SumaOeste = cine.Ventas.SumaOeste + cine.Venta;
                            cine.Ventas.Sumatotal = cine.Ventas.Sumatotal + cine.Venta;
                        }
                        result.Object=cine;
                    }
                }
                cine.Ventas.PromNorte = (cine.Ventas.SumaNorte / cine.Ventas.Sumatotal) * 100;
                cine.Ventas.PromSur = (cine.Ventas.SumaSur / cine.Ventas.Sumatotal) * 100;
                cine.Ventas.PromEste = (cine.Ventas.SumaEste / cine.Ventas.Sumatotal) * 100;
                cine.Ventas.PromOeste = (cine.Ventas.SumaOeste / cine.Ventas.Sumatotal) * 100;
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}