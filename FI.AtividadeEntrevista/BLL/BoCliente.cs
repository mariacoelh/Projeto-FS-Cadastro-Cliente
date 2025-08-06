using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        public long Incluir(Cliente cliente)
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.Incluir(cliente);
        }

        public void Alterar(Cliente cliente)
        {
            DaoCliente daoCliente = new DaoCliente();
            daoCliente.Alterar(cliente);
        }

        public Cliente Consultar(long id)
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.Consultar(id);
        }

        public void Excluir(long id)
        {
            DaoCliente daoCliente = new DaoCliente();
            daoCliente.Excluir(id);
        }

        public List<Cliente> Listar()
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.Listar();
        }

        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        public bool VerificarExistencia(string CPF)
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.VerificarExistencia(CPF);
        }
    }
}