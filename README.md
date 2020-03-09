# Desafio Noverde - Engenheiro de Software Senior

## Instruções para teste

### 1. Certifique-se de ter instalado o Visual Studio 2019
Para executar a aplicação é necessário ter o Visual Studio 2019 instalado no computador com o pacote **ASP.NET e desenvolvimento WEB** e também **Desenvolvimento multiplataforma com NET.Core**

### 2. API
Para testar a **API** será marcar o projeto **Platform.API** como **Projeto Inicializado**. Para isso:
* Com o cursor posicionado em cima do nome do projeto, clique com o botão direito e procure a opção "Selecionar como Projeto de Inicialização" / "Set as Startup Project"
Após esse passo basta pressionar **F5** para inicializar o projeto

#### 2.1 Requisições
* **`POST`** `http://localhost:59703/loan`

**Request**

```json
 {
    "Name": "Roberto",
    "CPF": "12345678910",
    "BirthDate": "1990-11-22",
    "Amount": 5000,
    "Terms": 9,
    "Income":20000
 }
```

**Response**

```json
 {
    "id: "63903efa-0214-4e8a-a30a-b8c36034e988"
 }
```

* **`GET`** `http://localhost:59703/loan?Id=`

**Response**

```json
 {
    "Id": "63903efa-0214-4e8a-a30a-b8c36034e988",
    "Status": "Completed",
    "Result": "Refused",
    "Refused_Policity": "Commitment",
    "Amout": null,
    "Terms": null
}
```

### 3. Worker
Para testar o WindowService Worker é necessário coloca-lo como projeto de inicialização

* Ao inicializar o Worker, ele irá realizar a analise com base nas políicas de crédito pré estabelecidas.

**Após a execução do Worker, será possível realizar o Get na API par obter o resultado da anáise.**
