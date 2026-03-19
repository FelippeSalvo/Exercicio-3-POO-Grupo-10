public class Contrato
{
    public string Imovel { get; private set; } //Mudar de string para tipo Complexo assim que o mesmo tiver pronto
    public Inquilino Inquilino { get; private set; } //Tipo Mudado para Inquilino, após criação. BY: Bruno
    public double ValorMensal { get; private set; }
    public bool Ativo { get; private set; }

    public void Gerar(string imovel, Inquilino inquilino, double valorMensal)
    {
        Imovel = imovel;
        Inquilino = inquilino;
        ValorMensal = valorMensal;
        Ativo = true;

        //Imovel.MarcarComoIndisponivel(); Método de outra classe, descomentar essa linha assim que a mesma estiver pronta 
    }

    public void Rescindir()
    {
        Ativo = false;

        //Imovel.MarcarComoDisponivel(); Método de outra classe, descomentar essa linha assim que a mesma estiver pronta
    }

    public double CalcularMulta(double percentual)
    {
        return ValorMensal * percentual;
    }
}

public class Inquilino
{
    public string Nome { get; private set; }
    public string Contato { get; private set; }
    public Contrato Contrato { get; private set; }
    public List<string> Reclamacoes { get; private set; } = new List<string>();

    public Inquilino(string nome, string contato)
    {
        Nome = nome;
        Contato = contato;
    }

    public void EnviarProposta(string imovel, double valorProposto) //Mudar de string para tipo Complexo (Imovel) assim que o mesmo estiver pronto
    {
        Console.WriteLine($"{Nome} enviou proposta para o imovel: {imovel} no valor de R$ {valorProposto}");
    }

    public void AssinarContrato(Contrato contrato)
    {
        if (contrato == null)
        {
            Console.WriteLine("Contrato inválido.");
            return;
        }

        Contrato = contrato;
        Console.WriteLine($"{Nome} assinou o contrato. Valor mensal: R$ {contrato.ValorMensal}");
    }

    public void RegistrarReclamacao(string descricao)
    {
        Reclamacoes.Add(descricao);
        Console.WriteLine($"Reclamação registrada pelo {Nome}: {descricao}");
    }
}


public class Proprietario
{
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public string Contato { get; private set; }
    private List<Imovel> imoveis = new List<Imovel>();
    private List<Contrato> contratos = new List<Contrato>();

    public Proprietario(string nome, string cpf, string contato)
    {
        Nome = nome;
        CPF = cpf;
        Contato = contato;
    }

    public void AdicionarImovel(Imovel imovel)
    {
        if (imovel == null)
        {
            Console.WriteLine("Imóvel inválido.");
            return;
        }
        imoveis.Add(imovel);
        Console.WriteLine($"Imóvel adicionado ao portfólio de {Nome}.");
    }

    public void RemoverImovel(Imovel imovel)
    {
        if (contratos.Any(c => c.Ativo && c.Imovel == imovel?.ToString()))
        {
            Console.WriteLine("Não é possível remover um imóvel com contrato ativo.");
            return;
        }
        imoveis.Remove(imovel);
        Console.WriteLine($"Imóvel removido do portfólio de {Nome}.");
    }

    public List<Imovel> ListarImoveis()
    {
        return new List<Imovel>(imoveis);
    }


    public Contrato GerarContrato(Imovel imovel, Inquilino inquilino, double valorMensal)
    {
        if (!imoveis.Contains(imovel))
        {
            Console.WriteLine("Este imóvel não pertence a este proprietário.");
            return null;
        }

        if (contratos.Any(c => c.Ativo && c.Imovel == imovel?.ToString()))
        {
            Console.WriteLine("Este imóvel já possui um contrato ativo.");
            return null;
        }

        var contrato = new Contrato();
        contrato.Gerar(imovel?.ToString(), inquilino, valorMensal); //Substituir imovel?.ToString() por imovel diretamente quando a classe Imovel estiver pronta
        contratos.Add(contrato);
        inquilino.AssinarContrato(contrato);
        Console.WriteLine($"Contrato gerado por {Nome} para {inquilino.Nome}. Valor mensal: R$ {valorMensal:F2}");
        return contrato;
    }

    public void RescinidirContrato(Contrato contrato)
    {
        if (contrato == null || !contratos.Contains(contrato))
        {
            Console.WriteLine("Contrato não encontrado no portfólio deste proprietário.");
            return;
        }
        contrato.Rescindir();
        Console.WriteLine($"Contrato rescindido por {Nome}.");
    }

    public List<Contrato> ListarContratos()
    {
        return new List<Contrato>(contratos);
    }

    public List<Contrato> ListarContratosAtivos()
    {
        return contratos.Where(c => c.Ativo).ToList();
    }


    public double CalcularReceitaMensal()
    {
        return contratos.Where(c => c.Ativo).Sum(c => c.ValorMensal);
    }

    public void ExibirResumoFinanceiro()
    {
        var ativos = ListarContratosAtivos();
        Console.WriteLine($"=== Resumo Financeiro de {Nome} ===");
        Console.WriteLine($"Imóveis cadastrados: {imoveis.Count}");
        Console.WriteLine($"Contratos ativos: {ativos.Count}");
        Console.WriteLine($"Receita mensal total: R$ {CalcularReceitaMensal():F2}");
    }


    public void AtualizarContato(string novoContato)
    {
        Contato = novoContato;
        Console.WriteLine($"Contato de {Nome} atualizado para: {novoContato}");
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Proprietário: {Nome} | CPF: {CPF} | Contato: {Contato}");
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Paga meus 35 real Bruno karai");
    }
}