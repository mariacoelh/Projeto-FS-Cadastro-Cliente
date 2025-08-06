using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (bo.VerificarExistencia(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("Já existe um cliente cadastrado com este CPF.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                if (model.Beneficiarios != null && model.Beneficiarios.Any())
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    foreach (var benef in model.Beneficiarios)
                    {
                        boBeneficiario.Incluir(new Beneficiario()
                        {
                            CPF = benef.CPF,
                            Nome = benef.Nome,
                            IdCliente = model.Id
                        });
                    }
                }

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                BoBeneficiario boBeneficiario = new BoBeneficiario();

                var beneficiarioExistente = boBeneficiario.ConsultarListaBeneficiario(model.Id);

                var cpfsAtuais = model.Beneficiarios?.Select(b => b.CPF.Replace(".", "").Replace("-", "")).ToList() ?? new List<string>();

                foreach (var antigo in beneficiarioExistente)
                {
                    if (!cpfsAtuais.Contains(antigo.CPF))
                    {
                        boBeneficiario.Excluir(antigo.CPF);
                    }
                }

                if (model.Beneficiarios != null && model.Beneficiarios.Any())
                {
                    foreach (var beneficiarioModel in model.Beneficiarios)
                    {
                        var beneficiario = new FI.AtividadeEntrevista.DML.Beneficiario
                        {
                            Id = beneficiarioModel.Id,
                            Nome = beneficiarioModel.Nome,
                            CPF = beneficiarioModel.CPF,
                            IdCliente = model.Id
                        };

                        bool existeCPFCadastrado = beneficiarioExistente.Exists(x => x.CPF == beneficiario.CPF);

                        if (beneficiarioExistente != null && beneficiarioExistente.Any() && existeCPFCadastrado)
                            boBeneficiario.Alterar(beneficiario);
                        else
                            boBeneficiario.Incluir(beneficiario);
                    }
                }

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF
                };

                BoBeneficiario boBenef = new BoBeneficiario();
                List<Beneficiario> listaBenef = boBenef.ConsultarListaBeneficiario(cliente.Id);

                model.Beneficiarios = new List<BeneficiarioModel>();

                foreach (var item in listaBenef)
                {
                    model.Beneficiarios.Add(new BeneficiarioModel()
                    {
                        CPF = item.CPF,
                        Nome = item.Nome,
                        IdCliente = item.IdCliente,
                        Id = item.Id
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}