from fastapi import FastAPI
from pydantic import BaseModel
from typing import List
import random

app = FastAPI(title="Sports Club ML Service")

# Request model
class PlayerStats(BaseModel):
    name: str
    sport: str
    goals: int
    assists: int
    matches: int

# Response model
class PlayerSummary(BaseModel):
    name: str
    sport: str
    average_score: float
    highlight: str

@app.post("/data", response_model=PlayerSummary)
def generate_summary(stats: PlayerStats):
    # Simulate some processing
    average_score = round((stats.goals + stats.assists) / max(stats.matches, 1), 2)
    highlight = random.choice([
        "Excellent performance!",
        "Needs improvement in defense.",
        "Top scorer potential.",
        "Great teamwork."
    ])
    return PlayerSummary(
        name=stats.name,
        sport=stats.sport,
        average_score=average_score,
        highlight=highlight
    )
