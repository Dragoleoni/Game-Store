using GameStoreLab.Domain.Models;
using System.Text;

namespace GameStoreLab.Infrastructure.Helpers
{
    public static class HtmlGenerationHelper
    {
        public static string GenerateFormPage(int formNumber, SearchFormModel model) => GenerateFormPage(formNumber, model.Text, model.Option);

        public static string GenerateFormPage(int formNumber, string input = null, string select = null)
        {

            var selectOptions = new Dictionary<string, string>
            {
                { "id", "" },
                { "title", "" },
                { "description", "" }
            };

            if (!string.IsNullOrEmpty(select) && selectOptions.ContainsKey(select))
            {
                selectOptions[select] = "selected";
            }

            return
                $$""""
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=utf-8>
                    <title>Search Form</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f5f5f5;
                            text-align: center;
                        }

                        h1 {
                            font-size: 24px;
                            color: #333;
                        }

                        #searchForm {
                            background-color: #fff;
                            border-radius: 8px;
                            padding: 20px;
                            margin: 20px auto;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
                            width: 300px;
                        }

                        label {
                            display: block;
                            margin-top: 10px;
                            font-weight: bold;
                        }

                        input[type="text"], select {
                            width: 90%;
                            padding: 10px;
                            border: 1px solid #ccc;
                            border-radius: 4px;
                            margin-top: 5px;
                        }

                        button[type="submit"] {
                            background-color: #333;
                            color: #fff;
                            padding: 10px 20px;
                            border: none;
                            border-radius: 4px;
                            cursor: pointer;
                            margin-top: 10px;
                            transition: background-color 0.3s;
                        }

                        button[type="submit"]:hover {
                            background-color: #555;
                        }

                        button[type="submit"]:active {
                            background-color: #222;
                        }
                    </style>
                </head>
                <body>
                    <h1>Поиск</h1>
                    <form id="searchForm" method="post" action="/search{{formNumber}}">
                        <label for="searchText">Искомые игры:</label>
                        <input value="{{input ?? string.Empty}}" type="text" id="searchText" name="searchText" placeholder="Введите текст для поиска" required>

                        <label for="searchType">Колонки:</label>
                        <select id="searchType" name="searchType">
                            <option value="id" {{selectOptions["id"]}}>Идентификатор</option>
                            <option value="title" {{selectOptions["title"]}}>Наименование</option>
                            <option value="description" {{selectOptions["description"]}}>Описание</option>
                        </select>

                        <button type="submit">Поиск</button>
                    </form>
                </body>
                </html>
                """";
        }

        public static string GenerateAboutPage()
        {
            return
                """
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=utf-8>
                    <title>About Me</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f5f5f5;
                            text-align: center;
                        }

                        header {
                            background-color: #333;
                            color: #fff;
                            padding: 20px;
                        }

                        h1 {
                            font-size: 36px;
                        }

                        p {
                            font-size: 18px;
                            line-height: 1.5;
                            margin: 20px;
                        }

                        .container {
                            background-color: #fff;
                            border-radius: 8px;
                            padding: 20px;
                            margin: 20px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
                        }
                    </style>
                </head>
                <body>
                    <header>
                        <h1>Обо мне</h1>
                    </header>
                    <div class="container">
                        <h2>Любовь Жиренкова ИТИ-31</h2>
                        <p>
                            Данная лабораторная работа выполнена с использованием ASP.NET Core и Entity Framework Core.
                            Лабораторная написана в полном соответствии с поставленной задачей.
                        </p>
                        <p>
                            Посмотреть репозиторий данной лабораторной можно ниже:
                        </p>
                        <a href="https://github.com/Dragoleoni" target="_blank">GitHub</a>
                    </div>
                </body>
                </html>
                """;
        }

        public static string GenerateTable(List<string> headers, string data)
        {
            return
                $$"""
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=utf-8>
                        <title>Game store</title>
                        <style>
                            table {
                                border-collapse: collapse;
                                width: 50%;
                                margin: 0 auto;
                                font-family: Arial;
                            }
                            th {
                                background-color: #333;
                                color: #fff;
                                border: 1px solid #fff;
                                padding: 10px;
                            }
                            td {
                                border: 1px solid #ddd;
                                padding: 10px;
                            }
                            tr:nth-child(odd) {
                                background-color: #f2f2f2;
                            }
                            tr:nth-child(even) {
                                background-color: #fff;
                            }
                        </style>
                    </head>
                    <body>
                        <table>
                            <thead>
                                <tr>
                                {{ string.Join("", headers.Select(h => $"<th>{h}</th>")) }}
                                </tr>
                            </thead>
                            <tbody>
                                {{ data }}
                            </tbody>
                        </table>
                    </body>
                    </html>
                    """;
        }

        public static string GenerateGamesData(List<Game> games)
        {
            var result = new StringBuilder();
            foreach (var game in games)
            {
                result.Append(
                    $$"""
                    <tr>
                        <td>{{game.GameId}}</td>
                        <td>{{game.Title}}</td>
                        <td>{{game.Description}}</td>
                        <td>{{game.DeveloperId}}</td>
                        <td>{{game.ReleaseYear}}</td>
                        <td>{{game.Price}}</td>
                    </tr>
                    """);
            }
            return result.ToString();
        }

        public static string GenerateUsersData(List<User> users)
        {
            var result = new StringBuilder();
            foreach (var user in users)
            {
                result.Append(
                    $$"""
                    <tr>
                        <td>{{user.UserId}}</td>
                        <td>{{user.Login}}</td>
                        <td>{{user.Password}}</td>
                        <td>{{user.Email}}</td>
                        <td>{{user.FirstName}}</td>
                        <td>{{user.LastName}}</td>
                        <td>{{user.Age}}</td>
                    </tr>
                    """);
            }
            return result.ToString();
        }

        public static string GenerateTransactionsData(List<Transaction> transactions)
        {
            var result = new StringBuilder();
            foreach (var transaction in transactions)
            {
                result.Append(
                    $$"""
                    <tr>
                        <td>{{transaction.TransactionId}}</td>
                        <td>{{transaction.GameId}}</td>
                        <td>{{transaction.BuyerId}}</td>
                        <td>{{transaction.TransactionDate}}</td>
                        <td>{{transaction.Price}}</td>
                    </tr>
                    """);
            }
            return result.ToString();
        }
    }
}
