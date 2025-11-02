using Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IGrupoRepository : IData<Grupo>
    {
        public Task<List<DTOGrupo>> GetAllByFinca(int IdFinca);
        public Task<DTOGrupo> GetGrupo(int id);
        public Task<bool> UpdateGrupo(DTOGrupo grupo);
        public Task<bool> DeleteGrupo(int id);
    }
}