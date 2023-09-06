using System;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class SubRedes
{
    public int quantidadeDeHosts = 0, quantidadeDeSubRedes;
    public char tipo = 'I';
    int diferencaIps = 0;
    string finalMascC = "", finalMascB = "";
    public char ClasseIp(string ipRede)
    {
        char tipoRede = 'I';
        string caracteres = "";
        for (int i = 0; i < ipRede.Length; i++)
        {
            if (ipRede[i] == '.')
            {
                if (int.Parse(caracteres) >= 0 && int.Parse(caracteres) < 128)
                {
                    tipoRede = 'A';
                    tipo = tipoRede;
                }
                if (int.Parse(caracteres) >= 128 && int.Parse(caracteres) < 192)
                {
                    tipoRede = 'B';
                    tipo = tipoRede;
                }
                if (int.Parse(caracteres) >= 192 && int.Parse(caracteres) < 224)
                {
                    tipoRede = 'C';
                    tipo = tipoRede;
                }
            }
            else caracteres += ipRede[i].ToString();
        }
        return tipoRede;
    }
    public void DefinirSubRede(string mascara)
    {
        int contP = 0;
        string mascaraSubRede = "";
        if (mascara.Length < 8 || mascara.Length > 15)
        {
            Console.WriteLine("Mascara Inválida");
        }
        else
        {
            if (tipo == 'A')
            {
                for (int i = 0; i < mascara.Length; i++)
                {

                    if (contP >= 1)
                    {
                        mascaraSubRede += mascara[i];
                    }
                    if (mascara[i] == '.')
                    {
                        contP++;
                    }
                }
                CalcularHosts(mascaraSubRede);
            }
            else if (tipo == 'B')
            {
                if (mascara.ToCharArray(0, 7).ToString() != "255.255")
                {
                    Console.WriteLine("Mascara Inválida");
                    tipo = 'I';
                }
                else
                {
                    for (int i = 0; i < mascara.Length; i++)
                    {
                        if (contP >= 2)
                        {
                            mascaraSubRede += mascara[i];
                        }
                        if (mascara[i] == '.')
                        {
                            contP++;
                        }
                    }
                    CalcularHosts(mascaraSubRede);
                } 
            }
            else if (tipo == 'C')
            {
                //if (mascara.tochararray(0, 11).tostring() != "255.255.255")
                //{
                //    console.writeline("mascara inválida");
                //    tipo = 'i';
                //}
                //else
                {
                    for (int i = 0; i < mascara.Length; i++)
                    {
                        if (contP == 3)
                        {
                            mascaraSubRede += mascara[i];
                        }
                        if (mascara[i] == '.')
                        {
                            contP++;
                        }

                    }
                    CalcularHosts(mascaraSubRede);
                }   
            }
            else
            {
                Console.WriteLine("SubRede nao foi definida!");
            }
        }
    }
    public void CalcularHosts(string subRede)
    {
        string referencia = "";
        int masc = 0, cont = 0, cont1 = 0, cont0 = 0;
        if (tipo == 'A')
        {
            cont = 0;
            cont0 = 16;
            for (int i = 0; i < subRede.Length; i++)
            {
                if (cont >= 1)
                {
                    referencia += subRede[i];
                }
                if (subRede[i] == '.')
                {
                    cont++;
                }
            }
            masc = int.Parse(referencia);

            while (masc != 0)
            {
                if (masc % 2 == 1)
                {
                    cont1++;
                }
                if (masc % 2 == 0)
                {
                    cont0++;
                }
                masc = masc / 2;
            }

            quantidadeDeHosts = Convert.ToInt32(Math.Pow(2, cont0)) - 2;
            quantidadeDeSubRedes = Convert.ToInt32(Math.Pow(2, cont1));
            diferencaIps = 256 - Convert.ToInt32(referencia);

            cont0 = 0;
            cont1 = 0;
        }
        else if (tipo == 'B')
        {
            cont0 = 8;
            for (int i = 0; i < subRede.Length; i++)
            {
                if (subRede[i] == '.')
                {
                    cont++;
                }
                if (cont < 1 )
                {
                    referencia += subRede[i];
                }
                
            }
            finalMascB = referencia;
            masc = int.Parse(referencia);

            while (masc != 0)
            {
                if (masc % 2 == 1)
                {
                    cont1++;
                }
                if (masc % 2 == 0)
                {
                    cont0++;
                }
                masc = masc / 2;
            }

            quantidadeDeHosts = Convert.ToInt32(Math.Pow(2, cont0)) - 2;
            quantidadeDeSubRedes = Convert.ToInt32(Math.Pow(2, cont1));
            diferencaIps = 256 - Convert.ToInt32(referencia);

            cont0 = 0;
            cont1 = 0;

        }
        else if (tipo == 'C')
        {
            referencia = subRede;
            finalMascC = subRede;
            masc = int.Parse(referencia);

            while (masc != 0)
            {
                if (masc % 2 == 1)
                {
                    cont1++;
                }
                if (masc % 2 == 0)
                {
                    cont0++;
                }
                masc = masc / 2;
            }
            quantidadeDeHosts = Convert.ToInt32(Math.Pow(2, cont0)) - 2;
            quantidadeDeSubRedes = Convert.ToInt32(Math.Pow(2, cont1));
            diferencaIps = 256 - Convert.ToInt32(referencia);
            cont0 = 0;
            cont1 = 0;
        }
        else
        {
            Console.WriteLine("SubRede nao foi definida!");
        }
    }
    public string MostrarSubRede(string ip)
    {
        string resposta = "", cond = "";
        Console.WriteLine("* Deseja salvar a Resposta? (s/sim)");
        cond = Console.ReadLine().ToUpper();
        int contP = 0;
        if (tipo == 'A')
        {

        }
        else if (tipo == 'B')
        {
            contP = 0;
            string ips = "";
            string referenciaA = "", referenciaB = "";
            for (int i = 0; i < ip.Length; i++)
            {
                if (contP == 2)
                {
                    if (ip[i] != '.')
                    {
                        referenciaA += ip[i];
                    }
                }else if (contP > 2)
                {
                    if (ip[i] != '.')
                    {
                        referenciaB += ip[i];
                    }
                }
                else
                {
                    ips += ip[i];
                }
                if (ip[i] == '.')
                {
                    contP++;
                }
            }

            if (Convert.ToInt32(referenciaA) != 0)
            {
                resposta += $"* IP da Rede: {ip}\n";
                for (int i = 0; i < 256; i += diferencaIps)
                {
                    for (int j = 0; j < 256; j ++)
                    { 
                        if (j == Convert.ToInt32(referenciaB))
                        {
                            if (i < Convert.ToInt32(referenciaA) && (i + diferencaIps) > Convert.ToInt32(referenciaA))
                            {
                                resposta += $"-----------------------------------\n" +
                                            $"Endereço Sub-Rede: {ips + i}.0\n" +
                                            $"Primeiro IP válido: {ips + (i)}.1\n" +
                                            $"Ultimo IP válido: {ips + ((i + diferencaIps) - 1)}.254\n" +
                                            $"Endereço BroadCast: {ips + ((i + diferencaIps) - 1)}.255\n" +
                                            $"Quantidade de Hosts Válidos: {quantidadeDeHosts}";
                            }
                        }
                    }
                }
                if (cond.Equals("S") || cond.Equals("SIM"))
                {
                    SalvarResposta(resposta);
                }
            }
            else
            {
                resposta += $"*Mascara: 255.255.{finalMascB}.0\n" +
                            "--------- Sub Redes ---------\n";
                for (int i = 0; i < 256; i += diferencaIps)
                {
                    resposta +=             $"Endereço Sub-Rede: {ips + i}.0\n" +
                                            $"Primeiro IP válido: {ips + (i)}.1\n" +
                                            $"Ultimo IP válido: {ips + ((i + diferencaIps) - 1)}.254\n" +
                                            $"Endereço BroadCast: {ips + ((i + diferencaIps) - 1)}.255\n" +
                                            $"Quantidade de Hosts Válidos: {quantidadeDeHosts}\n" +
                                            $"-----------------------------------\n";
                }
                if (cond.Equals("S") || cond.Equals("SIM"))
                {
                    SalvarResposta(resposta);
                }
            }
        }
        else if (tipo == 'C')
        {
            contP = 0;
            string ips = "";
            string referencia = "";
            for (int i = 0; i < ip.Length; i++)
            {
                if (contP > 2)
                {
                    referencia += ip[i];
                }
                else
                {
                    ips += ip[i];
                }
                if (ip[i] == '.')
                {
                    contP++;
                }
            }
            if (Convert.ToInt32(referencia) != 0)
            {
                resposta += $"* IP da Rede: {ip}\n";
                for (int i = 0; i < 256; i += diferencaIps)
                {
                    if (i < Convert.ToInt32(referencia) && (i + diferencaIps) > Convert.ToInt32(referencia))
                    {
                        resposta += $"-----------------------------------\n" +
                                    $"Endereço Sub-Rede: {ips + i}\n" +
                                    $"Primeiro IP válido: {ips + (i + 1)}\n" +
                                    $"Ultimo IP válido: {ips + ((i + diferencaIps) - 2)}\n" +
                                    $"Endereço BroadCast: {ips + ((i + diferencaIps) - 1)}\n" +
                                    $"Quantidade de Hosts Válidos: {quantidadeDeHosts}";
                    }
                }
                if (cond.Equals("S") || cond.Equals("SIM"))
                {
                    SalvarResposta(resposta);
                }
            }
            else
            {
                resposta += $"*Mascara: 255.255.255.{finalMascC}\n" +
                            "--------- Sub Redes ---------\n";
                for (int i = 0; i < 256; i += diferencaIps)
                {
                    resposta += $"Endereço Sub-Rede: {ips + i}\n" +
                                $"Primeiro IP válido: {ips + (i + 1)} \n" +
                                $"Ultimo IP válido: {ips + ((i + diferencaIps) - 2)}\n" +
                                $"Endereço BroadCast: {ips + ((i + diferencaIps) - 1)}\n" +
                                $"Quantidade de Hosts Válidos: {quantidadeDeHosts}\n"+
                                "-----------------------------------------------------\n";
                }
                if (cond.Equals("S") || cond.Equals("SIM"))
                {
                    SalvarResposta(resposta);
                }
            }
        }
        else
        {
            Console.WriteLine("SubRede nao foi definida!");
        }
        return resposta;
    }
    public void SalvarResposta(string textoParaSalvar)
    {
        /*StreamWriter sw = new StreamWriter("SubRedes.csv", true);

        int totalSteps = 100;

        Console.WriteLine("Salvando...");

            // Calcula a porcentagem de progresso
            double progress = (double)i / totalSteps;

            // Limpa a linha anterior
            Console.CursorLeft = 0;
            Console.Write(new string(' ', Console.WindowWidth - 1));

            // Define a posição do cursor e imprime a barra de progresso
            Console.CursorLeft = 0;
            Console.Write($"Progresso: [{new string('|', (int)(progress * 50))}] {progress * 100}%");
            
            //processo
            sw.WriteLine("====================;");
            string registro = $"{subRede};{primeiroIp};{ultimoIp};{broadCast}";
            sw.WriteLine(registro);
        */
        {
            Console.Write("Por favor, insira o nome do arquivo de Resposta para salvar:");
            string nomeDoArquivo = Console.ReadLine().ToLower();

            // Obtém diretorio da pasta
            string diretorioDoProjeto = AppDomain.CurrentDomain.BaseDirectory;
            int indice = diretorioDoProjeto.IndexOf("bin\\Debug\\net7.0\\");
            string pastaRespostas = Path.Combine(diretorioDoProjeto.Substring(0, indice), "respostas");
            string caminhoDoArquivo = "";

            nomeDoArquivo += $"{DateTime.Now:ddMMyy}_{DateTime.Now:mms}";
            if (!string.IsNullOrWhiteSpace(nomeDoArquivo))
            {
                // Certifica-se de que a extensão .txt esteja presente no nome do arquivo
                if (!nomeDoArquivo.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    nomeDoArquivo += ".txt"; // Adiciona a extensão .txt se não estiver presente
                }
                try
                {
                    // Caminho completo para o arquivo
                    caminhoDoArquivo = Path.Combine(pastaRespostas, nomeDoArquivo);
                    // Escreve o texto no arquivo
                    File.WriteAllText(caminhoDoArquivo, textoParaSalvar);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro ao salvar a resposta: " + ex.Message);
                }

                Process.Start("explorer.exe", pastaRespostas);
                Process.Start("explorer.exe", caminhoDoArquivo);
            }
            else
            {
                Console.WriteLine("Nome de arquivo inválido. Por favor, insira um nome de arquivo válido.");
            }
        }
    }
}