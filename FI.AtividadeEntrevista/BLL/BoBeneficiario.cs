using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.Beneficiario.DaoBeneficiario bo = new DAL.Beneficiario.DaoBeneficiario();
            return bo.Incluir(beneficiario);
        }

        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.Beneficiario.DaoBeneficiario bo = new DAL.Beneficiario.DaoBeneficiario();
            bo.Alterar(beneficiario);
        }

        public List<DML.Beneficiario> ConsultarListaBeneficiario(long id)
        {
            DAL.Beneficiario.DaoBeneficiario bo = new DAL.Beneficiario.DaoBeneficiario();
            return bo.ConsultarListaBeneficiario(id);
        }

        public void Excluir(string cpf)
        {
            DAL.Beneficiario.DaoBeneficiario bo = new DAL.Beneficiario.DaoBeneficiario();
            bo.Excluir(cpf);
        }
    }
}