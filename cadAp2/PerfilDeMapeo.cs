using AutoMapper;
using ViewModels;
using cadAp2.Models;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<AltaCadeteViewModel, Cadete>();
        // CreateMap<Cadete, AltaCadeteViewModel>().ReverseMap();
    }
}