import os
import glob
import requests
import json
import random
import datetime
from pypdf import PdfReader
from docx import Document

# Configuration
DATASET_DIR = "../dataset"
BACKEND_ML_URL = "http://localhost:8081/embed"

def extract_text_from_pdf(path):
    try:
        reader = PdfReader(path)
        text = ""
        for page in reader.pages:
            t = page.extract_text()
            if t: text += t + "\n"
        return text
    except Exception as e:
        print(f"Error reading PDF {path}: {e}")
        return None

def extract_text_from_docx(path):
    try:
        doc = Document(path)
        text = "\n".join([para.text for para in doc.paragraphs])
        return text
    except Exception as e:
        print(f"Error reading DOCX {path}: {e}")
        return None

def get_embedding(text):
    try:
        # Truncate text for embedding if too long (optional, but good practice)
        # SBERT usually handles truncation but sending 100 pages is bad.
        # Let's take first 2000 chars which is usually abstract + intro
        snippet = text[:4000] 
        resp = requests.post(BACKEND_ML_URL, json={"text": snippet})
        if resp.status_code == 200:
            return resp.json()["embedding"]
        else:
            print(f"Error getting embedding: {resp.status_code} {resp.text}")
            return None
    except Exception as e:
        print(f"Error calling ML service: {e}")
        return None

def escape_sql(text):
    if not text: return ""
    return text.replace("'", "''")

def main():
    print("Starting Ingestion & SQL Generation...")
    files = glob.glob(os.path.join(DATASET_DIR, "*.*"))
    print(f"Found {len(files)} files.")
    
    sql_statements = []
    
    # Start IDs from 1000
    current_id = 1000
    
    # We need to ensure we don't break FKs.
    # DepartmentId = 1 (Computer Science)
    # SupervisorId = 1 (System Admin)
    # Category = "Machine Learning" (default)
    
    sql_statements.append("SET IDENTITY_INSERT FYPProjects ON;")
    
    for f in files:
        filename = os.path.basename(f)
        print(f"Processing: {filename}")
        
        content = None
        if f.lower().endswith(".pdf"):
            content = extract_text_from_pdf(f)
        elif f.lower().endswith(".docx"):
            content = extract_text_from_docx(f)
            
        if not content or len(content) < 100:
            print("  -> Skipped (empty or too short)")
            continue
            
        # Generate fake metadata
        title = os.path.splitext(filename)[0].replace("_", " ").title()
        description = content[:500].replace("\n", " ").strip() + "..."
        year = 2023
        semester = "Spring"
        
        # Get Embedding
        embedding = get_embedding(content)
        if not embedding:
            print("  -> Skipped (no embedding)")
            continue
            
        embedding_json = json.dumps(embedding)
        
        # SQL for FYPProject
        # Id, Title, Description, Year, Semester, Category, Status, DepartmentId, SupervisorId, DifficultyLevel, PerformanceScore, FinalGrade, Citations, CreatedAt, UpdatedAt
        sql_project = f"""
INSERT INTO FYPProjects (Id, Title, Description, Year, Semester, Category, Status, DepartmentId, SupervisorId, DifficultyLevel, PerformanceScore, FinalGrade, Citations, CreatedAt, UpdatedAt)
VALUES ({current_id}, '{escape_sql(title)}', '{escape_sql(description)}', {year}, '{semester}', 'Machine Learning', 2, 1, 1, 'Medium', 85, 'A', 0, GETDATE(), GETDATE());
"""
        sql_statements.append(sql_project.strip())
        
        # SQL for IndexedDocument (for Uniqueness service)
        # Id, SourceType(1=Internal), SourceEntityId, Title, Url, Year, DepartmentId, Category, Embedding, MetadataJson, CreatedAt, UpdatedAt
        # Note: Id is identity, usually we let DB handle it. BUT if we want to insert, we can't SET IDENTITY_INSERT for two tables at once easily in one go usually? 
        # Actually IndexedDocument ID doesn't matter for FKs here. So let's NOT set ID for IndexedDocument.
        
        sql_idx = f"""
INSERT INTO IndexedDocuments (SourceType, SourceEntityId, Title, Url, Year, DepartmentId, Category, Embedding, MetadataJson, CreatedAt, UpdatedAt)
VALUES (1, {current_id}, '{escape_sql(title)}', '', {year}, 1, 'Machine Learning', '{embedding_json}', '', GETDATE(), GETDATE());
"""
        sql_statements.append(sql_idx.strip())
        
        current_id += 1
        print("  -> Generated SQL.")

    sql_statements.append("SET IDENTITY_INSERT FYPProjects OFF;")
    
    with open("seed_data.sql", "w", encoding="utf-8") as f:
        f.write("\n".join(sql_statements))
        
    print(f"Generated seed_data.sql with {len(sql_statements)} statements (approx).")

if __name__ == "__main__":
    main()
