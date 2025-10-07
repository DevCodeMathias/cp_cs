# 📌 Projeto ASP.NET Core + Oracle

Este projeto é um **MVC/API em ASP.NET Core** integrado a um banco de dados **Oracle**.  
Ele utiliza o **Entity Framework Core** para persistência e **Migrations** para versionamento do schema.



## ⚙️ Configuração do Banco de Dados

### 1. Connection String
No arquivo **`appsettings.json`** configure sua conexão:

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/cp"
  }
}
