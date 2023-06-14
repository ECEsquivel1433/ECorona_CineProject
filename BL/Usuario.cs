using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class Usuario
	{
		public static ML.Result GetByEmail(ML.Email email1)
		{
			ML.Result result = new ML.Result();
			
			try
			{
				using (DL.EcoronaCineProjectContext context = new DL.EcoronaCineProjectContext())
				{
					var queryEF = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email1.EmailDirection}'").AsEnumerable().FirstOrDefault();
					if (queryEF != null)
					{
						 ML.Usuario usuario = new ML.Usuario();

						usuario.IdUsuario = queryEF.IdUsuario;
						usuario.UserName = queryEF.Username;
						usuario.Nombre = queryEF.Nombre;
						usuario.ApellidoPaterno = queryEF.ApellidoPaterno;
						usuario.ApellidoMaterno = queryEF.ApellidoMaterno;
						usuario.Email = queryEF.Email;
						usuario.Password = queryEF.Password;
						

						result.Object = usuario;

						result.Correct = true;
					}
				}
			}
			catch (Exception ex)
			{
				result.Correct = false;
				result.ErrorMessage = "Ocurrio un error al insertar el usuario" + ex;
			}
			return result;
		}
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EcoronaCineProjectContext context = new DL.EcoronaCineProjectContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.UserName}', '{usuario.Email}', @Password", new SqlParameter("@Password", usuario.Password));

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result UpdatePassword(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EcoronaCineProjectContext context = new DL.EcoronaCineProjectContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdatePassword '{usuario.Email}', @Password", new SqlParameter("@Password", usuario.Password));

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
