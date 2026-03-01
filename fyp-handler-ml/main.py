from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer
import uvicorn
import os

app = FastAPI(title="FYP Handler ML Service")

# Load model globally to avoid reloading on every request
# all-MiniLM-L6-v2 is a good balance of speed and quality
MODEL_NAME = "all-MiniLM-L6-v2"
try:
    print(f"Loading model: {MODEL_NAME}...")
    model = SentenceTransformer(MODEL_NAME)
    print("Model loaded successfully.")
except Exception as e:
    print(f"Error loading model: {e}")
    model = None

class EmbedRequest(BaseModel):
    text: str

class EmbedResponse(BaseModel):
    embedding: list[float]

@app.get("/health")
def health_check():
    return {"status": "ok", "model_loaded": model is not None}

@app.post("/embed", response_model=EmbedResponse)
def create_embedding(request: EmbedRequest):
    if model is None:
        raise HTTPException(status_code=503, detail="Model not initialized")
    
    if not request.text or not request.text.strip():
        #Return zero vector if text is empty, matching expectation
        return {"embedding": [0.0] * 384}

    try:
        # Generate embedding
        # encode returns numpy array, convert to list for JSON serialization
        embedding = model.encode(request.text).tolist()
        return {"embedding": embedding}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    port = int(os.getenv("PORT", 8081))
    uvicorn.run(app, host="0.0.0.0", port=port)
