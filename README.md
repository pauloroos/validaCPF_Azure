# Azure Functions - Validação de CPF com Azure Functions

Este projeto foi desenvolvido para validar CPFs utilizando Azure Functions. Ele utiliza o emulador Azurite para simular os serviços de armazenamento do Azure Blob, Queue e Table localmente, garantindo uma experiência de desenvolvimento integrada e segura.

---

## **Passos Realizados**

### **1. Configuração do Ambiente Local**

#### **Instalação do Azurite**
Utilizamos o Azurite para simular os serviços de armazenamento do Azure Blob, Queue e Table localmente.

Comando para instalação:
```bash
npm install -g azurite
```
Comando para iniciar o Azurite:
```bash
azurite
```
> Certifique-se de que o Azurite esteja em execução enquanto você desenvolve ou publica a função.

#### **Configuração do Arquivo `local.settings.json`**
O arquivo `local.settings.json` foi configurado para usar o Azurite localmente:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
```
Este arquivo não deve ser enviado ao repositório, pois pode conter informações sensíveis.

---

### **2. Proteção de Informações Sensíveis**

#### **Atualização do `.gitignore`**
Foi atualizado o arquivo `.gitignore` para ignorar arquivos sensíveis, como `local.settings.json`, e diretórios gerados pelo Azurite:

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

#### **Remoção de Arquivos Já Rastreados**
Caso arquivos sensíveis tenham sido adicionados ao Git, foram removidos do rastreamento com o comando:

```bash
git rm --cached local.settings.json
```
E, no caso de diretórios:
```bash
git rm --cached -r __blobstorage__
```

Após isso, foi feito um commit para aplicar as mudanças:
```bash
git commit -m "Removendo arquivos sensíveis do rastreamento"
```

---

### **3. Publicação da Azure Function**

#### **Login no Azure CLI**
Autenticamos no Azure CLI antes de publicar a função:
```bash
az login
```
Selecionamos a assinatura correta (caso houvesse mais de uma):
```bash
az account set --subscription "<Nome ou ID da assinatura>"
```

#### **Publicação da Função**
Com o ambiente configurado e o login realizado, publicamos a Azure Function com o comando:
```bash
func azure functionapp publish <nome_da_function>
```

---

### **4. Estrutura Final do Projeto**

A estrutura final do projeto contém os seguintes arquivos e diretórios principais:
```
AzureFunctionsLearn
├── httpValidateCpf
│   ├── host.json
│   ├── local.settings.json (ignorado pelo Git)
│   ├── validatecpf.cs
│   ├── __blobstorage__ (ignorado pelo Git)
│   └── httpValidateCpf.csproj
└── .gitignore
```

---

## **Boas Práticas Adotadas**

1. **Proteção de informações sensíveis**:
   - `local.settings.json` foi ignorado pelo Git.
   - O diretório `__blobstorage__` também foi configurado para não ser rastreado.

2. **Uso de variáveis de ambiente**:
   - É recomendável utilizar variáveis de ambiente para credenciais e chaves sensíveis em produção.

3. **Publicação segura**:
   - Antes da publicação, validamos a estrutura do projeto e garantimos que nenhum dado sensível fosse enviado ao Azure.

---

### **Referências**
- [Documentação do Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [Configurando o Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

