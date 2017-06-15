# Falando sobre _Simple Injector_ 

_Simple Injector_ pode ser uns dos seus melhores amigos quando o assunto se trata de _Injeção de Dependência_ ( Clique [aqui](https://github.com/Dsouzavl/Metodos-injecao-de-dependencia) para ver meu artigo sobre maneiras de _Injetar Dependências_ ), o fato de ele ser um _Framework_ criado com essa finalidade, faz com que o seu uso seja uma tremenda mão na roda, quando está se lidando com aplicações de larga escala. E nesse artigo vou estar mostrando na prática o básico de como e quando utilizar o **_Simple Injector_**.

Vamos começar criando um novo ConsoleApp no Visual Studio _( Vou assumir que você já sabe como criar um novo projeto )_, e instalando o **_Simple Injector_** através do _NuGet Package Manager_:

![Esse aqui](C:/DEV/Projects/Falando-sobre-Simple-Injector/img/NuGet.PNG)

### _Overview_
A classe principal do _Simple Injector_ é o Container, dentro de um Container eu vou registrar **mapeamentos**, esse **mapeamento** ocorre entre uma abstração e sua implementação concreta, por exemplo: Imagine que eu tenho uma interface **IRepository** 
```csharp 
    public interface IRepository
    {
        bool Create(Guid id);

        bool Read(Guid id);

        bool Update(Guid id);

        bool Delete(Guid id);
    } 
```  
E uma classe SqlRepository que implementa essa interface

```csharp 
    public class SqlRepository : IRepository
    {
        public bool Create(Guid id)
        {
            return true;
        }

        public bool Read(Guid id)
        {
            return true;
        }

        public bool Update(Guid id)
        {
            return true;
        }

        public bool Delete(Guid id)
        {
            return true;
        }
    }
```
Interprete um Container como um dicionário, onde a chave é uma Abstração e o seu valor é a definição de como criar uma implementação concreta específica.

```csharp
    static class Program
    {
        static readonly Container container = new Container();

        static void Main(string[] args)
        {
            container.Register<IRepository,SqlRepository>();
            container.Verify();
        }
    }
``` 
O que estamos fazendo é, criando um novo Container, e dentro dele estamos registrando o **mapeamento** de **IRepository** para **SqlRepository**, o que esse registro faz é: Toda vez que eu solicitar um **IRepository** para utilizar, ele vai retornar uma instância de **SqlRepository**.

### _Pense um pouco_...

Se você leu meu [artigo](https://github.com/Dsouzavl/Metodos-injecao-de-dependencia), viu que fizemos um uso constante de interfaces para resolver questões de injeção de dependência, o que o Container do _Simple Injector_ faz é exatamente isso, só da forma dele, ao invés de termos que criar os construtores ou alterar _getters_ e _setters_ utilizamos os Containers que o _Simple Injector_ oferece.

**Voltando**, o ideal é que toda parte de registro e criação de Containers sejam mantidos em uma classe específica responsável por toda operação relacionada ao _Simple Injector_.

O ciclo do _SimpleInjector_ é:

1) Você cria um novo container.
2) Você registra os mapeamentos. 
3) Opcionalmente verifica o container ( _No processo de verificação ele vai checar a instância do Container,iterando sobre cada registro, se houver algum erro ele lança uma exceção_ )
4) Armazena o container para uso na aplicação.
5) Retorna as instâncias do container quando precisamos dela. Observe abaixo:
 
```csharp
 public class DatabaseOperations
    {
        SqlRepository sqlRepository = new SqlRepository();

        public bool SomeDatabaseOperation()
        {
            return sqlRepository.Create(Guid.NewGuid());
        }
    }
```
A nossa classe, apresenta uma dependência da classe **SqlRepository**, poderíamos injetar a dependência por outros métodos, mas como estamos falando sobre _Simple Injector_, observe como resolveríamos:

 
```csharp
    public class DatabaseOperations
    {
        static readonly Container container = new Container();

        public DatabaseOperations()
        {
            container.Register<IRepository, SqlRepository>(Lifestyle.Singleton);
            container.Verify();
        }       

        public bool SomeDatabaseOperation()
        {
            var sqlRepository = container.GetInstance<IRepository>();
            return sqlRepository.Create(Guid.NewGuid());
        }
    }
```
Agora, criamos um Container, registramos e verificamos dentro do construtor da classe, e quando precisamos instânciar uma dependência, o _Simple Injector_ nos oferece uma maneira de fazer isso, desse modo temos nossa dependência resolvida, de forma rápida e fácil, onde pode ser reaproveitada para os demais serviços que precisarmos. Saiba que é importante entender o processo manual de injeção de dependência, mas após ter um conhecimento, opte por utilizar _Frameworks_ como o _Simple Injector_ que vão lhe poupar tempo, e oferecer _N_ funcionalidades além.

### Overview: _Lifestyles_

Lifestyle é o comportamento que seu registro vai ter durante o tempo de execução, ele pode ser um _Singleton_, onde apenas uma instância do registro vai ser retornada durante todo o tempo de vida do Container, ou pode ser _Transiente_, onde cada vez que o registro for solicitado, será criado uma nova instância, você também pode criar um Lifestyle único.

## Final

Há muito mais sobre o _Simple Injector_ do que está escrito neste artigo, o que foi oferecido aqui foi um ponto de partida para todos que desejam conhecer sobre a ferramenta, que sua curiosidade se estenda, aproveite e visite o [link](http://simpleinjector.readthedocs.io/en/latest/using.html) da documentação oficial do _Simple Injector_ e teste em suas próprias aplicações.

Obrigados a todos os leitores desse artigo. Até a próxima!




