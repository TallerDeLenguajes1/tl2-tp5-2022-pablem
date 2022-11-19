using Models;
using ViewModels;

namespace Repositorios
{
    public interface IRepositorioPedido
    {
        int? GetLastId();
        Pedido? GetById(int? id);
        List<Pedido>? GetAll();
        void Save(Pedido pedido, int idCliente);
        void Update(Pedido pedido);
        void Delete(int id);

        List<Pedido>? PedidosPorCadete(int id); ////no van
        List<Pedido>? PedidosPorCliente(int id);

        int ObtenerCadeteId(int idPedido);
        
        void AsignarCadeteAPedido(int idCadete, int idPedido);
    }
}