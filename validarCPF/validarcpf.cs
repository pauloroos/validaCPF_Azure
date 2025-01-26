using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace validarCPF;

public static class validarcpf
{
    [FunctionName("validarcpf")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Iniciando a validação do CPF.");


        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        if (data == null)
        {
            return new OkObjectResult("Por favor, informe um CPF!");
        }
        string cpf = data?.cpf;

        if (!ValidarCPF(cpf))
        {
            return new OkObjectResult("CPF INVÁLIDO!!!");
        }

        var responseMessage = $"O {cpf} é válido e pode ser utilizado!";

        return new OkObjectResult(responseMessage);
    }


    public static bool ValidarCPF(string CPF)
    {
        if (string.IsNullOrEmpty(CPF))
            return false;

        CPF = CPF.Replace(".", "").Replace("-", "");

        if (CPF.Length != 11 || !long.TryParse(CPF, out _))
            return false;

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = CPF.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return CPF.EndsWith(digito);

    }
}