using Models;
using ViewModels;

namespace Repositorios
{
    public interface IRepositorioCadete
    {
        int? ProxId();
        Cadete? GetById(int? id);
        List<MostrarCadeteViewModel>? GetAll();
        void Save(Cadete cadete);
        void Update(Cadete cadete);
        void Delete(int id);
        void AsignarPedido(AsignarPedidoViewModel asignar);
    }
}