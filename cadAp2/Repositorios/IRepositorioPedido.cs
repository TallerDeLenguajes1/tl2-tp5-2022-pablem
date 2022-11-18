using Models;
using ViewModels;

namespace Repositorios
{
    public interface IRepositorioPedido
    {
        int? ProxId();
        Pedido? GetById(int? id);
        List<MostrarPedidoViewModel>? GetAll();
        ModificarPedidoViewModel? GetPedidoYCliente(int? id);
        void Save(AltaPedidoViewModel pedido);
        void Update(Pedido pedido);
        void Delete(int id);
        List<MostrarPedidoViewModel>? PedidosPorCadete(int id);
        List<MostrarPedidoViewModel>? PedidosPorCliente(int id);
        int ObtenerCadeteId(int idPedido);
        void AsignarCadete(AsignarCadeteViewModel asignar);
    }
}