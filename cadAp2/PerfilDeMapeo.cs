using AutoMapper;
using ViewModels;
using Models;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<AltaCadeteViewModel, Cadete>(); //unificar alta y modificar 
        CreateMap<BorrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<MostrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<ModificarCadeteViewModel, Cadete>().ReverseMap();

        CreateMap<AltaPedidoViewModel, Pedido>(); //unificar alta y modificar
            // .ForMember(dest => dest.Cliente.Nombre, opt => opt.MapFrom(src => src.Nombre))
            // .ForMember(dest => dest.Cliente.Direccion, opt => opt.MapFrom(src => src.Direccion))
            // .ForMember(dest => dest.Cliente.ReferenciaDireccion, opt => opt.MapFrom(src => src.ReferenciaDireccion))
            // .ForMember(dest => dest.Cliente.Telefono, opt => opt.MapFrom(src => src.Telefono));
        CreateMap<BorrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<MostrarPedidoViewModel, Pedido>().ReverseMap()
            .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Cliente.Direccion));
        CreateMap<ModificarPedidoViewModel, Pedido>().ReverseMap();

        CreateMap<AsignarPedidoViewModel, Pedido>().ReverseMap();

    }
}