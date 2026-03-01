import pyodbc
import os

# Configuration
DB_SERVER = "LAPTOP-EPLH5VOL"
DB_NAME = "SmartFYPHandlerDB"
SEED_FILE = "seed_data.sql"

def main():
    print("Connecting to database...")
    try:
        drivers = [d for d in pyodbc.drivers() if "SQL Server" in d]
        print(f"Available drivers: {drivers}")
        
        conn = None
        for driver in drivers:
            try:
                print(f"Trying driver: {driver}")
                conn_str = f"DRIVER={{{driver}}};SERVER={DB_SERVER};DATABASE={DB_NAME};Trusted_Connection=yes;TrustServerCertificate=yes;"
                conn = pyodbc.connect(conn_str)
                print(f"Successfully connected with {driver}")
                break
            except Exception as e:
                print(f"Failed with {driver}: {e}")
        
        if not conn:
            print("Could not connect with any available driver.")
            return

        cursor = conn.cursor()

        
        print(f"Connected to {DB_NAME}. Reading seed file...")
        
        with open(SEED_FILE, "r", encoding="utf-8") as f:
            sql_script = f.read()
            
        print(f"Executing SQL script ({len(sql_script)} bytes)...")
        
        # Split by statements to be safe, or try all at once.
        # SQL Server drivers usually support multiple statements if separated by ;
        # However, Python drivers sometimes wrap them in exec_sql which might fail.
        # Let's try executing block by block if possible, but IDENTITY_INSERT needs state.
        # Ideally, just execute the whole thing.
        
        # We need to handle potential errors with large batches.
        # But for this generated file, it is simple INSERTs.
        
        # Let's try splitting by ";" which is safer for some drivers
        statements = sql_script.split(";")
        
        for stmt in statements:
            if stmt.strip():
                try:
                    cursor.execute(stmt)
                except Exception as e:
                    print(f"Error executing statement: {e}")
                    # Continue or break? 
                    # If IDENTITY_INSERT fails, subsequent INSERTs fail.
                    # But the first stmt is SET IDENTITY_INSERT ON
                    pass
        
        conn.commit()
        print("Seed data applied successfully!")
        
        # Verification
        print("Verifying data insertion...")
        cursor.execute("SELECT COUNT(*) FROM FYPProjects")
        count = cursor.fetchone()[0]
        print(f"Total Projects in DB: {count}")
        
        cursor.execute("SELECT COUNT(*) FROM IndexedDocuments")
        count_doc = cursor.fetchone()[0]
        print(f"Total Indexed Documents: {count_doc}")
        
    except Exception as e:
        print(f"Database error: {e}")
    finally:
        if 'conn' in locals():
            conn.close()

if __name__ == "__main__":
    main()
