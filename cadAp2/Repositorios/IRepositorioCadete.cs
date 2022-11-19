using Models;
using ViewModels;

namespace Repositorios
{
    public interface IRepositorioCadete
    {
        int? GetLastId();
        Cadete? GetById(int? id);
        List<Cadete>? GetAll();
        void Save(Cadete cadete);
        void Update(Cadete cadete);
        void Delete(int id);
        void AsignarPedido(AsignarPedidoViewModel asignar);
    }
}