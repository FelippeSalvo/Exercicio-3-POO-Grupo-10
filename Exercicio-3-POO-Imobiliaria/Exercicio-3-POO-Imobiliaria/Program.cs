public class Imovel
{
    public bool Disponivel { get; private set; }

    public Imovel(bool disponivel)
    {
        Disponivel = disponivel;
    }

    public void MarcarComoDisponivel()
    {
        Disponivel = true;
    }

    public void MarcarComoIndisponivel()
    {
        Disponivel = false;
    }

    public double CalcularAluguelComTaxas(double valorAluguel, double taxa, double tempo)
    {
        return (valorAluguel * tempo) + taxa;
    }

}
public class Contrato
{
    public Imovel Imovel { get; private set; } 
    public Inquilino Inquilino { get; private set; } 
    public double ValorMensal { get; private set; }
    public bool Ativo { get; private set; }

    public void Gerar(Imovel imovel, Inquilino inquilino, double valorMensal)
    {
        Imovel = imovel;
        Inquilino = inquilino;
        ValorMensal = valorMensal;
        Ativo = true;

        Imovel.MarcarComoIndisponivel();
    }

    public void Rescindir()
    {
        Ativo = false;

        Imovel.MarcarComoDisponivel();
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

    public void EnviarProposta(Imovel imovel, double valorProposto)
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

public class Imobiliaria
{
    private List<Imovel> imoveis;
    private List<string> visitas;

    public Imobiliaria()
    {
        imoveis = new List<Imovel>();
        visitas = new List<string>();
    }

    public void CadastrarImovel(Imovel imovel)
    {
        imoveis.Add(imovel);
    }

    public void AgendarVisita(Imovel imovel, string data)
    {
        visitas.Add("Visita agendada para o imóvel na data: " + data);
    }

    public List<Imovel> ListarDisponiveis()
    {
        List<Imovel> disponiveis = new List<Imovel>();

        foreach (Imovel i in imoveis)
        {
            if (i.Disponivel)
            {
                disponiveis.Add(i);
            }
        }

        return disponiveis;
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
        if (contratos.Any(c => c.Ativo && c.Imovel == imovel))
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

        if (contratos.Any(c => c.Ativo && c.Imovel == imovel))
        {
            Console.WriteLine("Este imóvel já possui um contrato ativo.");
            return null;
        }

        var contrato = new Contrato();
        contrato.Gerar(imovel, inquilino, valorMensal); 
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
            Imovel imovel = new Imovel(true);
            Inquilino inquilino = new Inquilino("Bruno", "99999-9999");
            Proprietario proprietario = new Proprietario("Igor", "123.456.789-00", "88888-8888");
            Imobiliaria imobiliaria = new Imobiliaria();

            proprietario.AdicionarImovel(imovel);
            imobiliaria.CadastrarImovel(imovel);

            Console.WriteLine("Imóveis disponíveis: " + imobiliaria.ListarDisponiveis().Count);
        
            inquilino.EnviarProposta(imovel, 1200);
        
            Contrato contrato = proprietario.GerarContrato(imovel, inquilino, 1200);
        
            proprietario.ExibirResumoFinanceiro();

            inquilino.RegistrarReclamacao("Chuveiro não funciona");

            proprietario.RescinidirContrato(contrato);

            proprietario.ExibirResumoFinanceiro();

        
    }
}
