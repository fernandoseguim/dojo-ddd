# Dojo DDD

## ***Convenções***

### **Handlers**

Se você é fã de Domain Driven Design, certamente o conhece como Domain Service. Handlers, manipulam um entidade, aplicando as regras de negócio que determinam a validade do estado da entidade.

### **Specifications**

Combinam uma ou mais regras de negócio, que determinam a validade do estado de entidade, podendo ser utilizada também para simplificar consultas nos repositórios.

### **Rules**

Semelhante às Specifications, podem combinar uma ou mais regras de negócio que determinam a validade do estado da entidade e/ou caso de uso. Todavia, se diferenciam das Specifications pois permitem extender as regras de domínio para contextos externos, exemplo dependendo de um Provider para certificar que o saldo atual do cliente é compatível para realizar a operação.

### **Result**

Inspirado em programação funcional, em que todo input gera um output, o Result Pattern retorna um objeto indicando o sucesso ou falha de uma operação, evitando o lançar exceções desnecessárias.

### **Providers**

Talvez você conheça como "Client". Provider implementam um dependência forte de um serviço externo, se comunicando via algum protocolo como HTTP, gRPC, AMQP, Socket, etc. Chamá-lo de Provider ao invés de Client tem como objetivo reforçar a linguagem úbiqua e a relação cliente/fornecedor que o (micros)serviço tem com outros (micros)serviços.

### **UseCases**

Se você é fã de Domain Driven Design, certamente o conhece como Application Service. Use Cases como o nome sugere, implementam um fluxo de negócio, simples ou complexo, sendo agnóstico a design patterns. Alguns caso, você pode optar por um simples service, em casos mais complexos, você pode optar por Sagas (Orquestradas), Composition ou Decorators. Use cases não determinam como serão implementados os casos de usos, mas determinam o que faz cada fluxo de negócio.

---

## ***Dependências***

### **Serviços**

- RabbitMQ
- Redis
- RavenDB
- ELK
- ElasticAPM

### **Principais bibliotecas**

- [FluentResults](https://github.com/altmann/FluentResults)
- [NSpecifications](https://github.com/jnicolau/NSpecifications)
- [MassTransit](https://github.com/MassTransit/MassTransit)
- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [Elastic.Apm.NetCoreAll](https://github.com/elastic/apm-agent-dotnet)

---

## Run Project

```powershell
> .\build.ps1
```

heatlh check: http://localhost:23001/hc

hangfire: http://localhost:23001/hangfire

swagger: http://localhost:23001/swagger

OBS: Para usar a versão 2.0 da API lembre-se de incluir o header `api-version` com o valor `2`
