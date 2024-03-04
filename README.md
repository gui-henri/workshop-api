# Workshop API

Uma api para lhe ajudar a desenvolver aplicações de análise de participação em Workshops!

## Requisitos para executar

1) MySQL 8.0.36
2) .NET 8

## Instruções para executar

1) Clone este repositório e instale todas as dependências
2) Abra o MySQL Workbench e crie uma nova conexão. Lembre-se o nome de usuário e a senha.
3) Abra o arquivo ```appsettings.json```, você irá encontrar o seguinte trecho:
```
"ConnectionStrings": {
    "WorkshopContext": "server=<seu-servidor>;database=<nome-do-banco>;user=<seu-usuario>;password=<sua-senha>;"
}
```
4) Troque cada informação entre <> pelas informações de conexão que você configurou no MySQL Workbench.
5) Rode as migrações. Para fazer isso no Visual Studio, primeiro abra o console do gerenciador de pacotes e digite:
```
update-database
```
6) Rode o projeto. O Swagger deve abrir em ```https://localhost:7139/swagger```
