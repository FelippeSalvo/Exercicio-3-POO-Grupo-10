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
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Paga meus 35 real Bruno karai");
    }
}