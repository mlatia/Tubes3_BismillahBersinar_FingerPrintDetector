import sqlite3

def enkripsi_sqlite_xor(input_db_file, output_db_file):
    input_conn = sqlite3.connect(input_db_file)
    input_c = input_conn.cursor()

    output_conn = sqlite3.connect(output_db_file)
    output_c = output_conn.cursor()

    output_c.execute('''
    CREATE TABLE IF NOT EXISTS biodata (
        NIK VARCHAR(16) PRIMARY KEY NOT NULL, 
        nama VARCHAR(100) DEFAULT NULL, 
        tempat_lahir VARCHAR(50) DEFAULT NULL, 
        tanggal_lahir DATE DEFAULT NULL, 
        jenis_kelamin TEXT, 
        golongan_darah VARCHAR(5) DEFAULT NULL, 
        alamat VARCHAR(255) DEFAULT NULL, 
        agama VARCHAR(50) DEFAULT NULL, 
        status_perkawinan TEXT, 
        pekerjaan VARCHAR(100) DEFAULT NULL, 
        kewarganegaraan VARCHAR(50) DEFAULT NULL
    )
    ''')

    output_c.execute('''
    CREATE TABLE IF NOT EXISTS sidik_jari (
        berkas_citra TEXT,
        nama VARCHAR(100) DEFAULT NULL
    )
    ''')

    # XOR encryption function
    def xor_encrypt_decrypt(text, key):
        encrypted_chars = []
        for i in range(len(text)):
            key_c = key[i % len(key)]
            encrypted_c = chr(ord(text[i]) ^ ord(key_c))
            encrypted_chars.append(encrypted_c)
        return ''.join(encrypted_chars)

    key = 'mysecretkey' 

    input_c.execute('SELECT * FROM biodata')
    biodata_rows = input_c.fetchall()

    # encrypt database
    for row in biodata_rows:
        encrypted_row = [
            xor_encrypt_decrypt(str(row[0]), key) if row[0] is not None else None,  # NIK
            xor_encrypt_decrypt(row[1], key) if row[1] is not None else None,  # nama
            xor_encrypt_decrypt(row[2], key) if row[2] is not None else None,  # tempat_lahir
            row[3],  # tanggal_lahir
            xor_encrypt_decrypt(row[4], key) if row[4] is not None else None,  # jenis_kelamin
            xor_encrypt_decrypt(row[5], key) if row[5] is not None else None,  # golongan_darah
            xor_encrypt_decrypt(row[6], key) if row[6] is not None else None,  # alamat
            xor_encrypt_decrypt(row[7], key) if row[7] is not None else None,  # agama
            xor_encrypt_decrypt(row[8], key) if row[8] is not None else None,  # status_perkawinan
            xor_encrypt_decrypt(row[9], key) if row[9] is not None else None,  # pekerjaan
            xor_encrypt_decrypt(row[10], key) if row[10] is not None else None  # kewarganegaraan
        ]
        output_c.execute('''
        INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ''', encrypted_row)

    input_c.execute('SELECT * FROM sidik_jari')
    sidik_jari_rows = input_c.fetchall()

    for row in sidik_jari_rows:
        row = [
            row[0] if row[0] is not None else None,  # berkas_citra
            row[1] if row[1] is not None else None  # nama
        ]
        output_c.execute('''
        INSERT INTO sidik_jari (berkas_citra, nama) VALUES (?, ?)
        ''', row)

    input_conn.close()
    output_conn.commit()
    output_conn.close()

# encrypt the database
enkripsi_sqlite_xor('src\FingerPrintDetector/stima.db', 'src\FingerPrintDetector/stima_encrypted.db')
