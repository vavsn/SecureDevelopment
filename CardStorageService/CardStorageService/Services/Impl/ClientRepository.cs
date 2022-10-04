using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models;

namespace CardStorageService.Services.Impl
{
    public class ClientRepository : IClientRepositoryService
    {
        #region Services

        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        #endregion

        #region Constructors

        public ClientRepository(
            ILogger<ClientRepository> logger,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion

        #region METHODS
        public int Create(Client data)
        {
            _context.Clients.Add(data);
            _context.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            //throw new NotImplementedException();

            if (id == null)
                throw new ArgumentNullException("id");

            var client = _context.Clients.Find(id);
            if (client == null)
                return -1;


            //удаляем объект
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return id;

        }

        public IList<Client> GetAll()
        {
            // throw new NotImplementedException();
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            //throw new NotImplementedException();
            if (id == null)
                throw new ArgumentNullException("id");

            return _context.Clients.Find(id);
        }

        public int Update(Client data)
        {
            //throw new NotImplementedException();
            if (data == null)
                throw new ArgumentNullException("data");

            var client = _context.Clients.Find(data.ClientId);
            if (client == null)
                return -1;


            //редактируем объект
            client.FirstName = data.FirstName;
            client.Surname = data.Surname;
            client.Patronymic = data.Patronymic;
            _context.SaveChanges();
            return client.ClientId;
        }

        #endregion
    }
}
