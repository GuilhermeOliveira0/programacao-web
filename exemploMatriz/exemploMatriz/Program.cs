using System;

class Program
{
    static void Main()
    {
        int[,] matriz = new int[3,3];//variavel de definição da matriz

        for (int l = 0; l < 3; l++)//variavel l para determinar linhas
        {
            for (int c = 0; c < 3; c++)//variavel C para determinar colunas
            {
                Console.WriteLine($"Digite um valor para a posição [{l},{c}]");
                matriz[l, c] = int.Parse(Console.ReadLine());//leitura de matrizes inseridas no console
            }
        }
        for (int l = 0; l < 3; l++)
        {
            for (int c = 0; c < 3; c++)
            {

                Console.WriteLine("O valor do indice " + l + "," + c + "é" + matriz[l, c]);
            }
        }

    }
}
