using Microsoft.AspNetCore.Mvc;
using WEB.ASPNET.Cadastrar_Usuario.Models;
using System.Diagnostics;
using Servico;
using Servico.model;


namespace PrimeiraAplicaçãoWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private static List<Usuario> _list = new List<Usuario>()
        {
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult NovoUsuario(int? id)
        {

            Usuario? usuario = null;

            if (id != null)
            {
                var db = new Db();

                var usuarioTO = db.GetUserById(id.GetValueOrDefault());

                usuario = new Usuario()
                {
                    Id = usuarioTO.Id,
                    nome = usuarioTO.Nome,
                    email = usuarioTO.Email
                };
            }
            return View(usuario);
        }



        public IActionResult PersistirUsuario(int? id, string nome, string email)
        {
            var db = new Db();

            if (id == null)
            {
                var novoUsuario = new UsuarioTO()
                {
                    Nome = nome,
                    Email = email,
                };

                db.AddUsuario(novoUsuario);

            }
            else
            {
                var alterarUsuario = new UsuarioTO()
                {
                    Id = id.GetValueOrDefault(),
                    Nome = nome,
                    Email = email,
                };

                db.UpdateUserById(alterarUsuario);
            }

            return RedirectToAction("Usuario");
        }


        public IActionResult Usuario()
        {

            var Db = new Db();

            var listaTO = Db.GetUsers();

            var listaUsuario = new List<Usuario>();

            foreach (var usuarioTO in listaTO)
            {
                listaUsuario.Add(
                    new Usuario() { Id = usuarioTO.Id, nome = usuarioTO.Nome, email = usuarioTO.Email }
                );
            };

            var ViewModel = new UsuarioViewModel() { ListUsuario = listaUsuario };

            return View(ViewModel);

        }

        public IActionResult Deletar(int Id)
        {
            var db = new Db();

            db.DeleteUserById(Id);


            return RedirectToAction("Usuario");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}