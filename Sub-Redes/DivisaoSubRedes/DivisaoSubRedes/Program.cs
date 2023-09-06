using System.Diagnostics;

SubRedes rede = new SubRedes();
string mascara = "", ipRede = "", cond = "";
do
{
    Console.Clear();
    {
        Console.Write("Digite seu IP(X.X.X.X): ");
        ipRede = Console.ReadLine();
        while (rede.ClasseIp(ipRede) == 'I')
        {
            Console.Clear();
            Console.WriteLine("IP INVÁLIDO!");
            Console.Write("Digite seu IP(X.X.X.X): ");
            ipRede = Console.ReadLine();
        }
        if (rede.ClasseIp(ipRede) == 'A')
        {
            Console.Write("Digite sua Mascara(255.x.x.x): ");
            mascara = Console.ReadLine();
            rede.DefinirSubRede(mascara);
        }
        else if (rede.ClasseIp(ipRede) == 'B')
        {
            Console.Write("Digite sua Mascara(255.255.x.x): ");
            mascara = Console.ReadLine();
            rede.DefinirSubRede(mascara);
            Console.WriteLine(rede.MostrarSubRede(ipRede));
        }
        else if (rede.ClasseIp(ipRede) == 'C')
        {
            Console.Write("Digite sua Mascara(255.255.255.x): ");
            mascara = Console.ReadLine();
            rede.DefinirSubRede(mascara);
            Console.WriteLine(rede.MostrarSubRede(ipRede));
        }
        else Console.WriteLine("Tipo de Rede indefinido");
    }
    Console.WriteLine("\n* Deseja Refazer a pesquisa? (s/sim)");
    cond = Console.ReadLine().ToUpper();
} while (cond.Equals("S") || cond.Equals("SIM"));