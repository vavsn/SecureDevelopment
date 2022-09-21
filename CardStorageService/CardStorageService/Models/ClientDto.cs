using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CardStorageService.Models
{
    public class ClientDto
    {
        //TODO: Доработать самостоятельно
        public int ClientId { get; set; }

        public string? Surname { get; set; }

        public string? FirstName { get; set; }

        public string? Patronymic { get; set; }
    }
}
