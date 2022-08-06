using AgendaContatos.Data.Entites;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
    public class AccountController : Controller
    {
        //ROTA: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)

        {
            if(ModelState.IsValid)
            {
                try
                {
                    //consultar o usuário no banco de dados através do email e da senha
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    if(usuario != null)
                    {
                        //redirecionar para outra página
                        return RedirectToAction("Consulta", "Contatos");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado. Usuário inválido";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";
                }

            }
            
     
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)

        {
            //verificar se todos os campos trazidos pela classe model
            //passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = $"O email{model.Email} já está cadastrado com outro usuário. Tente outro email.";

                    }
                    else
                    {
                        var usuario = new Usuario();
                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataCadastro = DateTime.Now;


                        usuarioRepository.Create(usuario);

                        TempData["Mensagem"] = $"Parabéns{usuario.Nome}, sua conta foi cadastrada com sucesso!";

                    }


                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar {e.Message}";
                }
            }
 
            return View();
        }


        public IActionResult PasswordRecover()
        {
            return View();
        }
    }
}