using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class Usuario
	{
		public static ML.Result GetByUserName(string UserName)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (DL.EcoronaCineProjectContext context = new DL.EcoronaCineProjectContext())
				{
					var queryEF = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName {UserName}").AsEnumerable().FirstOrDefault();
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
	}
}
