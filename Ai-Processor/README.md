```
# 🏆 Sports Club Insights POC

This project demonstrates an **end-to-end pipeline** for generating sports club insights using:

- **FastAPI ML Service** → Simulates player stats processing (average scores, highlights).
- **.NET Web API** → Orchestrates calls to FastAPI and OpenAI, exposes endpoints.
- **OpenAI API** → Generates text-based insights (match reports, performance summaries).
- **Blazor UI** → Frontend to input stats and view AI-generated reports.

---

## 📂 Project Structure
(unchanged)

---

## ⚙️ Setup Instructions
(unchanged)

---

## 🚀 Usage

Once both services are running, you can access them here:

- 🎨 **Blazor UI (Player Insights Page)** → [http://localhost:5265/player-insights](http://localhost:5265/player-insights)  
- ⚡ **FastAPI Docs (127.0.0.1)** → [http://127.0.0.1:8000/docs#/default/generate_summary_data_post](http://127.0.0.1:8000/docs#/default/generate_summary_data_post)  
- ⚡ **FastAPI Docs (localhost)** → [http://localhost:8000/docs#/default/generate_summary_data_post](http://localhost:8000/docs#/default/generate_summary_data_post)  
- 📜 **.NET Swagger UI** → [http://localhost:5194/swagger/index.html](http://localhost:5194/swagger/index.html)  

### Example Request
```json
{
  "name": "John Doe",
  "sport": "Football",
  "goals": 10,
  "assists": 5,
  "matches": 8
}
```

### Example Response
```json
{
  "name": "John Doe",
  "sport": "Football",
  "averageScore": 1.88,
  "highlight": "Top scorer potential.",
  "aiReport": "John Doe showed excellent performance in Football..."
}
```

---


✨ Now your README has **clickable links** to all the relevant endpoints:  
- Blazor UI (`/player-insights`)  
- FastAPI docs (both `127.0.0.1` and `localhost`)  
- .NET Swagger  
