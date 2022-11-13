using AutoMapper;
using ViewModels;
using Models;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        /*Cadetes*/
        CreateMap<AltaCadeteViewModel, Cadete>(); //unificar alta y modificar?
        CreateMap<BorrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<MostrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<ModificarCadeteViewModel, Cadete>().ReverseMap();

        /*Pedidos*/
        CreateMap<AltaPedidoViewModel, Pedido>(); //unificar alta y modificar?
        CreateMap<BorrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<MostrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<ModificarPedidoViewModel, Pedido>().ReverseMap()
            .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id));///NO se Mapea

        CreateMap<AsignarPedidoViewModel, Pedido>().ReverseMap();

        /*Clientes*/
        CreateMap<ModificarPedidoViewModel, Cliente>().ReverseMap()
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id));///NO se Mapea
        CreateMap<AltaClienteViewModel, Cliente>(); 
        CreateMap<BorrarClienteViewModel, Cliente>().ReverseMap();
        CreateMap<MostrarClienteViewModel, Cliente>().ReverseMap();
        CreateMap<ModificarClienteViewModel, Cliente>().ReverseMap();



    }
}