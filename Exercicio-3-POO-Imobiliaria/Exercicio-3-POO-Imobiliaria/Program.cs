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

public class Imovel
{
    public bool Disponivel;
//Classe ja criada pra não dar erro
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