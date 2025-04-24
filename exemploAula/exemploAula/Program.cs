using System;//import de bibliotecas do sistema.
class Program//classe principal do sistema
{
    static void Main() //classe principal
    {
        int idade;//variavel inteira para armazenar idade
        Console.WriteLine("informe uma idade: ");
        idade = Convert.ToInt32(Console.ReadLine());//converte a entrada para inteiro
        if(idade > 0 && idade <= 12)
        {
            Console.WriteLine("Criança");
        } else if (idade > 12 && idade <18)
        {
            Console.WriteLine("Adolescente");
        }
        else if (idade >= 18 && idade <= 59)
        {
            Console.WriteLine("Adulto");
        } else if (idade >= 60)
        {
            Console.WriteLine("Idoso");
        }

        else// tudo que não atender as outras condições caem no else 
        {
            Console.WriteLine("Idade Inválida !");
        }
      
    }
}
