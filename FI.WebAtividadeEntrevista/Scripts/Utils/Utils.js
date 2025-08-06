function mascaraCPF(input) {
    let valor = input.replace(/\D/g, ''); 
    if (valor.length > 11) valor = valor.slice(0, 11);

    valor = valor.replace(/(\d{3})(\d)/, '$1.$2');
    valor = valor.replace(/(\d{3})(\d)/, '$1.$2');
    valor = valor.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    return valor;
}

function verificarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');

    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) {
        return false;
    }

    const digitoVerificador = (cpfArray, pesoInicial) => {
        let soma = 0;
        for (let i = 0; i < cpfArray.length; i++) {
            soma += cpfArray[i] * (pesoInicial - i);
        }
        const resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    };

    const cpfArray = cpf.split('').map(Number);
    const primeiroDV = digitoVerificador(cpfArray.slice(0, 9), 10);
    const segundoDV = digitoVerificador(cpfArray.slice(0, 10), 11);

    return primeiroDV === cpfArray[9] && segundoDV === cpfArray[10];
}