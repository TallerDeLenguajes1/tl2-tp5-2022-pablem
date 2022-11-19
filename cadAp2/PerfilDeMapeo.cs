using AutoMapper;
using ViewModels;
using Models;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        /*Cadetes*/
        CreateMap<AltaCadeteViewModel, Cadete>();
        CreateMap<BorrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<MostrarCadeteViewModel, Cadete>().ReverseMap();
        CreateMap<ModificarCadeteViewModel, Cadete>().ReverseMap();

        /*Pedidos*/
        CreateMap<AltaPedidoViewModel, Pedido>();
        CreateMap<BorrarPedidoViewModel, Pedido>().ReverseMap();
        CreateMap<MostrarPedidoViewModel, Pedido>().ReverseMap()
            .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Cliente.Direccion));
        CreateMap<ModificarPedidoViewModel, Pedido>().ReverseMap()
            .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.Id))///NO se Mapea
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Cliente.Id))///NO se Mapea
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Cliente.Direccion))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Cliente.Telefono))
            .ForMember(dest => dest.ReferenciaDireccion, opt => opt.MapFrom(src => src.Cliente.ReferenciaDireccion));

        /*Clientes*/
        CreateMap<AltaClienteViewModel, Cliente>(); 
        CreateMap<BorrarClienteViewModel, Cliente>().ReverseMap();
        CreateMap<MostrarClienteViewModel, Cliente>().ReverseMap();
        CreateMap<ModificarClienteViewModel, Cliente>().ReverseMap();

        CreateMap<ModificarPedidoViewModel, Cliente>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdCliente));///NO se Mapea



    }
}