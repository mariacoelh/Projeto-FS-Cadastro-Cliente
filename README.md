# Projeto-FS-Cadastro-Cliente

## Visão Geral

A atividade consiste na inclusão do campo de **CPF** no formulário de cadastro de clientes, com aplicação de **formatação** e **validação** adequada.

Também foi solicitado o desenvolvimento de um **modal para gerenciamento de beneficiários**, permitindo a inclusão, edição e exclusão de registros, contendo os campos **"Nome"** e **"CPF"**.

---

## Features Adicionadas

### Cadastro de Cliente

- O formulário de cadastro de cliente **não permite a adição de um CPF já existente** no banco de dados.

  Exemplo:
  ![Tela cadastro](Readme/CPFJaCadastrado.png)

- O campo de CPF possui as seguintes regras:
  - Máscara de formatação: `###.###.###-##`
  - Validação baseada no algoritmo de verificação do dígito verificador do CPF.

  Exemplo:
  ![Tela cadastro](Readme/CPFInvalido.png)

---

### Gerenciamento de Beneficiários

- O modal permite:
  - **Incluir**
  - **Editar**
  - **Remover** beneficiários

- Regras aplicadas:
  - Não é possível adicionar mais de um beneficiário com o **mesmo CPF** para o **mesmo cliente**.

  Exemplo:
  ![Tela beneficiario](Readme/CPFBenefExistente.png)

  Exemplo de inclusão:
  ![Tela beneficiario](Readme/IncluirBeneficiario.png)

---

## Banco de Dados

- Foi adicionada a coluna `CPF` na tabela `CLIENTES`.
- Foi criada a tabela `BENEFICIARIOS`, com os campos:
  - `ID`
  - `NOME`
  - `CPF`
  - `IDCLIENTE`
- Foram implementadas stored procedures específicas para o gerenciamento dos beneficiários.

Estrutura:
![Banco de dados](Readme/BancoDados.png)