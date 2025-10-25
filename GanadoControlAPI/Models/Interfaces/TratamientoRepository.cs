using Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
	public interface ITratamientoRepository : IData<Tratamiento>
	{
		Task<List<DTOTratamiento>> ObtenerTratamientosPorUsuario(int idUsuario);
		Task<List<DTOTratamiento>> ObtenerTratamientoPorGanado(string idGanado);
		Task<List<DTOTratamiento>> ObtenerTratamientoPorGrupo(int idGrupo);
		Task<List<DTOTratamiento>> ObtenerTratamientoPorFinca(int idFinca);
		Task<bool> EliminarTratamiento(int id);
	}
}
