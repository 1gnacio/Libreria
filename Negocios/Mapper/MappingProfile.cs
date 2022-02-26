using AccesoDatos.Data;
using AutoMapper;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibroDto, Libro>();
            CreateMap<Libro, LibroDto>();
            CreateMap<LibroImagen, LibroImagenDto>().ReverseMap();
            CreateMap<LibroPedidosDetalle, LibroPedidosDetalleDto>().ReverseMap();
        }
    }
}
