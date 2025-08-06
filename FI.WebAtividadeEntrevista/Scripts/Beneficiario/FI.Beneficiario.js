document.addEventListener("DOMContentLoaded", function () {
    const inputCPF = document.getElementById('cpfBeneficiario');
    const inputNome = document.getElementById('nomeBeneficiario');
    const form = document.getElementById('formAddBeneficiario');
    const tabela = document.getElementById('tabelaBeneficiarios');

    $('#cpfBeneficiario').on('input', function () {
        $(this).val(mascaraCPF($(this).val()));
    });

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        const cpf = inputCPF.value.trim();
        const nome = inputNome.value.trim();

        if (!cpf || !nome) {
            alert("Preencha os campos CPF e Nome.");
            return;
        }

        if (!verificarCPF(cpf.replace(/\D/g, ''))) {
            ModalDialog("CPF Inválido", "Por favor, informe um CPF válido.");
            return;
        }

        let cpfDuplicado = false;
        $('#tabelaBeneficiarios tr').each(function () {
            const cpfTabela = $(this).find('td').eq(0).text().trim();
            if (cpfTabela === cpf) {
                cpfDuplicado = true;
                return false; 
            }
        });

        if (cpfDuplicado) {
            ModalDialog("CPF Duplicado", "Este CPF já foi incluído como beneficiário.");
            return;
        }

        const indexExistente = beneficiariosList.findIndex(b => b.CPF.replace(/\D/g, '') === cpf.replace(/\D/g, ''));

        if (indexExistente !== -1) {
            beneficiariosList[indexExistente].Nome = nome;

            carregarTabelaBeneficiarios();

            inputCPF.value = '';
            inputNome.value = '';
            return;
        }

        beneficiariosList.push({ CPF: cpf, Nome: nome });
        carregarTabelaBeneficiarios();
        
        inputCPF.value = '';
        inputNome.value = '';
    });

    $('#modalBeneficiarios').on('hidden.bs.modal', function () {
        inputCPF.value = '';
        inputNome.value = '';
    });

    $('#modalBeneficiarios').on('show.bs.modal', function () {
        carregarTabelaBeneficiarios();
    });

    tabela.addEventListener('click', function (event) {
        const linha = event.target.closest('tr');
        if (!linha) return;

        const cpf = linha.children[0].textContent.trim();
        const nome = linha.children[1].textContent.trim();

        if (event.target.id === 'btnExcluir') {
            const index = beneficiariosList.findIndex(b => b.CPF.replace(/\D/g, '') === cpf.replace(/\D/g, ''));
            if (index !== -1) {
                beneficiariosList.splice(index, 1);
            }

            linha.remove();
        }

        if (event.target.id === 'btnAlterar') {
            inputCPF.value = cpf;
            inputNome.value = nome;

            linha.remove();
        }
    });

    function carregarTabelaBeneficiarios() {
        tabela.innerHTML = '';

        beneficiariosList.forEach(b => {
            const novaLinha = document.createElement('tr');
            novaLinha.innerHTML = `
            <td>${mascaraCPF(b.CPF)}</td>
            <td>${b.Nome}</td>
            <td>
                <button type="button" id="btnAlterar" class="btn btn-primary btn-sm">Alterar</button>
                <button type="button" id="btnExcluir" class="btn btn-primary btn-sm">Excluir</button>
            </td>
        `;
            tabela.appendChild(novaLinha);
        });
    }
});