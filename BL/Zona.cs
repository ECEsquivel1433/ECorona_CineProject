using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new();

            try
            {
                using DL.EcoronaCineProjectContext context = new();
                var departamentoList = context.Zonas.FromSqlRaw($"ZonaGetAll").ToList();

                result.Objects = new List<object>();
                foreach (var row in departamentoList)
                {
                    {
                        ML.Zona zona = new ML.Zona();

                        zona.IdZona = row.IdZona;
                        zona.Nombre = row.Nombre;

                        result.Objects.Add(zona);
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
    }
}
