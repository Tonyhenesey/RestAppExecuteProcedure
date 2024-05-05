using System;
using System.Data;
using System.Data.SqlClient;
using apbdcwiczenia7.Properties.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public WarehouseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // endpoint 
        [HttpPost("addProductProc")]
        public IActionResult AddProductUsingProcedure([FromBody] Warehouse addProduct)
        {
            if (addProduct.IdProduct <= 0 || addProduct.IdWarehouse <= 0 || addProduct.Amount <= 0)
            {
                return BadRequest("Wszystkie pola są wymagane.");
            }

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddProductToWarehousea", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@IdProduct", addProduct.IdProduct);
                    command.Parameters.AddWithValue("@IdWarehouse", addProduct.IdWarehouse);
                    command.Parameters.AddWithValue("@Amount", addProduct.Amount);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return BadRequest("Nie udało się dodać produktu do magazynu.");
                    }

                    return Ok(new { NewId = result });
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, $"Wystąpił błąd w bazie danych: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
                }
            }
        }



    }
}
