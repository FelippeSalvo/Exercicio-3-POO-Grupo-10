public class Contrato
{
    public string Imovel { get; private set; } //Mudar de string para tipo Complexo assim que o mesmo tiver pronto
    public string Inquilino { get; private set; } //Mudar de string para tipo Complexo assim que o mesmo tiver pronto
    public double ValorMensal { get; private set; }
    public bool Ativo { get; private set; }

    public void Gerar(string imovel, string inquilino, double valorMensal)
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



internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Paga meus 35 real Bruno karai");
    }
}