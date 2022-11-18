using Models;
// using ViewModels;

namespace Repositorios
{
    public interface IRepositorioCliente
    {
        int? ProxId();
        Cliente? GetById(int? id);
        List<Cliente>? GetAll();
        void Save(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
    }
}