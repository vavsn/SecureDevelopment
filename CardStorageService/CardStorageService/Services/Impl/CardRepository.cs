using CardStorageService.Data;
using CardStorageService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        #region Services

        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        #region Constructors

        public CardRepository(
            ILogger<ClientRepository> logger,
            IOptions<DatabaseOptions> databaseOptions,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _databaseOptions = databaseOptions;
            _context = context;
        }

        #endregion

        #region METHODS
        public string Create(Card data)
        {
            var client = _context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            data.Client = client;

            _context.Cards.Add(data);

            _context.SaveChanges();

            return data.CardId.ToString();
        }

        public IList<Card> GetByCardId(string id)
        {
            //List<Card> cards = new List<Card>();
            //using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            //{
            //    sqlConnection.Open();
            //    using (var sqlCommand = new SqlCommand(String.Format("select * from cards where ClientId = {0}", id), sqlConnection))
            //    {
            //        var reader = sqlCommand.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            cards.Add(new Card
            //            {
            //                CardId = new Guid(reader["CardId"].ToString()),
            //                CardNo = reader["CardNo"]?.ToString(),
            //                Name = reader["Name"]?.ToString(),
            //                CVV2 = reader["CVV2"]?.ToString(),
            //                ExpDate = Convert.ToDateTime(reader["ExpDate"])
            //            });
            //        }
            //    }

            //}
            //return cards;

            return _context.Cards.Where(card => card.ClientId == Int32.Parse(id)).ToList();
        }

        public int Delete(string id)
        {
            // throw new NotImplementedException();
            if (id == null)
                throw new ArgumentNullException("id");

            var card = _context.Cards.Find(new Guid(id));
            if (card == null)
                return -1;

            //удаляем объект
            _context.Cards.Remove(card);
            _context.SaveChanges();
            return 0;
        }

        public IList<Card> GetAll()
        {
            // throw new NotImplementedException();
            return _context.Cards.ToList();
        }

        public int Update(Card data)
        {
            //throw new NotImplementedException();
            if (data == null)
                throw new ArgumentNullException("data");

            var card = _context.Cards.Find(data.CardId);
            if (card == null)
                return -1;


            //редактируем объект
            card.CardNo = data.CardNo;
            card.CVV2 = data.CVV2;
            card.Name = data.Name;
            card.ExpDate = data.ExpDate;
            _context.SaveChanges();
            return card.ClientId;
        }

        public Card GetById(string id)
        {
            // throw new NotImplementedException();
            if (id == null)
                throw new ArgumentNullException("id");

            return _context.Cards.Find(new Guid(id));

        }


        #endregion

    }
}
