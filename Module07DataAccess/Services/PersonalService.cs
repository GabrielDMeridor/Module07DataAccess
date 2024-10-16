using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Module07DataAccess.Model;
using MySql.Data.MySqlClient;

namespace Module07DataAccess.Services
{
    public class PersonalService
    {
        private readonly string _connectionString;

        public PersonalService()
        {
            var dbService = new DatabaseConnectionService();
            _connectionString = dbService.GetConnectionString();
        }

        public async Task<List<Personal>> GetAllPersonalsAsync()
        {
            var personalServices = new List<Personal>(); // Changed to personalList
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                //Retrieve Data
                var cmd = new MySqlCommand("SELECT * FROM tblPersonal", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        personalServices.Add(new Personal
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Gender = reader.GetString("Gender"),
                            ContactNo = reader.GetString("ContactNo") // Changed to double quotes
                        });
                    }
                }
            }
            return personalServices;
        }


    }
}
