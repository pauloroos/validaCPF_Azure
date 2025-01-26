# Azure Functions - Validação de CPF com Azure Functions

Este repositório apresenta uma aplicação desenvolvida para validar CPFs utilizando Azure Functions. O projeto faz uso do Azurite, que emula localmente os serviços de armazenamento do Azure (Blob, Queue e Table), permitindo um ambiente de desenvolvimento controlado e seguro.

## Índice

- [Introdução](#introdução)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Testando com o Postman](#testando-com-o-postman)
- [Boas Práticas Adotadas](#boas-práticas-adotadas)
- [Referências](#referências)

---

## Introdução

Este projeto tem como objetivo implementar e validar CPFs de forma prática e eficiente utilizando Azure Functions. A configuração local do Azurite garante que você possa simular o ambiente de produção em sua máquina.

---

## Configuração do Ambiente

1. **Instale o Azurite**:
   ```bash
   npm install -g azurite
   azurite
   ```

2. **Configure o arquivo `local.settings.json`**:
   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet"
     }
   }
   ```

   > **Nota**: Não inclua este arquivo no controle de versão para evitar expor informações sensíveis.

3. **Atualize o `.gitignore`**:
   ```gitignore
   # Azure Functions
   local.settings.json

   # Azurite database files
   __blobstorage__/
   _azurite_db_*

   # VS Code settings (opcional)
   .vscode/

   # Build outputs
   bin/
   obj/
   ```

4. **Remova arquivos já rastreados (se necessário)**:
   ```bash
   git rm --cached local.settings.json
   git rm --cached -r __blobstorage__
   git commit -m "Removendo arquivos sensíveis do rastreamento"
   ```

---

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```
AzureFunctionsLearn/
├── validarCPF/
│   ├── host.json
│   ├── local.settings.json (ignorado pelo Git)
│   ├── validarcpf.cs
│   ├── __blobstorage__/ (ignorado pelo Git)
│   └── validarCPF.csproj
└── .gitignore
```

---

## Testando com o Postman

Você pode validar CPFs através da Azure Function utilizando o Postman. Siga os passos abaixo:

1. **Configurar a Requisição**:
   - Método: `POST`
   - URL: `http://localhost:<porta_da_function>/api/validarcpf`
   - Corpo (JSON):
     ```json
     {
       "cpf": "12345678909"
     }
     ```

2. **Adicionar Chave de Acesso (se aplicável)**:
   Caso a função exija autenticação, inclua a chave no parâmetro `code` da URL ou utilize o cabeçalho `x-functions-key`.

3. **Enviar a Requisição**:
   - Clique em "Enviar" no Postman e confira a resposta.

### Exemplos de Respostas

- **CPF Válido**:
  ```json
  {
    "status": "success",
    "message": "O {cpf} é válido e pode ser utilizado!"
  }
  ```

- **CPF Inválido**:
  ```json
  {
    "status": "error",
    "message": "CPF INVÁLIDO!!!"
  }
  ```

---

## Boas Práticas Adotadas

1. **Proteção de Informações Sensíveis**:
   - O arquivo `local.settings.json` e os diretórios do Azurite foram configurados para serem ignorados no Git.

2. **Uso de Variáveis de Ambiente**:
   - Em produção, dados confidenciais devem ser armazenados como variáveis de ambiente.

3. **Validação Pré-Publicação**:
   - A estrutura do projeto foi revisada para garantir que informações sensíveis não sejam publicadas.

---

## Referências

- [Documentação do Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [Postman - Ferramenta para Testes de API](https://www.postman.com/)
- [Configurando o Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)
