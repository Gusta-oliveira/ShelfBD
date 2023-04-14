using System.ComponentModel.Design;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ShelfBD;

internal class Program
{
    private static void Main(string[] args)
    {
        MongoClient mongo = new MongoClient("mongodb://localhost:27017");
        var bas = mongo.GetDatabase("shelf");
        var collection = bas.GetCollection<BsonDocument>("livro");

        do
        {
            Console.Clear();
            switch (Menu())
            {
                
                case 1:
                    Console.Clear();
                    Console.WriteLine("Adicionar Livro na Estante");
                    var b = CreateBook();
                    var bd = new BsonDocument
                    {
                        {"ISBN", b.Isbn },
                        {"Titulo", b.Title },
                        {"Editora", b.Editora },
                        {"Ano", b.Year }
                    };
                    collection.InsertOne(bd);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Informe o Titulo do livro que seseja: ");
                    string n = Console.ReadLine();
                    var bookexibition = SearchBook(n, collection);
                    Console.WriteLine(bookexibition.ToString());
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    break;
                case 6:
                    Console.Clear();
                    break;
            }
        } while (true);








        Console.WriteLine("Informe nome do livro");
        string t = Console.ReadLine();
        var filtro = Builders<BsonDocument>.Filter.Regex("Titulo", t);

        var p = collection.Find(filtro).FirstOrDefault();

        var book = BsonSerializer.Deserialize<Book>(p);
        Console.WriteLine(book.ToString());
        //foreach (var item in document)
        //{
        //    Console.WriteLine(item.ToString()); ;
        //}
    }

    private static int Menu()
    {
        Console.WriteLine("-------Estante de Livros-------");
        Console.Write("1 - Adicionar Livro;\n2 - Prucurar Livro;\n3 - Atualizar INFO;\n4 - Deletar Livro;\n5 - Exibir Livros;\n6 - FINALIZAR!!\n");
        Console.Write("-------------------------------\nInforme a operação: ");
        var op = int.Parse(Console.ReadLine());
        Console.Clear();
        return op;
    }
    private static Book CreateBook()
    {
        Console.Write("ISBN: ");
        int i = int.Parse(Console.ReadLine());
        Console.Write("Titulo: ");
        string t = Console.ReadLine();
        Console.Write("Editora: ");
        string e = Console.ReadLine();
        Console.Write("Ano: ");
        int y = int.Parse(Console.ReadLine());

        return new Book(i, t, e, y);
    }
    private static Book SearchBook(string n, IMongoCollection<BsonDocument> collection)
    {
        var filter = Builders<BsonDocument>.Filter.Regex("Titulo", n);
        var bookd = collection.Find(filter).FirstOrDefault();
        var book = BsonSerializer.Deserialize<Book>(bookd);
        return book;
    }
}