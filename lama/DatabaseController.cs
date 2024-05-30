using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data.SqlClient;

namespace Fingerprint_Detection
{
    public class DatabaseController
    {
        private string connectionString = "Server=localhost;Database=fingerprint;User Id=root;Password=shulhasql;";

        public List<Biodata> GetAllBiodata()
        {
            var biodataList = new List<Biodata>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM biodata", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var biodata = new Biodata
                        {
                            NIK = reader["NIK"].ToString(),
                            Nama = reader["nama"].ToString(),
                            TempatLahir = reader["tempat_lahir"].ToString(),
                            TanggalLahir = DateTime.Parse(reader["tanggal_lahir"].ToString()),
                            JenisKelamin = reader["jenis_kelamin"].ToString(),
                            GolonganDarah = reader["golongan_darah"].ToString(),
                            Alamat = reader["alamat"].ToString(),
                            Agama = reader["agama"].ToString(),
                            StatusPerkawinan = reader["status_perkawinan"].ToString(),
                            Pekerjaan = reader["pekerjaan"].ToString(),
                            Kewarganegaraan = reader["kewarganegaraan"].ToString()
                        };
                        biodataList.Add(biodata);
                    }
                }
            }
            return biodataList;
        }
    }
}

