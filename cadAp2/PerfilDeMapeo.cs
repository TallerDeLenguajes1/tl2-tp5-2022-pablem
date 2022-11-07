using AutoMapper;
using ViewModels;
using cadAp2.Models;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<AltaCadeteViewModel, Cadete>();
        CreateMap<BorrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<MostrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<ModificarCadeteViewModel, Cadete>().ReverseMap();

        CreateMap<AltaPedidoViewModel, Pedido>();
        CreateMap<BorrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<MostrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<ModificarPedidoViewModel, Pedido>().ReverseMap();

        CreateMap<AsignarPedidoViewModel, Pedido>().ReverseMap();
            // .ForMember(dest => dest.DetalleCorto, opt => opt.MapFrom(src => src.DetalleCorto()));

    }
}