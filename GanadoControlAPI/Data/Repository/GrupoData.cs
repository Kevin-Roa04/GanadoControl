

using Microsoft.Data.SqlClient;
using Models.DTO;
using Models.Entities;
using Models.Interfaces;
using System.Data;

namespace Data.Repository
{
    public class GrupoData : IGrupoRepository
    {
        private readonly string CadenaConexion;
        public GrupoData(string CadenaConexion)
        {
            this.CadenaConexion = CadenaConexion;
        }
        public async Task<int> Insertar(Grupo grupo)
        {
            int ultimoId = 0;
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("uspInsertarGrupo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 50)).Value = grupo.Nombre;
                cmd.Parameters.Add(new SqlParameter("@IdFinca", SqlDbType.Int)).Value = grupo.IdFinca;

                try
                {
                    await conexion.OpenAsync();
                    ultimoId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return ultimoId;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<List<DTOGrupo>> GetAllByFinca(int IdFinca)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllGrupos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdFinca", SqlDbType.Int)).Value = IdFinca;
                try
                {
                    await conexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    List<DTOGrupo> grupos = new List<DTOGrupo>();
                    while (dr.Read())
                    {

                        DTOGrupo grupo = new DTOGrupo()
                        {
                            IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                            IdFinca = Convert.ToInt32(dr["IdFinca"]),
                            Nombre = dr["Nombre"].ToString(),
                            CantidadGanado = Convert.ToInt32(dr["CantidadDeGanados"]),
                            FotoURL = dr["FotoURL"].ToString()
                        };

                        grupos.Add(grupo);
                    }

                    return grupos;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<DTOGrupo> GetGrupo(int id)
        {
            DTOGrupo grupo = new DTOGrupo();
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("uspGetGrupo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                try
                {
                    await conexion.OpenAsync();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            grupo = new DTOGrupo()
                            {
                                IdGrupo = Convert.ToInt32(dr["IdGrupo"]),
                                IdFinca = Convert.ToInt32(dr["IdFinca"]),
                                Nombre = dr["Nombre"].ToString(),
                                CantidadGanado = Convert.ToInt32(dr["CantidadDeGanados"]),
                                FotoURL = dr["FotoURL"].ToString(),
                            };
                        }
                    }
                    return grupo;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<bool> UpdateGrupo(DTOGrupo grupo)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("uspUpdateGrupo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 50)).Value = grupo.Nombre;
                cmd.Parameters.Add(new SqlParameter("@IdFinca", SqlDbType.Int)).Value = grupo.IdFinca;
                cmd.Parameters.Add(new SqlParameter("@IdGrupo", SqlDbType.Int)).Value = grupo.IdGrupo;
                cmd.Parameters.Add(new SqlParameter("@FotoURL", SqlDbType.VarChar, 100)).Value = grupo.FotoURL;
                try
                {
                    await conexion.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<bool> DeleteGrupo(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("uspEliminarGrupo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                try
                {
                    await conexion.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}