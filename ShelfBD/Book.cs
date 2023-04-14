using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace ShelfBD
{
    [BsonIgnoreExtraElements]
    internal class Book
    {
        
        [BsonElement("Titulo")]
        public string Title { get; set; }
        [BsonElement("Autor")]
        public string[] Author { get; set; }
        [BsonElement("ISBN")]
        public int Isbn { get; set; }
        [BsonElement("Editora")]
        public string Editora { get; set; }
        [BsonElement("Ano")]
        public int Year { get; set; }

        public Book(int i, string t, string e, int y)
        {
            Title = t;
            Isbn = i;
            Editora = e;
            Year = y;
        }

        public override string ToString()
        {
            return $"Title: {Title} |Author: {Author} |Edition: {Editora}| Isbn: {Isbn}";
        }
    }
}
