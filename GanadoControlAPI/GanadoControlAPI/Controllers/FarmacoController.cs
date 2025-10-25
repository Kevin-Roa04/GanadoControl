using Data;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Entities;
using Models.Interfaces;

namespace GanadoControlAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FarmacoController : ControllerBase
	{
		private readonly IFarmacoRepository farmacoRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public FarmacoController(IFarmacoRepository farmacoRepository, IWebHostEnvironment webHostEnvironment)
		{
			this.farmacoRepository = farmacoRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet("finca")]
		public async Task<IActionResult> GetFarmacos(int idFinca)
		{
			try
			{
				return Ok(await farmacoRepository.ObtenerFarmacosPorFinca(idFinca));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] DTOInsertarFarmaco farmacoDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (farmacoDTO is null)
			{
				return BadRequest("El objeto Farmaco es nulo");
			}
			try
			{
				Farmaco farmaco = new Farmaco()
				{
					Id = farmacoDTO.Id,
					FechaCaducidad = farmacoDTO.FechaCaducidad,
					FechaEntrega = farmacoDTO.FechaEntrega,
					IdFinca = farmacoDTO.IdFinca,
					Cantidad = farmacoDTO.Cantidad,
					Precio = farmacoDTO.Precio,
					Proveedor = farmacoDTO.Proveedor,
					Nombre = farmacoDTO.Nombre,
					Tipo = farmacoDTO.Tipo,
					UnidadMedida = farmacoDTO.UnidadMedida,
				};
				if (farmacoDTO.Foto != null)
				{
					farmaco.FotoURL = await ImageUtility.CrearImagen(farmacoDTO.Foto, "FotosDeFarmacos", _webHostEnvironment.WebRootPath, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());
				}
				else
				{
					farmaco.FotoURL = await ImageUtility.InsertImagen("FotosDeFarmacos", _webHostEnvironment.WebRootPath, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString(), "FARMACO.png");
				}
				return Ok(await farmacoRepository.Insertar(farmaco));
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error al insertar fármaco: {ex.Message}");
			}
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Put([FromRoute] int id ,[FromForm] DTOInsertarFarmaco farmacoDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(farmacoDTO);
			}
			if (farmacoDTO is null)
			{
				return BadRequest("El objeto Farmaco es nulo");
			}
			try
			{
				Farmaco farmaco = new Farmaco()
				{
					Id = id,
					FechaCaducidad = farmacoDTO.FechaCaducidad,
					FechaEntrega = farmacoDTO.FechaEntrega,
					IdFinca = farmacoDTO.IdFinca,
					Cantidad = farmacoDTO.Cantidad,
					Precio = farmacoDTO.Precio,
					Proveedor = farmacoDTO.Proveedor,
					Nombre = farmacoDTO.Nombre,
					Tipo = farmacoDTO.Tipo,
					UnidadMedida = farmacoDTO.UnidadMedida,
				};
				if (farmacoDTO.Foto != null)
				{
					farmaco.FotoURL = await ImageUtility.CrearImagen(farmacoDTO.Foto, "FotosDeFarmacos", _webHostEnvironment.WebRootPath, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());
				}
				else
				{
					farmaco.FotoURL = await ImageUtility.InsertImagen("FotosDeFarmacos", _webHostEnvironment.WebRootPath, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString(), "FARMACO.png");
				}
				farmacoDTO.Id = id;
				return Ok(await farmacoRepository.ActualizarFarmaco(farmaco));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			try
			{
				return Ok(await farmacoRepository.EliminarFarmaco(id));

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
