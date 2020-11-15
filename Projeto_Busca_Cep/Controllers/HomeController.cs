using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Busca_Cep.Models;

namespace Projeto_Busca_Cep.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Busca(string cep)
        {
            var cepObj = Cep.Busca(cep);

            if (!ObjetoNulo(cepObj))
            {
                TempData["msg"] = "Cep nao encontrado!!";
            }
            return View("Index", cepObj);
    }
    public bool ObjetoNulo(object myObject)
    {
        foreach (PropertyInfo pi in myObject.GetType().GetProperties())
        {
            if (pi.PropertyType == typeof(string))
            {
                string value = (string)pi.GetValue(myObject);
                if (!string.IsNullOrEmpty(value))
                    return true;
            }
        }
        return false;
    }
}
}
