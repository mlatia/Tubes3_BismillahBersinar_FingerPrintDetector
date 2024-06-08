using System;
using Microsoft.Data.Sqlite;

namespace FingerPrintDetector{
    public class Database{
    public static string GetPersonNameByImagePath(string imagePath) {
     
        string connectionString = $"Data Source=fingerprintalay.db";
        // string connectionString = $"Data Source=stima.db";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT nama FROM sidik_jari WHERE berkas_citra = @imagePath";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@imagePath", imagePath);
                var result = command.ExecuteScalar();
                return result?.ToString();
            }
        }
    }

    public static Biodata GetBiodataByName(string name) {

        string connectionString = $"Data Source=fingerprintalay.db";
        // string connectionString = $"Data Source=stima.db";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM biodata WHERE nama = @name";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Biodata
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
                    }
                }
            }
        }
        return null;
    }
    }

    
}