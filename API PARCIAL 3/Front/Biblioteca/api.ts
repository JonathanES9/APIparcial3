const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5000"

export interface RegisterRequest {
    firstName: string
    lastName: string
    age: number
}

export interface RegisterResponse {
    playerId: number
}

export interface StartGameRequest {
    playerId: number
}

export interface StartGameResponse {
    gameId: number
    playerId: number
    createAt: string
}

export interface GuessRequest {
    gameId: number
    attemptedNumber: string
}

export interface GuessResponse {
    gameId: number
    attemptedNumber: string
    message: string
    attemptNumber: number
}

export interface ApiError {
    message: string
}

export async function registerPlayer(data: RegisterRequest): Promise<RegisterResponse> {
    const response = await fetch(`${API_BASE_URL}/api/game/v1/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const error: ApiError = await response.json()
        throw new Error(error.message)
    }

    return response.json()
}

export async function startGame(data: StartGameRequest): Promise<StartGameResponse> {
    const response = await fetch(`${API_BASE_URL}/api/game/v1/start`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const error: ApiError = await response.json()
        throw new Error(error.message)
    }

    return response.json()
}

export async function makeGuess(data: GuessRequest): Promise<GuessResponse> {
    const response = await fetch(`${API_BASE_URL}/api/game/v1/guess`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const error: ApiError = await response.json()
        throw new Error(error.message)
    }

    return response.json()
}
