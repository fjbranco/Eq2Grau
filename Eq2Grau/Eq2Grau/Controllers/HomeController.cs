using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Eq2Grau.Models;

namespace Eq2Grau.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Cálculo das raízes de uma equação do 2º grau
    /// </summary>
    /// <param name="A">parâmetro do x2</param>
    /// <param name="B">parâmetro do x</param>
    /// <param name="C">parâmetro independente</param>
    /// <returns></returns>
    public IActionResult Index(string A, string B, string C)
    {
        /* ALGORITMO
         * 1- ler parâmetros a, b, c
         * 2- verificar se os parâmetros são números
         *    se não, criar msg aviso
         *    se sim, continuo
         * 3- a =/= 0 ????
         *    se não, mostro msg de aviso
         *    se sim, continuo
         * 4- calcular DELTA = b2-4ac
         *    4.1- se DELTA > 0, calcular raízes reais
         *         x1 = (-b - sqrt(DELTA))/2/a
         *         x2 = (-b + sqrt(DELTA))/2/a
         *    4.2- se DELTA = 0
         *         x1 = x2 = (-b)/2/a
         *    4.3- se DELTA < 0, calcular raízes complexas, conjugadas
         *         x1 = (-b)/2/a '+' sqrt(-DELTA))/2/a 'i'
         *         x2 = (-b)/2/a '-' sqrt(-DELTA))/2/a 'i'
         * 5- mostrar o resultado na VIEW
         */

        //determinar se há dados para efectuar o cálculo
        if (
            !(string.IsNullOrEmpty(A) &&
              string.IsNullOrEmpty(B) &&
              string.IsNullOrEmpty(C))
           )
        {


            // 2- verificar se os parâmetros são números
            double auxA;
            if (!double.TryParse(A, out auxA))
            {
                //mensagem de aviso
                string msg = "O parâmetro A tem de ser número";
                ViewBag.mensagem = msg;
                // devolver controlo à view
                return View();
            }
            double auxB;
            if (!double.TryParse(B, out auxB))
            {
                //mensagem de aviso
                string msg = "O parâmetro B tem de ser número";
                ViewBag.mensagem = msg;
                // devolver controlo à view
                return View();
            }
            double auxC;
            if (!double.TryParse(C, out auxC))
            {
                //mensagem de aviso
                string msg = "O parâmetro C tem de ser número";
                ViewBag.mensagem = msg;
                // devolver controlo à view
                return View();
            }

            // 3-
            if (auxA == 0)
            {
                //mensagem de aviso
                string msg = "A não pode ser 0";
                ViewBag.mensagem = msg;
                // devolver controlo à view
                return View();
            }

            // 4- calcular DELTA = b2-4ac
            double delta = auxB * auxB - 4 * auxA * auxC;
            string x1 = "";
            string x2 = "";
            if (delta > 0)
            {
                x1 = (-auxB - Math.Sqrt(delta)) / 2 / auxA + "";
                x2 = (-auxB + Math.Sqrt(delta)) / 2 / auxA + "";
                ViewBag.mensagem = "Existem duas raízes reais distintas";
            }

            if (delta == 0)
            {

                x1 = (-auxB) / 2 / auxA + "";
                x2 = x1;
                ViewBag.mensagem = "Existem duas raízes reais, mas iguais";
            }
            if (delta < 0)
            {
                x1 = -auxB / 2 / auxA + " - " + Math.Sqrt(-delta) / 2 / auxA + "i";
                x2 = -auxB / 2 / auxA + " + " + Math.Sqrt(-delta) / 2 / auxA + "i";
                ViewBag.mensagem = "Existem duas raízes complexas conjugadas";
            }


            // 5-
            ViewBag.x1 = x1;
            ViewBag.x2 = x2;
        }

            return View();
        }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
