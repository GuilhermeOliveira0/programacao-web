using System;

class Program
{
    static void Main()
    {
        int[] numero = new int[5];//variaveis vetoriais 
        int i;

        for(i=0; i<5; i++)// condição numeros entre 0 a 5 em 1+1
        {
            Console.Write("informe um valor: ");
            numero[i] = int.Parse(Console.ReadLine());//leitura dos numeros inseridos no console
        }
        for(i=0; i<5; i++)
        {
          //  if (numero[i]%2==0)  
            Console.Write(numero[i]+"");//imprime resultado
        }

    }
}