using System;
using System.Xml.Serialization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Informe o valor: ");
        int valor;
        valor = int.Parse(Console.ReadLine());

        for(int i=0; i<=10; i++)//valor de i será numeros seguidos de 0 até 10
        {
            int res;
            res = valor * i;//irá pegar o valor digitado pelo usuário e multiplicar por i até (de 0 a 10)
            Console.WriteLine(valor+"X"+i+"="+res);// ira imprimir o resultado da operação anterior (concatenado com strings)
        }
    }
}