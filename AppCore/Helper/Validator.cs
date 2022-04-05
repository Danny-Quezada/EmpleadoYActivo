using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppCore.Helper
{
    public static class Validator
    {
        public static void Validar(string correo, string numero, string cedula)
        {
			if (!Regex.IsMatch(correo, @"\A(\w+\.?\w*\@\w+\.)(com)\Z"))
			{
				throw new ArgumentException("Correo electronico invalido");
				
			}
			if (!Regex.IsMatch(numero, @"\A[0-9]{4}(\-)[0-9]{4}\Z"))
			{
				throw new ArgumentException("Número de telefono invalido");
				
			}
			if (!Regex.IsMatch(cedula, @"\A[0-9]{3}(\-)[0-9]{6}(\-)[0-9]{4}[A-Z]\Z"))
			{
				throw new ArgumentException("Cédula invalida");
			}

		}
	}
}
